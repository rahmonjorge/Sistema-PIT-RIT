import { GRPCClient } from '$/lib/grpc/GRPCClient';
import { error, redirect } from '@sveltejs/kit';

import type { PageServerLoad } from './$types';
import { _validate } from '$/routes/(protected)/api/pit/validar/+server';

export const load = (async ({ parent, params, fetch }) => {
	const { session } = await parent();
	const { ano } = params;

	console.log('o meu deus');

	if (!session) {
		throw error(401, 'Não autorizado');
	}

	const { user } = session;

	if (!user) {
		throw error(401, 'Não autorizado');
	}

	const userInfo = await GRPCClient.database.gui.user.getUserInfo({
		id: user.id
	});

	const pitSheet = await GRPCClient.database.gui.pit.getPit({
		ano: Number(ano),
		userId: user.id
	});

	if (!_validate(pitSheet.response, userInfo.response).valid) {
		throw redirect(302, '/');
	}

	if (!user) {
		throw error(401, 'Não autorizado');
	}

	try {
		const res = await GRPCClient.sheets.getPitPDF({
			ano: Number(ano),
			userId: user?.id
		});
		return { pdf: Buffer.from(res.response.pdf).toString('base64') };
	} catch (err) {
		console.error(err);
		throw error(500, 'Erro');
	}
}) satisfies PageServerLoad;
