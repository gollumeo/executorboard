<template>
  <main class="bg-ivory">
    <HeroSection />
    <GoldDivider />
    <HowItWorks />
    <GoldDivider />
    <WhyItMatters />
    <GoldDivider />

    <CtaBento
      title="A structured, sovereign workspace for estate communication"
      description="A dedicated environment where executors and attorneys maintain clarity, document progress, and keep heirs aligned without repeated outreach."
    >
      <template #actions>
        <RouterLink class="btn btn-primary max-md:py-3" to="/for-attorneys">For attorneys</RouterLink>
        <RouterLink class="btn btn-secondary btn-sm max-md:py-3" to="/guides/how-to-keep-heirs-updated">
          The approach
        </RouterLink>
      </template>
    </CtaBento>
  </main>
</template>

<script setup lang="ts">
import HeroSection from "../modules/hero/HeroSection.vue";
import HowItWorks from "../modules/howitworks/HowItWorks.vue";
import WhyItMatters from "../modules/why/WhyItMatters.vue";
import CtaBento from "../ui/CtaBento.vue";
import GoldDivider from "../ui/GoldDivider.vue";
</script>
