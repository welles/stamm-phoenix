import { defineConfig } from 'astro/config'
import solidJs from '@astrojs/solid-js'
import tailwind from '@astrojs/tailwind'
import astroI18next from 'astro-i18next'
import node from '@astrojs/node'

// https://astro.build/config
export default defineConfig({
	output: 'server',
	integrations: [
		solidJs(),
		tailwind({
			applyBaseStyles: false,
		}),
		astroI18next(),
	],
	adapter: node({ mode: 'standalone' }),
	i18n: {
		defaultLocale: 'de',
		locales: ['de', 'en'],
		routing: {
			prefixDefaultLocale: false,
		},
	},
})
