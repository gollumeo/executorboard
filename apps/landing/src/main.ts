import { createApp } from "vue";
import App from "./app.vue";
import { router } from "./router";

import "./core/styles/theme.css";
import "./core/styles/main.css";

createApp(App).use(router).mount("#app");
