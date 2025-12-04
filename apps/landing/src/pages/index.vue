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

    <div class="cta-bento-wrapper">
      <section class="section">
        <div class="container max-w-5xl">
          <div class="cta-bento space-y-6">
            <p class="cta-eyebrow">ExecutorBoard</p>
            <h2 class="heading-lg">Quiet luxury for estate executors</h2>
            <p class="text-body">
              A calm, sovereign workspace for attorneys and executors to keep heirs informed, decisions traceable,
              and estates moving without friction.
            </p>
            <div class="flex flex-wrap gap-3">
              <RouterLink class="btn btn-primary" to="/for-attorneys">Talk with our team</RouterLink>
              <RouterLink
                class="btn btn-secondary btn-sm"
                to="/guides/how-to-keep-heirs-updated"
              >
                Read the guide
              </RouterLink>
            </div>
          </div>
        </div>
      </section>
    </div>
  </main>
</template>

<script setup lang="ts">
import HeroSection from '../modules/hero/HeroSection.vue';
import HowItWorks from '../modules/howitworks/HowItWorks.vue';
import WhyItMatters from "../modules/why/WhyItMatters.vue";
</script>
