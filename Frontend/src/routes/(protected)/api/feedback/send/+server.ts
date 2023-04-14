import { GRPCClient } from '$/lib/grpc/GRPCClient';
import { error } from '@sveltejs/kit';
import type { RequestHandler } from './$types';
import { z } from 'zod';
import { Type } from '$/lib/protos/mail';

const sendFeedbackRequestSchema = z.object({
	email: z.optional(z.string().email('O email informado não é válido')),
	name: z.optional(z.string()),
	type: z.enum(['BUG', 'FEATURE', 'QUESTION'], { invalid_type_error: 'Tipo de feedback inválido' }),
	message: z.string().nonempty('A mensagem não pode ser vazia')
});

export const POST = (async ({ request }) => {
	const body = await request.json();

	try {
		const validationResult = sendFeedbackRequestSchema.parse(body);

		let type: Type;

		switch (validationResult.type) {
			case 'BUG':
				type = Type.bug;
				break;
			case 'FEATURE':
				type = Type.feature;
				break;
			case 'QUESTION':
				type = Type.question;
		}

		const response = await GRPCClient.mail.sendEmail({
			from: validationResult.email,
			name: validationResult.name,
			message: validationResult.message,
			type
		});

		return new Response(JSON.stringify(response.request), {
			status: 200
		});
	} catch (e) {
		if (e instanceof z.ZodError) {
			throw error(400, e.message);
		}
		const err = e as { message: string };
		throw error(400, err.message);
	}
}) satisfies RequestHandler;
