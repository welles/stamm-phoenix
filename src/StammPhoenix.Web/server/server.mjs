import bun from "bun";
import fs from "fs";
import mime from "mime";
import { handler as ssrHandler } from "../dist/server/entry.mjs";

const clientRoot = new URL("../dist/client/", import.meta.url);

async function handle(req, res) {
  ssrHandler(req, res, async (err) => {
    if (err) {
      res.writeHead(500);
      res.end(err.stack);
      return;
    }

    const local = new URL(`.${req.url}`, clientRoot);

    try {
      const data = await fs.promises.readFile(local);
      res.writeHead(200, {
        "Content-Type": mime.getType(req.url),
      });
      res.end(data);
    } catch {
      res.writeHead(404);
      res.end();
    }
  });
}

const port = process.env.PORT || 80;

bun.listen({
  port,
  hostname: "0.0.0.0",
  handler: handle,
  development: true,
});

// Silence weird <time> warning
console.error = () => {};
