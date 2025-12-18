<template>
  <main class="bg-ivory">
    <HeroSplit gap-class="gap-8">
      <template #left>
        <RouterLink class="muted text-sm inline-flex items-center gap-2 hover:text-anthracite" to="/">
          ← Back to home
        </RouterLink>

        <p class="muted uppercase tracking-wide text-sm max-md:pt-0 max-md:pb-1 text-wrap">
          Reference document
        </p>
        <h1 class="heading-xl">The approach</h1>
        <p class="text-body text-wrap">
          ExecutorBoard exists to preserve clarity in estate communication—when pressure, grief, and misalignment make
          ordinary messaging unstable. It does not replace counsel. It does not decide. It provides a shared form that
          keeps facts, documents, and decisions coherent.
        </p>
        <div class="flex flex-wrap items-center gap-6">
          <RouterLink class="muted text-sm hover:text-anthracite" to="/for-attorneys">For attorneys</RouterLink>
        </div>
      </template>

      <template #right>
        <aside class="card card-warm card-surface space-y-4">
          <h2 class="heading-md">A minimal structure</h2>
          <ul class="space-y-3">
            <li class="flex items-start gap-3">
              <span class="mt-1 h-2.5 w-2.5 rounded-full bg-gold-quiet"></span>
              <p class="text-body text-wrap">One shared reference, not a stream of messages.</p>
            </li>
            <li class="flex items-start gap-3">
              <span class="mt-1 h-2.5 w-2.5 rounded-full bg-gold-quiet"></span>
              <p class="text-body text-wrap">One sequence of updates, grouped with context.</p>
            </li>
            <li class="flex items-start gap-3">
              <span class="mt-1 h-2.5 w-2.5 rounded-full bg-gold-quiet"></span>
              <p class="text-body text-wrap">Explicit boundaries: record, not advice.</p>
            </li>
          </ul>
        </aside>
      </template>
    </HeroSplit>

    <GoldDivider />

    <section aria-labelledby="more-title" class="section">
      <SectionHeading
        id="more-title"
        align="left"
        title="The problem with “more communication”"
        subtitle="Under stress, volume often creates noise."
      />

      <div class="container mt-6 md:mt-8">
        <div class="max-w-3xl space-y-4">
          <p class="text-body text-wrap">
            Estates do not break because people cannot send messages. They break when messages multiply without a shared
            form, and each reader reconstructs a different story.
          </p>
          <p class="text-body text-wrap">
            More outreach can increase exposure to tone, timing, and interpretation. The intent is to reassure; the
            effect is often a wider surface for misunderstanding.
          </p>
        </div>
      </div>
    </section>

    <GoldDivider />

    <section aria-labelledby="structure-title" class="section">
      <SectionHeading
        id="structure-title"
        align="left"
        title="Why structure matters more than speed"
        subtitle="Clarity comes from form before it comes from frequency."
      />

      <div class="container mt-6 md:mt-8">
        <div class="max-w-3xl space-y-4">
          <p class="text-body text-wrap">
            Most stakeholders are not asking for more information. They are asking for a stable way to interpret what
            already exists: what has been done, what remains, and what is waiting on others.
          </p>
          <p class="text-body text-wrap">
            When updates follow a consistent shape, they become legible. The sequence becomes traceable. Disagreement
            shifts from speculation to facts that can be referenced.
          </p>
        </div>
      </div>
    </section>

    <GoldDivider />

    <section aria-labelledby="reference-title" class="section">
      <SectionHeading
        id="reference-title"
        align="left"
        title="The role of a shared reference"
        subtitle="One place to consult before anyone escalates."
      />

      <div class="container mt-6 md:mt-8">
        <div class="max-w-3xl space-y-4">
          <p class="text-body text-wrap">
            A shared reference does not remove conflict, grief, or impatience. It reduces the need to retell and
            reframe each update across separate threads and separate memories.
          </p>
          <p class="text-body text-wrap">
            It also protects the executor—and counsel—by making communication less personal. The record becomes the
            point of contact for routine questions, and direct outreach becomes more intentional.
          </p>
        </div>
      </div>
    </section>

    <GoldDivider />

    <section aria-labelledby="enforces-title" class="section">
      <SectionHeading
        id="enforces-title"
        align="left"
        title="What ExecutorBoard enforces (and why)"
        subtitle="A small set of constraints that keeps the record coherent."
      />

      <div class="container mt-6 md:mt-8">
        <div class="max-w-3xl space-y-8">
          <div class="space-y-2">
            <h3 class="heading-md">A consistent update format</h3>
            <p class="text-body text-wrap">
              Updates are short, factual, and comparable over time. A consistent form reduces ambiguity and keeps tone
              from becoming the primary signal.
            </p>
          </div>

          <div class="space-y-2">
            <h3 class="heading-md">A single place for documents</h3>
            <p class="text-body text-wrap">
              Relevant documents are attached to the record, near the update that explains them. Stakeholders stop
              building parallel archives across inboxes and drives.
            </p>
          </div>

          <div class="space-y-2">
            <h3 class="heading-md">A traceable sequence</h3>
            <p class="text-body text-wrap">
              The estate progresses through a visible sequence of milestones and decisions. This makes “where are we”
              easier to answer without reopening past conversations.
            </p>
          </div>

          <div class="space-y-2">
            <h3 class="heading-md">A calm perimeter</h3>
            <p class="text-body text-wrap">
              ExecutorBoard separates the record from direct messaging. It does not compete with your existing
              communication; it reduces the need for it.
            </p>
          </div>
        </div>
      </div>
    </section>

    <GoldDivider />

    <section aria-labelledby="boundaries-title" class="section">
      <SectionHeading
        id="boundaries-title"
        align="left"
        title="Boundaries as a feature"
        subtitle="Limits are part of the design."
      />

      <div class="container mt-6 md:mt-8">
        <div class="max-w-3xl space-y-8">
          <div class="space-y-2">
            <h3 class="heading-md">No legal advice</h3>
            <p class="text-body text-wrap">
              The workspace is for factual progress and supporting documents. Legal interpretation remains with counsel
              and the responsible parties.
            </p>
          </div>

          <div class="space-y-2">
            <h3 class="heading-md">No legal automation</h3>
            <p class="text-body text-wrap">
              ExecutorBoard does not evaluate options, recommend outcomes, or simulate judgment. It documents what was
              decided, by whom, and when it was shared.
            </p>
          </div>

          <div class="space-y-2">
            <h3 class="heading-md">No pressure to perform</h3>
            <p class="text-body text-wrap">
              The system is deliberately conservative. It is designed to maintain a stable record, not to accelerate a
              process at the expense of trust.
            </p>
          </div>
        </div>
      </div>
    </section>

    <GoldDivider />

    <section aria-labelledby="closing-title" class="section">
      <SectionHeading
        id="closing-title"
        align="left"
        title="Institutional closing"
        subtitle="A paid product, provisioned per estate, designed to stay in its lane."
      />

      <div class="container mt-6 md:mt-8">
        <div class="max-w-3xl space-y-4">
          <p class="text-body text-wrap">
            ExecutorBoard is offered as a paid subscription per estate. The goal is not adoption at any cost, but a
            reliable reference that reduces confusion and protects relationships when stakes are high.
          </p>
          <p class="text-body text-wrap">
            If a practice or an executor needs a stable way to publish factual progress without turning every update
            into a negotiation, the approach is simple: one record, one form, clear boundaries.
          </p>
        </div>
      </div>
    </section>
  </main>
</template>

<script setup lang="ts">
import { RouterLink } from 'vue-router';
import GoldDivider from '../ui/GoldDivider.vue';
import HeroSplit from '../ui/HeroSplit.vue';
import SectionHeading from '../ui/SectionHeading.vue';
</script>
