// @ts-expect-error IDE false positive
import vue from "@vitejs/plugin-vue";
import { defineConfig } from "vite";
// @ts-expect-error IDE false positive
import tailwindcss from "@tailwindcss/vite";

export default defineConfig({
  plugins: [vue(), tailwindcss()],
  ssr: { noExternal: ["vite-ssg"] },
});
