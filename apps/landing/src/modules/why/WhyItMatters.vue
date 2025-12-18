<template>
  <section aria-labelledby="why-title" class="section section--why-tight mb-10 md:mb-16">
    <SectionHeading
      id="why-title"
      title="Why it matters"
      subtitle="ExecutorBoard changes the entire dynamic of estate administration."
    />

    <ul class="container mt-6 md:mt-8 grid card-gap md:gap-y-12 grid-cols-1 sm:grid-cols-2 md:grid-cols-2 xl:grid-cols-3">
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

      <li class="card card-warm card-surface space-y-3 md:col-span-2 md:max-w-lg md:mx-auto xl:col-span-1 xl:max-w-full">
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
