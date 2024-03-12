import { createServer } from "http";
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

const server = createServer((req, res) => {
  handle(req, res).catch((err) => {
    console.error(err);
    res.writeHead(500, {
      "Content-Type": "text/plain",
    });
    res.end(err.toString());
  });
});

// Use the PORT environment variable, or fallback to 80 if not set
const port = process.env.PORT || 80;

server.listen(port, () => {
  console.log(`Serving at http://localhost:${port}`);
});

// Silence weird <time> warning
console.error = () => {};
