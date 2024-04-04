import axios, { type AxiosError, type AxiosResponse } from 'axios'

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

const checkToken = async (token: string): Promise<boolean> => {
	try {
		// send request to check if given token is valid
		const response: AxiosResponse = await axios.get(
			'https://dev-api.stamm-phoenix.de/auth/check',
			{
				headers: {
					Authorization: `Bearer ${token}`,
				},
			},
		)

		// check if response status is 204 (meaning token is valid)
		if (response.status === 204) {
			return true
		} else {
			return false
		}

		// catch errors
	} catch (error: AxiosError | any) {
		// check if error is an axios error
		if (axios.isAxiosError(error)) {
			console.error(
				'An error occurred while checking the token:',
				error.message,
			)
		} else {
			console.error('An error occurred while checking the token:', error)
		}

		// return false if an error occurred
		return false
	}
}

export { checkToken }
