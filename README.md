# ExecutorBoard

Backend foundation and marketing site for a sovereign “executor board” that keeps estate communication structured. The repository is a monorepo with a .NET 10 Clean Architecture API and a Vue 3 static landing page.

## Repository layout
- `apps/api/ExecutorBoard` – .NET 10 solution with Domain/Application/Infrastructure/Persistence/Presentation/API projects (`ExecutorBoard.sln`).
- `apps/landing` – Vue 3 + Vite + ViteSSG static site with a few marketing pages and custom styling.

## Backend (apps/api/ExecutorBoard)
- Architecture: Domain-driven layering (Domain → Application → Persistence → Infrastructure → Presentation/API). The API host is wired through `Runeforge`; controllers and endpoints are not implemented yet.
- Domain: Estates and Auth aggregates with value objects (`EstateId`, `ExecutorId`, `EstateName`, `ParticipantId`, `InvitationToken`, `Email`, `PasswordHash`, `SessionToken`, etc.). Key rules covered by tests include required executor IDs, trimmed/normalized estate names (min length 2), unique estate names per executor, no renames after closing or when participants exist, participants unique by email, updates allowed only by the executor on an active estate, and closing an estate clears participants.
- Application: Command/query flows for creating/renaming/closing estates, posting updates, granting/revoking access, inviting/accepting participants, projecting estates/participants, and basic auth (sign-up, sign-in returning a session token, sign-out).
- Persistence: EF Core 10 with PostgreSQL and SQLite providers. `ExecutorBoardDbContext` maps `Users` and `Estates`; repository implementations (`UsersRepositoryEf`, `EstatesRepositoryEf`) fulfill the application ports.
- Tests: xUnit suite in `ExecutorBoard.Tests` covering domain invariants, application flows, and EF repositories. Run with:
  - `dotnet test apps/api/ExecutorBoard/ExecutorBoard.Tests/ExecutorBoard.Tests.csproj`
- Local services: `apps/api/ExecutorBoard/docker-compose.yml` provides PostgreSQL, Redis, and pgAdmin containers (the API is not yet wired to Redis).

## Frontend (apps/landing)
- Stack: Vue 3 + TypeScript, Vite, ViteSSG, and Tailwind-inspired custom styles.
- Routes: `/` (hero, how-it-works, why-it-matters sections), `/for-attorneys`, `/guides/how-to-keep-heirs-updated`.
- UI: Static marketing content with an email capture form that currently logs to the console.
- Scripts: `pnpm dev`, `pnpm build`, `pnpm preview`, `pnpm lint` (see `package.json`).

## Getting started
- Prereqs: .NET 10 SDK (preview/RC), Node.js 18+, pnpm.
- Backend: `cd apps/api/ExecutorBoard && dotnet restore`; run tests with the command above; API host is scaffolded but has no public endpoints yet.
- Landing: `cd apps/landing && pnpm install`; run `pnpm dev` for local preview or `pnpm build` for static generation.

## Notes
- The codebase follows Clean Architecture and DDD boundaries; avoid inventing new domain rules without tests.
- Infrastructure and Presentation projects are placeholders awaiting wiring to the API host.
