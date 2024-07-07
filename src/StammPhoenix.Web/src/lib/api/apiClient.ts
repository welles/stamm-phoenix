import axios, { type AxiosInstance } from "axios"

export const createApiClient = (
	baseURL: string,
	token?: string,
): AxiosInstance => {
	return axios.create({
		baseURL,
		headers: token ? { Authorization: `Bearer ${token}` } : {},
	})
}
