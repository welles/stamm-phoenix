import { test, expect } from "@playwright/test";

test("test login", async ({ page }) => {
  await page.goto("/");
  await page.getByRole("link", { name: "Login" }).click();
  await page.getByLabel("E-Mail:").click();
  await page.getByLabel("E-Mail:").fill("admin@stamm-phoenix.de");
  await page.getByLabel("Passwort:").click();
  await page.getByLabel("Passwort:").fill("admin");
  await page.getByRole("button", { name: "Login" }).click();
  await expect(page.getByTestId('login-token')).toContainText()
});
