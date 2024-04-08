interface ErrorResponse {
	statusCode: number
	message: string
	errors: {
		[key: string]: string[]
	}
}

interface Events {
	year: number
	count: number
	events: Event[]
}

interface Event {
	name: string
	link: string
	startDate: string
	endDate: string
}

export type { Events, Event, ErrorResponse }
