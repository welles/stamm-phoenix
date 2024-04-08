import axios, { type AxiosResponse } from "axios"
import type { ErrorResponse, Events } from "../types"

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
const getEventsPerYear = async (
	year: number,
): Promise<Events | ErrorResponse> => {
	try {
		// send request to get all events of a given year
		const response: AxiosResponse<Events | ErrorResponse> = await axios.get(
			`https://dev-api.stamm-phoenix.de/events/public/${year}`,
		)

		// check if response status is 200
		if (response.status === 200) {
			return response.data as Events
		}
		return response.data as ErrorResponse
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
			return errorResponse
		}
		console.error("An unexpected error occurred:", error)
		return {
			statusCode: 500,
			message: "An unexpected error occurred",
			errors: {},
		}
	}
}

export { getEventsPerYear }
