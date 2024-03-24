import { defineConfig } from 'astro/config'
import solid from '@astrojs/solid-js'
import tailwind from '@astrojs/tailwind'
import astroI18next from 'astro-i18next'
import node from '@astrojs/node'

// https://astro.build/config
export default defineConfig({
	output: 'server',
	integrations: [
		solid(),
		tailwind({
			applyBaseStyles: false,
		}),
		astroI18next(),
	],
	adapter: node({ mode: 'standalone' }),
	i18n: {
		defaultLocale: 'de',
		locales: ['en', 'de'],
		routing: {
			prefixDefaultLocale: false,
		},
	},
})
