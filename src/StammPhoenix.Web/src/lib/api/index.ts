export * from "./auth"
export * from "./events"
export * from "./meta"
export type * from "./types"

import { APIClient } from "./apiClient"

export const setToken = (newToken: string) => {
	// Update token for all API clients
	APIClient.prototype.api.defaults.headers.Authorization = `Bearer ${newToken}`
}
