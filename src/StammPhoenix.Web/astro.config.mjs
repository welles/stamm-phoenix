import { defineConfig } from 'astro/config';
import solid from "@astrojs/solid-js";
import tailwind from "@astrojs/tailwind";

import node from "@astrojs/node";

// https://astro.build/config
export default defineConfig({
  output: 'server',
  integrations: [solid(), tailwind({
    applyBaseStyles: false
  })],
  adapter: node({
    mode: "standalone"
  })
});
