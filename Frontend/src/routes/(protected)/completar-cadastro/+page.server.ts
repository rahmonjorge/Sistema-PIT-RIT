import type { Actions, PageServerLoad } from './$types';
import { z } from 'zod';
import { GRPCClient } from '$/lib/grpc/GRPCClient';
import { fail, redirect } from '@sveltejs/kit';
import { superValidate } from 'sveltekit-superforms/server';
import type { User } from '@auth/core/types';

const completarCadastroRequestSchema = z.object({
	name: z
		.string()
		.nonempty({ message: 'O nome é obrigatório.' })
		.min(6, { message: 'Digite o seu nome completo' })
		.trim(),
	siape: z
		.string()
		.nonempty({ message: 'A matrícula SIAPE é obrigatória.' })
		.trim()
		.length(7, { message: 'A matrícula SIAPE deve ter 7 dígitos.' })
		.regex(/^[0-9]+$/, { message: 'A matrícula SIAPE deve conter apenas números.' }),
	dpto: z.string().nonempty({ message: 'O departamento é obrigatório.' }).trim(),
	vinculo: z.string().nonempty({ message: 'O vínculo é obrigatório.' }).trim(),
	regime: z.string().nonempty({ message: 'O regime é obrigatório.' }).trim(),
	reducao: z.string().nonempty({ message: 'A redução é obrigatória.' }).trim()
});

export const load = (async (event) => {
	const { session } = await event.parent();

	if (session?.user?.cadastroCompleto) {
		throw redirect(302, '/');
	}

	const form = await superValidate(event, completarCadastroRequestSchema);

	return {
		form
	};
}) satisfies PageServerLoad;

export const actions = {
	default: async (event) => {
		const form = await superValidate(event, completarCadastroRequestSchema);
		const session = await event.locals.getSession();

		if (!session) {
			return fail(400, {
				message: 'Usuário não autenticado.'
			});
		}

		if (!form.valid) {
			return fail(400, {
				form
			});
		}

		const user = session.user;

		try {
			const userComCadastroCompleto = await GRPCClient.database.gui.user.completarCadastro({
				id: (user as User).id,
				name: form.data.name,
				siape: form.data.siape,
				dpto: form.data.dpto,
				vinculo: form.data.vinculo,
				regime: form.data.regime,
				reducao: form.data.reducao
			});

			return {
				form,
				success: true,
				message: 'Cadastro completado com sucesso.'
			};
		} catch (error) {
			return fail(500, {
				form,
				success: false,
				message: 'Não foi possível completar o cadastro.'
			});
		}
	}
} satisfies Actions;
