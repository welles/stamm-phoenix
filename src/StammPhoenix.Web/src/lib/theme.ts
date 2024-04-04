// src/utils/theme.ts
interface ThemeModule {
	toggleTheme: () => void
	initTheme: () => void
}

const theme: ThemeModule = (() => {
	function toggleTheme() {
		const body = document.body
		const isDarkMode = body.classList.contains('dark')

		if (isDarkMode) {
			body.classList.remove('dark')
			if (typeof window !== 'undefined') {
				localStorage.setItem('theme', 'light')
			}
		} else {
			body.classList.add('dark')
			if (typeof window !== 'undefined') {
				localStorage.setItem('theme', 'dark')
			}
		}
	}

	function initTheme() {
		if (typeof window !== 'undefined') {
			const storedTheme = localStorage.getItem('theme')
			const body = document.body

			if (storedTheme === 'dark') {
				body.classList.add('dark')
			} else {
				body.classList.remove('dark')
			}
		}
	}

	return { toggleTheme, initTheme }
})()

export default theme
