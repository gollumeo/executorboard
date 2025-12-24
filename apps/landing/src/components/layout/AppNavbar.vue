<template>
  <nav class="sticky top-0 z-40 w-full border-b border-anthracite/15 bg-[#EFECE3]">
    <div class="container flex items-center justify-between py-3 md:py-4">
      <RouterLink
        class="font-serif text-sm md:text-base tracking-[0.08em] text-anthracite/80 hover:text-anthracite"
        to="/"
      >
        ExecutorBoard
      </RouterLink>
      <div class="flex items-center gap-6 md:gap-8">
        <AppNavItem to="/for-attorneys">For attorneys</AppNavItem>
        <AppNavItem to="/the-approach">The approach</AppNavItem>
      </div>
    </div>
  </nav>
</template>

<script setup lang="ts">
import AppNavItem from "./AppNavItem.vue";
</script>
