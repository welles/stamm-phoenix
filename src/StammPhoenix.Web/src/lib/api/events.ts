import { createApiClient } from "./apiClient"
import type { GetPublicEventsResponse, PostEventRequest } from "./types"

const BASE_URL = "https://dev-api.stamm-phoenix.de"

export const addEvent = async (
	data: PostEventRequest,
	token: string,
): Promise<void> => {
	const api = createApiClient(BASE_URL, token)
	await api.post("/events/", data)
}

export const getPublicEvents = async (
	year: number,
): Promise<GetPublicEventsResponse> => {
	const api = createApiClient(BASE_URL)
	const response = await api.get<GetPublicEventsResponse>(
		`/events/public/${year}`,
	)
	return response.data
}
