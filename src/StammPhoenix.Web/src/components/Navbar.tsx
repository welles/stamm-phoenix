import { createSignal, Show, createEffect, onCleanup } from 'solid-js'
import i18next from 'i18next'
import { localizePath } from 'node_modules/astro-i18next/src'
import { getRelativeLocaleUrl } from 'astro:i18n'
import i18nConfig from '../../astro-i18next.config.mts'

const GlassMorphism = (props) => {
	return (
		<div class="py-4" {...props}>
			{props.children}
		</div>
	)
}

const translatePath = (
	path: string,
	fromLocale: string,
	toLocale: string,
): string => {
	const { routes } = i18nConfig
	const fromRoutes = routes[fromLocale] || {}
	const toRoutes = routes[toLocale] || {}
	const pathSegments = path.split('/').filter((segment) => segment !== '')
	const translatedSegments = pathSegments.map((segment) => {
		const translatedSegment = toRoutes[fromRoutes[segment]] || segment
		return translatedSegment
	})
	return `${translatedSegments.join('/')}`
}

const handleTrailingSlash = (path: string, trailingSlash: string): string => {
	if (path === '/') {
		return path
	}
	switch (trailingSlash) {
		case 'always':
			return path.endsWith('/') ? path : `${path}/`
		case 'never':
			return path.replace(/\/$/, '')
		default:
			return path
	}
}

const { defaultLocale, locales, trailingSlash } = {
	defaultLocale: i18nConfig.defaultLocale,
	locales: i18nConfig.locales,
	trailingSlash: 'ignore',
}

interface LanguageTarget {
	currentLang: string
	currentPath: string
	targetLang: string
	targetPath: string
}

const getLanguageTarget = (pathname: string): LanguageTarget => {
	const isEnglish = pathname.startsWith('/en')
	return {
		currentLang: isEnglish ? 'en' : 'de',
		currentPath: isEnglish ? '/en' : '/',
		targetLang: isEnglish ? 'de' : 'en',
		targetPath: isEnglish ? '/' : '/en',
	}
}

const handleLanguageChange = () => {
	const pathname: string = window.location.pathname
	const { currentLang, currentPath, targetLang, targetPath } =
		getLanguageTarget(pathname)
	const pathWithoutLang = pathname.slice(currentPath.length)
	const translatedPath = translatePath(
		pathWithoutLang,
		currentLang,
		targetLang,
	)
	window.location.pathname = `${targetPath}/${translatedPath}`
}

const Navbar = () => {
	const [locale, setLocale] = createSignal(i18next.language)

	// Memoize the localizePath function
	const memoizedLocalizePath = (() => {
		const cache: { [key: string]: string } = {}
		return (path: string, locales = locale()) => {
			const cacheKey = `${path}|${locales}`
			if (cache[cacheKey]) return cache[cacheKey]
			const result = localizePath(path, locales)
			cache[cacheKey] = result
			return result
		}
	})()

	const navLinks = [
		{
			href: getRelativeLocaleUrl(locale(), memoizedLocalizePath('/')),
			label: i18next.t('navbar.home'),
		},
		{
			href: getRelativeLocaleUrl(
				locale(),
				memoizedLocalizePath('/datenschutz'),
			),
			label: i18next.t('navbar.privacy'),
		},
		{
			href: getRelativeLocaleUrl(
				locale(),
				memoizedLocalizePath('/impressum'),
			),
			label: i18next.t('navbar.imprint'),
		},
		{
			href: getRelativeLocaleUrl(
				locale(),
				memoizedLocalizePath('/anmeldung'),
			),
			label: i18next.t('navbar.login'),
		},
	]

	let buttonRef: HTMLButtonElement | undefined
	const addClickListener = (el: HTMLButtonElement) => {
		buttonRef = el
		buttonRef.addEventListener('click', handleLanguageChange)
	}
	const removeClickListener = () => {
		buttonRef?.removeEventListener('click', handleLanguageChange)
	}

	createEffect(() => {
		buttonRef?.addEventListener('click', handleLanguageChange)
	})

	onCleanup(() => {
		removeClickListener()
	})

	return (
		<nav class="d-nav rounded-lg">
			<GlassMorphism>
				<div class="container mx-auto px-4 flex justify-between items-center">
					<div class="navbar-brand float-left">
						<a
							href={getRelativeLocaleUrl(locale(), '')}
							class="text-black font-bold"
						>
							<div class="backdrop-blur-md bg-[url('/favicon.ico')]">
								<img
									data-twe-animation-start="onLoad"
									class="animate-bounce w-[27px] z-1"
									src="/favicon.ico"
								/>
							</div>
						</a>
					</div>
					<ul class="navbar-nav flex space-x-4 float-right">
						<Show
							when={navLinks.length}
							fallback={<span>Loading...</span>}
						>
							{navLinks.map(({ href, label }) => (
								<li class="nav-item">
									<a
										class="nav-link text-black hover:text-gray-600"
										href={href}
									>
										{label}
									</a>
								</li>
							))}
						</Show>
						<li class="nav-item">
							<button id="change-lang-btn" ref={addClickListener}>
								<span>ChangeLang</span>
							</button>
						</li>
					</ul>
				</div>
			</GlassMorphism>
		</nav>
	)
}

export default Navbar
