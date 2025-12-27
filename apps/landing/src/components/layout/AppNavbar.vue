<template>
  <nav class="nav-shell">
    <div class="container flex items-center justify-between py-3 md:py-4">
      <RouterLink
        class="nav-brand"
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
