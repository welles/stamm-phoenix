import axios, { AxiosError, type AxiosResponse } from "axios"
import { APIError, type ErrorResponse } from "../types"

interface ApiMetaData {
	status: string
	// biome-ignore lint: has to be snake_case
	startup_time: string
	version: string
}

/**
 * This function checks if the api is alive and returns meta data.
 *
 * @example
 * ```ts
 * const metaData = await isAlive()
 * ```
 *
 */
const isAlive = async (): Promise<ApiMetaData> => {
	try {
		// send request to check if the api is alive
		const response: AxiosResponse = await axios.get(
			"https://dev-api.stamm-phoenix.de/is-alive",
		)

		// check if the api is alive
		if (response.status === 200) {
			// return meta data
			return response.data
		}
		// throw error if the api is not alive
		throw new AxiosError("API is not alive")
		// catch error if an error occurs
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
		console.error("An unexpected error occurred:", error)
		throw new APIError({
			statusCode: 500,
			message: "An unexpected error occurred",
			errors: {},
		} as ErrorResponse)
	}
}

export { isAlive }
