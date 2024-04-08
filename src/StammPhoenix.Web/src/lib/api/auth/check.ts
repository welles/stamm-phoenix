import axios, { type AxiosError, type AxiosResponse } from "axios"
import type { ErrorResponse } from "../types"

/**
 * This function checks if a jwt token is valid.
 *
 * @param token - The jwt token to check.
 * @returns A boolean value indicating if the token is valid.
 *
 * @example
 * ```ts
 * const isValid: boolean = await checkToken('your_jwt_token')
 * ```
 *
 */

const checkToken = async (token: string): Promise<boolean | ErrorResponse> => {
	try {
		// send request to check if given token is valid
		const response: AxiosResponse = await axios.get(
			"https://dev-api.stamm-phoenix.de/auth/check",
			{
				headers: {
					Authorization: `Bearer ${token}`,
				},
			},
		)

		// check if response status is 204 (meaning token is valid)
		if (response.status === 204) {
			return true
		}
		return false

		// catch errors
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

export { checkToken }
