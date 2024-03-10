// src/components/Login.jsx
import { createSignal, onMount } from "solid-js";
import { login, checkToken } from "../lib/loginLogic";

const LoginForm = () => {
    const [email, setEmail] = createSignal("");
    const [password, setPassword] = createSignal("");
    const [token, setToken] = createSignal(null);
    const [error, setError] = createSignal(null);

    const handleLogin = async (e: Event) => {
        e.preventDefault();
        const { token, error } = await login(email(), password());
        setToken(token);
        setError(error);
    };

    onMount(() => {
        checkToken(token());
    });

    return (
        <div>
            <h1>Login</h1>
            <form onSubmit={handleLogin}>
               <div>
                    <label for="email">Email:</label>
                    <input
                        type="email"
                        id="email"
                        value={email()}
                        onInput={(e) => setEmail(e.currentTarget.value)}
                        required
                    />
                </div>
                <div>
                    <label for="password">Password:</label>
                    <input
                        type="password"
                        id="password"
                        value={password()}
                        onInput={(e) => setPassword(e.currentTarget.value)}
                        required
                    />
                </div>
                {error() && <p>{error()}</p>}
                <button type="submit">Login</button>
            </form>
        </div>
    );
};

export default LoginForm;
