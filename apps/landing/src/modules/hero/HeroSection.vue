<template>
  <header class="section">
    <div class="container grid items-center gap-6 md:grid-cols-2">
      <div class="space-y-6">
        <h1 class="heading-xl">A single, orderly board for every step of the estate</h1>
        <p class="text-body">
          EstateClear consolidates updates, documents, and key decisions into one structured view, so expectations stay
          aligned and questions stop repeating.
        </p>
        <EmailCapture />
      </div>

      <aside class="card-glass space-y-4">
        <h2 class="heading-md">Executor confidence</h2>
        <ul class="space-y-3">
          <li class="flex items-start gap-3">
            <span class="mt-1 h-2.5 w-2.5 rounded-full bg-gold-quiet"></span>
            <p class="text-body">Provide a clear, consistent account of the estate’s progress.</p>
          </li>
          <li class="flex items-start gap-3">
            <span class="mt-1 h-2.5 w-2.5 rounded-full bg-gold-quiet"></span>
            <p class="text-body">Centralize documents and milestones in a single, quiet reference point.</p>
          </li>
          <li class="flex items-start gap-3">
            <span class="mt-1 h-2.5 w-2.5 rounded-full bg-gold-quiet"></span>
            <p class="text-body">Reduce misunderstandings by giving every heir the same, structured information.</p>
          </li>
        </ul>
      </aside>
    </div>
  </header>
</template>

<script setup lang="ts">
import EmailCapture from '../../ui/EmailCapture.vue';
</script>
