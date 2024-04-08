import axios, { type AxiosResponse } from "axios"
import { APIError, type ErrorResponse } from "../types"

interface Credentials {
	login_email: string
	password: string
}

/**
 * this is a login function for the stamm-phoenix API
 *
 * @param email - email address of the user
 * @param password - password of the user
 * @returns a jwt token or an error
 *
 * @example
 * ```ts
 * const { token: string | null, error: string | null } = await login('YxRjF@example.com', 'password123')
 * ```
 *
 */
const login = async (
	email: string,
	password: string,
): Promise<{ token: string | null; error: string | null }> => {
	// start try block for login
	try {
		// set credentials
		const credentials: Credentials = { login_email: email, password }

		// send API request and get response
		const response: AxiosResponse = await axios.post(
			"https://dev-api.stamm-phoenix.de/auth/login",
			credentials,
		)

		// check if response was successful and return token or error
		if (response.status === 200) {
			return { token: response.data.token, error: null }
		}
		return { token: null, error: response.data.error }

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
			throw new APIError(errorResponse)
		}
		console.error("An unexpected error occurred:", error)
		throw new APIError({
			statusCode: 500,
			message: "An unexpected error occurred",
			errors: {},
		})
	}
}

export { login }
