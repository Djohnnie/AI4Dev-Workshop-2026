# Implementatieplan — Communicatieflow ziekte en afwezigheden (vervolg fase 3)

> Stap-voor-stap realisatie van de analyse `PP-Communicatieflow ziekte en afwezigheden (vervolg fase 3)`.
> Per stap staat *wat* je doet en *wat er gebeurt / waarom*. De paden zijn relatief t.o.v. `C:\Odisee\eloket\src`.

---

## 1. Samenvatting van de analyse

De analyse vraagt een uitgebreidere, configureerbare communicatieflow bij afwezigheid/ziekte. Concreet zijn er **vier** bouwstenen:

| # | Bouwsteen | Kern |
|---|-----------|------|
| A | **Ontvangersprofielen** (nieuw) | Herbruikbaar profiel met *personen*, *vrije mailadressen* en *bovenliggende organisatieniveaus*. Te beheren onder **Instellingen > Communicatie > Ontvangersprofielen**. |
| B | **Organigram-node e-mailadres** (nieuw veld) | Extra `E-mail`-veld op een organigram-node, zodat communicatie per niveau verstuurd kan worden. |
| C | **"Communicatie naar" per verlofgroep/afwezigheid uitbreiden** | Lijst inkorten + nieuwe opties **N+1**, **N+2**, **Supervisor**, **Ontvangersprofiel** en **Andere ontvangers**. |
| D | **Verzendlogica** | Bij een afwezigheidsmelding de werkelijke ontvangers afleiden (rollen + organigram-walk + profiel-expansie + mailtemplate). |

De analyse zegt expliciet dat de werkwijze gebaseerd is op de bestaande **aanvraagprofielen voor DO's** en **meldingsprofielen bij ziekte/afwezigheid**. Daarom hergebruiken we dat patroon (`AanvraagProfiel`) zo veel mogelijk.

### Bestaande situatie in de code (vertrekpunt)

- `ELoket.Domain\Beheer\Afwezigheid.cs` — heeft al `AfwezigheidCommunicatie` (lijst van `Rol`), `AfwezigheidCommunicatieAndere` (vrije mailadressen) en een FK naar `AanvraagProfiel`.
- `ELoket.Domain\Beheer\AfwezigheidCommunicatie.cs` — koppelt `(AfwezigheidId, Rol)`.
- `ELoket.Enums\Rol.cs` — `Personeelslid=1 … Supervisor=11, OntvangerMeldingDO=12`. **Nog géén N+1 / N+2.**
- `ELoket.Domain\AanvraagProfiel\*` — referentiepatroon (entity + elementen + many-to-many koppeling).
- `ELoket.Domain\Organigram\OrganigramNode.cs` — node met `ParentNodeId`; nog **zonder** e-mailveld.
- `GetManagerUrnService` (`ELoket.NetUpgrade.Application\Services\ETalent\GetManagerUrnService`) — loopt het organigram naar boven; referentie voor N+1/N+2-resolutie.
- Laatste migratie: **`M252_Weddeschaal.cs`** → nieuwe migraties starten bij **M253**.

> **Beslispunt vooraf (architectuur):** N+1/N+2 zijn "rollen" in het afwezigheidsformulier, maar inhoudelijk een *organigram-walk*. Twee opties:
> 1. **N+1/N+2 als nieuwe `Rol`-waarden** (13, 14) — past in het bestaande `AfwezigheidCommunicatie`-mechanisme, minste DB-impact. **(Aanbevolen.)**
> 2. N+1/N+2 als apart veld op `Afwezigheid` (int "aantal niveaus"). Flexibeler maar meer maatwerk.
> Dit plan gaat uit van optie 1.

---

## 2. Fasering in één oogopslag

1. **Fase 1 — Backend datamodel** (Ontvangersprofiel-entity, DB-migraties, EF-config)
2. **Fase 2 — Backend CRUD** (MediatR handlers, DTO's, AutoMapper, controller)
3. **Fase 3 — Organigram e-mailveld** (domein + migratie + config + endpoint)
4. **Fase 4 — "Communicatie naar" uitbreiden** (Rol-enum N+1/N+2, koppeling Ontvangersprofiel aan Afwezigheid)
5. **Fase 5 — Frontend beheer** (menu, overzicht, aanmaak/wijzig-form Ontvangersprofiel)
6. **Fase 6 — Frontend afwezigheidsform & organigram-node** (checkboxen + profielselectie + e-mailveld)
7. **Fase 7 — Verzendlogica** (ontvangers resolven + mailtemplate)
8. **Fase 8 — Aandachtspunten & edge cases** (Artevelde, HOGent, terminologie, KDG)

---

## FASE 1 — Backend datamodel: `Ontvangersprofiel`

### Stap 1.1 — Domeinentiteit `Ontvangersprofiel` aanmaken
**Doen:** Maak `ELoket.Domain\Ontvangersprofiel\Ontvangersprofiel.cs` (en submodellen) naar analogie van `ELoket.Domain\AanvraagProfiel\AanvraagProfiel.cs`.

Voorgestelde velden:
- `Id`, `InstellingId` (multi-tenancy — verplicht, alle queries filteren hierop).
- `Naam` (profielnaam).
- `int AantalBovenliggendeNiveaus` (de "Aantal bovenliggende organisatieniveaus", default 0).
- `ICollection<OntvangersprofielPersoon>` — gekoppelde personeelsleden (people picker).
- `ICollection<OntvangersprofielEmail>` — vrije mailadressen.
- *(Latere fase)* mailtemplate-velden: `MailOnderwerp`, `MailInhoud`, `bool TemplateActief`.

**Wat gebeurt er:** Dit is het centrale herbruikbare profiel. Eén profiel kan door meerdere afwezigheidstypes/verlofgroepen gebruikt worden, en later ook door andere modules (dienstonderbrekingen). Door `InstellingId` op te nemen blijft het strikt per hogeschool gescheiden.

### Stap 1.2 — Submodellen `OntvangersprofielPersoon` en `OntvangersprofielEmail`
**Doen:** Maak twee child-entiteiten in dezelfde namespace, naar analogie van `AanvraagProfielElement` / `AanvraagProfielCodeDO`.
- `OntvangersprofielPersoon`: `OntvangersprofielId`, `PersoneelslidId` (of `MedewerkerId` zoals het organigram dat gebruikt).
- `OntvangersprofielEmail`: `OntvangersprofielId`, `Email`.

**Wat gebeurt er:** De personen en mailadressen worden genormaliseerd opgeslagen (één rij per koppeling), net zoals bij de aanvraagprofielen. Zo blijven validatie en wijzigen (toevoegen/verwijderen via diff) eenvoudig — zie het bestaande `Update`-patroon in `Afwezigheid.cs` (`UpdateCommunicatie`).

### Stap 1.3 — FluentMigrator-migratie `M253_Ontvangersprofiel`
**Doen:** Maak `ELoket.Database\Migrations\M253_Ontvangersprofiel.cs` naar analogie van `M238_AanvraagProfiel.cs`. Drie tabellen:
- `Ontvangersprofiel` (Id PK identity, InstellingId FK → Instelling, Naam, AantalBovenliggendeNiveaus).
- `OntvangersprofielPersoon` (Id, OntvangersprofielId FK cascade, PersoneelslidId).
- `OntvangersprofielEmail` (Id, OntvangersprofielId FK cascade, Email).

**Wat gebeurt er:** Schema-migraties gebeuren via FluentMigrator (console-app `ELoket.Database`), **niet** via EF-migraties. De nummering is sequentieel (`M252` is de laatste). Bij `dotnet run --project src\ELoket.Database` worden de tabellen aangemaakt.

### Stap 1.4 — EF-config voor de drie tabellen
**Doen:** Maak in `ELoket.Infrastructure\EntityFramework\Config\`:
- `OntvangersprofielConfig.cs`
- `OntvangersprofielPersoonConfig.cs`
- `OntvangersprofielEmailConfig.cs`

naar analogie van `AanvraagProfielConfig.cs`. Registreer ze in de `ELoketContext` (`OnModelCreating` / assembly-scan, zoals de bestaande configs).

**Wat gebeurt er:** EF Core leert de entiteiten kennen en mapt ze op de tabellen uit stap 1.3. Zet de relaties (`HasMany`/`WithOne`) en cascade-delete correct zodat het verwijderen van een profiel zijn personen/mails meeneemt.

### Stap 1.5 — DbSet ontsluiten via `IELoketContext`
**Doen:** Voeg `DbSet<Ontvangersprofiel> Ontvangersprofielen` toe aan `IELoketContext` en `ELoketContext`.

**Wat gebeurt er:** De MediatR-handlers (volgende fase) kunnen het profiel via de geïnjecteerde `IELoketContext` bevragen, consistent met de rest van de service-laag.

---

## FASE 2 — Backend CRUD voor Ontvangersprofielen

> Patroon: één bestand per operatie met `Request` + `Response` + `Handler`, in map `ELoket.Service\Beheer\Ontvangersprofiel\` (analoog aan `ELoket.Service\Beheer\Aanvraagprofiel\`).

### Stap 2.1 — Query-handlers (lezen)
**Doen:** Maak:
- `GetOntvangersprofielenRequestHandler.cs` — lijst voor het overzichtsscherm (gefilterd op `InstellingId`).
- `GetOntvangersprofielByIdRequestHandler.cs` — detail incl. personen + mails (+ niveaus).

**Wat gebeurt er:** Het overzicht en het wijzig-scherm halen hun data op. Filtering op `InstellingId` is verplicht (multi-tenancy) — nooit over instellingen heen queryen.

### Stap 2.2 — Command-handlers (schrijven)
**Doen:** Maak:
- `CreateOntvangersprofielRequestHandler.cs` — nieuw profiel met personen/mails/niveaus.
- `UpdateOntvangersprofielRequestHandler.cs` — wijzig naam, niveaus en de set personen/mails (diff-based add/remove zoals `UpdateCommunicatie`).
- `DeleteOntvangersprofielRequestHandler.cs` — verwijder (blokkeer of waarschuw indien nog in gebruik door een afwezigheid; zie stap 4.3).

**Wat gebeurt er:** Dit is de volledige beheer-cyclus voor het overzichtsscherm uit de analyse (rij met "bewerk"/"verwijder"-iconen). Validatie (unieke naam binnen instelling, geldig e-mailformaat) hoort hier thuis.

### Stap 2.3 — DTO's
**Doen:** Maak in `ELoket.Host\Dto\Ontvangersprofiel\`: `Ontvangersprofiel.cs`, `CreateOntvangersprofiel.cs`, `UpdateOntvangersprofiel.cs` (met `Personen`, `Emails`, `AantalBovenliggendeNiveaus`).

**Wat gebeurt er:** Scheidt het API-contract van het domeinmodel; de Angular-client genereert hieruit (of consumeert) zijn modellen.

### Stap 2.4 — AutoMapper-profielen
**Doen:** Maak `ELoket.Host\Infrastructure\AutoMapper\Ontvangersprofiel\OntvangersprofielProfile.cs` (domein ↔ DTO).

**Wat gebeurt er:** Mapping tussen entiteit en DTO conform de bestaande conventie (zie `AanvraagProfielProfile.cs`).

### Stap 2.5 — Controller-endpoints
**Doen:** Voeg endpoints toe aan een controller in `ELoket.Host\Controllers\` — ofwel uitbreiden van `BeheerderController.cs` (waar AanvraagProfiel ook zit), ofwel een nieuwe `OntvangersprofielController.cs`. Endpoints: `GET` (lijst), `GET/{id}`, `POST`, `PUT/{id}`, `DELETE/{id}`. Dispatch via `IMediator`.

**Wat gebeurt er:** De frontend krijgt een REST-oppervlak. Pas autorisatie toe (beheerder-rol), consistent met de andere beheer-endpoints.

---

## FASE 3 — Organigram-node e-mailveld

### Stap 3.1 — Veld `Email` op `OrganigramNode`
**Doen:** Voeg `string? Email` toe aan `ELoket.Domain\Organigram\OrganigramNode.cs` en aan de constructor/`Update`-methode.

**Wat gebeurt er:** Een node krijgt een eigen mailadres (bv. clustersecretariaat, onderwijsgroep, expertisenetwerk). Dit adres is het "bovenliggende niveau"-doel dat het Ontvangersprofiel gebruikt.

### Stap 3.2 — Migratie `M254_AddEmailToOrganigramNode`
**Doen:** Maak `ELoket.Database\Migrations\M254_AddEmailToOrganigramNode.cs` (kolom `Email NVARCHAR(...) NULL`), naar analogie van `M192_AddDisplayNaamToNodes.cs`.

**Wat gebeurt er:** Voegt de kolom toe aan de bestaande `OrganigramNode`-tabel. Bestaande nodes krijgen `NULL` (= geen mail → voor dat niveau wordt niets verstuurd, zie info-tekst in de analyse).

### Stap 3.3 — EF-config + node-endpoint
**Doen:** Map het veld in `OrganigramNodeConfig.cs` en neem `Email` mee in `EditOrganigramNodeRequestHandler.cs` + de bijhorende DTO.

**Wat gebeurt er:** Het mailadres kan via het bestaande node-bewerk-endpoint opgeslagen worden — exact het scherm uit de analyse (Node Id / Weergavenaam / **E-mail** / Opslaan).

---

## FASE 4 — "Communicatie naar" uitbreiden

### Stap 4.1 — `Rol`-enum uitbreiden met N+1 en N+2
**Doen:** Voeg toe in `ELoket.Enums\Rol.cs`: `NPlus1 = 13`, `NPlus2 = 14`.

**Wat gebeurt er:** N+1/N+2 worden gewone communicatie-rollen die in `AfwezigheidCommunicatie` opgeslagen kunnen worden — geen nieuw tabelmechanisme nodig. **Let op:** hou bestaande enum-waarden ongewijzigd (DB bevat ints).

### Stap 4.2 — `OntvangersProfielId` op `Afwezigheid`
**Doen:**
- Voeg `int? OntvangersProfielId` (+ navigatie `Ontvangersprofiel`) toe aan `ELoket.Domain\Beheer\Afwezigheid.cs` (constructor, `Update`, een `Detach`-methode zoals `DetachAanvraagProfiel`).
- Migratie `M255_AfwezigheidOntvangersProfiel.cs` met FK-kolom (analoog aan `M245_AfwezigheidAanvraagProfiel.cs`).
- Pas `AfwezigheidConfig.cs` aan.

**Wat gebeurt er:** Per afwezigheidstype/verlofgroep/kalenderjaar kan nu een Ontvangersprofiel gekozen worden ("Ontvangersprofiel → selecteer een profiel" in het scherm). De vrije mailadressen ("Andere ontvangers") bestaan al via `AfwezigheidCommunicatieAndere`.

### Stap 4.3 — Beschermen tegen verwijderen van een gebruikt profiel
**Doen:** In `DeleteOntvangersprofielRequestHandler` (stap 2.2): controleer of er nog `Afwezigheid`-rijen naar het profiel verwijzen; zo ja, weiger met een duidelijke melding.

**Wat gebeurt er:** Voorkomt verweesde verwijzingen en stille fouten in de verzendlogica.

### Stap 4.4 — Wijzig-/aanmaak-handlers afwezigheid aanpassen
**Doen:** Neem `OntvangersProfielId` mee in `NieuweAfwezigheidRequestHandler.cs` en `WijzigAfwezigheidRequestHandler.cs` + de DTO's `NieuweAfwezigheid.cs` / `WijzigAfwezigheid.cs`. De `Communicatie`-lijst (`Rol[]`) bevat voortaan ook eventueel `NPlus1`/`NPlus2`.

**Wat gebeurt er:** De volledige configuratie uit het scherm "Afwezigheid wijzigen" (Personeelslid, Dossierbeheerder/PA, N+1, N+2, Supervisor, Ontvangersprofiel, Andere ontvangers) wordt opgeslagen.

> **Opkuis (analyse):** de bestaande lijst "Communicatie naar" mag fors ingekort worden (VrijafOverurenGoedkeurder, UurroosterGoedkeurder, ApplicatieBeheerder, OrganigramBeheerder, OrganigramLezer, Boekhouding zijn doorgestreept). Verwijder die opties **alleen uit de UI-keuzelijst**; laat de enum-waarden bestaan om historische data niet te breken.

---

## FASE 5 — Frontend: beheer van Ontvangersprofielen

### Stap 5.1 — Menu-item "Ontvangersprofielen"
**Doen:** Voeg in `ELoket.Client\src\app\features\beheerder-algemeen\beheerder-algemeen-header\beheerder-algemeen-header.component.ts` een `SubMenuItem` toe onder "Communicatie" (`/beheerderalgemeen/communicatie/ontvangersprofielen`). Voeg de vertaalsleutel `BeheerAlgemeen.Communicatie.Header.Ontvangersprofielen` toe aan de i18n-bestanden.

**Wat gebeurt er:** De rubriek verschijnt in het Communicatie-submenu naast Meldingen / E-mail vrijaf / E-mail ex. prest. / E-mail Personalia (exact zoals de screenshot).

### Stap 5.2 — Overzichtscomponent
**Doen:** Maak `…\communicatie\beheerder-communicatie-ontvangersprofielen\` met zoekveld, lijst (kolom *Naam* + acties bewerk/verwijder) en knop "Ontvangersprofiel toevoegen". Voeg een API-service toe (of breid `beheerder.service.ts` uit) die de endpoints uit stap 2.5 aanspreekt. Registreer de route.

**Wat gebeurt er:** Dit is het overzichtsscherm uit de analyse (lijst met "ATP te verwittigen bij ziekte", "OP te verwittigen bij ziekte", …).

### Stap 5.3 — Aanmaak-/wijzig-form
**Doen:** Maak een form-component met de drie secties uit de analyse:
1. **Profielnaam** — tekstveld.
2. **Personen binnen de organisatie** — people picker (hergebruik het bestaande people-picker-element; zie `aanvraagprofiel`-form en de "People Picker"-formulierveld in de analyse) met chips "Geselecteerde personen".
3. **Vrije e-mailadressen** — invoer + "E-mail toevoegen" + chips.
4. **Ontvangers op basis van organisatiestructuur** — dropdown "Aantal bovenliggende organisatieniveaus" (0–N) + info-tekst dat per niveau een mailadres in het organigram nodig is.
5. *(Latere fase)* **Mail template** — onderwerp + rich-text inhoud met placeholders `{Voornaam} {Naam} {StartDatum} {EindDatum} {Extra info}` + "Template actief"-checkbox.

Knoppen "Annuleer" / "Bewaar en sluit".

**Wat gebeurt er:** De beheerder stelt één herbruikbaar ontvangersprofiel samen. De people picker en chips volgen het bestaande aanvraagprofiel-UI-patroon.

---

## FASE 6 — Frontend: afwezigheidsform & organigram-node

### Stap 6.1 — "Communicatie naar" in afwezigheidsform
**Doen:** Pas `…\beheerder-kalenderjaar\verlofgroepen\…\verlofgroepen-afwezigheden-form\verlofgroepen-afwezigheden-form.component.ts` aan:
- Toon checkboxes: Personeelslid, Dossierbeheerder / PA, **N+1**, **N+2**, Supervisor.
- Voeg een checkbox **Ontvangersprofiel** + dropdown "Selecteer een profiel" toe (gevuld via de service uit stap 5.2).
- Hou checkbox **Andere ontvangers** + vrij tekstveld (mailadressen gescheiden door puntkomma) — bindt op `AfwezigheidCommunicatieAndere`.
- Verwijder de doorgestreepte opties uit de keuzelijst.

**Wat gebeurt er:** De configuratie per verlofgroep/afwezigheidstype komt overeen met het scherm "Afwezigheid wijzigen".

### Stap 6.2 — E-mailveld op organigram-node (frontend)
**Doen:** Voeg in `…\beheerder-organigram\beheerder-organigram-node\beheerder-organigram-node-form\` een **E-mail**-invoerveld toe (onder Weergavenaam), gebonden aan het veld uit fase 3.

**Wat gebeurt er:** Beheerders kunnen per node een mailadres ingeven dat de N+x-/niveau-logica gebruikt.

---

## FASE 7 — Verzendlogica (ontvangers resolven + mailtemplate)

### Stap 7.1 — Ontvangers-resolver
**Doen:** Maak een service (bv. `ELoket.Service\Communicatie\OntvangersResolver`) die voor een afwezigheidsmelding de uiteindelijke mailadressen bepaalt door alle bronnen samen te voegen en te dedupliceren:
- **Rollen** uit `AfwezigheidCommunicatie` (Personeelslid, DossierBeheerder, Supervisor, …) → bestaande lookup.
- **N+1 / N+2** → loop het organigram naar boven via `ParentNodeId` en zoek de functiehouder met taak "Leidinggevende"; hergebruik/refactor de logica uit `GetManagerUrnService`.
- **Ontvangersprofiel** (indien gekoppeld): personen + vrije mails + node-mailadressen van de eerste *N* bovenliggende niveaus (`AantalBovenliggendeNiveaus`); niveaus zonder mailadres worden overgeslagen.
- **Andere ontvangers** uit `AfwezigheidCommunicatieAndere`.

**Wat gebeurt er:** Eén centrale plek levert de definitieve ontvangerslijst, herbruikbaar voor toekomstige modules (dienstonderbrekingen). Dedup voorkomt dubbele mails.

### Stap 7.2 — Mailtemplate toepassen (latere fase)
**Doen:** Als het profiel een actieve template heeft, render onderwerp/inhoud met de placeholders `{Voornaam} {Naam} {StartDatum} {EindDatum} {Extra info}` en verstuur via de bestaande mail-infrastructuur (`ELoket.Infrastructure`). Zo niet, val terug op de standaardmail.

**Wat gebeurt er:** Communicatie krijgt een per-profiel gepersonaliseerde inhoud. Dit is in de analyse expliciet "latere fase", dus afsplitsbaar.

### Stap 7.3 — Inhaken op het verzendmoment
**Doen:** Roep de resolver aan op het punt waar afwezigheidsmeldingen vandaag al verstuurd worden (bv. bij indienen/wijzigen afwezigheid, "bij terug in dienst", "bij verlenging" — zie de "Wanneer gebruiken"-kolom in de analyse). Lokaliseer dit verzendpunt in de bestaande afwezigheid-/mailflow.

**Wat gebeurt er:** De nieuwe configuratie wordt effectief gebruikt i.p.v. de oude hardgecodeerde ontvangers.

---

## FASE 8 — Aandachtspunten & edge cases (uit de analyse)

| Punt | Aanpak |
|------|--------|
| **Artevelde — 5 externe HR-partners** | Instelbaar via organigram-node-mailadressen (fase 3) en/of vrije mailadressen in het profiel. |
| **HOGent — werkt niet met organigram** | N+x/niveau-logica valt weg; communicatie via Ontvangersprofiel met *personen* + *vrije mailadressen* (methode 2 uit de analyse: instellingen i.p.v. organigram). Resolver moet gracieus omgaan met een ontbrekend/leeg organigram. |
| **Terminologie verschilt per HS** (studiegebied, expertisenetwerk, cluster, onderwijsgroep…) | Gebruik neutrale term "bovenliggend organisatieniveau" in de UI; de concrete benaming komt uit de node (`Weergavenaam`). Geen per-HS hardcoding. |
| **KDG — medewerker moet zelf andere departementen kunnen aanduiden** | (Medewerker-interface, buiten beheer) people picker bij het indienen; eventueel "favoriet" als nice-to-have. Apart in te plannen. |
| **HOGENT — people picker (medewerker-interface)** | Idem — people picker bij indienen. |

---

## 9. Volgorde van opleveren (PR-suggestie)

1. **PR 1 — Datamodel & migraties:** fase 1 + 3.1–3.2 + 4.1–4.2 (entiteiten, migraties M253–M255, EF-config). *Backend bouwt, geen UI.*
2. **PR 2 — Backend CRUD:** fase 2 + 3.3 + 4.3–4.4 (handlers, DTO's, controller).
3. **PR 3 — Frontend beheer:** fase 5 (menu, overzicht, form).
4. **PR 4 — Afwezigheidsform & node-veld:** fase 6.
5. **PR 5 — Verzendlogica:** fase 7.1 + 7.3.
6. **PR 6 — Mailtemplate (latere fase):** fase 7.2 + template-UI in fase 5.3.

Elke PR is zelfstandig bouwbaar en testbaar (`dotnet build src\Eloket.sln`, `dotnet test`, `ng serve`).

---

## 10. Definition of done (per stap toetsen)

- [ ] Migraties draaien clean (`dotnet run --project src\ELoket.Database`).
- [ ] CRUD-handlers gedekt met unit-/integratietests (xUnit; `ELoket.Dosp.Tests` vereist Docker).
- [ ] Multi-tenancy: elke query filtert op `InstellingId`.
- [ ] Verwijderen van een in-gebruik-zijnd profiel wordt geblokkeerd.
- [ ] Ontvangers-resolver dedupliceert en slaat niveaus zonder mailadres over.
- [ ] Bestaande enum-waarden ongewijzigd; doorgestreepte opties alleen uit UI weg.
- [ ] i18n-sleutels toegevoegd voor alle nieuwe labels.
