<template>
  <HeroSplit>
    <template #left>
      <p class="muted eyebrow text-wrap">
        A private space for estate clarity
      </p>
      <h1 class="heading-xl">A single, orderly board for every step of the estate</h1>
      <p class="text-body text-wrap">
        ExecutorBoard consolidates updates, documents, and key decisions into one structured view, so expectations stay aligned and questions stop repeating.
      </p>
      <EmailCapture />
    </template>

    <template #right>
      <aside class="card-glass card-glass-elevated">
        <h2 class="heading-md">Executor confidence</h2>
        <ul class="space-y-3">
          <li class="flex items-start gap-3">
            <span class="mt-1 h-2.5 w-2.5 rounded-full bg-gold-quiet"></span>
            <p class="text-body text-wrap">Provide a clear, consistent account of the estate’s progress.</p>
          </li>
          <li class="flex items-start gap-3">
            <span class="mt-1 h-2.5 w-2.5 rounded-full bg-gold-quiet"></span>
            <p class="text-body text-wrap">Centralize documents and milestones in a single, quiet reference point.</p>
          </li>
          <li class="flex items-start gap-3">
            <span class="mt-1 h-2.5 w-2.5 rounded-full bg-gold-quiet"></span>
            <p class="text-body text-wrap">Reduce misunderstandings by giving every heir the same, structured information.</p>
          </li>
        </ul>
      </aside>
    </template>
  </HeroSplit>
</template>

<script setup lang="ts">
import EmailCapture from '../../ui/EmailCapture.vue';
import HeroSplit from '../../ui/HeroSplit.vue';
</script>
