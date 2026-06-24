# From Zero to Hero

## Hoe ik leerde AI *efficiënt* te gebruiken in een ongedocumenteerd legacy-systeem — en mijn token-factuur verlaagde terwijl ik betere antwoorden kreeg


# Dag één.

Ik open de solution: tientallen projecten, geen README, geen wiki en amper documentatie.

De consultant die dit allemaal gebouwd heeft, is al maanden weg.

En ergens daarbuiten rekenen **scholen** er elke dag op.

> *Dit is het verhaal van hoe ik in een paar weken ging van "ik snap hier niks van" naar "iedereen belt mij" en waarom het uitmaakte hoe ik AI gebruikte.*



## Waar dit verhaal naartoe gaat

In een paar weken ging ik van "ik begrijp niets van dit systeem" naar "iedereen, ook de scholen, belt mij". De versneller was AI, maar niet de manier waarop de meeste mensen AI gebruiken.

De komende 60 minuten neem ik je mee door die ommekeer. Tegen het einde heb je een set gewoontes die je AI-antwoorden tegelijk **korter, goedkoper en beter** maken. Want token-efficiënte prompts zijn niet alleen goedkoper, het zijn ook gewoon betere prompts.

**Wat we behandelen:**
- Hoe tokens (en je factuur) echt werken, het mentale model dat alles verandert
- Lokaliseren voordat je vraagt, en je referenties scherp houden
- Output-discipline: vraag de fix, geen levensverhaal
- Context één keer instellen, plannen voor je begint, en het juiste model voor de klus

Geen theorie om de theorie. Elke halte is een echt moment uit dit project, meestal eentje waarop ik het zelf eerst verkeerd deed.


# Act 1 — Het landschap

## Twee applicaties die één moesten worden

## Voor ik iets kon doen, moest ik begrijpen wat het deed

Bij Odisee aangekomen kreeg ik 2 applicaties voorgeschoteld, beide met amper of verouderde documentatie. 2 andere ontwikkelaars en 1 analyst die zeer veel kennis hebben over de werking, maar niet over hoe de code het nu exact doet. Ze kennen het begin en einde, niet ertussen.

> *Even voorstellen: ELoket en PersonA.*


| Systeem | Wat het is | Het probleem |
|--------|-----------|-------------|
| **ELoket** | Angular 14 + .NET REST API op Azure | Een lappendeken: .NET Core 3.1 / 6 / 7 / 8, stuksgewijs geüpgraded |
| **PersonA** | 15 jaar oude WPF desktop-app | Praat **rechtstreeks met de DB**, per school uitgerold door binaries te zippen en te mailen |

**De visie:** beide vervangen door één **Blazor Server + REST API op Azure**. Geen zip-bestanden. Geen on-prem DB's. Geen versie-fragmentatie.


## Even stilstaan bij PersonA, want dit is echt zorgwekkend

Een update uitrollen betekende hier letterlijk: **binaries zippen en mailen.**

- Geen centrale deployment, geen rollback-knop.
- Geen zicht op welke versie een school draait, tot het ergens breekt.
- En zo had elke school stilaan zijn eigen, licht afwijkende realiteit.

**Hier moesten we dus vanaf. Maar eerst moest ik begrijpen waar we stonden, en dat was het lastigste deel.**

## Mijn voorganger

- De volledige stack was gebouwd door een **vorige externe consultant**, een zeer capabel persoon.
- Azure-infra, IdentityServer 4, auth-flows, CI/CD opgezet, en credit where credit is due, alles werkte.
- Maar er was niets, maar dan ook niets, van deftig gedocumenteerd.

Het interne team bestond uit goede developers met een systeem dat ze niet helemaal konden uitleggen. De levende documentatie was met de vorige consultant de deur uit gewandeld.

## Dus had ik één bondgenoot

Niemand in de kamer kende alle antwoorden. De documentatie was weg, de auteur was weg.

Maar er was één "collega" die elke IS4-RFC had gelezen, elke OpenIdDict-issue, elke ASP.NET-internals deep-dive.

> AI was niet zomaar handig. AI werd **essentieel**, de enige manier om snel diepe kennis op te bouwen in een kamer waar niemand de antwoorden had.

**Maar alleen als ik het goed vroeg. En de eerste weken vroeg ik het ronduit slecht.**

# Act 2 — De dure fouten

## (zodat jij ze niet hoeft te maken)

---

<!-- _class: scene -->

## Een eerlijke bekentenis.

De eerste weken behandelde ik AI zoals bijna iedereen dat doet:
ik praatte ermee. Lange berichten, veel context, veel beleefdheid.

En ik kreeg lange, generieke, onbruikbare antwoorden terug, en betaalde voor elk woord.

## Week 1: mijn instinct was om alles te dumpen

Mijn vroege prompts zagen er zo uit:

> *"We have a legacy IdentityServer 4 setup on Azure and I'm trying to understand the token flow between our Angular frontend, our .NET API, and a legacy WPF app called PersonA. Can you help me understand how authentication flows through the whole system?"*

Het antwoord was lang, grondig en generiek, een tutorial over hoe IS4 in het algemeen werkt. Onbruikbaar, want ik gaf het *uitleg over* mijn probleem in plaats van de *echte config* om mee te redeneren.

**Ik verbrandde tokens aan een inleiding en kreeg boilerplate terug.**


## Het verraderlijke teken

Het antwoord *klonk* goed. Grondig zelfs. Daarom duurde het even voor het kwartje viel.

Ik vroeg veel te algemene informatie en pakte alles te generiek aan. 

---

## Hoe tokens echt werken (de versie van 90 seconden)

- Een **token** ≈ 4 tekens / ¾ van een woord. Code is token-dicht.
- **Twee soorten, beide gefactureerd:**
  - **Input** = je prompt + **de volledige gespreksgeschiedenis** + bestandscontext
  - **Output** = alles wat het model terugschrijft
- **De valkuil:** de *hele geschiedenis* wordt bij **elke** beurt opnieuw verstuurd.

```
Turn 1:  [Prompt 200] → [Response 300]            =   500 tokens
Turn 2:  [History 500 + Prompt 150] → [Resp 250]  =   900 tokens
Turn 3:  [History 1400 + Prompt 100] → [Resp 400] =  1900 tokens
         ↑ history blijft groeien — je betaalt er elke beurt opnieuw voor
```

**Een onderzoek van 20 prompts kan meer kosten dan 50 gerichte single-turn prompts over dezelfde stof.**

---

## Hoe code "kost" — .NET tokeniseren

Het model telt geen woorden of regels. Een **tokenizer** splitst tekst in sub-word stukken (BPE). Voor Engels proza is dat ~**4 tekens / token**. **Code is veel dichter** — elke bracket, operator, `.`, `;`, inspringing, generic en `CamelCase`-grens breekt vaak in een eigen token.

| Zelfde ~45 tekens | ≈ Tokens |
|---|---|
| Engels: *"Fetch the leave group by its id"* | ~7 |
| C#: `var vg = await _ctx.Verlofgroepen.FindAsync(id);` | **~17** |

Die ene C#-regel splitst ruwweg in:
```
var · vg · = · await · _ctx · . · Verlofgroepen · . · Find · Async · ( · id · ) · ;   →  ~17 tokens
```

> **Zelfde aantal tekens, ~2× de tokens.** Een C#-bestand van 100 regels is al snel **800–1.200 tokens** — en als het in de history zit, betaal je dat elke beurt opnieuw. "Gewoon het bestand plakken" is nooit gratis.

## Hoe voorkom je dan dat de history opzwelt?

- **Start een nieuwe chat voor een nieuw onderwerp** — de goedkoopste fix van allemaal. Een verse chat draagt **nul** history mee. Van onderwerp wisselen in dezelfde thread betekent het oude onderwerp eeuwig blijven betalen.
- **Comprimeer / vat de sessie samen.** De meeste tools bieden *"summarize conversation"* (Copilot) of `/compact` (Claude Code, Cursor). Het plooit 20 beurten scrollback in een korte samenvatting en gaat daarvandaan verder — doe het **vóór** een lange agent-sessie opzwelt, niet erna.
- **Vervang verouderde context — stapel ze niet op.** Dat bestand van 200 regels uit beurt 1 wordt in beurt 20 nog steeds aangerekend. Snoei het:
  > *"Forget the earlier version. Here's the current file: [paste new only]"*
- **Bewerk een eerder bericht i.p.v. een beurt toe te voegen.** De meeste chat-UI's laten je edit-and-resubmit, dat **herschrijft** de history ter plaatse en **gooit de foute tak weg**, in plaats van een correctie erbovenop te stapelen.
---

## En als history onvermijdelijk is: maak het goedkoop

- **Externaliseer naar bestanden, refereer via pad.** Plannen, beslissingen, gegenereerde code, lange logs → schrijf ze naar disk en wijs ernaar (`#file:` / een pad). Alles buiten het context-venster kost **niets** om te "onthouden". *(We zien dit bij `migration-plan.md` in Act 8.)*
- **Oogst, herstart dan.** Als een lange verkenning tot een conclusie komt, vang de takeaway op — in docs of `.github/copilot-instructions.md` — en open dan een **schone chat met enkel die conclusie.** Je houdt de kennis, dropt het token-gewicht.
- **Houd het stabiele deel van je context identiek en bovenaan.** Dezelfde prefix hergebruiken maakt het **cache-eligible** — gecachte input is **~10× goedkoper** dan verse. History die je niet kan vermijden is veel goedkoper wanneer ze gecacht is dan wanneer ze elke beurt verschuift.

> Vuistregel: zit je voorbij ~6–8 beurten en ben je nog aan het corrigeren? **Stop.** Consolideer wat je leerde in één schone prompt in een nieuwe chat.

---

## Twee cijfers die elke prompt zouden moeten hervormen

<div class="tip">

**1. Output-tokens kosten 5–10× meer dan input-tokens.**
→ Een kort antwoord is het goedkoopste antwoord. Vraag de fix, niet het essay.

**2. Modelkeuze kan een ~240× kostenverschil zijn voor *hetzelfde* antwoord.**
→ Een topmodel gebruiken voor *"what's the C# switch-expression syntax?"* is pure verspilling.

</div>

| Zelfde vraag | Op een zwaar model | Op een licht model |
|---|---|---|
| 200 in / 350 out tokens | ~0.59 AIC | ~0.075 AIC → **8× goedkoper** |



## Output-tokens minimaliseren — een .NET-voorbeeld

**Het model vult elke ruimte die je geeft.** Output is het 5–10× dure token-type, dus de *output-instructie* in je prompt is je grootste kostenknop.

<span class="bad">❌ Open (~8-token prompt → ~180-token antwoord):</span>
```
How do I add caching to this method?
```
→ Een paragraaf over `IMemoryCache` vs `IDistributedCache`, de code, *en* caveats die je niet vroeg.

<span class="good">✅ Begrensd (~18-token prompt → ~60-token antwoord):</span>
```
Add IMemoryCache to this PersonaBaseClient token lookup, keyed per InstellingId.
Return only the updated method, no explanation.
```
```csharp
internal async Task<string> GetTokenAsync(CancellationToken ct) =>
    await _memoryCache.GetOrCreateAsync($"PersonaApiToken_{InstellingId}", e => {
        e.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(55);
        return GetBearerToken(ct);
    });
```

> **~67% minder output-tokens** — dezelfde werkende code.

---

## De output-knoppen om naar te grijpen

Schroef er één op elke generatie-prompt:

- `"Return only the updated method, no explanation"` — code, geen commentaar
- `"Show only the lines that change, unified diff"` — geen volledige herschrijving *(~76% eraf bij refactors)*
- `"Max 5 bullet points"` · `"one sentence, most likely cause first"` — harde caps
- `"Signature only"` / `"names only, no bodies"` — als je de rest zelf invult
- `"List the issues, don't show corrected code"` — voor reviews

> Het model gaat standaard breedsprakerig en omzichtig te werk. **Beknoptheid moet je expliciet vragen** — en net daar zit de besparing. *(Act 6 maakt hier een volwaardige werkgewoonte van.)*

# Act 3 — Navigeer eerst, vraag daarna

## De valkuil waar ik twee keer in trapte

Ik moest weten wat wat raakte, verspreid over tientallen projecten.

- <span class="bad"> Instinct #1:</span> een hoop bestanden plakken en AI het laten uitpuzzelen.
- <span class="bad"> Instinct #2 (voelt slim, is het niet):</span> een volledige `tree /F`-dump plakken. Een grote .NET-solution is **duizenden regels**, zelfs nadat je `bin`/`obj` eruit filtert. Je hebt "code dumpen" gewoon vervangen door "een bestandslijst dumpen", zelfde factuur.

> De echte les: **gebruik het goedkoopst mogelijke gereedschap om te vinden wat je nodig hebt, vóór je AI er überhaupt bij betrekt.**

## Het locate → execute patroon

```
Phase 1 — Locate (bijna gratis)  → IDE search · dotnet sln list · scoped folder listing
Phase 2 — Execute (gericht)      → #file: op exact het juiste bestand
```

**Stap 1 — Je IDE is gratis (0 tokens):**
- `Ctrl+T` Go to Symbol —> vind `TokenValidator`, `IdentityConfig` op naam
- `Ctrl+Shift+F` Find in Files —> grep `AddIdentityServer`, `ValidateToken`
- `F12` Go to Definition —> volg de call chain

**Negen op tien keer brengt de IDE je er in minder dan 30 seconden. Gratis.**
---

## Als je niet eens weet waarnaar te zoeken

Dump de tree niet. Geef het **structuur, geen inhoud:**

```powershell
dotnet sln list      # ~20-30 regels, een paar honderd tokens
```

```
ELoket.Host/ELoket.Host.csproj
ELoket.Service/ELoket.Service.csproj
ELoket.Domain/ELoket.Domain.csproj
ELoket.Infrastructure/ELoket.Infrastructure.csproj
ELoket.OpenIddict/ELoket.OpenIddict.csproj
...
```

> *"Given these project names, which most likely contains the OpenIddict token/SAML config? Project name only."*

**Kost: ~50 input-tokens. Antwoord: één projectnaam.** Navigeer dan binnenin met de IDE.
---

## Lokaliseren: het contrast

```
  @workspace how does authentication work?
        → scant veel bestanden · enorme input · enorme uitleg-output

  [plakt volledige solution-tree, duizenden regels]
        → enorme input · AI gokt nog steeds zonder de code te zien

  Step 1: Ctrl+Shift+F "AddOpenIddictConfiguration"  → Program.cs in ELoket.OpenIddict
    Step 2: #file:ELoket.OpenIddict/Program.cs "How are the SAML providers wired up here?"
        → één bestand in context · scoped, kort antwoord
```

<div class="tip">

**Tip — gebruik `#file:`-referenties, geen geplakte inhoud.** De tool injecteert het bestand; jij schrijft ~15 tokens in plaats van 300+ te plakken. Tot ~95% minder input-tokens.

</div>

> Vraag om een **schatkaart**, niet om de schat. Een projectnaam kost ~10 output-tokens; `@workspace` dat bestanden scant kan 10.000+ kosten. Zelfde bestemming, 200× de factuur.

---

# Act 4 — De eerste echte taak

## Een codebase die ik niet begreep upgraden naar .NET 10

---

## Dan kwam de eerste echt spannende opdracht.

"Upgrade ELoket naar .NET 10."

Een codebase verspreid over **vier .NET-versies**, geschreven door handen die ik nooit zou ontmoeten,
die ik nog altijd niet begreep — en waar elke school op draaide.

> *Hoe upgrade je veilig wat je niet begrijpt? Door eerst een vangnet te spannen.*

---

## Waarom dit gevaarlijk was

ELoket = projecten op .NET Core 3.1, 6, 7, 8 — verschillende runtimes, verschillende API-oppervlakken, breaking-change workarounds van verschillende handen. **En ik wist niet wat iets ervan deed.**

Code upgraden die je niet begrijpt betekent dat je niet kan onderscheiden:
- een nieuwe fout van een bestaande
- een regressie van een oude bug die nu pas zichtbaar wordt
- een veilig-te-onderdrukken warning van een tijdbom in productie

> AI werd mijn **junior developer**: het deed het analysewerk, zodat ik snel de beslissingen kon nemen.

**Dus het allereerste wat ik deed — nog voor ik één regel aanraakte — was een test-vangnet bouwen.**

---

## Eerste zet: gedrag vastpinnen met unit tests

De enige eerlijke verdediging tegen *"is deze fout nieuw, of deed het dat altijd al?"* zijn **characterization tests**: leg het gedrag van vandaag vast vóór de runtime verandert, zodat de upgrade iets heeft om tegen te falen.

Maar een naïef *"write unit tests for this class"* is een van de **duurste prompts die er bestaan**. Het hergenereert de hele klasse, elke `using`, de fixture-setup en een muur van bijna identieke `[Fact]`-methodes — en biedt dan vrolijk aan om er nog meer te schrijven.

---

## Token-zuinige tests (1): spec, geen source — plan de matrix eerst

<div class="tip">

**Geef de signature + een one-line contract, niet de hele body.** Het model schrijft correcte tests vanuit *gedrag*, niet implementatie. Plak de body enkel voor echt lastige edge cases.
```
Write xUnit tests for the handler:
Task<CreateAanvraagProfielMetElementenResponse> Handle(CreateAanvraagProfielMetElementenRequest req)
Contract: unknown InstellingId → Bag.InstellingNotFound (IsValid=false);
valid req → IsValid, AanvraagProfiel persisted with Id. Test methods only.
```
</div>

<div class="tip">

**Plan de test-matrix eerst als enkel *namen* — bijna gratis.** Schrap dan wat je niet nodig hebt en genereer enkel die.
```
List the test case NAMES you'd write for CreateAanvraagProfielMetElementen. No code.
```
→ ~12 namen, ~40 output-tokens. Jij kiest er 6 — en betaalt nooit om de 6 te genereren die je toch zou schrappen.

</div>

---

## Token-zuinige tests (2): collapse, anchor, mock in één regel

<div class="tip">

**Eén `[Theory]` verslaat tien `[Fact]`'s.** Data-driven tests proppen veel cases in een fractie van de output-tokens — en lezen beter.
```csharp
[Theory]
[InlineData(0, false)] [InlineData(480, true)] [InlineData(-30, false)]
public void IsGeldigAantalMinuten(int minuten, bool expected) =>
    Assert.Equal(expected, _sut.IsGeldigAantalMinuten(minuten));
```
*Tien losse `[Fact]`-methodes → ~250 tokens. Eén `[Theory]` → ~60.*

</div>

<div class="tip">

**Veranker de stijl; vraag enkel methodes.** Plak **één** bestaande test: *"Match this style. Return only new `[Fact]`/`[Theory]` methods — no class shell, no usings, no fixture."*

**Beschrijf mocks in één regel — plak de dependency niet.** *"Mock `IELoketContext.Instellingen` to return no matching Instelling"* verslaat het plakken van de hele DbContext.

</div>

---

## Token-zuinige tests (3): vier die optellen

<div class="tip">

**Test enkel wat de upgrade raakt.** Characterization, geen 100% coverage — genereer tests voor het risicovolle/gewijzigde gedrag, sla de triviale getters over. Minder scope = minder tokens *én* minder ruis.

**"No comments, one logical assert per test."** Stopt het model van padding met narratie en drie redundante asserts.

</div>

<div class="tip">

**Breid uit, her-genereer niet.** *"I have happy-path + null tests. Add only the boundary cases I'm missing — `[InlineData]` rows only."* Je krijgt rijen, geen verse klasse.

**Offload de boilerplate.** Mock/test-data-generatie en simpele CRUD-tests hebben het vlaggenschip-model niet nodig — een **klein of lokaal model** (Ollama `qwen2.5-coder`) doet het voor **nul AIC**.

</div>

---

## Unit-test-generatie: vóór & na

Tests genereren voor `CreateAanvraagProfielMetElementenRequestHandler` — naïef vs token-zuinig:

| | Prompt (input) | Response (output) | Totaal |
|---|---|---|---|
| <span class="bad"> Vóór</span> — volledige klasse plakken + *"write unit tests for this"* | ~215 | ~620 *(usings, fixture, 9 `[Fact]`'s, comments, "want more?")* | **~835** |
| <span class="good"✅ Na</span> — spec + style anchor + `[Theory]` + methods-only | ~70 | ~190 *(2 `[Theory]` + 2 `[Fact]`, geen boilerplate)* | **~260** |
| **Bespaard** | **~67%** | **~69%** | **~69%** |

> Denk aan de 5–10×-regel: de **output**-kolom is waar het echte geld zit. Negen `[Fact]`'s tot twee `[Theory]`'s plooien en de boilerplate droppen is wat het kraakt.

---

## Dan — en pas dan — de upgrade zelf

**Tests zijn groen. Vraag AI nu nooit om de hele upgrade in één keer** — daar liggen correction spirals en enorme tokenverspilling.

**Krijg een scoped checklist, geen essay over zes versies:**
```
.NET Core 3.1 → .NET 10 breaking changes relevant to ASP.NET Core Web API.
List only: removed APIs, changed defaults, package renames.
No migration-guide prose. Bullet points only.
```

**Laat de tooling het mechanische deel eerst doen** (.NET Upgrade Assistant), vraag AI dan enkel over de *residuele* oordelen:
```
This code uses [deprecated API] removed in .NET 8.
Stack: .NET 10, ASP.NET Core, no external packages.
Show only the replacement — no explanation.
[just the relevant method, ~30 tokens]
```
→ Response: de fix. 20–40 tokens. Volgend probleem.

---

## "Ik @-mention gewoon het bestand" — is dat gratis?

De meesten plakken niet meer. We typen `@AfwezigheidController.cs` (Claude) of `#file:AfwezigheidController.cs` (Copilot) en gaan door. Het *voelt* gratis.

<div class="tip">

**Dat is het niet.** De tool **leest het hele bestand en injecteert de volledige inhoud als input-tokens.** `@file` is een gemak bovenop copy-paste — geen korting. Een bestand van 500 regels is ~500 regels tokens, of je nu plakte of `@`-mentionde.

</div>

**En het is erger in een gesprek:** dat aangehechte bestand reist mee in de **history**, elke volgende beurt opnieuw aangerekend (Act 2). Drie bestanden aanhechten "voor context" = drie volledige bestanden, elke beurt opnieuw verstuurd.

> `@file` beter dan **plakken** (geen kopieerfouten, altijd de huidige versie). `@file` **niet** automatisch goedkoop.

---

## Refereer klein — niet "het bestand", maar het *deel*

De fix is niet "stop met `@`" — het is **scope wat de referentie binnentrekt.** Hetzelfde instinct als een paste trimmen, toegepast op referenties:

- **Refereer de methode/selectie, niet het hele bestand.** Markeer de actie en vraag over de *selectie* (Copilot `#selection`; in Claude selecteer je gewoon de regels). Eén methode ≈ 30 tokens vs een bestand van 500 regels.
- **Hecht één bestand aan, niet vijf "voor de zekerheid".** Elk is volledige inhoud, elke beurt. Voeg het tweede pas toe als het antwoord het nodig heeft.
- **`@file` ≫ `@workspace`/codebase.** Een codebase-brede referentie haalt op over vele bestanden — de duurste optie (Act 3). Wijs naar het *ene* bestand dat je bedoelt.
- **Splits dikke bestanden.** Een referentie naar een god-class van 1.000 regels kost altijd 1.000 regels; een gefocust bestand van 80 regels kost 80.
- **Het zit nu in de history — dus laat het vallen.** Lange thread met een groot bestand aangehecht? Start een verse chat en re-refereer enkel wat nog nodig is.
- **Plak je tóch — strip eerst.** XML docs, comment-ruis, ongebruikte `using`s — weg vóór het erin gaat. ~68% minder input-tokens, en een scherper antwoord omdat het model geen vulsel leest.

> Netto: `@file` verslaat plakken, en een **scoped selectie** verslaat `@file`. Gemak *én* kost — je hoeft niet te kiezen.

---

## Line-scoping in de praktijk — Claude Code

Beide tools laten je **onder bestands-granulariteit** gaan. In Claude Code neemt de `@`-mention rechtstreeks een **line range:**

```text
# Mention only lines 20–35 — not the whole 600-line file:
@ELoket.Service/Dossierbeheerder/GetExportRapportVrijafRequestHandler.cs#L20-35  why does this throw ArgumentNullException for an unknown VerlofgroepId?
```

- **VS Code:** `@ELoket.Client/src/app/afwezigheid.service.ts#5-10`   ·   **JetBrains:** `@ELoket.OpenIddict/Program.cs#L1-99`
- **Selectie wordt automatisch gedeeld** — markeer de methode en de prompt-box toont *"N lines selected."* (eye-slash icoon schakelt het uit).
- Voeg de line-scoped mention in met een shortcut: **`Alt+K`** (VS Code, Win/Linux) / **`Option+K`** (Mac); **`Ctrl+Alt+K`** (JetBrains).
- Of laat de agent gewoon **grep'en en enkel die range lezen** (het leest met een offset/limit, niet het hele bestand).


---

## Line-scoping in de praktijk — GitHub Copilot

Copilot scopet via **editor-selectie en symbol**, niet via een `:line`-syntax:

```text
# 1. Select the lines in the editor → they attach automatically (a chip appears).
#    Then reference that selection explicitly:
#selection   fix the null handling here — method only, no explanation

# 2. Attach ONE symbol (the method), not the whole file:
#Handle   add a guard clause for a null Verlofgroep

# 3. Inline chat, scoped to the highlighted lines only:
#    select code → Ctrl+I (Win) / Cmd+I (Mac)
```

- Een selectie wordt **impliciet** als context toegevoegd — maar `#selection` komt enkel in de prompt als je ernaar refereert.
- `#`-mention een **file, folder of symbol**; de symbol-picker trekt enkel die methode-definitie binnen.

> Beide tools: **één methode ≈ tientallen tokens; het hele bestand ≈ honderden–duizenden** — elke beurt dat het blijft hangen.

---

## Eén project per chat-sessie

- `ELoket.Host` kreeg zijn eigen gesprek. Klaar → **sluiten.**
- `ELoket.OpenIddict` startte **vers.**

> **Vers starten is gratis. Verouderde context meeslepen kost je bij elke vervolgbeurt opnieuw.**

<div class="tip">

**Gesprekshygiëne:** een nieuw onderwerp = een nieuwe chat. Elk bericht rekent de volledige history opnieuw aan; de context van gisteren meedragen naar de vraag van vandaag is pure belasting.

</div>

---

# Act 5 — AI als junior developer

## Schoolvragen beantwoorden tijdens de upgrade

---

## En ondertussen ging de telefoon.

Ik zat tot over mijn oren in de migratie van een systeem dat ik amper kende —
en plots hingen er **scholen** aan de lijn met bugs die *gisteren* al opgelost moesten zijn.

Geen tijd om de codebase rustig te leren kennen. Geen tijd om te panikeren.

> *Hier promoveerde AI van "leerhulp" naar "junior collega" — en hier maakte discipline het verschil tussen een antwoord in tien minuten en een verloren dag.*

---

## De scholen stopten niet met bellen

> *"Authentication stopt na ~45 minuten voor onze gebruikers — gekend probleem?"*

Ik kende de codebase amper. Maar ik gooide hem niet in paniek bij AI. **Ik stelde eerst zelf een diagnose:**

1. **Reproduceer** het symptoom, of haal exacte stappen op
2. **Vind** de relevante code — gerichte search, geen `@workspace`
3. **Vorm zelf een hypothese** — wat denk ik dat er gebeurt?
4. **Valideer met AI** — deel enkel de relevante snippet plus mijn hypothese
---

## Stap 1: een vaag rapport → een zoektocht

Scholen beschrijven dingen vaag: **"de tellerstand-export klopt niet voor personeel in meerdere verlofgroepen."** Geen feature-naam, geen scherm, geen idee waar de code staat.

De valkuil: code plakken en AI laten zoeken. **AI kent mijn codebase nog niet** — ik zou tokens verbranden aan een gok. In plaats daarvan liet ik AI het symptoom omzetten in een **zoekopdracht:**

```text
School reports: "tellerstand export shows wrong saldo for personeelsleden
assigned to multiple verlofgroepen." Stack: .NET, EF Core, Dapper, SQL.
Give me: (a) most likely feature area, (b) 6 code-search keywords to grep,
(c) 3 clarifying questions for the school. Bullets only, no explanation.
```

→ Een kort, goedkoop antwoord. Dan grep ik die keywords met de **gratis IDE-search** (Act 3) — nul tokens om de code daadwerkelijk te lokaliseren.

<div class="tip">

**Vraag AI om de *zoektermen*, niet om de zoektocht.** Het is geweldig in raden hoe een feature heet (`GetExportRapportTellerstand`, `VerlofgroepTeller`, `Tellerstand`, `GroupBy`) — laat de **gratis** IDE het vinden doen.

</div>

---

## Stap 2: reproductiestappen — gratis

Ik vroeg AI niet te *onderzoeken*. Ik vroeg een **repro-hypothese** en de **vragen voor de school** — want de mens die het rapporteerde is de **goedkoopste, meest accurate** bron van stappen.

```text
Give the minimal steps to reproduce this, numbered, max 6 steps.
Mark any step needing data I don't have as [ASK SCHOOL]. No prose.
```

- De `[ASK SCHOOL]`-markers werden de e-mail die ik terugstuurde — exacte stappen kwamen van een **mens, aan nul AIC.**
- Cappen op *"max 6 steps, no prose"* houdt de dure output-kolom klein.

<div class="tip">

**Eén regel in, niet de hele thread.** Vat de klacht samen in één zin — forward nooit de volledige support-mail. Dezelfde input-token-discipline als het samenvatten van een stack trace (volgende slide).

</div>


---

## Valideer een hypothese — besteed het denken niet uit

Mijn hypothese: token-lifetime config. Ik haalde **enkel** de relevante settings eruit en vroeg:

```
This IS4 client config has AccessTokenLifetime = 2700s. The Angular app uses
silent token renewal. What causes auth to fail exactly at expiry with no visible
renewal attempt? 3 bullets, most likely cause first.
```

Antwoord: gericht, beknopt, actiegericht — een timing-offset in het silent-renewal venster. **De school had zijn antwoord binnen de tien minuten.**

<div class="tip">

**Vat fouten samen vóór je ze plakt.** Eén regel — exception + locatie — verslaat een stack trace van 30 regels. Tot **~88% minder input-tokens**, hetzelfde antwoord:
*"NullReferenceException at ExportPersoneelHelper.GetPersoneelsleden line 42 — dossierbeheerder.Leidinggevendefunctie is null despite the dossierbeheerder existing. How do I guard this?"*

**Pre-filter eerst met je eigen gratis tools.** `grep` de log, isoleer de 5 relevante regels, plak enkel die. Je IDE en shell kosten nul tokens.

</div>

> Als de zaal één beeld onthoudt uit deze talk: **strip een stack trace vóór je hem plakt.** 🙂 Een muur van 30 frames is 30 frames input die het model overslaat — geef het de ene regel die telt.

---

## En zo werd ik "de Hero"

Tien minuten van klacht tot antwoord. Dan de volgende. En de volgende.

De scholen merkten het. Ze begonnen **mij** te bellen in plaats van een ticket aan te maken.

> Niet omdat ik het systeem het beste kende — dat deed ik nog lang niet. Maar omdat ik **snel betrouwbare antwoorden** gaf, en AI als een scalpel gebruikte in plaats van als een sloophamer.

**Hier kantelde het. En vanaf hier verschuift het verhaal van "fouten vermijden" naar "gewoontes bouwen die zichzelf terugbetalen".**

---

# Act 6 — Output-discipline

## De duurste tokens zijn die je niet nodig had

---

## Vraag de fix, niet de tutorial

Echt voorbeeld — een ELoket-controller reviewen tijdens de upgrade:

| | Prompt | Response | Totaal |
|---|---|---|---|
| <span class="bad">❌ Vóór</span> | ~180 tok (volledig bestand + "review it") | ~230 tok essay, eindigt met *"want me to rewrite the whole class?"* | **~410** |
| <span class="good">✅ Na</span> | ~45 tok (enkel de methode) | ~70 tok — de gecorrigeerde methode | **~115** |

**Zelfde resultaat. ~72% minder tokens.**

Voeg een expliciete output-constraint toe aan *elke* substantiële prompt:
- `"Code only, no explanation"`
- `"Max 5 bullet points"` · `"One sentence, most likely cause first"`
- `"Return only the changed method signature"`

---

## Diffs, geen herschrijvingen

Aan het refactoren? Vraag niet het hele bestand terug.

```
  Add async/await to ExportPersoneelHelper. Return the full updated class.
        → ~250 output tokens, grotendeels ongewijzigde regels

  Add async/await to ExportPersoneelHelper. Show only the method signatures
    that change, no explanation.
        → ~60 output tokens
```
```csharp
public async Task<Personeelslid?> GetPersoneelslidAsync(int id) { ... }
public async Task<IEnumerable<VerlofgroepTeller>> GetTellersAsync(int verlofgroepId) { ... }
public async Task SaveTellerstandAsync(GebruikerTellerstand stand) { ... }
```

> **~76% minder output-tokens** — en output is het 6–10× dure soort.

---

## Vraag één ding. Vraag het beste ding.

<div class="tip">

**Eén taak per prompt.** *"Refactor this AND explain async/await AND write tests"* forceert één opgeblazen antwoord dat in drie dingen tegelijk fout is. Drie gefocuste prompts geven elk een strak, bruikbaar antwoord.

</div>

<div class="tip">

**Vraag geen alternatieven tenzij je ze nodig hebt.** *"Give me 3 ways to schedule the nightly AutoBerekening job"* → ~300 tokens. *"Best way to schedule the nightly AutoBerekening job — we already use Hangfire?"* → ~80 tokens. Vraag de runner-up enkel als de beste niet past.

</div>

<div class="tip">

**De nuance: *batch* verwante sub-vragen, *splits* niet-verwante.** Vijf vragen over **dezelfde** methode → één prompt (één context-load, één gedeeld antwoord). Vijf **verschillende** onderwerpen → vijf chats. "Eén taak per prompt" betekent één *onderwerp*, niet één zin.

</div>

---

# Act 7 — Stel context één keer in

## Stop met elke ochtend je stack overtypen

---

## Een patroon dat ik bij mezelf betrapte.

Elke ochtend, elke nieuwe chat, tikte ik dezelfde zin:
*"I'm working on ELoket, a .NET ASP.NET Core Web API, Angular 14 frontend..."*

Dag in, dag uit. Dezelfde ~40 tokens. **Aangerekend in elk bericht.**

> *Wat als ik dit één keer kon zeggen — en het voor altijd kon laten meegaan, bijna gratis?*

---

## Het preamble-probleem

Elk gesprek begon vroeger met:

> *"I'm working on ELoket, a .NET ASP.NET Core Web API, Angular 14 frontend, IdentityServer 4 being migrated to OpenIdDict, EF Core with SQL Server, hosted on Azure..."*

**~40 tokens overhead. Elk bericht. Elke dag.**

De fix: leg het één keer vast in **`.github/copilot-instructions.md`** — automatisch geïnjecteerd in elke Copilot-request, en het profiteert van **cached pricing (~10× goedkoper)** na het eerste gebruik.

> De context die ik vroeger in elk bericht herhaalde werd effectief **gratis.**

---

## `.github/copilot-instructions.md`

```markdown
## Stack
- .NET 10, C#, ASP.NET Core Web API
- Angular 14 (ELoket — being replaced by Blazor Server)
- WPF app (PersonA) — 15y old, direct DB access, migration in progress
- OpenIdDict for auth (migrated from IdentityServer 4)
- EF Core, SQL Server · Azure App Service, Key Vault, managed identities

## Conventions
- Async/await everywhere — never .Result or .Wait()
- CQRS via MediatR: *Request/*Response pairs, IRequestHandler<,>
- Multi-tenancy: every query filters by InstellingId
- Validation via BaseResponse + ValidationBag, not exceptions
- Schema changes use FluentMigrator (ELoket.Database), not EF migrations
- PascalCase public, camelCase private · No XML docs on internal methods
```

**Regels:** houd het onder ~500 regels, bullets geen proza, geen voorbeelden tenzij essentieel. Het wordt bij *elke* request geladen — een opgeblazen bestand belast je eeuwig.

---

## Het juiste bestand voor de juiste job — GitHub Copilot

| Bestand | Wanneer het laadt | Kost |
|------|--------------|------|
| `.github/copilot-instructions.md` | **Elke** request vanuit de repo | Altijd — maar cached |
| `.github/instructions/*.instructions.md` | Enkel als actief bestand matcht met `applyTo` glob | Gericht — goedkoper |
| `.github/prompts/*.prompt.md` | Enkel als je het expliciet aanroept | **Nul** tot gebruikt |
| `AGENTS.md` | Wanneer de cloud coding agent draait | Per agent-sessie |

> Onboarding op een nieuwe repo? Laat AI een *eerste versie* van het instructions-bestand *genereren* uit een **representatieve sample** (csproj + 3–4 source files + 2–3 tests). Eenmalige kost; elke toekomstige request wordt goedkoper. **Altijd reviewen — AI ziet patronen, geen intentie.**

---

## Het juiste bestand voor de juiste job — Claude Code

Zelfde idee, andere bestandsnamen. Claude Code leest **`CLAUDE.md`**, niet `AGENTS.md`:

| Bestand | Wanneer het laadt | Kost |
|------|--------------|------|
| `CLAUDE.md` (project root) | **Elke** sessie in de repo (+ ancestor `CLAUDE.md`'s) | Altijd — maar cached |
| `~/.claude/CLAUDE.md` | Elke sessie, **alle** projecten (je persoonlijke prefs) | Altijd — maar cached |
| nested `CLAUDE.md` / `.claude/rules/*.md` (`paths:` frontmatter) | Enkel bij werk in die subtree / matchende files | Gericht — on demand |
| `.claude/commands/*.md` · `.claude/skills/*/SKILL.md` | Enkel als je `/name` aanroept (of Claude het relevant vindt) | **Nul** tot gebruikt |

> **`@path`-imports** trekken andere bestanden inline in `CLAUDE.md` — bv. `@AGENTS.md` om een gedeeld agent-bestand te hergebruiken. Zelfde regel als Copilot: houd het lean, het laadt elke sessie.

---

## Een onverwachte bonus

Dat instructions-bestand bleek **meer dan een token-truc.**

Het werd de eerste echte **beschrijving van het systeem die ooit bestond** — niet in mijn hoofd, maar op schijf.

> Nieuwe devs konden het lezen. Scholen met technische vragen konden ernaar verwezen worden. De documentatie die de vorige consultant nooit schreef, was nu **een neveneffect van efficiënt prompten.**

**Discipline om geld te besparen produceerde de documentatie waar het hele team op wachtte.**

---


# Act 8 — Plan vóór je doet

## De high-stakes migratie die niemand wou aanraken

---

## En dan de migratie waar iedereen bang voor was.

IdentityServer 4. End-of-life. De auth-server waar **elke school** door inlogt.

Krijg dit fout, en niemand raakt nog binnen — overal, tegelijk.

> *Iedereen keek de andere kant op. Ik nam hem aan. Maar ik begon niet met code schrijven — ik begon met AI hardop te laten nadenken.*

---

## IdentityServer 4 → OpenIdDict

IS4 was **end-of-life.** Een niet-onderhouden auth-server in productie, die elke school bedient, is een onaanvaardbaar veiligheidsrisico.

Gaat auth onderuit, dan logt niemand nog in — bij elke school tegelijk.

Dus ik begon **niet** met AI vragen om migratiecode te schrijven.

> **Ik vroeg AI om eerst een *plan* te schrijven.**

---

## Plan eerst — en review het zelf

```
Migrating IS4 → OpenIdDict in a .NET 10 app. IS4 has: 3 clients
(Angular SPA+PKCE, PersonA WPF client_credentials, server-to-server),
custom claims transforms, SQL PersistedGrant store, SAML federation
(KU Leuven, Artevelde). Create a step-by-step migration plan as a
markdown checklist. No code yet. Each step a single testable action.
Flag high-risk steps.
```

Bij het lezen van het plan **ving ik wat AI niet kon weten:**
- Het nam aan dat ik alle clients atomair kon omschakelen — maar **PersonA staat bij scholen** en kan niet in lockstep updaten.
- Een **ontbrekende stap** voor het migreren van persisted grants → bestaande sessies stil gekilld bij cutover.
- Een volgorde die **beide auth-servers tegelijk live** liet, zonder infra-plan voor de overlap.

**Ik bewerkte `migration-plan.md` zelf. De migratie ging live met nul productie-downtime.**

---

## Even stilstaan bij wat daar net gebeurde

AI schreef een plan dat **technisch correct** was — en in de praktijk **catastrofaal** zou zijn uitgepakt.

Het kon niet weten dat PersonA fysiek bij de scholen draait. Dat soort context zat in **mijn** hoofd, niet in het model.

> Net daarom plan ik eerst: niet omdat AI dom is, maar omdat het plan de plek is waar **mijn kennis en die van het model samenkomen** — nog voor er één regel code bestaat die het verschil duur maakt.

**En het mooiste: die planstap was niet alleen veiliger. Hij was ook goedkoper.**

---

## Waarom eerst plannen een *token*-winst is

Het voelt als een *extra* stap — één prompt meer vóór er code is. Het is net de goedkoopste token die je uitgeeft, want het verplaatst je fouten naar de fase waar ze bijna niets kosten.

1. **Geef goedkope tokens uit om dure te vermijden.** Een plan is een korte bulleted lijst — kleine **output**. Code is grote output, en output kost **6–10×** (Act 2). Een plan van ~400 tokens reviewen laat je een foute aanpak killen *vóór* je betaalt om er code voor te genereren.

2. **Een plan fixen is gratis; code fixen niet.** Ik corrigeerde de atomic-cutover-aanname door **één regel te typen** in `migration-plan.md` — **nul AI-tokens.** Het *na* generatie vangen betekent een regenerate (dure output) **plus** de hele conversatie opnieuw verstuurd.

3. **Eén foute aanname gevangen = veel beurten bespaard.** Een foute aanname in code faalt niet één keer — het compileert fout → je corrigeert → dat breekt het volgende → je corrigeert opnieuw. **Elke** correctie herverstuurt de hele groeiende history. Het plan zet die beslissingen vooraan in **één** goedkope beurt.

> De plan-beurt is de goedkoopste token die je uitgeeft — en degene die de duurste voorkomt.

---

## Waarom het plan in een *bestand* gaat, niet in de chat

```
❌ DUUR:
   Turn 6: [full plan 400 tok] + [step 1 history] + [step 2 history] + ...
   → je betaalt het hele plan elke beurt opnieuw

✅ EFFICIENT:
   migration-plan.md (extern)  → gratis, niet in het context-venster
   Elke beurt: plak enkel stap N + de relevante config-snippet
```

<div class="tip">

**De correction spiral** — *"actually… oh and… now it doesn't compile…"* — herverstuurt de hele history, **inclusief elke foute poging**, op elke beurt. Een **planpauze van 60 seconden vóór beurt 1 vervangt 5 correctie-beurten.**

</div>

<div class="tip">

**Ontsnap eraan met een *gestructureerde* prompt — zeg alles één keer.** In plaats van requirements over beurten te druppelen, zet ze vooraan:
```
Task: GetExportRapportTellerstand MediatR handler
Pair: GetExportRapportTellerstandRequest → ...Response : BaseResponse
Deps: inject IELoketContext, IConfiguration · Validate via ValidationBag, not exceptions
Output: request + response + handler only, no explanation
```
Eén strakke prompt → één bruikbaar antwoord. Geen spiral om te betalen.

</div>

---

# Act 9 — Juist model, juist tool

## Breng geen bulldozer om een zaadje te planten

---

## Match capaciteit aan complexiteit

| Taak | Model-tier |
|------|-----------|
| Syntax-lookup, simpele completion | Klein — of gewoon IntelliSense/docs |
| Een methode uitleggen, een test schrijven | Klein / medium |
| Een race condition debuggen, complexe LINQ | Medium |
| Architectuur, migratie-planning | Groot |
| Multi-file refactor met volledige context | Groot of agent mode |

> **Probeer eerst het kleinere model. Escaleer enkel als het antwoord fout of te oppervlakkig is.**

**Inline completions (ghost text) zijn GRATIS** — ze verbruiken geen AI Credits. Enkel Chat, Agent en Code Review wel.

<div class="tip">

**En gebruik AI helemaal niet als zoekmachine.** *"What's the C# `switch` expression syntax?"* hoort in IntelliSense of de docs — gratis, instant. Besteed AI aan **redeneren en debuggen**, niet aan lookups die je gratis kan beantwoorden.

</div>

---

## En soms: helemaal van de cloud weg

Voor repetitief of gevoelig werk draai ik een **lokaal LLM** (Ollama: `qwen2.5-coder`, `phi4-mini`):

- **Nul AI-credits** — draait op mijn hardware
- **Gevoelige schooldata verlaat de machine nooit**

Goede lokale jobs:
- Mock `Personeelslid` / `Verlofgroep` test-data genereren uit de entity
- Boilerplate FluentMigrator-diffs reviewen
- Snelle regex / SQL / bash snippets
- Interne docs samenvatten (schooldata blijft on-prem)

> Tools: **Ollama** (CLI) of **LM Studio** (GUI) + de **Continue** VS Code-extensie voor een Copilot-achtige ervaring.

<div class="tip">

**Cascade goedkoop → duur.** Laat een **klein/lokaal model een doc van 2.000 tokens samenpersen tot een samenvatting van 200 tokens**, geef dan enkel de samenvatting aan het dure model voor het harde redeneren. Je betaalt het vlaggenschip-tarief op 200 tokens, niet 2.000.

</div>

---

# Act 10 — De payoff

## Weken, geen maanden

---


## Terug naar dag één.

De man die alles bouwde was weg. De documentatie bestond niet. De scholen wachtten.

**Spoel enkele weken vooruit.**

> *Dit is wat er gebeurde toen ik AI niet als gesprekspartner, maar als precisie-instrument gebruikte.*

---

## Wat AI me echt gaf

Het gaf me niet de expertise. **Het liet me expertise opbouwen aan een onnatuurlijke snelheid.**

- **Binnen één maand** — scholen hadden een directe technische lijn naar **mij**, niet naar het interne team. Ik wist hoe de Azure-infra bedraad was en waarom de auth-flows zich gedroegen zoals ze deden.
- **Binnen zes weken** — uitgenodigd in de **technical steering board** voor de Blazor-migratie, met presentatie van de voorgestelde architectuur.

De IS4-migratie die iedereen vreesde — **klaar, geen downtime.**
De PersonA-docs die nooit bestonden — **geschreven, nu de spec waar het Blazor-team op bouwt.**
De Azure-architectuur die niemand kon uitleggen — **gemapt, gedocumenteerd, gepresenteerd.**


---

## Waarom dit werkte — en waarom het anders niet was gelukt

Elke efficiënte prompt liet **budget en tijd over voor de volgende vraag.** Dat is het hele geheim.

- Achteloos prompten zou mijn AI-budget hebben leeggezogen **en** mijn dagen vol correction spirals hebben gezet.
- Gedisciplineerd prompten gaf me **meer vragen per dag, scherpere antwoorden, en de ruimte om dieper te graven.**

> Het stapelt zich op. Elke bespaarde token, elke vermeden spiral, elke verse chat — samen vormen ze het verschil tussen "weken" en "maanden".

**Discipline was niet de prijs die ik voor snelheid betaalde. Discipline *was* de snelheid.**

---

## Het ene idee om mee te nemen

<div class="tip">

# Token-efficiënte prompts zijn niet alleen goedkoper — het zijn **betere** prompts.

</div>

De discipline om **exact te zeggen wat je nodig hebt**, in **het formaat dat je nodig hebt**, met **enkel de context die vereist is** — is dezelfde discipline die je een helderder denker en een betere engineer maakt.

> **Helderheid van prompt weerspiegelt helderheid van denken.**
