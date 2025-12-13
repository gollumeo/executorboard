import {
  createMemoryHistory,
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

export const history = import.meta?.env?.SSR
  ? createMemoryHistory(import.meta.env.BASE_URL)
  : createWebHistory(import.meta.env.BASE_URL);
