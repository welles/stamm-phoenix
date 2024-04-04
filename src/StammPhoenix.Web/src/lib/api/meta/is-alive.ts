import axios, { AxiosError, type AxiosResponse } from 'axios'

interface APIMetaData {
	status: string
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
const isAlive = async (): Promise<APIMetaData> => {
	try {
		// send request to check if the api is alive
		const response: AxiosResponse = await axios.get(
			'https://dev-api.stamm-phoenix.de/is-alive',
		)

		// check if the api is alive
		if (response.status === 200) {
			// return meta data
			return response.data
		} else {
			// throw error if the api is not alive
			throw new AxiosError('API is not alive')
		}
		// catch error if an error occurs
	} catch (error: AxiosError | any) {
		if (axios.isAxiosError(error)) {
			console.error(
				'An error occurred while checking if the API is alive:',
				error.message,
			)
		} else {
			console.error(
				'An error occurred while checking if the API is alive:',
				error,
			)
		}

		// return error
		return { status: 'error', startup_time: '', version: '' }
	}
}

export { isAlive }
