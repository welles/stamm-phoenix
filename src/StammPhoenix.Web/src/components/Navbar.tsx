import { createSignal, Show } from 'solid-js'
import i18next from 'i18next'
import { localizePath } from 'node_modules/astro-i18next/src'
import { getRelativeLocaleUrl } from 'astro:i18n'
import GlassMorphism from './GlassMorphism'

const Navbar = () => {
	const [locale, setLocale] = createSignal(i18next.language)

	const btn = document.getElementById('change-lang-btn')
	btn?.addEventListener('click', () => {
		const newLocale = locale() === 'de' ? 'en' : 'de'
		setLocale(newLocale)

		const currentPath = window.location.pathname
		const newPath = currentPath.includes('/en')
			? currentPath.replace('/en', '')
			: `/en${currentPath}`

		window.location.pathname = newPath
	})

	const navLinks = [
		{
			href: getRelativeLocaleUrl(locale(), localizePath('/')),
			label: i18next.t('navbar.home'),
		},
		{
			href: getRelativeLocaleUrl(locale(), localizePath('/datenschutz')),
			label: i18next.t('navbar.privacy'),
		},
		{
			href: getRelativeLocaleUrl(locale(), localizePath('/impressum')),
			label: i18next.t('navbar.imprint'),
		},
		{
			href: getRelativeLocaleUrl(locale(), localizePath('/anmeldung')),
			label: i18next.t('navbar.login'),
		},
	]

	return (
		<nav class="d-nav rounded-lg">
			<GlassMorphism>
				<div class="container mx-auto px-4 flex justify-between items-center">
					<div class="navbar-brand float-left">
						<a
							href={getRelativeLocaleUrl(locale(), '')}
							class="text-black font-bold"
						>
							<img
								data-twe-animation-start="onLoad"
								class="animate-bounce w-[2rem] z-1"
								src="/favicon.ico"
							/>
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
							<button id="change-lang-btn">
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
