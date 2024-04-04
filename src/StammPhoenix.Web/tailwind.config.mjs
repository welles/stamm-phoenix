/** @type {import('tailwindcss').Config} */
export default {
	darkMode: ['class'],
	content: ['./src/**/*.{astro,html,js,jsx,md,mdx,svelte,ts,tsx,vue}'],
	prefix: '',
	theme: {
		container: {
			center: true,
			padding: '2rem',
			screens: {
				'2xl': '1400px',
			},
		},
		extend: {
			colors: {
				'dpsg-green': '#006633',
				'dpsg-green-dark': '#004d26',
				'dpsg-gray-light': '#f0f0f0',
				'dpsg-gray': '#ccc',
				'dpsg-gray-dark': '#333',
				'dpsg-red': '#cc0000',
				'dpsg-blue': '#003056',
			},
			fontFamily: {
				sans: ['Segoe UI', 'Tahoma', 'Geneva', 'Verdana', 'sans-serif'],
			},
			fontSize: {
				base: '16px',
			},
			fontWeight: {
				normal: 400,
				bold: 700,
			},
			lineHeight: {
				base: 1.5,
			},
			spacing: {
				xs: '0.25rem',
				sm: '0.5rem',
				md: '1rem',
				lg: '1.5rem',
				xl: '2rem',
			},
			borderRadius: {
				sm: '0.25rem',
				md: '0.5rem',
				lg: '1rem',
			},
			borderWidth: {
				sm: '1px',
				md: '2px',
			},
			boxShadow: {
				sm: '0 1px 2px rgba(0, 0, 0, 0.05)',
				md: '0 4px 6px rgba(0, 0, 0, 0.1)',
				lg: '0 10px 15px rgba(0, 0, 0, 0.15)',
			},
			transitionDuration: {
				base: '200ms',
			},
			transitionTimingFunction: {
				base: 'ease-in-out',
			},
		},
	},
	plugins: [require('tailwindcss-animate'), require('@tailwindcss/forms')],
}
