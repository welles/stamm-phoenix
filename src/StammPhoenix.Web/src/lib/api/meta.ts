import { createApiClient } from "./apiClient"

const BASE_URL = "https://dev-api.stamm-phoenix.de"

export const getMetadata = async (): Promise<string> => {
	const api = createApiClient(BASE_URL)
	const response = await api.get<string>("/")
	return response.data
}

export const isAlive = async (): Promise<string> => {
	const api = createApiClient(BASE_URL)
	return await api.get("/is-alive").then(function (res) { return res.data })
}
