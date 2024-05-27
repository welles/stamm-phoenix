import { astroI18n, l, t } from "astro-i18n"
import type { Component } from "solid-js"
import GlassMorphism from "./GlassMorphism"
import LocaleSwitcher from "./LocaleSwitcher"

const Navbar: Component = () => {
	const baseUrl: string = `${window.location.origin}${
		astroI18n.locale === "de" ? "" : `/${astroI18n.locale}`
	}`

	const navLinks = [
		{
			route: baseUrl + l("/"),
			label: t("navbar.links.home"),
		},
		{
			route: baseUrl + l("/anmeldung"),
			label: t("navbar.links.login"),
		},
		{
			route: baseUrl + l("/datenschutz"),
			label: t("navbar.links.privacy"),
		},
		{
			route: baseUrl + l("/impressum"),
			label: t("navbar.links.legal-notice"),
		},
	]

	return (
		<nav class="d-nav rounded-lg">
			<GlassMorphism>
				<div class="container mx-auto px-4 flex justify-between items-center">
					<div class="navbar-brand float-left">
						<a href={baseUrl + l("/")} class="text-black font-bold">
							<img
								alt="phoenix rising from a flame"
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
								labels={{ de: "German", en: "English" }}
							/>
						</li>
					</ul>
				</div>
			</GlassMorphism>
		</nav>
	)
}

export default Navbar
