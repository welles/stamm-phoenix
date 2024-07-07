import { APIClient } from "../apiClient"
import type { IsAliveResponse } from "../types"

class MetaAPI extends APIClient {
	async checkStatus(): Promise<IsAliveResponse> {
		return this.get("/is-alive")
	}
}

const metaAPI = new MetaAPI("https://dev-api.stamm-phoenix.de")

export const checkStatus = () => metaAPI.checkStatus()
