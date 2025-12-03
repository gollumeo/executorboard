import { ViteSSG } from 'vite-plugin-ssg'
import App from './app.vue'
import { history, routes } from './router'
import './core/styles/theme.css'
import './core/styles/main.css'

export const createApp = ViteSSG(App, {
  routes,
  base: import.meta.env.BASE_URL,
  history,
})
