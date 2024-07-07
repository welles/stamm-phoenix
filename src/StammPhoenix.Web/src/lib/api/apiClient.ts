import axios, {
	type AxiosInstance,
	type AxiosRequestConfig,
	type AxiosResponse,
} from "axios"

export class APIClient {
	public api: AxiosInstance

	constructor(baseURL: string, token?: string) {
		const config: AxiosRequestConfig = {
			baseURL,
		}
		if (token) {
			config.headers = { Authorization: `Bearer ${token}` }
		}
		this.api = axios.create(config)
	}

	protected async get<T>(url: string): Promise<T> {
		return this.api.get(url).then(this.handleResponse)
	}

	protected async post<T>(url: string, data: any): Promise<T> {
		return this.api.post(url, data).then(this.handleResponse)
	}

	private handleResponse<T>(response: AxiosResponse<T>): T {
		return response.data
	}
}
