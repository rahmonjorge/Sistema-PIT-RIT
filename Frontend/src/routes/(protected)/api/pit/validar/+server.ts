import { GRPCClient } from '$/lib/grpc/GRPCClient';
import { error } from '@sveltejs/kit';
import type { RequestHandler } from './$types';
import { z } from 'zod';
import type { Sheet, UserInfo } from '$/lib/protos/database.gui';

const isValidRequestSchema = z.object({
	ano: z
		.number({
			required_error: 'O ano é obrigatório.',
			invalid_type_error: 'O ano deve ser um número.'
		})
		.int('O ano deve ser um número inteiro.')
		.positive('O ano deve ser positivo.'),
	userId: z.string({
		required_error: 'O ID do usuário é obrigatório.',
		invalid_type_error: 'O ID do usuário deve ser uma string.'
	})
});

const validate: (sheet: Sheet, user: UserInfo) => { valid: boolean; errors: string[] } = (
	sheet,
	user
) => {
	const errors: string[] = [];
	let valid = true;

	// Validando se a carga horária semanal de aulas em cursos de graduação está nos limites
	const minimoGrad = user.reducao === 'Sim (Art. 9º)' ? 0 : 4;
	if (sheet.chGrad < minimoGrad) {
		valid = false;
		errors.push(
			`A carga horária semanal de aulas em cursos de graduação deve ser no mínimo ${minimoGrad} horas`
		);
	}

	// Validando se o subtotal de ministração de aulas está nos limites
	const minimoSubtotalMinistracao =
		user.reducao === 'Não' ? 8 : user.reducao === 'Sim (Art. 10º)' ? 4 : 0;
	const maximoSubtotalMinistracao = user.regime === '20h' ? 12 : 20;
	if (sheet.chGrad + sheet.chPos < minimoSubtotalMinistracao) {
		valid = false;
		errors.push(
			`O subtotal de ministração de aulas deve ser no mínimo ${minimoSubtotalMinistracao} horas`
		);
	}
	if (sheet.chGrad + sheet.chPos > maximoSubtotalMinistracao) {
		valid = false;
		errors.push(
			`O subtotal de ministração de aulas deve ser no máximo ${maximoSubtotalMinistracao} horas`
		);
	}

	// Validando se o subtotal de atividades de pesquisa está nos limites
	if (sheet.chPesquisa > 20) {
		valid = false;
		errors.push(`O subtotal de atividades de pesquisa deve ser no máximo 20 horas`);
	}

	// Validando se o subtotal de atividades de extensão está nos limites
	if (sheet.chExtensao > 20) {
		valid = false;
		errors.push(`O subtotal de atividades de extensão deve ser no máximo 20 horas`);
	}

	// Validando se o total final está nos limites
	const objectiveTotal = user.regime === '20h' ? 20 : 40;

	if (
		sheet.chGrad +
			sheet.chPos +
			sheet.chEnsino +
			sheet.chPesquisa +
			sheet.chExtensao +
			sheet.chAdm !==
		objectiveTotal
	) {
		valid = false;
		errors.push(`O total final deve ser exatamente ${objectiveTotal} horas`);
	}

	return { valid, errors };
};

export const POST = (async ({ request }) => {
	const body = await request.json();

	try {
		const parsedBody = isValidRequestSchema.parse(body);
		const resPit = await GRPCClient.database.gui.pit.getPit({
			ano: Number(parsedBody.ano),
			userId: parsedBody.userId
		});

		const resUser = await GRPCClient.database.gui.user.getUserInfo({
			id: parsedBody.userId
		});

		const pit = resPit.response;
		const user = resUser.response;

		return new Response(JSON.stringify(validate(pit, user)));
	} catch (e) {
		if (e instanceof z.ZodError) {
			throw error(400, e.message);
		}
		const err = e as { message: string };
		throw error(409, err.message);
	}
}) satisfies RequestHandler;
