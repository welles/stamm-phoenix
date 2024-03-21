// src/utils/authLogic.ts

export interface Credentials {
  login_email: string;
  password: string;
}

export const login = async (email: string, password: string): Promise<{ token: string | null; error: string | null }> => {
  try {
    const creds: Credentials = { login_email: login_email, password };
    const response = await fetch("https://dev-api.stamm-phoenix.de/auth/login", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(creds),
    });

    const data = await response.json();

    if (response.ok) {
      return { token: data.token, error: null };
    } else {
      return { token: null, error: data.error };
    }
  } catch (error) {
    return { token: null, error: `An error occurred while logging in: ${error}` };
  }
};

export const checkToken = async (token: string): Promise<boolean> => {
  try {
    const response = await fetch("https://dev-api.stamm-phoenix.de/auth/check", {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    });

    // Check if the response is valid JSON
    if (!response.ok) {
      console.error("Token is invalid:", response.statusText);
      return false;
    }

    const data = await response.json();

    if (response.ok) {
      console.log("Token is valid:", data);
      return true;
    } else {
      console.error("Token is invalid:", data.error);
      return false;
    }
  } catch (error) {
    console.error("An error occurred while checking the token:", error);
    return false;
  }
};