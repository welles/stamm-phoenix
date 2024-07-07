import { APIClient } from "../apiClient"

class MetaAPI extends APIClient {
	async showMetadata(): Promise<string> {
		return this.get("/")
	}
}

const metaAPI = new MetaAPI("https://dev-api.stamm-phoenix.de")

export const showMetadata = () => metaAPI.showMetadata()
