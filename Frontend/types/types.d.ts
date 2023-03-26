import type { Session as OGSession, DefaultSession, User as OGUser } from '@auth/core/types';
import { SvelteKitAuthConfig as OGSvelteKitAuthConfig } from '@auth/sveltekit';
import type { CustomAdapter } from '$lib/prisma/client';

declare module '@auth/core/types' {
	interface Session extends OGSession {
		user?: {
			cadastroCompleto: boolean;
		} & DefaultSession['user'];
	}

	interface User extends OGUser {
		cadastroCompleto: boolean;
	}
}

declare module '@auth/sveltekit' {
	interface SvelteKitAuthConfig extends OGSvelteKitAuthConfig {
		adapter: CustomAdapter;
	}
}
