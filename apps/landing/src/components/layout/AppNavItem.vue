<template>
  <RouterLink v-slot="{ href, navigate, isExactActive }" custom :to="to">
    <a
      :href="href"
      :aria-current="isExactActive ? 'page' : undefined"
      :class="[
        'font-sans text-sm md:text-base tracking-tight',
        'py-1',
        isExactActive
          ? 'text-anthracite underline underline-offset-4 decoration-anthracite/30'
          : 'text-anthracite/70 hover:text-anthracite',
      ]"
      @click="navigate"
    >
      <slot />
    </a>
  </RouterLink>
</template>

<script setup lang="ts">
defineProps<{
  to: string;
}>();
</script>
