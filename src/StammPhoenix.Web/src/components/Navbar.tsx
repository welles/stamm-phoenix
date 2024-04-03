import i18next from 'i18next'
import { localizePath } from 'node_modules/astro-i18next/src'
import { getRelativeLocaleUrl } from 'astro:i18n'
import GlassMorphism from './GlassMorphism'
import { type Component } from 'solid-js'

const Navbar: Component = () => {
	//	const btn = document.getElementById('change-lang-btn')
	//	btn?.addEventListener('click', () => {
	//		const newLocale = i18next.language === 'de' ? 'en' : 'de'
	//
	//		const currentPath = window.location.pathname
	//		const path = currentPath.includes('/en')
	//			? currentPath.replace('/en', '')
	//			: `${currentPath}`
	//
	//		window.location.href = getRelativeLocaleUrl(
	//			newLocale,
	//			localizePath(path),
	//		)
	//	})

	const navLinks = [
		{
			href: getRelativeLocaleUrl(i18next.language, localizePath('/')),
			label: i18next.t('navbar.home'),
		},
		{
			href: getRelativeLocaleUrl(
				i18next.language,
				localizePath('/datenschutz'),
			),
			label: i18next.t('navbar.privacy'),
		},
		{
			href: getRelativeLocaleUrl(
				i18next.language,
				localizePath('/impressum'),
			),
			label: i18next.t('navbar.imprint'),
		},
		{
			href: getRelativeLocaleUrl(
				i18next.language,
				localizePath('/anmeldung'),
			),
			label: i18next.t('navbar.login'),
		},
	]

	return (
		<nav class="d-nav rounded-lg">
			<GlassMorphism>
				<div class="container mx-auto px-4 flex justify-between items-center">
					<div class="navbar-brand float-left">
						<a
							href={getRelativeLocaleUrl(i18next.language, '')}
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
