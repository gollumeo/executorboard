<template>
  <section aria-labelledby="how-it-works-title" class="section">
    <SectionHeading
      id="how-it-works-title"
      title="How it works"
      subtitle="ExecutorBoard turns chaotic communication into a calm, structured process."
    />

    <ol class="container cards-grid-3-relaxed">
      <li class="card card-warm card-surface space-y-3">
        <span class="step-badge">1</span>
        <h3 class="heading-md">Private space restores order</h3>
        <p class="text-body muted text-wrap">
          Establish a secure place where updates, documents, and next steps stay grouped without scattering.
        </p>
      </li>

      <li class="card card-warm card-surface space-y-3">
        <span class="step-badge">2</span>
        <h3 class="heading-md">Structured records prevent drift</h3>
        <p class="text-body muted text-wrap">
          Post milestones and notes once. Everything remains in a single, orderly reference that stays intact over time.
        </p>
      </li>

      <li class="card card-warm card-surface card-featured-md">
        <span class="step-badge">3</span>
        <h3 class="heading-md">Shared clarity aligns heirs</h3>
        <p class="text-body muted text-wrap">
          Heirs consult the board for clear, consistent information, reducing repeated questions and expectations drift.
        </p>
      </li>
    </ol>
  </section>
</template>

<script setup lang="ts">
import SectionHeading from '../../ui/SectionHeading.vue';
</script>
