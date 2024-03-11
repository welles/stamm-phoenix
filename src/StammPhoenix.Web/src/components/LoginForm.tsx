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
    <div class="max-w-sm mx-auto mt-10 bg-white rounded-lg shadow-md p-6">
      <h1 class="text-2xl font-bold mb-6 text-center">Login</h1>
      <form onSubmit={handleLogin}>
        <div class="mb-4">
          <label
            class="block text-gray-700 font-bold mb-2"
            for="email"
          >
            Email:
          </label>
          <input
            class="form-input rounded-md w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
            type="email"
            id="email"
            value={email()}
            onInput={(e) => setEmail(e.currentTarget.value)}
            required
          />
        </div>
        <div class="mb-6">
          <label
            class="block text-gray-700 font-bold mb-2"
            for="password"
          >
            Password:
          </label>
          <input
            class="form-input rounded-md w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
            type="password"
            id="password"
            value={password()}
            onInput={(e) => setPassword(e.currentTarget.value)}
            required
          />
        </div>
        {error() && (
          <p class="text-red-500 text-sm mb-4">{error()}</p>
        )}
        <button
          class="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded focus:outline-none focus:shadow-outline"
          type="submit"
        >
          Login
        </button>
      </form>
    </div>
  );
};

export default LoginForm;
