# IMPLEMENTATION PROMPTS: Communicatieflow ziekte en afwezigheden — Ontvangersprofielen (fase 3 vervolg)

> Each prompt below is **self-contained** and **directly copyable** into a coding assistant (Claude Code). Feed them one at a time, in order. They carry over technical decisions (namespaces, migration numbering M253→M255, the `AanvraagProfiel` reference pattern, mandatory `InstellingId` multi-tenancy filtering) from one step to the next.
>
> All paths are relative to `C:\Odisee\eloket\src`. Architecture is fixed: **FluentMigrator** (console app `ELoket.Database`) — *never* EF migrations; **CQRS via MediatR**; **two DbContexts** (`ELoketContext`/`IELoketContext`).

---

## PHASE 1 — Backend datamodel: `Ontvangersprofiel`
*Context & goal: introduce the central, reusable recipient profile (people + free e-mail addresses + N upper organisation levels) as a first-class domain entity, persisted via FluentMigrator and mapped through EF Core, modelled directly on the existing `AanvraagProfiel` vertical slice. End state: backend compiles and the schema migration runs clean; no UI yet.*

### Prompt 1.1 — Domeinentiteit `Ontvangersprofiel` aanmaken
**Goal:** Create the root domain entity `Ontvangersprofiel` mirroring the `AanvraagProfiel` aggregate.
**System Prompt / Role:** Act as a Senior .NET Backend Developer expert in Clean Architecture, EF Core, and FluentMigrator.
**Instructions for the Coding AI:**
```text
First, READ and ANALYZE the reference file `ELoket.Domain\AanvraagProfiel\AanvraagProfiel.cs` (and any sibling files in that folder) so you reproduce the EXACT coding style: constructor shape, private setters, encapsulated collections (private backing field + read-only ICollection exposure), domain Update/mutator methods, and namespace conventions used in `ELoket.Domain`.

Then create a NEW root entity at `ELoket.Domain\Ontvangersprofiel\Ontvangersprofiel.cs` in namespace `ELoket.Domain.Ontvangersprofiel`, modelled on `AanvraagProfiel`.

Fields / properties:
- int Id
- int InstellingId            // multi-tenancy — REQUIRED on the aggregate; every query downstream filters on this. Never expose a setter that lets it change after creation.
- string Naam                 // profile name
- int AantalBovenliggendeNiveaus  // "Aantal bovenliggende organisatieniveaus", default 0
- ICollection<OntvangersprofielPersoon> Personen   // encapsulated (private List backing field), read-only exposure
- ICollection<OntvangersprofielEmail>   Emails      // encapsulated, read-only exposure

Behaviour (match how AanvraagProfiel does it):
- A public constructor taking InstellingId + Naam + AantalBovenliggendeNiveaus that initializes the collections.
- A private/protected parameterless constructor for EF if AanvraagProfiel has one.
- An Update(...) method to change Naam + AantalBovenliggendeNiveaus.
- Add/remove helpers for Personen and Emails that mutate the encapsulated collections (do NOT expose the raw List). Mirror the diff-style add/remove approach used in `ELoket.Domain\Beheer\Afwezigheid.cs` (`UpdateCommunicatie`).

Do NOT add the mailtemplate fields (MailOnderwerp / MailInhoud / TemplateActief) — those belong to a later phase.
Do NOT add any comments referencing git history, PRs, issues, or tasks.

Expected Output (files to CREATE):
- ELoket.Domain\Ontvangersprofiel\Ontvangersprofiel.cs
(The two child entities are created in Prompt 1.2; reference them by type name here.)
```

### Prompt 1.2 — Submodellen `OntvangersprofielPersoon` & `OntvangersprofielEmail`
**Goal:** Create the two normalized child entities of the `Ontvangersprofiel` aggregate.
**System Prompt / Role:** Act as a Senior .NET Backend Developer expert in Clean Architecture, EF Core, and FluentMigrator.
**Instructions for the Coding AI:**
```text
First, READ and ANALYZE the reference child entities `ELoket.Domain\AanvraagProfiel\AanvraagProfielElement.cs` and `ELoket.Domain\AanvraagProfiel\AanvraagProfielCodeDO.cs` (or whatever the child files in that folder are called) to match the child-entity style: parent FK property, private setters, EF constructor.

Then create TWO child entities in the SAME namespace `ELoket.Domain.Ontvangersprofiel` as the root from Prompt 1.1:

1) `ELoket.Domain\Ontvangersprofiel\OntvangersprofielPersoon.cs`
   - int Id
   - int OntvangersprofielId   // FK to parent
   - int PersoneelslidId        // person reference. IMPORTANT: confirm whether the organigram / people picker uses `PersoneelslidId` or `MedewerkerId` by inspecting `ELoket.Domain\Organigram\OrganigramLid.cs` and the AanvraagProfiel person link; use the SAME identifier name the codebase already uses for people.

2) `ELoket.Domain\Ontvangersprofiel\OntvangersprofielEmail.cs`
   - int Id
   - int OntvangersprofielId   // FK to parent
   - string Email

Match the encapsulation/constructor style of the reference children. No git/PR/issue comments.

Expected Output (files to CREATE):
- ELoket.Domain\Ontvangersprofiel\OntvangersprofielPersoon.cs
- ELoket.Domain\Ontvangersprofiel\OntvangersprofielEmail.cs
Also CONFIRM the chosen person-identifier name back to me before finalizing if it is ambiguous.
```

### Prompt 1.3 — FluentMigrator-migratie `M253_Ontvangersprofiel`
**Goal:** Create the three SQL tables via FluentMigrator (sequential number M253).
**System Prompt / Role:** Act as a Senior .NET Backend Developer expert in Clean Architecture, EF Core, and FluentMigrator.
**Instructions for the Coding AI:**
```text
First, READ and ANALYZE `ELoket.Database\Migrations\M238_AanvraagProfiel.cs` to copy EXACTLY: the migration attribute/numbering convention, how Up()/Down() are written, how identity PKs, FK constraints, cascade rules, and string column lengths are declared, and the schema/table naming style. Also confirm the latest existing migration is `M252_Weddeschaal.cs` so the new number `M253` is correct and sequential.

Then create `ELoket.Database\Migrations\M253_Ontvangersprofiel.cs` creating THREE tables:

1) Ontvangersprofiel
   - Id INT PK IDENTITY
   - InstellingId INT NOT NULL, FK -> Instelling (match how AanvraagProfiel references Instelling)
   - Naam NVARCHAR(...) NOT NULL  (use the same length convention as comparable name columns)
   - AantalBovenliggendeNiveaus INT NOT NULL DEFAULT 0

2) OntvangersprofielPersoon
   - Id INT PK IDENTITY
   - OntvangersprofielId INT NOT NULL, FK -> Ontvangersprofiel, ON DELETE CASCADE
   - PersoneelslidId INT NOT NULL  (use the SAME column name chosen in Prompt 1.2)

3) OntvangersprofielEmail
   - Id INT PK IDENTITY
   - OntvangersprofielId INT NOT NULL, FK -> Ontvangersprofiel, ON DELETE CASCADE
   - Email NVARCHAR(...) NOT NULL

Cascade behaviour: deleting a profile MUST cascade-delete its Personen and Emails rows. Implement a proper Down() that drops the tables in reverse dependency order. Use FluentMigrator only — do NOT generate EF migrations. No git/PR/issue comments.

Expected Output (files to CREATE):
- ELoket.Database\Migrations\M253_Ontvangersprofiel.cs

Verification: `dotnet run --project src\ELoket.Database` runs the migration clean.
```

### Prompt 1.4 — EF-config voor de drie tabellen
**Goal:** Map the three entities onto the M253 tables with correct relationships and cascade delete.
**System Prompt / Role:** Act as a Senior .NET Backend Developer expert in Clean Architecture, EF Core, and FluentMigrator.
**Instructions for the Coding AI:**
```text
First, READ and ANALYZE `ELoket.Infrastructure\EntityFramework\Config\AanvraagProfielConfig.cs` (and the configs for its children) to reproduce the IEntityTypeConfiguration<T> style: ToTable, HasKey, Property mappings, HasMany/WithOne relationship setup, OnDelete(DeleteBehavior.Cascade), and how these configs are registered in `ELoketContext` (assembly scan via ApplyConfigurationsFromAssembly vs. explicit registration in OnModelCreating). Match whichever mechanism is already in use.

Then create in `ELoket.Infrastructure\EntityFramework\Config\`:
- OntvangersprofielConfig.cs        -> maps Ontvangersprofiel; HasMany(Personen)/HasMany(Emails) WithOne, OnDelete Cascade; maps InstellingId, Naam, AantalBovenliggendeNiveaus.
- OntvangersprofielPersoonConfig.cs -> maps OntvangersprofielPersoon to table OntvangersprofielPersoon.
- OntvangersprofielEmailConfig.cs   -> maps OntvangersprofielEmail to table OntvangersprofielEmail.

Ensure the FK column names match M253 exactly. If `ELoketContext` requires explicit DbSet/config registration (not assembly scan), wire it up the same way the AanvraagProfiel configs are wired. No git/PR/issue comments.

Expected Output (files to CREATE):
- ELoket.Infrastructure\EntityFramework\Config\OntvangersprofielConfig.cs
- ELoket.Infrastructure\EntityFramework\Config\OntvangersprofielPersoonConfig.cs
- ELoket.Infrastructure\EntityFramework\Config\OntvangersprofielEmailConfig.cs
(Modify ELoketContext only if explicit registration is the existing pattern.)
```

### Prompt 1.5 — DbSet ontsluiten via `IELoketContext`
**Goal:** Expose `Ontvangersprofielen` on the context interface + implementation.
**System Prompt / Role:** Act as a Senior .NET Backend Developer expert in Clean Architecture, EF Core, and FluentMigrator.
**Instructions for the Coding AI:**
```text
First, READ how `AanvraagProfiel` is exposed as a DbSet on `IELoketContext` and `ELoketContext` (search for "AanvraagProfiel" in both files) to match the exact property style (DbSet<T> { get; } on the interface, DbSet<T> => Set<T>() or auto-property on the implementation).

Then add a `DbSet<Ontvangersprofiel> Ontvangersprofielen` to BOTH:
- the IELoketContext interface
- the ELoketContext class

Use namespace ELoket.Domain.Ontvangersprofiel. Only the root aggregate needs a DbSet (children are reached via navigation), unless AanvraagProfiel also exposes its children directly — in that case follow the same convention. No git/PR/issue comments.

Expected Output (files to MODIFY):
- the IELoketContext interface file (locate it; likely ELoket.Infrastructure or ELoket.Domain abstractions)
- ELoketContext.cs

Verification: `dotnet build src\Eloket.sln` succeeds.
```

---

## PHASE 2 — Backend CRUD voor Ontvangersprofielen
*Context & goal: full MediatR-based read/write cycle for the recipient profiles (one file per operation: Request + Response + Handler), DTOs, AutoMapper, and a REST controller surface — modelled on `ELoket.Service\Beheer\Aanvraagprofiel\`. Every query filters on `InstellingId`. End state: the frontend (phase 5) has a complete REST API to consume.*

### Prompt 2.1 — Query-handlers (lezen)
**Goal:** Read endpoints — list (overview) and by-id (detail with people + emails + levels).
**System Prompt / Role:** Act as a Senior .NET Backend Developer expert in Clean Architecture, EF Core, and FluentMigrator.
**Instructions for the Coding AI:**
```text
First, READ and ANALYZE the existing query handlers under `ELoket.Service\Beheer\Aanvraagprofiel\` (the Get... handlers) to match EXACTLY: the Request+Response+Handler-in-one-file layout, the IRequestHandler<TRequest,TResponse> signature, how IELoketContext is injected, how the current InstellingId is obtained (inspect how those handlers resolve the tenant — e.g. an injected current-user/tenant service), async EF query style, and projection to DTOs.

Then create in `ELoket.Service\Beheer\Ontvangersprofiel\`:
- GetOntvangersprofielenRequestHandler.cs   -> returns the list for the overview screen. MUST filter `.Where(x => x.InstellingId == <currentInstellingId>)`. Project to a lightweight list DTO (Id + Naam at minimum).
- GetOntvangersprofielByIdRequestHandler.cs  -> returns one profile by Id INCLUDING Personen, Emails and AantalBovenliggendeNiveaus. MUST also constrain on InstellingId (never load another institution's profile). Use Include/projection consistent with the reference.

Multi-tenancy is mandatory: never query across institutions.
No git/PR/issue comments. DTO types come from Prompt 2.3 — reference them by name (create placeholders only if needed to compile, but prefer to align names with 2.3).

Expected Output (files to CREATE):
- ELoket.Service\Beheer\Ontvangersprofiel\GetOntvangersprofielenRequestHandler.cs
- ELoket.Service\Beheer\Ontvangersprofiel\GetOntvangersprofielByIdRequestHandler.cs
```

### Prompt 2.2 — Command-handlers (schrijven)
**Goal:** Create / Update / Delete handlers with validation and in-use protection.
**System Prompt / Role:** Act as a Senior .NET Backend Developer expert in Clean Architecture, EF Core, and FluentMigrator.
**Instructions for the Coding AI:**
```text
First, READ and ANALYZE the Create/Update/Delete handlers under `ELoket.Service\Beheer\Aanvraagprofiel\` AND the diff-based update logic in `ELoket.Domain\Beheer\Afwezigheid.cs` (`UpdateCommunicatie`) so the Update handler reuses the same add/remove-by-diff approach for child collections.

Then create in `ELoket.Service\Beheer\Ontvangersprofiel\`:
- CreateOntvangersprofielRequestHandler.cs
    * Builds a new Ontvangersprofiel with InstellingId (from current tenant), Naam, AantalBovenliggendeNiveaus, plus the supplied Personen and Emails.
    * Validation: Naam unique within the institution; valid e-mail format for each Email. Mirror how the AanvraagProfiel handler reports validation failures.
- UpdateOntvangersprofielRequestHandler.cs
    * Loads the profile (constrained on InstellingId), updates Naam + AantalBovenliggendeNiveaus, and diffs Personen/Emails (add new, remove missing) via the domain mutator methods from Prompt 1.1 — do NOT clear-and-re-add blindly; follow the UpdateCommunicatie diff pattern.
- DeleteOntvangersprofielRequestHandler.cs
    * In-use protection: before deleting, check whether any `Afwezigheid` row references this profile (the FK `OntvangersProfielId` is added in Prompt 4.2 — if that field does not exist yet at compile time, guard the check behind a TODO-free conditional that you will wire in 4.3, OR implement the query defensively). If referenced, REFUSE with a clear domain/validation error message; otherwise delete (cascade removes children).

Multi-tenancy mandatory on every load. No git/PR/issue comments.

Expected Output (files to CREATE):
- ELoket.Service\Beheer\Ontvangersprofiel\CreateOntvangersprofielRequestHandler.cs
- ELoket.Service\Beheer\Ontvangersprofiel\UpdateOntvangersprofielRequestHandler.cs
- ELoket.Service\Beheer\Ontvangersprofiel\DeleteOntvangersprofielRequestHandler.cs
```

### Prompt 2.3 — DTO's
**Goal:** API contract DTOs decoupled from the domain model.
**System Prompt / Role:** Act as a Senior .NET Backend Developer expert in Clean Architecture, EF Core, and FluentMigrator.
**Instructions for the Coding AI:**
```text
First, READ the DTOs under `ELoket.Host\Dto\` used for AanvraagProfiel (locate the AanvraagProfiel DTO folder) to match naming, property casing, and how create/update DTOs differ from read DTOs.

Then create in `ELoket.Host\Dto\Ontvangersprofiel\`:
- Ontvangersprofiel.cs        -> read DTO: Id, Naam, AantalBovenliggendeNiveaus, Personen (list of person id/display), Emails (list of string).
- CreateOntvangersprofiel.cs  -> Naam, AantalBovenliggendeNiveaus, Personen (ids), Emails (strings). No Id.
- UpdateOntvangersprofiel.cs  -> Id, Naam, AantalBovenliggendeNiveaus, Personen, Emails.

Keep property names aligned with what the handlers in 2.1/2.2 consume. No git/PR/issue comments.

Expected Output (files to CREATE):
- ELoket.Host\Dto\Ontvangersprofiel\Ontvangersprofiel.cs
- ELoket.Host\Dto\Ontvangersprofiel\CreateOntvangersprofiel.cs
- ELoket.Host\Dto\Ontvangersprofiel\UpdateOntvangersprofiel.cs
```

### Prompt 2.4 — AutoMapper-profielen
**Goal:** Map domain ↔ DTO following existing conventions.
**System Prompt / Role:** Act as a Senior .NET Backend Developer expert in Clean Architecture, EF Core, and FluentMigrator.
**Instructions for the Coding AI:**
```text
First, READ `ELoket.Host\Infrastructure\AutoMapper\...\AanvraagProfielProfile.cs` to match the Profile class style: CreateMap directions, member mapping for nested collections, and how the profile is auto-discovered/registered.

Then create `ELoket.Host\Infrastructure\AutoMapper\Ontvangersprofiel\OntvangersprofielProfile.cs`:
- Map Ontvangersprofiel (domain) -> Ontvangersprofiel (DTO), including Personen and Emails.
- Map CreateOntvangersprofiel / UpdateOntvangersprofiel (DTO) -> the values the handlers need (or map to a small input model if AanvraagProfiel does that).

Ensure it is registered the same way the AanvraagProfiel profile is. No git/PR/issue comments.

Expected Output (files to CREATE):
- ELoket.Host\Infrastructure\AutoMapper\Ontvangersprofiel\OntvangersprofielProfile.cs
```

### Prompt 2.5 — Controller-endpoints
**Goal:** Expose the REST surface dispatching via `IMediator`, with beheerder authorization.
**System Prompt / Role:** Act as a Senior .NET Backend Developer expert in Clean Architecture, EF Core, and FluentMigrator.
**Instructions for the Coding AI:**
```text
First, READ `ELoket.Host\Controllers\BeheerderController.cs` (where AanvraagProfiel endpoints live) to match: routing attributes, authorization attributes/roles applied to beheer endpoints, how IMediator is injected and used, DTO in/out, and status-code conventions.

Decision: ADD the endpoints to a NEW `ELoket.Host\Controllers\OntvangersprofielController.cs` (preferred for cohesion) using the SAME base route prefix/authorization as BeheerderController. If the codebase convention is to keep beheer endpoints together, extend BeheerderController instead — match the dominant pattern.

Endpoints (all dispatch through IMediator to the handlers from 2.1/2.2):
- GET    /...ontvangersprofielen           -> list (GetOntvangersprofielen)
- GET    /...ontvangersprofielen/{id}       -> detail (GetOntvangersprofielById)
- POST   /...ontvangersprofielen            -> create
- PUT    /...ontvangersprofielen/{id}        -> update
- DELETE /...ontvangersprofielen/{id}        -> delete (returns the in-use refusal cleanly, e.g. 409/400 with message)

Apply the SAME beheerder-role authorization as the other beheer endpoints. No git/PR/issue comments.

Expected Output (files to CREATE or MODIFY):
- ELoket.Host\Controllers\OntvangersprofielController.cs (new)  [or modify BeheerderController.cs]

Verification: `dotnet build src\Eloket.sln` succeeds; endpoints resolve.
```

---

## PHASE 3 — Organigram-node e-mailveld
*Context & goal: add an `Email` field to an organigram node so communication can target an organisation level directly. This is the "bovenliggend niveau" target the recipient profile uses. Migration number M254 (after M253). End state: node email is editable through the existing node-edit endpoint.*

### Prompt 3.1 — Veld `Email` op `OrganigramNode`
**Goal:** Add nullable `Email` to the `OrganigramNode` domain entity.
**System Prompt / Role:** Act as a Senior .NET Backend Developer expert in Clean Architecture, EF Core, and FluentMigrator.
**Instructions for the Coding AI:**
```text
First, READ `ELoket.Domain\Organigram\OrganigramNode.cs` fully to understand its constructor, the existing `Update`/mutator method(s), and how `DisplayNaam` (or similar recently added fields) is threaded through.

Then add `string? Email` to OrganigramNode:
- Add the property (nullable, private setter consistent with the entity).
- Thread it through the constructor AND the Update method exactly like an existing optional field (e.g. follow how DisplayNaam is passed). Do not break existing callers — add the parameter in a way consistent with the entity's existing optional-field handling.

No git/PR/issue comments.

Expected Output (files to MODIFY):
- ELoket.Domain\Organigram\OrganigramNode.cs
Report any constructor/Update call sites that now need the new argument.
```

### Prompt 3.2 — Migratie `M254_AddEmailToOrganigramNode`
**Goal:** Add the nullable `Email` column to the `OrganigramNode` table.
**System Prompt / Role:** Act as a Senior .NET Backend Developer expert in Clean Architecture, EF Core, and FluentMigrator.
**Instructions for the Coding AI:**
```text
First, READ `ELoket.Database\Migrations\M192_AddDisplayNaamToNodes.cs` to copy the add-column migration style (attribute/number, Alter.Table(...).AddColumn(...), nullable string length, Down() that removes the column). Confirm M253 (from Prompt 1.3) is now the latest, so M254 is the correct next number.

Then create `ELoket.Database\Migrations\M254_AddEmailToOrganigramNode.cs`:
- Add column `Email NVARCHAR(...) NULL` to the OrganigramNode table (use the same length convention as DisplayNaam).
- Existing rows get NULL (NULL = no mail for that level → nothing is sent for it).
- Implement Down() to drop the column.

FluentMigrator only. No git/PR/issue comments.

Expected Output (files to CREATE):
- ELoket.Database\Migrations\M254_AddEmailToOrganigramNode.cs

Verification: `dotnet run --project src\ELoket.Database` runs clean.
```

### Prompt 3.3 — EF-config + node-endpoint
**Goal:** Map `Email` and persist it through the existing node-edit endpoint.
**System Prompt / Role:** Act as a Senior .NET Backend Developer expert in Clean Architecture, EF Core, and FluentMigrator.
**Instructions for the Coding AI:**
```text
First, READ:
- `ELoket.Infrastructure\EntityFramework\Config\OrganigramNodeConfig.cs` (how DisplayNaam is mapped),
- `EditOrganigramNodeRequestHandler.cs` (locate it; how it loads the node and calls Update),
- the DTO that endpoint consumes (the edit-node DTO).

Then:
1) Map `Email` in OrganigramNodeConfig.cs (column name/length matching M254).
2) Add `Email` to the edit-node DTO.
3) Pass `Email` through EditOrganigramNodeRequestHandler into the node's Update method (the field added in Prompt 3.1).

This must back the existing node-edit screen (Node Id / Weergavenaam / E-mail / Opslaan). No git/PR/issue comments.

Expected Output (files to MODIFY):
- ELoket.Infrastructure\EntityFramework\Config\OrganigramNodeConfig.cs
- EditOrganigramNodeRequestHandler.cs
- the edit-node DTO file

Verification: `dotnet build src\Eloket.sln` succeeds.
```

---

## PHASE 4 — "Communicatie naar" uitbreiden
*Context & goal: extend the recipient options on an absence/leave-group with N+1, N+2 and a link to an `Ontvangersprofiel`. N+1/N+2 are implemented as new `Rol` enum values (option 1 — least DB impact), kept in the existing `AfwezigheidCommunicatie` mechanism. Migration M255. Existing enum values stay frozen.*

### Prompt 4.1 — `Rol`-enum uitbreiden met N+1 en N+2
**Goal:** Add `NPlus1 = 13` and `NPlus2 = 14` without disturbing existing values.
**System Prompt / Role:** Act as a Senior .NET Backend Developer expert in Clean Architecture, EF Core, and FluentMigrator.
**Instructions for the Coding AI:**
```text
First, READ `ELoket.Enums\Rol.cs`. Current values end at `Supervisor = 11, OntvangerMeldingDO = 12`.

Then add two new members:
- NPlus1 = 13
- NPlus2 = 14

CRITICAL: do NOT change, reorder, or renumber any existing enum value — the database stores these as ints and historical rows must keep their meaning. Match the existing naming/casing style in the enum.

No git/PR/issue comments.

Expected Output (files to MODIFY):
- ELoket.Enums\Rol.cs
```

### Prompt 4.2 — `OntvangersProfielId` op `Afwezigheid`
**Goal:** Add an optional FK from `Afwezigheid` to `Ontvangersprofiel` (domain + migration M255 + EF config).
**System Prompt / Role:** Act as a Senior .NET Backend Developer expert in Clean Architecture, EF Core, and FluentMigrator.
**Instructions for the Coding AI:**
```text
First, READ:
- `ELoket.Domain\Beheer\Afwezigheid.cs` — focus on how the existing `AanvraagProfiel` FK + navigation are declared, how the constructor/Update set it, and the `DetachAanvraagProfiel` method.
- `ELoket.Database\Migrations\M245_AfwezigheidAanvraagProfiel.cs` — the analogous FK-column migration.
- `AfwezigheidConfig.cs` — how the AanvraagProfiel relationship is configured.

Then implement, MIRRORING the AanvraagProfiel wiring exactly:
1) Domain (`ELoket.Domain\Beheer\Afwezigheid.cs`):
   - Add `int? OntvangersProfielId` + navigation property `Ontvangersprofiel` (type from ELoket.Domain.Ontvangersprofiel).
   - Thread it through the constructor and `Update`.
   - Add a `DetachOntvangersProfiel()` method analogous to `DetachAanvraagProfiel`.
2) Migration `ELoket.Database\Migrations\M255_AfwezigheidOntvangersProfiel.cs`:
   - Add nullable column `OntvangersProfielId INT NULL` to Afwezigheid + FK -> Ontvangersprofiel. Match M245's constraint/naming style. NO cascade delete from profile to afwezigheid (deletion is guarded in 4.3 instead) — use the same delete behaviour M245 uses for the AanvraagProfiel FK. Implement Down().
3) EF config (`AfwezigheidConfig.cs`):
   - Configure the optional relationship (HasOne/WithMany or WithOne as appropriate, matching AanvraagProfiel).

Confirm M254 is the latest before using M255. No git/PR/issue comments.

Expected Output:
- MODIFY ELoket.Domain\Beheer\Afwezigheid.cs
- CREATE ELoket.Database\Migrations\M255_AfwezigheidOntvangersProfiel.cs
- MODIFY AfwezigheidConfig.cs

Verification: migration runs clean; `dotnet build src\Eloket.sln` succeeds.
```

### Prompt 4.3 — Beschermen tegen verwijderen van een gebruikt profiel
**Goal:** Wire the real in-use check into the delete handler now that the FK exists.
**System Prompt / Role:** Act as a Senior .NET Backend Developer expert in Clean Architecture, EF Core, and FluentMigrator.
**Instructions for the Coding AI:**
```text
The FK `Afwezigheid.OntvangersProfielId` now exists (Prompt 4.2). Revisit `ELoket.Service\Beheer\Ontvangersprofiel\DeleteOntvangersprofielRequestHandler.cs` (Prompt 2.2).

Implement the in-use guard concretely:
- Before deleting, query `context.Afwezigheden.Any(a => a.OntvangersProfielId == id)` (constrained on the same InstellingId).
- If any reference exists, REFUSE the delete and return a clear, user-facing message (match how other beheer handlers surface a blocking validation error). Do NOT delete.
- Otherwise proceed (cascade removes Personen/Emails).

No git/PR/issue comments.

Expected Output (files to MODIFY):
- ELoket.Service\Beheer\Ontvangersprofiel\DeleteOntvangersprofielRequestHandler.cs
```

### Prompt 4.4 — Wijzig-/aanmaak-handlers afwezigheid aanpassen
**Goal:** Carry `OntvangersProfielId` (and N+1/N+2 roles) through the absence create/update handlers and DTOs.
**System Prompt / Role:** Act as a Senior .NET Backend Developer expert in Clean Architecture, EF Core, and FluentMigrator.
**Instructions for the Coding AI:**
```text
First, READ `NieuweAfwezigheidRequestHandler.cs`, `WijzigAfwezigheidRequestHandler.cs` and their DTOs `NieuweAfwezigheid.cs` / `WijzigAfwezigheid.cs` — note how `AanvraagProfielId` and the `Communicatie` (Rol[]) list are currently passed and persisted (including `UpdateCommunicatie`).

Then:
1) Add `int? OntvangersProfielId` to the DTOs `NieuweAfwezigheid.cs` and `WijzigAfwezigheid.cs`.
2) In both handlers, set/detach the profile on the Afwezigheid (use the constructor/Update + `DetachOntvangersProfiel` from Prompt 4.2). When the incoming id is null, detach; otherwise validate the profile belongs to the same InstellingId before linking.
3) The existing `Communicatie` (Rol[]) flow now legitimately accepts `NPlus1`/`NPlus2` values — ensure no validation/whitelist rejects them. The free-mail "Andere ontvangers" continues via the existing `AfwezigheidCommunicatieAndere`.

Multi-tenancy: validate the referenced profile's InstellingId. No git/PR/issue comments.

Expected Output (files to MODIFY):
- NieuweAfwezigheidRequestHandler.cs
- WijzigAfwezigheidRequestHandler.cs
- NieuweAfwezigheid.cs (DTO)
- WijzigAfwezigheid.cs (DTO)
```

---

## PHASE 5 — Frontend: beheer van Ontvangersprofielen
*Context & goal: Angular admin UI under Instellingen > Communicatie — menu item, overview list, and create/edit form — consuming the REST API from phase 2. Reuses the existing aanvraagprofiel people-picker/chips UI pattern.*

### Prompt 5.1 — Menu-item "Ontvangersprofielen"
**Goal:** Add the submenu entry + i18n key.
**System Prompt / Role:** Act as a Senior Angular Frontend Developer expert in this project's component, routing, and i18n conventions.
**Instructions for the Coding AI:**
```text
First, READ `ELoket.Client\src\app\features\beheerder-algemeen\beheerder-algemeen-header\beheerder-algemeen-header.component.ts` and note how existing `SubMenuItem`s under "Communicatie" (Meldingen / E-mail vrijaf / E-mail ex. prest. / E-mail Personalia) are declared (route, label/translation key, ordering).

Then:
1) Add a new `SubMenuItem` under "Communicatie" pointing to route `/beheerderalgemeen/communicatie/ontvangersprofielen`, placed consistently with the siblings.
2) Add the i18n key `BeheerAlgemeen.Communicatie.Header.Ontvangersprofielen` to ALL relevant translation files (locate the i18n files — likely nl/en JSON/XLIFF under ELoket.Client). Use the same key style as the sibling entries.

Match the exact style; do not introduce a new menu mechanism. No git/PR/issue comments.

Expected Output (files to MODIFY):
- beheerder-algemeen-header.component.ts
- the i18n translation file(s)
```

### Prompt 5.2 — Overzichtscomponent
**Goal:** List screen with search, Naam column, edit/delete actions, "toevoegen" button, API service, route.
**System Prompt / Role:** Act as a Senior Angular Frontend Developer expert in this project's component, routing, and service conventions.
**Instructions for the Coding AI:**
```text
First, READ an existing communicatie overview component (e.g. the Meldingen or aanvraagprofiel overview under `…\beheerder-algemeen\communicatie\…`) AND `beheerder.service.ts` to match: component folder structure (ts/html/scss), table/list markup, search-field handling, edit/delete action icons, "toevoegen" button wiring, route registration, and how services call the API.

Then create `…\beheerder-algemeen\communicatie\beheerder-communicatie-ontvangersprofielen\`:
- An overview component with: a search field, a list showing column "Naam" + edit/delete action icons per row, and an "Ontvangersprofiel toevoegen" button.
- Either EXTEND `beheerder.service.ts` or add a dedicated service that calls the phase-2.5 endpoints (GET list, GET/{id}, POST, PUT/{id}, DELETE/{id}).
- Register the route `/beheerderalgemeen/communicatie/ontvangersprofielen`.
- Delete action: handle the backend in-use refusal (4.3) by showing the returned error message (match existing error-toast/dialog pattern).
- Add i18n keys for all labels.

This is the overview from the analysis (e.g. "ATP te verwittigen bij ziekte", "OP te verwittigen bij ziekte"). No git/PR/issue comments.

Expected Output (files to CREATE/MODIFY):
- the new component folder (ts/html/scss)
- service file (new or modified beheerder.service.ts)
- routing module entry
- i18n files
```

### Prompt 5.3 — Aanmaak-/wijzig-form
**Goal:** Create/edit form with name, people picker, free emails, and upper-levels dropdown.
**System Prompt / Role:** Act as a Senior Angular Frontend Developer expert in this project's reactive-forms, people-picker, and chips conventions.
**Instructions for the Coding AI:**
```text
First, READ the existing aanvraagprofiel form component (locate it) to reuse: the people-picker element, the "chips" display for selected people, free-text + "add" + chips for emails, reactive form setup, and save/cancel wiring.

Then create a form component (under the same feature folder as 5.2) with these sections:
1) Profielnaam — text input.
2) "Personen binnen de organisatie" — REUSE the existing people-picker element; show selected people as chips ("Geselecteerde personen").
3) "Vrije e-mailadressen" — input + "E-mail toevoegen" button + chips; client-side e-mail format validation.
4) "Ontvangers op basis van organisatiestructuur" — a dropdown "Aantal bovenliggende organisatieniveaus" (0–N) + an info text explaining that each level needs an e-mail configured on the organigram node.
   - Do NOT yet build the "Mail template" section (subject + rich text + "Template actief") — that is an explicit later phase.
Buttons: "Annuleer" / "Bewaar en sluit", wired to POST (create) / PUT (update) via the service from 5.2.

Bind to the DTOs from phase 2.3 (Personen, Emails, AantalBovenliggendeNiveaus). Add i18n keys for every label. Match the aanvraagprofiel UI pattern exactly. No git/PR/issue comments.

Expected Output (files to CREATE/MODIFY):
- the form component folder (ts/html/scss)
- routing entry for create + edit
- i18n files
```

---

## PHASE 6 — Frontend: afwezigheidsform & organigram-node
*Context & goal: surface the new recipient options on the leave-group absence form and the node email field on the organigram-node form.*

### Prompt 6.1 — "Communicatie naar" in afwezigheidsform
**Goal:** Add N+1/N+2/Ontvangersprofiel options and prune struck-through options.
**System Prompt / Role:** Act as a Senior Angular Frontend Developer expert in this project's reactive-forms conventions.
**Instructions for the Coding AI:**
```text
First, READ `…\beheerder-kalenderjaar\verlofgroepen\…\verlofgroepen-afwezigheden-form\verlofgroepen-afwezigheden-form.component.ts` (and its template) to see how the current "Communicatie naar" checkboxes bind to the Rol list and how "Andere ontvangers" binds to AfwezigheidCommunicatieAndere.

Then update the form:
- Show checkboxes: Personeelslid, Dossierbeheerder / PA, N+1, N+2, Supervisor.
  (N+1/N+2 map to Rol.NPlus1=13 / Rol.NPlus2=14 from Prompt 4.1.)
- Add a checkbox "Ontvangersprofiel" + a "Selecteer een profiel" dropdown, populated via the service from Prompt 5.2; bind the selection to OntvangersProfielId (DTO field from Prompt 4.4).
- Keep checkbox "Andere ontvangers" + free text field (emails separated by ';') bound to AfwezigheidCommunicatieAndere.
- REMOVE from the UI choice list ONLY the struck-through options: VrijafOverurenGoedkeurder, UurroosterGoedkeurder, ApplicatieBeheerder, OrganigramBeheerder, OrganigramLezer, Boekhouding. Do NOT remove the enum values (historical data must not break) — only hide them from this picker.
- Add/adjust i18n keys for N+1, N+2, Ontvangersprofiel labels.

No git/PR/issue comments.

Expected Output (files to MODIFY):
- verlofgroepen-afwezigheden-form.component.ts (+ its .html)
- i18n files
```

### Prompt 6.2 — E-mailveld op organigram-node (frontend)
**Goal:** Add the node E-mail input bound to the phase-3 field.
**System Prompt / Role:** Act as a Senior Angular Frontend Developer expert in this project's reactive-forms conventions.
**Instructions for the Coding AI:**
```text
First, READ `…\beheerder-organigram\beheerder-organigram-node\beheerder-organigram-node-form\` (ts + html) to see how Weergavenaam is bound and saved through the node-edit API.

Then add an "E-mail" input field directly under "Weergavenaam", bound to the node Email field exposed by the phase-3.3 endpoint/DTO. Include client-side e-mail format validation. Add the i18n key for the "E-mail" label.

No git/PR/issue comments.

Expected Output (files to MODIFY):
- beheerder-organigram-node-form component (ts + html)
- i18n files
```

---

## PHASE 7 — Verzendlogica (ontvangers resolven + mailtemplate)
*Context & goal: derive the actual recipient e-mail list for an absence notification by merging all sources (roles + organigram walk for N+1/N+2 + profile expansion + free mails), dedup, optionally apply a per-profile mail template, and hook it into the existing send moment.*

### Prompt 7.1 — Ontvangers-resolver
**Goal:** A central service that resolves and deduplicates the final recipient list.
**System Prompt / Role:** Act as a Senior .NET Backend Developer expert in Clean Architecture, EF Core, and FluentMigrator.
**Instructions for the Coding AI:**
```text
First, READ and ANALYZE `ELoket.NetUpgrade.Application\Services\ETalent\GetManagerUrnService` — it walks the organigram upward via ParentNodeId and finds the OrganigramNodeFunctie holder whose FunctieTaken includes "Leidinggevende" active today. You will reuse/refactor this walk for N+1/N+2 resolution. Also inspect how existing role→email lookups work for AfwezigheidCommunicatie (Personeelslid, DossierBeheerder, Supervisor).

Then create a service `ELoket.Service\Communicatie\OntvangersResolver` (interface + implementation, registered in DI like sibling services) that, given an absence notification context, returns a deduplicated set of e-mail addresses by merging:
1) ROLES from AfwezigheidCommunicatie (Personeelslid, DossierBeheerder, Supervisor, …) via the existing lookup.
2) N+1 / N+2: walk the organigram upward via ParentNodeId; for N+1 find the 1st-level "Leidinggevende" function holder, for N+2 the 2nd. REUSE/refactor GetManagerUrnService logic rather than duplicating it; resolve the holder's e-mail.
3) ONTVANGERSPROFIEL (if Afwezigheid.OntvangersProfielId is set): expand profile Personen (their e-mails) + free Emails + the node e-mail of the first N upper levels (N = AantalBovenliggendeNiveaus). SKIP any level whose node has no e-mail (NULL).
4) ANDERE ONTVANGERS from AfwezigheidCommunicatieAndere.

Requirements:
- DEDUPLICATE the final list (case-insensitive e-mail compare).
- Filter all data access on InstellingId.
- Handle gracefully a missing/empty organigram (HOGent case): N+x/level logic yields nothing, fall back to profile personen + free mails without throwing. Do NOT let an unresolved manager throw and abort the whole send.

No git/PR/issue comments.

Expected Output (files to CREATE):
- ELoket.Service\Communicatie\OntvangersResolver\IOntvangersResolver.cs
- ELoket.Service\Communicatie\OntvangersResolver\OntvangersResolver.cs
- DI registration (in the existing service-registration file)
```

### Prompt 7.2 — Mailtemplate toepassen (latere fase)
**Goal:** Render a per-profile template with placeholders, else fall back to the standard mail.
**System Prompt / Role:** Act as a Senior .NET Backend Developer expert in Clean Architecture, EF Core, and FluentMigrator.
**Instructions for the Coding AI:**
```text
PRECONDITION: this step depends on the mailtemplate fields (MailOnderwerp, MailInhoud, bool TemplateActief) which were deferred from Prompt 1.1. FIRST add them:
- Add the three fields to Ontvangersprofiel (domain + EF config) and a new sequential FluentMigrator migration (next free Mxxx number — check the latest before writing) adding the columns. Mirror the patterns from Prompts 1.1/1.3/1.4.
- Surface them in the create/edit DTOs (2.3) and the form (5.3 "Mail template" section: subject + rich-text body + "Template actief" checkbox).

Then implement template rendering:
- READ how the existing mail infrastructure in `ELoket.Infrastructure` builds and sends absence mails.
- If the linked profile has TemplateActief == true, render MailOnderwerp/MailInhoud replacing placeholders {Voornaam} {Naam} {StartDatum} {EindDatum} {Extra info} with the notification's values; otherwise fall back to the existing standard mail.

No git/PR/issue comments.

Expected Output (files to CREATE/MODIFY):
- ELoket.Domain\Ontvangersprofiel\Ontvangersprofiel.cs (add template fields)
- new Mxxx migration
- EF config + DTOs + form
- the template-rendering integration in ELoket.Infrastructure / the send path
```

### Prompt 7.3 — Inhaken op het verzendmoment
**Goal:** Invoke the resolver at the existing absence-mail send point(s).
**System Prompt / Role:** Act as a Senior .NET Backend Developer expert in Clean Architecture, EF Core, and FluentMigrator.
**Instructions for the Coding AI:**
```text
First, LOCATE where absence notifications are sent today (search the absence/mail flow — e.g. on indienen/wijzigen afwezigheid, "bij terug in dienst", "bij verlenging"; trace from WijzigAfwezigheid/NieuweAfwezigheid handlers and the mail infrastructure). Identify the current hardcoded recipient logic.

Then replace the old hardcoded recipient derivation with a call to `IOntvangersResolver` (Prompt 7.1) at each relevant send moment, so the configured roles/N+x/profile/andere-ontvangers drive the actual recipients. If 7.2 is in place, route through the template; otherwise keep the standard mail body.

Preserve existing behaviour for absences with no new configuration (graceful fallback). No git/PR/issue comments.

Expected Output (files to MODIFY):
- the absence-send handler(s)/service(s) you located
Report exactly which send points you changed.
```

---

## PHASE 8 — Aandachtspunten & edge cases
*Context & goal: verification checklist — not new feature files, but explicit acceptance checks to run/validate against the analysis edge cases.*

### Prompt 8.1 — Edge-case verificatie & hardening
**Goal:** Validate the implementation against the institution-specific edge cases.
**System Prompt / Role:** Act as a Senior .NET Backend Developer + QA engineer reviewing the completed feature.
**Instructions for the Coding AI:**
```text
Review the implemented Ontvangersprofiel + verzendlogica against these cases and add tests/guards where a gap exists:

1) Artevelde (5 external HR partners): confirm external partners can be reached via organigram-node e-mails (phase 3) and/or free e-mail addresses in the profile (phase 1/5). No code change expected — verify with a test.
2) HOGent (no organigram): confirm the resolver (7.1) does NOT throw when the organigram is missing/empty and still returns profile Personen + free mails. Add a unit test for this path.
3) Terminology differs per HS (studiegebied/expertisenetwerk/cluster/onderwijsgroep): confirm the UI uses the neutral term "bovenliggend organisatieniveau" and the concrete name comes from the node Weergavenaam — NO per-HS hardcoding anywhere.
4) KDG / HOGent medewerker-interface people picker: note as a SEPARATE future task (out of beheer scope) — do not implement now; just confirm nothing blocks it.

Also re-verify the global Definition of Done:
- Migrations run clean (`dotnet run --project src\ELoket.Database`).
- CRUD handlers covered by xUnit tests (ELoket.Dosp.Tests requires Docker).
- Every query filters on InstellingId.
- Deleting an in-use profile is blocked (4.3).
- Resolver deduplicates and skips levels without an e-mail.
- Existing enum values unchanged; struck-through options removed from UI only.
- i18n keys added for all new labels.

No git/PR/issue comments.

Expected Output:
- any new xUnit tests (e.g. resolver empty-organigram test, in-use delete-block test)
- a short written report of which DoD items pass and any remaining gaps
```

---

## Aanbevolen oplevervolgorde (PR-suggestie)
1. **PR 1 — Datamodel & migraties:** Prompts 1.1–1.5, 3.1–3.2, 4.1–4.2 (entities, migrations M253–M255, EF config). *Backend builds, no UI.*
2. **PR 2 — Backend CRUD:** Prompts 2.1–2.5, 3.3, 4.3–4.4.
3. **PR 3 — Frontend beheer:** Prompts 5.1–5.3.
4. **PR 4 — Afwezigheidsform & node-veld:** Prompts 6.1–6.2.
5. **PR 5 — Verzendlogica:** Prompts 7.1, 7.3.
6. **PR 6 — Mailtemplate (latere fase):** Prompt 7.2 + template UI in 5.3.

Each PR is independently buildable/testable: `dotnet build src\Eloket.sln`, `dotnet test`, `ng serve`.
