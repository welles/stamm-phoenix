import { APIClient } from "../apiClient"

class AuthAPI extends APIClient {
	async checkToken(): Promise<void> {
		return this.get("/auth/check")
	}
}

const authAPI = new AuthAPI("https://dev-api.stamm-phoenix.de")

export const checkToken = () => authAPI.checkToken()
