import { test, expect } from "@playwright/test";

test("test login", async ({ page }) => {
  await page.goto("/");
  await page.getByRole("link", { name: "Login" }).click();
  await page.getByLabel("E-Mail:").click();
  await page.getByLabel("E-Mail:").fill("admin@stamm-phoenix.de");
  await page.getByLabel("Passwort:").click();
  await page.getByLabel("Passwort:").fill("admin");
  await page.getByRole("button", { name: "Login" }).click();
  const success = await page.locator('#login-token').count() > 0;

  if (elementExists) {
    console.log('everything is fine');
    // Perform additional operations on the element
  } else {
    console.error('Something went wrong');
    // Handle the case when the element is not found
  }
});
