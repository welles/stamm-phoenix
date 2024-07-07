import { createApiClient } from "./apiClient"
import type { LoginRequest, LoginResponse } from "./types"

const BASE_URL = "https://dev-api.stamm-phoenix.de"

export const login = async (data: LoginRequest): Promise<LoginResponse> => {
	const api = createApiClient(BASE_URL)
	const response = await api.post<LoginResponse>("/auth/login", data)
	return response.data
}

export const isTokenValid = async (token: string): Promise<boolean> => {
	const api = createApiClient(BASE_URL, token)
	try {
		await api.get("/auth/check")
		return true
	} catch {
		return false
	}
}
