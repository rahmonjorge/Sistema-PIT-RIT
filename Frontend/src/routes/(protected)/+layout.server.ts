import { redirect } from '@sveltejs/kit';
import type { LayoutServerLoad } from './$types';

export const load = (async ({ parent, route }) => {
	const { session } = await parent();

	if (!session?.user) {
		throw redirect(302, '/login');
	}

	if (!session?.user?.cadastroCompleto && route.id !== '/(protected)/completar-cadastro') {
		throw redirect(302, '/completar-cadastro');
	}

	return {};
}) satisfies LayoutServerLoad;
