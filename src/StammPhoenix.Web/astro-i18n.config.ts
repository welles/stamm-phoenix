import { defineAstroI18nConfig } from 'astro-i18n'

export default defineAstroI18nConfig({
	primaryLocale: 'de', // default app locale
	secondaryLocales: ['en'], // other supported locales
	fallbackLocale: 'de', // fallback locale (on missing translation)
	trailingSlash: 'always', // "never" or "always"
	run: 'client+server', // "client+server" or "server"
	showPrimaryLocale: false, // "/en/about" vs "/about"
	translationLoadingRules: [], // per page group loading
	translationDirectory: {}, // translation directory names
	translations: {}, // { [translation_group1]: { [locale1]: {}, ... } }
	routes: {
		en: {
			anmeldung: 'login',
			datenschutz: 'privacy',
			impressum: 'legal-notice',
			kontakt: 'contact',
		},
	}, // { [secondary_locale1]: { about: "about-translated", ... } }
})
