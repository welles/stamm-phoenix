// src/utils/loginLogic.ts

export const login = async (email: string, password: string) => {
    try {

        const creds = { login_email: email, password: password };

    const response = await fetch("https://dev-api.stamm-phoenix.de/auth/login", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(creds),
      mode: 'no-cors',
    });
    const data = await response.json();
    if (response.ok) {
      return { token: data.token, error: null };
    } else {
        return { token: null, error: "an error" }
    }
  } catch (error) {
    return { token: null, error: `An error occurred while logging in. ${error}` };
  }
};

export const checkToken = async (token: string | null) => {
  if (token) {
    try {
      const response = await fetch("https://dev-api.stamm-phoenix.de/auth/check", {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      });
      const data = await response.json();
      if (response.ok) {
        // Token is valid
        console.log("Token is valid:", data);
      } else {
        // Token is invalid
        console.log("Token is invalid:", data.error);
      }
    } catch (error) {
      console.error("An error occurred while checking the token:", error);
    }
  }
};
