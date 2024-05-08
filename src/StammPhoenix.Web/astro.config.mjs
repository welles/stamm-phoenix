import node from "@astrojs/node"
import solidJs from "@astrojs/solid-js"
import tailwind from "@astrojs/tailwind"
import { defineConfig } from "astro/config"

// https://astro.build/config
export default defineConfig({
	output: "server",
	integrations: [
		solidJs(),
		tailwind({
			applyBaseStyles: false,
		}),
	],
	adapter: node({ mode: "standalone" }),
	i18n: {
		defaultLocale: "de",
		locales: ["de", "en"],
		routing: {
			prefixDefaultLocale: false,
		},
	},
})
