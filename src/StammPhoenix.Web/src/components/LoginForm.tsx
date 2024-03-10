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
        const { token, error: string } = await login(email(), password());
        setToken(token);
        setError(error);
    };

    onMount(() => {
        checkToken(token());
    });

    return (
        <div class="rounded-md w-fit p-5 bg-sky-500 place-self-center">
            <h1>Login</h1>
            <form onSubmit={handleLogin}>
               <div>
                    <label for="email">Email:</label>
                    <br />
                    <input
                        class="appearance-none rounded-full cursor-text bg-sky-600 hover:bg-sky-700"
                        type="email"
                        id="email"
                        value={email()}
                        onInput={(e) => setEmail(e.currentTarget.value)}
                        required
                    />
                </div>
                <div>
                    <label for="password">Password:</label>
                    <br />
                    <input
                        class="appearance-none rounded-full cursor-text bg-sky-600 hover:bg-sky-700"
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
