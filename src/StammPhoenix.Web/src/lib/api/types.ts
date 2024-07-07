export interface LoginRequest {
	login_email?: string
	password: string
}

export interface LoginResponse {
	token: string
}

export interface IsAliveResponse {
	status: string
	startup_time: string
	version: string
}

export interface PostEventRequest {
	name: string
	link: string
	startDate: string
	endDate?: string
}

export interface GetPublicEventsResponse {
	year: number
	count: number
	events: PublicEventModel[]
}

export interface PublicEventModel {
	name: string
	link: string
	startDate: string
	endDate?: string
}
