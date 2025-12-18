import {
  createRouter,
  createWebHistory,
  type RouteRecordRaw,
} from "vue-router";

export const routes: RouteRecordRaw[] = [
  {
    path: "/",
    name: "home",
    component: () => import("../pages/index.vue"),
  },
  {
    path: "/the-approach",
    name: "the-approach",
    component: () => import("../pages/the-approach.vue"),
  },
  {
    path: "/for-attorneys",
    name: "for-attorneys",
    component: () => import("../pages/for-attorneys.vue"),
  },
  {
    path: "/guides/how-to-keep-heirs-updated",
    name: "guides-how-to-keep-heirs-updated",
    component: () => import("../pages/guides/how-to-keep-heirs-updated.vue"),
  },
];

export const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes,
  scrollBehavior() {
    return { left: 0, top: 0 };
  },
});
