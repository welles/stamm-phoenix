interface ErrorResponse {
	statusCode: number
	message: string
	errors: {
		[key: string]: string[]
	}
}

class APIError extends Error {
	error: ErrorResponse

	constructor(error: ErrorResponse) {
		super(error.message)
		this.error = error
		// Set the prototype explicitly.
		Object.setPrototypeOf(this, APIError.prototype)
	}

	getErrorResponse = (): ErrorResponse => {
		return this.error
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

export { type Events, type Event, type ErrorResponse, APIError }
