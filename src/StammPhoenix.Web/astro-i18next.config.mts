/** @type {import('astro-i18next').AstroI18nextConfig} */

interface ConfigType {
	defaultLocale: string
	locales: string[]
	routes: {
		[locale: string]: { [route: string]: string | {} }
	}
}

const config: ConfigType = {
	defaultLocale: 'de',
	locales: ['en', 'de'],
	routes: {
		en: {
			anmeldung: 'login',
			datenschutz: 'privacy',
			impressum: 'legal-notice',
			kontakt: 'contact',
		},
	},
}

export default config
