import { astroI18n, l } from "astro-i18n"
import { type Component, createSignal, onCleanup, onMount } from "solid-js"
import GlassMorphism from "./GlassMorphism" // Adjust the import path as necessary

const localeEmojiMap: Record<string, string> = {
	// biome-ignore format: preserve emojis
	en: "🇬", // English (UK flag)
	// biome-ignore format: preserve emojis
	de: "🇩", // German
}

const LocaleSwitcher: Component = () => {
	const [isOpen, setIsOpen] = createSignal(false)
	let switcherRef: HTMLDivElement | undefined

	const params: Record<string, string> = {}
	const searchParams = new URLSearchParams(window.location.search)
	for (const [key, value] of searchParams.entries()) {
		params[key] = value
	}

	const links = astroI18n.locales
		.filter((locale) => locale !== astroI18n.locale)
		.map((locale) => ({
			locale,
			href: l(window.location.pathname, params, {
				targetLocale: locale,
			}),
			label: localeEmojiMap[locale],
		}))

	const toggleDropdown = () => setIsOpen(!isOpen())

	const handleClickOutside = (event: MouseEvent) => {
		if (switcherRef && !switcherRef.contains(event.target as Node)) {
			setIsOpen(false)
		}
	}

	onMount(() => {
		document.addEventListener("click", handleClickOutside)
	})

	onCleanup(() => {
		document.removeEventListener("click", handleClickOutside)
	})

	return (
		<div class="relative" ref={switcherRef}>
			<button
				onClick={toggleDropdown}
				class="cursor-pointer p-2 rounded-md hover:bg-gray-100 focus:outline-none focus:none"
				aria-haspopup="true"
				aria-expanded={isOpen()}
			>
				{localeEmojiMap[astroI18n.locale]}
			</button>
			{isOpen() && links.length > 0 && (
				<GlassMorphism class="absolute top-full left-0 mt-1 z-10">
					<ul class="py-1">
						{links.map(({ href, label, locale }) => (
							<li class="px-1">
								<a
									class="block px-4 py-2 text-sm text-gray-700 hover:bg-gray-100 rounded-md"
									href={href}
									title={locale.toUpperCase()}
									onClick={() => setIsOpen(false)}
								>
									{label}
								</a>
							</li>
						))}
					</ul>
				</GlassMorphism>
			)}
		</div>
	)
}

export default LocaleSwitcher
