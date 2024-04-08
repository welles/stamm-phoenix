import axios, { type AxiosResponse } from "axios"
import { APIError, type ErrorResponse, type Events } from "../types"

/**
 * This function gets all events of a given year from the API.
 *
 * @param year - The year, which events should be fetched.
 * @returns Promise<Events | ErrorResponse>
 *
 * @example
 * ```ts
 * const events: Events | ErrorResponse = await getEventsPerYear(2023);
 * ```
 */
const getEventsPerYear = async (year: number): Promise<Events> => {
	try {
		// send request to get all events of a given year
		const response: AxiosResponse<Events | ErrorResponse> = await axios.get(
			`https://dev-api.stamm-phoenix.de/events/public/${year}`,
		)

		// check if response status is 200
		if (response.status === 200) {
			return response.data as Events
		}
		throw new APIError(response.data as ErrorResponse)
	} catch (error: unknown) {
		if (axios.isAxiosError(error)) {
			console.error(
				"An error occurred while fetching events:",
				error.message,
			)
			const errorResponse: ErrorResponse = {
				statusCode: error.response?.status || 500,
				message: error.message,
				errors: error.response?.data || {},
			}
			throw new APIError(errorResponse)
		}
		if (error instanceof APIError) {
			console.error("An unexpected error occurred:", error.message)
			throw new APIError(error.getErrorResponse())
		}
		console.error("An unexpected error occurred:", error)
		throw new APIError({
			statusCode: 500,
			message: "An unexpected error occurred",
			errors: {},
		})
	}
}

export { getEventsPerYear }
