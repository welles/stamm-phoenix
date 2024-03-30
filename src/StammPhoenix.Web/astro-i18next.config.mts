/** @type {import('astro-i18next').AstroI18nextConfig} */
export default {
	defaultLocale: 'de',
	locales: ['en', 'de'],
	routes: {
		en: {
			anmeldung: 'login',
			datenschutz: 'privacy',
			impressum: 'legal-notice',
			kontakt: 'contact',
			dashboard: {},
		},
	},
}
