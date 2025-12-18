<template>
  <header class="section section--hero">
    <div :class="containerClass">
      <div class="space-y-6">
        <slot name="left" />
      </div>
      <slot name="right" />
    </div>
  </header>
</template>

<script setup lang="ts">
import { computed } from 'vue';

const props = withDefaults(
  defineProps<{
    gapClass?: string;
  }>(),
  {
    gapClass: 'gap-6',
  },
);

const containerClass = computed(() => {
  return ['container', 'grid', 'items-center', props.gapClass, 'md:grid-cols-2'].join(' ');
});
</script>

