import axios, { AxiosError, type AxiosResponse } from 'axios'

export interface Credentials {
	login_email: string
	password: string
}

export const login = async (
	email: string,
	password: string,
): Promise<{ token: string | null; error: string | null }> => {
	try {
		const creds: Credentials = { login_email: email, password }
		// ^?
		const response = await axios.post(
			'https://dev-api.stamm-phoenix.de/auth/login',
			creds,
		)

		if (response.status === 200) {
			return { token: response.data.token, error: null }
		} else {
			return { token: null, error: response.data.error }
		}
	} catch (error: AxiosError | any) {
		if (axios.isAxiosError(error)) {
			return {
				token: null,
				error: `An error occurred while logging in: ${error.message}`,
			}
		} else {
			return {
				token: null,
				error: `An error occurred while logging in: ${error}`,
			}
		}
	}
}

export const checkToken = async (token: string): Promise<boolean> => {
	try {
		const response: AxiosResponse = await axios.get(
			'https://dev-api.stamm-phoenix.de/auth/check',
			{
				headers: {
					Authorization: `Bearer ${token}`,
				},
			},
		)

		if (response.status === 204) {
			console.log('Token is valid:', response.data)
			return true
		} else {
			console.error('Token is invalid:', response.data.error)
			return false
		}
	} catch (error: AxiosError | any) {
		if (axios.isAxiosError(error)) {
			console.error(
				'An error occurred while checking the token:',
				error.message,
			)
		} else {
			console.error('An error occurred while checking the token:', error)
		}
		return false
	}
}
