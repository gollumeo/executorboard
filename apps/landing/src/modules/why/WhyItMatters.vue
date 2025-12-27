<template>
  <section aria-labelledby="why-title" class="section section--why-tight mb-10 md:mb-16">
    <SectionHeading
      id="why-title"
      title="Why it matters"
      subtitle="ExecutorBoard changes the entire dynamic of estate administration."
    />

    <ul class="container cards-grid-3-relaxed">
      <li class="card card-warm card-surface space-y-3">
        <h3 class="heading-md">Clarity reduces friction</h3>
        <p class="text-body muted text-wrap">
          A consistent view of the estate minimizes misunderstandings and keeps communication calm.
        </p>
      </li>

      <li class="card card-warm card-surface space-y-3">
        <h3 class="heading-md">Structure lowers the load</h3>
        <p class="text-body muted text-wrap">
          With updates and documents grouped in one place, executors avoid repetition and maintain predictable
          progress.
        </p>
      </li>

      <li class="card card-warm card-surface card-featured-md">
        <h3 class="heading-md">Transparency supports fairness</h3>
        <p class="text-body muted text-wrap">
          Every heir receives the same information, reinforcing trust and supporting documented, equitable
          communication.
        </p>
      </li>
    </ul>
  </section>
</template>

<script setup lang="ts">
import SectionHeading from '../../ui/SectionHeading.vue';
</script>
