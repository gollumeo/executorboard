<template>
  <header :class="wrapperClass">
    <h2 :id="id" class="heading-lg heading-lg-responsive">
      {{ title }}
    </h2>
    <p v-if="subtitle" class="text-body muted max-md:px-6 text-wrap">
      {{ subtitle }}
    </p>
  </header>
</template>

<script setup lang="ts">
import { computed } from 'vue';

type Alignment = 'left' | 'center';

const props = withDefaults(
  defineProps<{
    id?: string;
    title: string;
    subtitle?: string;
    align?: Alignment;
  }>(),
  {
    id: undefined,
    align: 'center',
    subtitle: undefined,
  },
);

const wrapperClass = computed(() => {
  if (props.align === 'left') {
    return 'mx-auto max-w-3xl space-y-2 text-left';
  }

  return 'mx-auto max-w-2xl space-y-2 text-center';
});
</script>
