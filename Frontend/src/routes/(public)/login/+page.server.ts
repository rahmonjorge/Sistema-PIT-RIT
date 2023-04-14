import { redirect } from '@sveltejs/kit';
import type { PageServerLoad } from './$types';

export const load = (async (event) => {
	const { session } = await event.parent();
	if (session?.user) {
		throw redirect(302, '/');
	}
	return {};
}) satisfies PageServerLoad;
