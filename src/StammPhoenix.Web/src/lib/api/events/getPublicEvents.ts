import { APIClient } from "../apiClient"
import type { GetPublicEventsResponse } from "../types"

class EventsAPI extends APIClient {
	async getPublicEvents(year: number): Promise<GetPublicEventsResponse> {
		return this.get(`/events/public/${year}`)
	}
}

const eventsAPI = new EventsAPI("https://dev-api.stamm-phoenix.de")

export const getPublicEvents = (year: number) => eventsAPI.getPublicEvents(year)
