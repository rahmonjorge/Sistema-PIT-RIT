import { GRPCClient } from '$/lib/grpc/GRPCClient';
import { error } from '@sveltejs/kit';
import type { RequestHandler } from './$types';

export const POST = (async ({ request }) => {
	const body = await request.json();

	if (!body) {
		return new Response(JSON.stringify({ error: 'Body is empty' }), {
			status: 400,
			headers: {
				'Content-Type': 'application/json'
			}
		});
	}

	try {
		const response = await GRPCClient.database.gui.pit.createPit({
			ano: Number(body.ano),
			userId: body.userId
		});

		return new Response(JSON.stringify(response.request), {
			status: 200
		});
	} catch (e) {
		const err = e as { message: string };
		throw error(409, err.message);
	}
}) satisfies RequestHandler;
