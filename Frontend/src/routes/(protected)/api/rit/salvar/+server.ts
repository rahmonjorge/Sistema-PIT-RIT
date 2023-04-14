import { GRPCClient } from '$/lib/grpc/GRPCClient';
import { error } from '@sveltejs/kit';
import type { RequestHandler } from './$types';
import { z } from 'zod';

const UpdatePitSchema = z.object({
	ano: z
		.number()
		.nonnegative()
		.int()
		.min(2021)
		.max(new Date().getFullYear() + 2),
	userId: z.string(),
	sheet: z.object({
		chGrad: z.number(),
		chPos: z.number(),
		ensino: z.array(z.boolean()),
		chEnsino: z.number(),
		pesquisa: z.array(z.boolean()),
		chPesquisa: z.number(),
		extensao: z.array(z.boolean()),
		chExtensao: z.number(),
		adm: z.array(z.boolean()),
		chAdm: z.number()
	})
});

export const POST = (async ({ request }) => {
	const body = await request.json();
	try {
		const parsedBody = UpdatePitSchema.parse(body);

		const response = await GRPCClient.database.gui.rit.updateRit({
			ano: parsedBody.ano,
			userId: parsedBody.userId,
			sheet: parsedBody.sheet
		});

		return new Response(JSON.stringify(response.request), {
			status: 200
		});
	} catch (err) {
		if (err instanceof z.ZodError) {
			throw error(400, 'Invalid request body');
		}
		throw error(500, 'Erro ao salvar RIT');
	}
}) satisfies RequestHandler;
