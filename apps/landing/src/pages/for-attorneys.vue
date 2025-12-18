<template>
  <main class="bg-ivory">
    <header class="section section--hero">
      <div class="container grid items-center gap-8 md:grid-cols-2">
        <div class="space-y-6">
          <p class="muted uppercase tracking-wide text-sm max-md:pt-0 max-md:pb-1 text-wrap">
            For attorneys
          </p>
          <h1 class="heading-xl">A quiet record for estate communication</h1>
          <p class="text-body text-wrap">
            ExecutorBoard is a structured reference point for updates, documents, and decisions—so estate communication
            stays consistent, calm, and traceable without turning your practice into the messenger.
          </p>
          <div class="flex flex-wrap gap-3">
            <RouterLink class="btn btn-primary max-md:py-3" to="/guides/how-to-keep-heirs-updated">
              The approach
            </RouterLink>
            <RouterLink class="btn btn-secondary btn-sm max-md:py-3" to="/">Back to home</RouterLink>
          </div>
        </div>

        <aside class="card card-warm card-surface space-y-4">
          <h2 class="heading-md">Practice alignment</h2>
          <ul class="space-y-3">
            <li class="flex items-start gap-3">
              <span class="mt-1 h-2.5 w-2.5 rounded-full bg-gold-quiet"></span>
              <p class="text-body text-wrap">One stable reference for every stakeholder.</p>
            </li>
            <li class="flex items-start gap-3">
              <span class="mt-1 h-2.5 w-2.5 rounded-full bg-gold-quiet"></span>
              <p class="text-body text-wrap">A disciplined way to share factual progress, without legal advice.</p>
            </li>
            <li class="flex items-start gap-3">
              <span class="mt-1 h-2.5 w-2.5 rounded-full bg-gold-quiet"></span>
              <p class="text-body text-wrap">A visible record of what was shared and when.</p>
            </li>
            <li class="flex items-start gap-3">
              <span class="mt-1 h-2.5 w-2.5 rounded-full bg-gold-quiet"></span>
              <p class="text-body text-wrap">Fewer routine interruptions on repeated questions.</p>
            </li>
          </ul>
        </aside>
      </div>
    </header>

    <GoldDivider />

    <section aria-labelledby="reality-title" class="section">
      <SectionHeading
        id="reality-title"
        title="The reality attorneys face"
        subtitle="Estate administration is rarely limited to law. It is communication under stress."
      />

      <ul class="container mt-6 md:mt-8 grid card-gap md:gap-y-12 grid-cols-1 sm:grid-cols-2">
        <li class="card card-warm card-surface space-y-3">
          <h3 class="heading-md">Emotional pressure</h3>
          <p class="text-body muted text-wrap">
            Clients make decisions while grieving. Silence is interpreted. Tone matters as much as content.
          </p>
        </li>

        <li class="card card-warm card-surface space-y-3">
          <h3 class="heading-md">Misaligned heirs</h3>
          <p class="text-body muted text-wrap">
            Different versions of the same update circulate. Expectations drift. Trust becomes fragile.
          </p>
        </li>

        <li class="card card-warm card-surface space-y-3">
          <h3 class="heading-md">Repetition becomes routine</h3>
          <p class="text-body muted text-wrap">
            Small questions multiply across calls and emails. Your team becomes the relay for status updates.
          </p>
        </li>

        <li class="card card-warm card-surface space-y-3">
          <h3 class="heading-md">A fragile record</h3>
          <p class="text-body muted text-wrap">
            Key documents and decisions are scattered. Context gets lost. Follow-up becomes harder than it should be.
          </p>
        </li>
      </ul>
    </section>

    <GoldDivider />

    <section aria-labelledby="changes-title" class="section">
      <SectionHeading
        id="changes-title"
        title="What changes when information is structured"
        subtitle="Not more communication—one coherent reference."
      />

      <ul class="container mt-6 md:mt-8 grid card-gap md:gap-y-12 grid-cols-1 sm:grid-cols-2 xl:grid-cols-3">
        <li class="card card-warm card-surface space-y-3">
          <h3 class="heading-md">A single account</h3>
          <p class="text-body muted text-wrap">
            Updates live in one place, in one format. Contradictions and memory gaps lose ground.
          </p>
        </li>

        <li class="card card-warm card-surface space-y-3">
          <h3 class="heading-md">A calmer boundary</h3>
          <p class="text-body muted text-wrap">
            Heirs consult the record first. Escalation becomes intentional, not constant.
          </p>
        </li>

        <li class="card card-warm card-surface space-y-3 sm:col-span-2 sm:max-w-lg sm:mx-auto xl:col-span-1 xl:max-w-full">
          <h3 class="heading-md">A traceable narrative</h3>
          <p class="text-body muted text-wrap">
            The estate progresses in a documented sequence—documents, milestones, and decisions grouped with context.
          </p>
        </li>
      </ul>
    </section>

    <GoldDivider />

    <section id="fit" aria-labelledby="fit-title" class="section">
      <SectionHeading
        id="fit-title"
        title="How ExecutorBoard fits into a legal practice"
        subtitle="A neutral space that supports your work and protects the attorney–client relationship."
      />

      <ol class="container mt-6 md:mt-8 grid card-gap md:gap-y-12 grid-cols-1 sm:grid-cols-2 xl:grid-cols-3">
        <li class="card card-warm card-surface space-y-3">
          <span class="step-badge">1</span>
          <h3 class="heading-md">Counsel sets the perimeter</h3>
          <p class="text-body muted text-wrap">
            Define what belongs in the estate file: factual milestones, documents, and neutral notes.
          </p>
        </li>

        <li class="card card-warm card-surface space-y-3">
          <span class="step-badge">2</span>
          <h3 class="heading-md">The executor maintains the file</h3>
          <p class="text-body muted text-wrap">
            Your client keeps the record current as work progresses, without ad-hoc narratives scattered across inboxes.
          </p>
        </li>

        <li class="card card-warm card-surface space-y-3 sm:col-span-2 sm:max-w-lg sm:mx-auto xl:col-span-1 xl:max-w-full">
          <span class="step-badge">3</span>
          <h3 class="heading-md">Stakeholders consult the reference</h3>
          <p class="text-body muted text-wrap">
            Everyone sees the same record. Routine questions settle. Communication becomes easier to keep consistent.
          </p>
        </li>
      </ol>
    </section>

    <GoldDivider />

    <section aria-labelledby="definition-title" class="section">
      <SectionHeading
        id="definition-title"
        title="What ExecutorBoard is / What it is not"
        subtitle="Clear boundaries preserve trust."
      />

      <div class="container mt-6 md:mt-8 grid card-gap grid-cols-1 md:grid-cols-2">
        <div class="card card-warm card-surface space-y-4">
          <h3 class="heading-md">What it is</h3>
          <ul class="space-y-3">
            <li class="flex items-start gap-3">
              <span class="mt-1 h-2.5 w-2.5 rounded-full bg-gold-quiet"></span>
              <p class="text-body muted text-wrap">A structured workspace for factual estate communication.</p>
            </li>
            <li class="flex items-start gap-3">
              <span class="mt-1 h-2.5 w-2.5 rounded-full bg-gold-quiet"></span>
              <p class="text-body muted text-wrap">A neutral record of updates, documents, and decisions.</p>
            </li>
            <li class="flex items-start gap-3">
              <span class="mt-1 h-2.5 w-2.5 rounded-full bg-gold-quiet"></span>
              <p class="text-body muted text-wrap">A shared reference that supports consistent messaging.</p>
            </li>
          </ul>
        </div>

        <div class="card card-warm card-surface space-y-4">
          <h3 class="heading-md">What it is not</h3>
          <ul class="space-y-3">
            <li class="flex items-start gap-3">
              <span class="mt-1 h-2.5 w-2.5 rounded-full bg-gold-quiet"></span>
              <p class="text-body muted text-wrap">Legal advice, legal automation, or a substitute for counsel.</p>
            </li>
            <li class="flex items-start gap-3">
              <span class="mt-1 h-2.5 w-2.5 rounded-full bg-gold-quiet"></span>
              <p class="text-body muted text-wrap">A firm case-management system.</p>
            </li>
            <li class="flex items-start gap-3">
              <span class="mt-1 h-2.5 w-2.5 rounded-full bg-gold-quiet"></span>
              <p class="text-body muted text-wrap">A messaging inbox designed to replace existing communication.</p>
            </li>
            <li class="flex items-start gap-3">
              <span class="mt-1 h-2.5 w-2.5 rounded-full bg-gold-quiet"></span>
              <p class="text-body muted text-wrap">A decision engine.</p>
            </li>
          </ul>
        </div>
      </div>
    </section>

    <section class="cta-bento-wrapper">
      <div class="container max-w-5xl">
        <div class="cta-bento space-y-6">
          <p class="cta-eyebrow">EXECUTORBOARD</p>
          <h2 class="heading-lg">A disciplined reference, offered per estate</h2>
          <p class="text-body max-sm:text-[15px] max-sm:leading-relaxed text-wrap">
            ExecutorBoard is a paid subscription, provisioned per estate. It exists to maintain order in communication
            and documentation—without providing legal advice or automating legal judgment.
          </p>
          <div class="flex flex-wrap gap-3">
            <RouterLink class="btn btn-primary max-md:py-3" to="/guides/how-to-keep-heirs-updated">
              The approach
            </RouterLink>
            <RouterLink class="btn btn-secondary btn-sm max-md:py-3" to="/">Back to home</RouterLink>
          </div>
        </div>
      </div>
    </section>

    <div class="container pb-10 md:pb-14">
      <RouterLink class="muted text-sm inline-flex items-center gap-2 hover:text-anthracite" to="/">
        ← Back to home
      </RouterLink>
    </div>
  </main>
</template>

<script setup lang="ts">
import { RouterLink } from 'vue-router';
import GoldDivider from '../ui/GoldDivider.vue';
import SectionHeading from '../ui/SectionHeading.vue';
</script>
