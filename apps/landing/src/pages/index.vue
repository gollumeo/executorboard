<template>
  <main class="bg-ivory">
    <HeroSection />
    <div class="separator-gold divider-space">
      <span class="dot"></span>
    </div>
    <HowItWorks />
    <div class="separator-gold divider-space">
      <span class="dot"></span>
    </div>
    <WhyItMatters />
    <div class="separator-gold divider-space">
      <span class="dot"></span>
    </div>

    <section class="cta-bento-wrapper">
      <div class="container max-w-5xl">
        <div class="cta-bento space-y-6">
          <p class="cta-eyebrow">EXECUTORBOARD</p>
          <h2 class="heading-lg">
            A structured, sovereign workspace for estate communication
          </h2>
          <p
            class="text-body max-sm:text-[15px] max-sm:leading-relaxed text-wrap"
          >
            A dedicated environment where executors and attorneys maintain
            clarity, document progress, and keep heirs aligned without repeated
            outreach.
          </p>
          <div class="flex flex-wrap gap-3">
            <RouterLink class="btn btn-primary max-md:py-3" to="/for-attorneys"
              >For attorneys</RouterLink
            >
            <RouterLink
              class="btn btn-secondary btn-sm max-md:py-3"
              to="/guides/how-to-keep-heirs-updated"
            >
              The approach
            </RouterLink>
          </div>
        </div>
      </div>
    </section>
  </main>
</template>

<script setup lang="ts">
import HeroSection from "../modules/hero/HeroSection.vue";
import HowItWorks from "../modules/howitworks/HowItWorks.vue";
import WhyItMatters from "../modules/why/WhyItMatters.vue";
</script>
