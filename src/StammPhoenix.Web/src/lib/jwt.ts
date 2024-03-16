// jwt.ts
import { serialize, parse } from 'cookie';
import type { Request, Response } from 'bun';

// Set the JWT in an HTTPOnly cookie with variable duration
export const setJwtCookie = (response: Response, jwt: string, remember: boolean): void => {
  const maxAge = remember ? 60 * 60 * 24 * 90 : 60 * 60 * 24; // 90 days or 1 day
  const cookie = serialize('jwt', jwt, {
    httpOnly: true,
    secure: true,
    path: '/',
    maxAge: maxAge // Set the cookie to expire after 90 days or 1 day
  });

  response.headers.set('Set-Cookie', cookie);
};

// Get the JWT from the cookie in the request
export const getJwtFromCookie = (request: Request): string | null => {
  const cookieHeader = request.headers.get('Cookie');
  if (!cookieHeader) return null;

  const cookies = parse(cookieHeader);
  return cookies.jwt || null;
};

// Example handler using the JWT functions
export const handler = async (request: Request) => {
  const jwt = getJwtFromCookie(request);
  // Use the JWT for authentication, authorization, etc.
};
