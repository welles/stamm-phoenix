import { astroI18n, l } from "astro-i18n"
import type { Component } from "solid-js"

interface Props {
	showCurrent: boolean
	labels: {
		[locale: string]: string
	}
}

const LocaleSwitcher: Component<Props> = (props: Props) => {
	const { showCurrent, labels } = props

	const params: Record<string, string> = {}
	for (const [key, value] of Object.entries(
		new URLSearchParams(window.location.search),
	)) {
		if (value === undefined) {
			continue
		}
		params[key] = String(value)
	}

	let links = astroI18n.locales.map((locale) => ({
		locale,
		href: l(window.location.pathname, params, {
			targetLocale: locale,
		}),
		label: labels[locale] || locale.toUpperCase(),
	}))

	if (!showCurrent) {
		links = links.filter((link) => link.locale !== astroI18n.locale)
	}

	return (
		<ul>
			{links.map(({ href, label }) => (
				<li>
					<a
						class="nav-link text-black hover:text-gray-600"
						href={href}
					>
						{label}
					</a>
				</li>
			))}
		</ul>
	)
}

export default LocaleSwitcher
