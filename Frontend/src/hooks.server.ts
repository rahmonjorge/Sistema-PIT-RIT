import { SvelteKitAuth } from '@auth/sveltekit';
import Google from '@auth/core/providers/google';
import { config } from '$lib/config.server';
import type { Handle } from '@sveltejs/kit';
import { GRPCAdapter } from '$/lib/grpc/GRPCAdapter';
import { GRPCClient } from '$/lib/grpc/GRPCClient';

export const handle = (async (...args) => {
	const [{ event }] = args;
	return SvelteKitAuth({
		adapter: GRPCAdapter(GRPCClient),
		providers: [
			// eslint-disable-next-line @typescript-eslint/ban-ts-comment
			// @ts-ignore
			Google({
				clientId: config.GOOGLE_CLIENT_ID,
				clientSecret: config.GOOGLE_CLIENT_SECRET
			})
		],
		callbacks: {
			async session({ session, user }) {
				session.user = {
					name: user.name,
					email: user.email,
					image: user.image,
					cadastroCompleto: user.cadastroCompleto
				};
				event.locals.session = session;
				return session;
			}
		}
	})(...args);
}) satisfies Handle;
