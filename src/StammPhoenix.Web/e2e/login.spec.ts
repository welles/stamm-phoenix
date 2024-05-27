import { test } from "@playwright/test"

test("Login Test", async ({ page }) => {
	// Navigate to the website
	await page.goto("/")
const vp_width: int | undefined = page.viewportSize?.width
	if (!(vp_width <= 550)) {
		// Click on the Login link in the navbar
	await page.getByRole("link", { name: "Anmelden" }).click()

	// Fill in the email and password fields
	await page.getByLabel("E-Mail:").click()
	await page.getByLabel("E-Mail:").fill("admin@stamm-phoenix.de")
	await page.getByLabel("Passwort:").click()
	await page.getByLabel("Passwort:").fill("admin")

	// Click the Login button
	await page.getByRole("button", { name: "Anmelden" }).click()

	// Check if there is a cookie named jwt or if the URL is /success
	const cookies = await page.context().cookies()
	const jwtCookie = cookies.find((cookie) => cookie.name === "jwt")

	if (jwtCookie) {
	} else {
		throw new Error("Login failed, no JWT cookie")
	}
	}
})
