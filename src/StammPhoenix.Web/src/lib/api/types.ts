export interface LoginRequest {
	login_email?: string
	password: string
}

export interface LoginResponse {
	token: string
}

export interface PostEventRequest {
	name: string
	link: string
	startDate: string // format: date
	endDate?: string // format: date
}

export interface GetPublicEventsResponse {
	year: number
	count: number
	events: PublicEventModel[]
}

export interface PublicEventModel {
	name: string
	link: string
	startDate: string // format: date
	endDate?: string // format: date
}
