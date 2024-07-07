import { APIClient } from "../apiClient"
import type { LoginRequest, LoginResponse } from "../types"

class AuthAPI extends APIClient {
	async login(loginData: LoginRequest): Promise<LoginResponse> {
		return this.post("/auth/login", loginData)
	}
}

const authAPI = new AuthAPI("https://dev-api.stamm-phoenix.de")

export const login = (loginData: LoginRequest) => authAPI.login(loginData)
