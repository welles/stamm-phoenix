import { APIClient } from "../apiClient"
import type { PostEventRequest } from "../types"

class EventsAPI extends APIClient {
	async addEvent(eventData: PostEventRequest): Promise<void> {
		return this.post("/events/", eventData)
	}
}

const eventsAPI = new EventsAPI("https://dev-api.stamm-phoenix.de")

export const addEvent = (eventData: PostEventRequest) =>
	eventsAPI.addEvent(eventData)
