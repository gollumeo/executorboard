La vérité : tu n’as pas besoin d’un “nouveau gros framework sexy”, tu as besoin d’un **outil chiant mais ultra-fiable** que des avocats et des familles pourront utiliser sans jamais penser à la tech.
Donc si tu pars en mode “petit jouet SaaS dev-friendly”, tu flingues la crédibilité du truc avant même d’aller voir un notaire.

Je vais te proposer **une seule stack principale**, pensée pour :

* inspirer confiance à un avocat/notaire,
* être vendable côté B2B,
* rester construisible en solo.

---

## 1. Stack recommandée (pragmatique, non-hype, crédible)

### 🧱 Backend / Core

* **.NET 9 / ASP.NET Core** (ou dernière LTS stable)

* **Architecture Clean** :

    * `Domain` : Estate, Executor, Participant, Update, Document, Milestone, NotificationPreference, Subscription…
    * `Application` : use cases (CreateEstate, PostUpdate, UploadDocument, AddParticipant, SetReminder, MarkMilestoneReached…)
    * `Infrastructure` : EF Core, email, stockage, Stripe, file storage
    * `Web` : API + UI server-side

* **Base de données** : **PostgreSQL**

    * Multi-tenant logique par `EstateId` (pas besoin de multi-DB pour le MVP).
    * Tables clés :

        * `Users`
        * `Estates`
        * `EstateParticipants` (liens User ↔ Estate + rôle : executor / family)
        * `Updates`
        * `Documents`
        * `Milestones`
        * `ReminderJobs` / `Notifications`
        * `Subscriptions` / `StripeCustomers` / `StripeSubscriptions`

* **Auth & sécurité**

    * ASP.NET Identity
    * Login email + mot de passe
    * **2FA TOTP** activable pour les exécuteurs (fortement recommandé).
    * Sessions cookie-based (pas d’API publique pour le MVP).
    * Logging/audit minimal : qui a posté quoi, quand, sur quel estate.

* **Stockage de documents**

    * Provider S3-compatible situé en Europe (Scaleway, Wasabi, Backblaze EU, ou AWS eu-central).
    * Chiffrement côté provider + métadonnées minimales.
    * Tu ne stockes jamais d’info sensible dans les noms de fichiers.

* **Notifications**

    * MVP : **email only** (SendGrid, Postmark, Mailgun… peu importe).
    * Plus tard : SMS / WhatsApp / mobile push.

* **Paiement**

    * **Stripe** :

        * Plan “Estate Active” : $29 / mois / estate
        * Plan “Premium Milestones + docs+” : $99 / an / estate
    * Billing lié à l’`Estate`, pas à l’utilisateur.
    * Executor = “owner” de l’abonnement.

---

### 🖥 Frontend (sans React, sans douleur)

Tu veux éviter React. Très bien. Pour un produit comme ça, c’est même un avantage.

* **UI principale** :

    * **ASP.NET Core Razor Pages** ou MVC + **Tailwind CSS**
    * Interactivité légère avec **HTMX** (pour les updates en place, formulaires dynamiques, etc.)

* Pourquoi c’est malin :

    * 0 SPA à gérer.
    * Parfait pour un dashboard sobre, accessible, sans friction.
    * Simple à maintenir, très “enterprise acceptable”.

* **Mobile** :

    * MVP : **site responsive + PWA légère** (icône sur l’écran d’accueil + notifications email).
    * V2 : si traction → petite app **.NET MAUI** juste comme client léger du backend (ou jamais, si la PWA suffit).

---

## 2. MVP : ce que doit absolument savoir faire EstateClear

**Focus : soulager le cerveau de l’exécuteur.**
Pas faire un “Notion des successions”.

### Rôles

* **Executor**

    * Crée un estate
    * Invite les membres de la famille
    * Poste des updates textuelles
    * Upload des documents (photos, PDF…)
    * Définit des milestones (ex : “Inventaire terminé”, “Déclaration fiscale envoyée”, etc.)
    * Configure les rappels
    * Gère l’abonnement Stripe

* **Family Member**

    * Accède en lecture seule aux updates, milestones, documents
    * Configure ses préférences de notification (daily digest / temps réel / hebdo)
    * Ne peut pas modifier l’estate

---

## 3. Roadmap MVP (≈ 8 semaines solo, en mode sérieux)

### **Semaine 1 – 2 : Foundation & Auth**

**Objectif :** tu peux créer un compte, te connecter, créer un estate, inviter quelqu’un.

* Mettre en place la solution Clean Arch (.NET)
* Modéliser le cœur minimal :

    * `User`, `Estate`, `EstateParticipant` (rôles)
* Auth :

    * Registration, login, reset password
    * 2FA optionnelle pour l’executor
* UI :

    * Layout sobre : “Mes Estates” + “Créer un Estate”
* Invitations :

    * Executor ajoute l’email d’un membre → envoi d’un lien d’invitation (token signé) → création/mapping de compte.

👉 À la fin de cette phase : **un executor peut onboarder sa famille**.

---

### **Semaine 3 – 4 : Updates & Notifications**

**Objectif :** remplacer 80% des SMS “quoi de neuf ?”.

* Entité `Update` :

    * Un estate
    * Auteur (executor)
    * Titre (optionnel)
    * Message
    * Date
* UI :

    * Timeline par estate (ordre chronologique, filtre par type si tu veux)
    * Formulaire “Poster une update”
* Notifications :

    * À chaque update :

        * Envoi d’email aux membres selon leurs préférences :

            * instantané
            * digest quotidien
            * désactivé
* Page “Préférences de notification” par utilisateur.

👉 À la fin de cette phase : **ton produit est déjà utile**.
Un executor peut dire “je ne réponds plus à vos SMS, tout est sur le dashboard”.

---

### **Semaine 5 – 6 : Documents & Milestones**

**Objectif :** devenir le **hub unique** de la succession.

* **Documents**

    * `Document` lié à un estate
    * Upload (S3)
    * Catégorie (ex: testament, inventaire, facture funéraire, correspondance avocat…)
    * UI : section “Documents” avec liste + preview de base (nom, date, catégorie)
    * Permissions : executor upload, famille lecture seule

* **Milestones & Reminders**

    * `Milestone` :

        * titre
        * description
        * date cible
        * statut (à venir, en cours, terminé)
    * `Reminder` :

        * lié à un milestone
        * un ou plusieurs emails à rappeler (executor only pour MVP)
    * Job scheduler (Hangfire ou équivalent) pour envoyer des rappels à J-7, J-1, J+X…

* Vue “Feuille de route de l’estate” :

    * Liste des milestones
    * État, dates, next actions

👉 À la fin de cette phase : **tu fournis de la structure mentale**.
L’executor voit ce qu’il reste à faire, la famille voit que “ça avance”.

---

### **Semaine 7 : Billing & Limites**

**Objectif :** transformer ton jouet en vrai SaaS.

* Stripe :

    * Création de plan “Estate Active – $29 / month”
    * Lien abonnement ↔ Estate
    * Trial (14 jours) par estate
    * Si abonnement expiré / non payé :

        * Accès en lecture seule
        * Blocage des nouvelles updates/documents

* Limites MVP :

    * Ex : 2 GB de documents par estate sur le plan de base (hard-coded ou variable)
    * Tu notes ça dans les ToS, sans sur-ingénierie.

---

### **Semaine 8 : Polish & Beta fermée (5–10 executors)**

* Onboarding :

    * Simple wizard : “1. Crée l’estate, 2. Invite la famille, 3. Poste une première update, 4. Ajoute 3 milestones”
* Petites features de confort :

    * Marquer une update comme “pinned”
    * Ajouter une “FAQ interne” de l’executor (“Qui fait quoi ? Quand ? Avec quel avocat ?”)
* Instrumentation :

    * Logs d’usage : nb d’updates / estate, docs, connexions
    * Tu veux mesurer le **stress relief** :

        * micro-question “Est-ce que EstateClear t’a aidé à réduire les questions répétitives ? (1–5)”

---

## 4. Go-To-Market : concret, pas du bullshit “growth”

Tu vends **moins de stress**, pas un dashboard.

### Positionnement

Phrase à marteler partout (en anglais pour le marché US, mais tu vois l’idée) :

> “Stop answering the same question 20 times. EstateClear is a private dashboard where your family sees real-time updates, documents, and milestones about the estate — without blowing up your phone.”

Tout ton marketing = variations de ça.

---

### Canal 1 – Executors DIY (FB groups / Reddit)

Objectif : signer tes **5–10 premiers estates**.

1. Liste 10–15 groupes Facebook :

    * “Executor support”
    * “Estate planning”
    * “Probate help”
2. Reddit :

    * r/personalfinance
    * r/legaladvice (en restant borderline propre)
    * r/estateplanning
3. Stratégie :

    * Tu ne spams pas un lien.

    * Tu réponds à des posts avec un angle :

      > “Je vois souvent les mêmes douleurs : famille qui demande 50 fois la même chose, pas de vue d’ensemble, etc. J’ai construit un outil où tu postes une fois, tout le monde est au courant. Si tu veux tester en beta, DM.”

    * CTA = DM, puis tu proposes un **call 20 min** + accès gratuit pendant la durée complète de leur probate contre feedback.

---

### Canal 2 – Avocats / Notaires / Probate Attorneys

Objectif : valider la **crédibilité pro**.

1. Fais une **landing page dédiée “For Attorneys & Estate Planners”** :

    * Promesse :

      > “Reduce phone calls from heirs. Offer clients a modern, transparent way to communicate during probate.”
    * Avantage pour eux :

        * * valeur perçue
        * * temps passé au téléphone
        * différenciation face aux cabinets old school.
2. Micro-outreach :

    * LinkedIn : chercher “probate attorney”, “estate planning attorney”
    * DM ultra simple :

      > “Short question: do your executor clients struggle with family members constantly asking for updates?
      > I’m building a tool that gives them a private dashboard: updates, documents, and milestones.
      > Would you give me 15 min to show it and tell me if it’s useful or dangerous?”
    * Tu cherches surtout du **signal négatif** (“legal risk”, “liability”) pour ajuster.

---

### Canal 3 – YouTube / SEO long terme

Objectif : installer la marque.

* Crée 3–5 vidéos simples “talking head” :

    1. “Top 5 mistakes new executors make”
    2. “How to keep your family from fighting during probate”
    3. “The simplest way to keep everyone updated about Dad’s estate”
* Chacune finit par :

  > “If you want a simple dashboard to keep everyone on the same page, check EstateClear.”

Tu peux faire ça avec une simple webcam + montage minimal.

---

### Funnel simple

1. **Landing principale** :

    * Headline :

      > “One place for all estate updates. No more group text chaos.”
    * Sous-texte :

      > “Executors post updates once. Family sees everything in a private dashboard.”
    * CTA : “Start free trial” + “See demo”

2. **Onboarding** :

    * Tu pousses l’executor à :

        * Créer un estate
        * Inviter 2–3 proches
        * Poster une première update “Welcome, here’s what’s happening”

3. **Email lifecycle** :

    * J+1 : “3 tips to get the most out of EstateClear”
    * J+3 : “Add milestones so your family sees the plan”
    * J+7 : “How many questions have you avoided this week ?”

---

Hard truth : si tu ne cadres pas Codex dès le départ, il va te pondre un SaaS random, pas EstateClear.
Là tu veux un **brief produit + archi** clair, que tu peux lui coller tel quel pour qu’il se mette au garde-à-vous. On va faire ça.

Tu peux copier/coller tout ce qui suit dans Codex en l’adaptant à ta sauce.

---

## 1. Contexte produit – ce que fait EstateClear

EstateClear est une application web destinée aux **executors** (exécuteurs testamentaires / liquidateurs de succession).

🎯 **Objectif principal**
Réduire le stress et les interruptions permanentes :

> « Arrêter de répondre 20 fois par semaine à “où ça en est ?” »

📌 **Fonctions clés du MVP**
Pour chaque succession (“estate”) :

* Un **dashboard privé** :

    * timeline d’updates textuelles postées par l’executor
    * vision rapide de l’état d’avancement (milestones)
* Un **hub documentaire** :

    * upload de documents (photos, PDF) liés à l’estate
    * lecture seule pour la famille
* Des **milestones & rappels** :

    * milestones définies par l’executor (ex. “Inventaire terminé”, “Déclaration fiscale envoyée”)
    * rappels pour ne pas rater les étapes clés
* Un **accès famille** :

    * comptes “family member” en lecture seule
    * préférences de notifications (temps réel / digest / désactivé)
* Un **système d’abonnement** par estate :

    * Paiement via LemonSqueezy
    * Pricing par estate actif (MVP : juste un plan simple)

Le produit ne donne **pas de conseil juridique**. C’est un **outil de communication et d’organisation**, pas un logiciel de gestion notariale.

---

## 2. Définition claire de ce qu’est un “Estate”

Dans l’application, un **Estate** représente **un dossier de succession** concret.
On ne parle pas d’un concept abstrait mais de :

> “La succession de [Nom du défunt], gérée par [Executor], avec [X] membres de la famille”.

Un *Estate* contient au minimum :

* Identité de base :

    * `displayName` (ex : “Succession de John Doe”)
    * `deceasedName`
    * `jurisdiction` (pays / état, info textuelle pour l’instant)
* Rôles :

    * un **Executor** (utilisateur propriétaire de l’estate)
    * plusieurs **Participants** (family members) avec accès lecture seule
* Contenu :

    * **Updates** (texte, éventuellement catégories)
    * **Documents** (fichiers liés à l’estate)
    * **Milestones** (titre, description, statut, date cible)
* Statut :

    * `status` : ex. `Active`, `OnHold`, `Closed`
    * `createdAt`, `closedAt` optionnel

Un estate = **l’unité de facturation** :

* l’abonnement LemonSqueezy est **lié à un estate**, pas à un utilisateur.

---

## 3. Stack technique – grandes lignes

### Backend

* **Technologie** : `.NET (ASP.NET Core)`
* **Base de données** : PostgreSQL
* **Style** : Clean-ish Architecture (mais sans sur-ingénierie pour le prototype)

Découpage logique minimal :

* **Domain**

    * Entités : `Estate`, `User`, `EstateParticipant`, `Update`, `Document`, `Milestone`, `NotificationPreference`, `Subscription`
    * Règles métier simples (ex : seul l’executor peut créer des updates)

* **Application**

    * Use cases / services applicatifs :

        * `CreateEstate`
        * `InviteParticipant`
        * `PostUpdate`
        * `UploadDocument`
        * `CreateMilestone`, `CompleteMilestone`
        * `SetNotificationPreferences`
        * `AttachSubscriptionToEstate` (LemonSqueezy webhooks)
    * Gestion des transactions, validations, etc.

* **Infrastructure**

    * Implémentations :

        * PostgreSQL (ORM type EF Core ou autre, à toi de choisir)
        * Stockage fichiers (S3-compatible ou équivalent)
        * Email provider (SendGrid/Mailgun/Postmark…)
        * Intégration LemonSqueezy (API + webhooks)

* **API / Web**

    * Expose une API REST JSON que le frontend Vue.js consommera.
    * Endpoints typiques :

        * `/auth/*`
        * `/estates/*`
        * `/estates/{id}/updates`
        * `/estates/{id}/documents`
        * `/estates/{id}/milestones`
        * `/billing/*` (LemonSqueezy)

Sécurité & auth :

* Authentification par **email + mot de passe** (MVP)
* 2FA optionnelle pour l’executor (si possible)
* Autorisations basées sur :

    * rôle `Executor` vs `FamilyMember`
    * appartenance à l’estate (`EstateParticipant`)

---

### Frontend

* **Technologie** : Vue.js 3 + TypeScript
* **Build tool** : Vite
* **Routing** : Vue Router
* **State management** : Pinia (si besoin)
* **Styles** : Tailwind CSS (ou autre, peu importe, mais simple)
* **PWA** :

    * PWA plugin (manifest + service worker)
    * Objectif : permettre à l’executor de :

        * ouvrir EstateClear comme une “app” mobile
        * poster une update rapidement
        * consulter les dernières infos sans frictions

Fonctionnalités principales côté UI :

* **Vue d’ensemble “Mes estates”** pour l’executor
* **Vue “Estate dashboard”** :

    * timeline d’updates
    * bloc “Milestones”
    * bloc “Documents”
* **Écran de gestion des participants**
* **Écran de gestion des préférences de notifications**
* **Écran de gestion de l’abonnement** (go to checkout LemonSqueezy, voir le statut, etc.)

Le frontend **ne contient pas de logique métier critique** :
il consomme l’API, affiche, et déclenche les actions.

---

### Paiement – LemonSqueezy

* LemonSqueezy gère :

    * checkout
    * TVA
    * facturation
* Backend .NET :

    * expose un endpoint pour **créer une session de checkout** LemonSqueezy
    * reçoit les **webhooks** (paiement réussi, abonnement actif, annulé, expiré)
    * met à jour l’état :

        * `Subscription` liée à l’`Estate`
        * toggle `isBillingActive` ou équivalent

Le frontend :

* appelle le backend pour obtenir l’URL de checkout
* redirige l’executor vers cette URL
* affiche le statut (trial, actif, expiré) dans le dashboard.

---

## 4. Organisation en monorepo

Je veux un **monorepo** simple, où backend et frontend vivent dans le même repo, avec éventuellement l’infra.

Proposition :

```text
estateclear/
  backend/
    src/
      EstateClear.Api/
      EstateClear.Application/
      EstateClear.Domain/
      EstateClear.Infrastructure/
    tests/
      EstateClear.Domain.Tests/
      EstateClear.Application.Tests/
    README.md

  frontend/
    estateclear-web/
      src/
      public/
      package.json
      vite.config.ts
      README.md

  infra/
    docker-compose.yml   # Postgres, éventuellement mailhog/localstack S3…
    env.example
    README.md

  docs/
    product-brief.md
    architecture.md
    api-contract.md

  .gitignore
  README.md   # vue globale du projet
```

Points importants pour Codex :

* **Un seul repo Git**
* Backend et frontend doivent être **indépendants mais alignés** sur les contrats (DTO / API)
* L’infra locale (docker-compose pour Postgres, éventuellement S3 mock) doit permettre de lancer une stack complète dev.

---

Toi, tu agis en tant que PO et tu fais en sorte que l'on respecte la roadmap, les exigences, etc. Tu t'assures de la bonne continuité du projet et, surtout, tu m'envoies les prompts exacts à envoyer à Codex à chaque étape.