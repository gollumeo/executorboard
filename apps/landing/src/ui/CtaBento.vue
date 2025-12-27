<template>
  <section class="cta-bento-wrapper">
    <div class="container max-w-5xl">
      <div class="cta-bento space-y-6">
        <p class="cta-eyebrow">{{ eyebrow }}</p>
        <h2 class="heading-lg">{{ title }}</h2>
        <p class="text-body text-body-compact text-wrap">
          {{ description }}
        </p>
        <div class="flex flex-wrap gap-3">
          <slot name="actions" />
        </div>
      </div>
    </div>
  </section>
</template>

<script setup lang="ts">
withDefaults(
  defineProps<{
    eyebrow?: string;
    title: string;
    description: string;
  }>(),
  {
    eyebrow: 'EXECUTORBOARD',
  },
);
</script>
