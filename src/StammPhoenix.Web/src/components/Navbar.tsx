import GlassMorphism from './GlassMorphism'
import LocaleSwitcher from './LocaleSwitcher'
import { type Component } from 'solid-js'
import { astroI18n, t, l } from 'astro-i18n'

const Navbar: Component = () => {
	const baseURL: string = `${window.location.origin}${astroI18n.locale === 'de' ? '' : '/' + astroI18n.locale}`

	const navLinks = [
		{
			route: baseURL + l('/'),
			label: t('navbar.links.home'),
		},
		{
			route: baseURL + l('/anmeldung'),
			label: t('navbar.links.login'),
		},
		{
			route: baseURL + l('/datenschutz'),
			label: t('navbar.links.privacy'),
		},
		{
			route: baseURL + l('/impressum'),
			label: t('navbar.links.legal-notice'),
		},
	]

	return (
		<nav class="d-nav rounded-lg">
			<GlassMorphism>
				<div class="container mx-auto px-4 flex justify-between items-center">
					<div class="navbar-brand float-left">
						<a href={baseURL + l('/')} class="text-black font-bold">
							<img
								data-twe-animation-start="onLoad"
								class="animate-bounce w-[2rem] z-1"
								src="/favicon.ico"
							/>
						</a>
					</div>
					<ul class="navbar-nav flex space-x-4 float-right">
						{navLinks.map(({ route, label }) => (
							<li class="nav-item">
								<a
									class="nav-link text-black hover:text-gray-600"
									href={route}
								>
									{label}
								</a>
							</li>
						))}
						<li class="nav-item">
							<LocaleSwitcher
								showCurrent={false}
								labels={{ de: 'German', en: 'English' }}
							/>
						</li>
					</ul>
				</div>
			</GlassMorphism>
		</nav>
	)
}

export default Navbar
