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
    <div class="max-w-md mx-auto mt-10 bg-dpsg-gray-light rounded-md shadow-md p-8">
      <h1 class="text-3xl font-bold mb-8 text-center text-dpsg-green">
        Anmeldung
      </h1>
      <form onSubmit={handleLogin}>
        <div class="mb-6">
          <label class="block text-dpsg-gray-dark font-bold mb-2" for="email">
            E-Mail:
          </label>
          <input
            class="appearance-none rounded-md w-full py-3 px-4 text-dpsg-gray-dark leading-tight focus:outline-none focus:shadow-outline border border-dpsg-green"
            type="email"
            id="email"
            value={email()}
            onInput={(e) => setEmail(e.currentTarget.value)}
            required
          />
        </div>
        <div class="mb-6">
          <label class="block text-dpsg-gray-dark font-bold mb-2" for="password">
            Passwort:
          </label>
          <input
            class="appearance-none rounded-md w-full py-3 px-4 text-dpsg-gray-dark leading-tight focus:outline-none focus:shadow-outline border border-dpsg-green"
            type="password"
            id="password"
            value={password()}
            onInput={(e) => setPassword(e.currentTarget.value)}
            required
          />
        </div>
        {error() && (
          <p class="text-dpsg-red text-sm mb-6">{error()}</p>
        )}
        <button
          class="bg-dpsg-green hover:bg-dpsg-green-dark text-white font-bold py-3 px-6 rounded-md focus:outline-none focus:shadow-outline transition-colors duration-base ease-in-out"
          type="submit"
        >
          Anmelden
        </button>
      </form>
    </div>
  );
};

export default LoginForm;
