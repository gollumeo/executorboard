<template>
  <nav class="w-full">
    <div class="container flex items-center justify-between py-3 md:py-4">
      <RouterLink
        class="font-serif text-sm md:text-base tracking-[0.08em] text-anthracite/80 transition-colors hover:text-anthracite"
        exact-active-class="text-anthracite"
        to="/"
      >
        ExecutorBoard
      </RouterLink>
      <div class="flex items-center gap-6 md:gap-8">
        <RouterLink class="nav-link" to="/for-attorneys">
          For attorneys
        </RouterLink>
        <RouterLink class="nav-link" to="/the-approach">
          The approach
        </RouterLink>
      </div>
    </div>
  </nav>
</template>
