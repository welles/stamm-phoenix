import { test, expect } from '@playwright/test'

test('Login Test', async ({ page }) => {
	// Navigate to the website
	await page.goto('/')

	if (page.viewportSize()?.width <= 550) {
		await page.locator('#menu').click()
	}

	// Click on the Login link in the navbar
	await page.getByRole('link', { name: 'Anmeldung' }).click()

	// Fill in the email and password fields
	await page.getByLabel('E-Mail:').click()
	await page.getByLabel('E-Mail:').fill('admin@stamm-phoenix.de')
	await page.getByLabel('Passwort:').click()
	await page.getByLabel('Passwort:').fill('admin')

	// Click the Login button
	await page.getByRole('button', { name: 'Login' }).click()

	// Check if there is a cookie named jwt or if the URL is /success
	const cookies = await page.context().cookies()
	const jwtCookie = cookies.find((cookie) => cookie.name === 'jwt')

	if (jwtCookie) {
		console.log('JWT cookie found:', jwtCookie.value)
	} else {
		throw new Error('Login failed, no JWT cookie')
	}
})
