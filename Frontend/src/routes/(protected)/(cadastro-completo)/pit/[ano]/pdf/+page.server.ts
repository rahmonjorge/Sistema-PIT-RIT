import { GRPCClient } from '$/lib/grpc/GRPCClient';
import { error, redirect } from '@sveltejs/kit';

import type { PageServerLoad } from './$types';

export const load = (async ({ parent, params, fetch }) => {
	const { session } = await parent();
	const { ano } = params;

	if (!session) {
		throw error(401, 'Não autorizado');
	}

	const { user } = session;

	const isValid = await fetch('http://localhost:5173/api/validar', {
		method: 'POST',
		headers: {
			'Content-Type': 'application/json'
		},
		body: JSON.stringify({
			userId: user?.id,
			ano: Number(ano)
		})
	});

	if (!isValid.ok) {
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
