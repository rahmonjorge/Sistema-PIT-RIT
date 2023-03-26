import { z } from 'zod';
import * as environment from '$env/static/private';

export const ServerConfigSchema = z.object({
	GOOGLE_CLIENT_ID: z.string(),
	GOOGLE_CLIENT_SECRET: z.string(),
	AUTH_SECRET: z.string()
});

export type ServerConfig = z.infer<typeof ServerConfigSchema>;

export const config = ServerConfigSchema.parse(environment);
