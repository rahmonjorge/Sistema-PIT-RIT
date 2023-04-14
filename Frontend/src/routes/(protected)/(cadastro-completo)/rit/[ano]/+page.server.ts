import { GRPCClient } from '$/lib/grpc/GRPCClient';
import { redirect } from '@sveltejs/kit';
import type { PageServerLoad } from './$types';
import { serializeNonPOJOs } from '$/lib/utils';

export const load = (async ({ params, parent }) => {
	const { session } = await parent();

	const user = session?.user;

	if (!user) {
		throw redirect(302, '/login');
	}

	const ano = Number(params.ano as string);

	try {
		const userInfoResponse = await GRPCClient.database.gui.user.getUserInfo({
			id: user.id
		});

		const userInfo = userInfoResponse.response;

		const pitResponse = await GRPCClient.database.gui.pit.getPit({
			ano,
			userId: user.id
		});

		const ritResponse = await GRPCClient.database.gui.rit.getRit({
			ano,
			userId: user.id
		});

		const pit = pitResponse.response;
		const rit = ritResponse.response;

		return {
			userInfo: serializeNonPOJOs(userInfo),
			planilha: {
				minAula: [
					{
						text: 'Carga horária semanal de ministração de aulas teóricas, práticas, de laboratório ou de campo em cursos de graduação',
						value: { pit: pit.chGrad, rit: rit.chGrad },
						minimo: userInfo.reducao === 'Sim (Art. 9º)' ? -1 : 4
					},
					{
						text: 'Carga horária semanal de ministração de aulas teóricas, práticas, de laboratório ou de campo em programas de pós-graduação',
						value: { pit: pit.chPos, rit: rit.chPos },
						minimo: -1
					}
				],
				chMinAula: {
					minimo: userInfo.reducao === 'Não' ? 8 : userInfo.reducao === 'Sim (Art. 10º)' ? 4 : -1,
					maximo: userInfo.regime === '20h' ? 12 : 20
				},
				ensino: [
					{
						text: 'Atividades de manutenção de ensino em cursos de graduação e programas de pós-graduação (planejamento de ensino, preparação de aulas, confecção de material didático, criação de recursos educacionais, atendimento e acompanhamento do discente, preparação e correção de avaliações, avaliação das atividades discentes e manutenção do registro escolar)',
						value: { pit: false, rit: false }
					},
					{
						text: 'Participação no planejamento, na organização, na execução e na avaliação referentes ao ensino oferecido pela UFRPE (reuniões pedagógicas, reuniões de coordenação e reuniões de gestão)',
						value: { pit: false, rit: false }
					},
					{
						text: 'Orientação e/ou supervisão principal de alunos de graduação (programas de estímulo à docência, monografia, bolsa permanência, monitoria, tutorial de 1º ano, cooperação internacional, PAVI, TCC, ESO, ACC, incluindo estágios não obrigatórios), Turoria PET',
						value: { pit: false, rit: false }
					},
					{
						text: 'Tutoria ou preceptoria de Programas de Residência (PR)',
						value: { pit: false, rit: false }
					},
					{
						text: 'Orientação principal de alunos de pós-graduação (Stricto sensu e Lato sensu)',
						value: { pit: false, rit: false }
					},
					{
						text: 'Co-orientação de alunos de pós-graduação (Stricto sensu e Lato sensu)',
						value: { pit: false, rit: false }
					},
					{
						text: 'Coordenação de núcleos e grupos de estudos institucionalizados em CTA',
						value: { pit: false, rit: false }
					},
					{
						text: 'Coordenação de projeto de ensino com financiamento externo',
						value: { pit: false, rit: false }
					},
					{
						text: 'Coordenação de projeto de ensino sem financiamento externo',
						value: { pit: false, rit: false }
					},
					{
						text: 'Colaboração em projeto de ensino com ou sem financiamento externo',
						value: { pit: false, rit: false }
					},
					{
						text: 'Supervisão/orientação de residência multiprofissional',
						value: { pit: false, rit: false }
					},
					{
						text: 'Participação como membro titular de banca de trabalhos de conclusão de curso',
						value: { pit: false, rit: false }
					},
					{
						text: 'Formação continuada em docência na área de atuação ou afim',
						value: { pit: false, rit: false }
					},
					{
						text: 'Carga horária semanal de ministração de aulas não remuneradas em cursos de graduação em outras instituições de ensino público',
						value: { pit: false, rit: false }
					},
					{
						text: 'Carga horária semanal de ministração de aulas não remuneradas em cursos de pós-graduação em outras instituições de ensino público',
						value: { pit: false, rit: false }
					},
					{ text: 'Outras atividades de ensino', value: { pit: false, rit: false } }
				].map((atividadeDeEnsino, index) => {
					return {
						...atividadeDeEnsino,
						value: { pit: pit.ensino[index], rit: rit.ensino[index] }
					};
				}),
				chEnsino: {
					text: 'Subtotal outras atividades de ensino',
					value: { pit: pit.chEnsino, rit: rit.chEnsino },
					maximo: false
				},
				pesquisa: [
					{
						text: 'Coordenação de grupos de pesquisa reconhecidos oficialmente pela UFRPE',
						value: { pit: false, rit: false }
					},
					{
						text: 'Colaboração em grupos de pesquisa reconhecidos oficialmente pela UFRPE',
						value: { pit: false, rit: false }
					},
					{
						text: 'Coordenação de projeto de pesquisa com financiamento (exceto bolsa)',
						value: { pit: false, rit: false }
					},
					{
						text: 'Coordenação de projeto de pesquisa sem financiamento',
						value: { pit: false, rit: false }
					},
					{
						text: 'Colaboração em projeto de pesquisa com ou sem financiamento externo',
						value: { pit: false, rit: false }
					},
					{ text: 'Supervisor de estágio pós-doutoral', value: { pit: false, rit: false } },
					{
						text: 'Participação em conselhos editoriais de revistas científicas, técnicas e culturais ou de instituições de capital público ou privadas',
						value: { pit: false, rit: false }
					},
					{
						text: 'Participação em Comirês e Comissões Científicas',
						value: { pit: false, rit: false }
					},
					{ text: 'Editor de periódicos científicos', value: { pit: false, rit: false } },
					{
						text: 'Orientação principal de iniciação científica ou tecnológica (em programas oficiais da UFRPE ou outros órgãos de fomento)',
						value: { pit: false, rit: false }
					},
					{
						text: 'Coorientação de iniciação científica ou tecnológica (em programas oficiais da UFRPE ou outros órgãos de fomento)',
						value: { pit: false, rit: false }
					},
					{ text: 'Revisor de periódico científico', value: { pit: false, rit: false } },
					{
						text: 'Participação como titular em bancas de pós-graduação (sem ser orientador)',
						value: { pit: false, rit: false }
					},
					{
						text: 'Avaliador de programas (PIBIC, CIEPEX, etc.) e eventos técnico-científicos',
						value: { pit: false, rit: false }
					},
					{
						text: 'Desenvolvimento de aplicativos computacionais, registrados ou publicados em livros ou  revistas indexadas',
						value: { pit: false, rit: false }
					},
					{ text: 'Registro de patente', value: { pit: false, rit: false } },
					{
						text: 'Elaboração e submissão para publicação de livro (científico, didático, cultural ou técnico), produção de manual técnico e/ou didático',
						value: { pit: false, rit: false }
					},
					{
						text: 'Elaboração e submissão para publicação de capítulo de livro, artigo científico em revista indexada',
						value: { pit: false, rit: false }
					},
					{
						text: 'Elaboração de parecer em agências de fomento',
						value: { pit: false, rit: false }
					},
					{
						text: 'Revisão e elaboração de parecer Ad-hoc em artigos e projetos',
						value: { pit: false, rit: false }
					},
					{
						text: 'Elaboração e publicação de relatórios técnicos',
						value: { pit: false, rit: false }
					},
					{
						text: 'Tradução de artigos científicos e livros científicos, didáticos, culturais ou técnicos',
						value: { pit: false, rit: false }
					},
					{
						text: 'Editoração, organização e/ou tradução de livro técnico-científico, didático, cultural ou técnico',
						value: { pit: false, rit: false }
					},
					{
						text: 'Produção cientifica em congressos, simpósios, workshops, seminários regionais, nacionais ou internacionais, como primeiro autor ou autor correspondente',
						value: { pit: false, rit: false }
					},
					{
						text: 'Produção científica em periódicos nacionais e/ou internacionais',
						value: { pit: false, rit: false }
					},
					{
						text: 'Planejamento ou organização de eventos acadêmicos-científicos na condição de Coordenador',
						value: { pit: false, rit: false }
					},
					{
						text: 'Planejamento ou organização de eventos acadêmicos-científicos na condição de Colaborador',
						value: { pit: false, rit: false }
					},
					{
						text: 'Formação continuada científica na área de atuação ou afim',
						value: { pit: false, rit: false }
					},
					{ text: 'Outras atividades de pesquisa', value: { pit: false, rit: false } }
				].map((atividadeDePesquisa, index) => {
					return {
						...atividadeDePesquisa,
						value: { pit: pit.pesquisa[index], rit: rit.pesquisa[index] }
					};
				}),
				chPesquisa: {
					text: 'Subtotal das Atividades de Pesquisa',
					value: { pit: pit.chPesquisa, rit: rit.chPesquisa },
					maximo: true
				},
				extensao: [
					{
						text: 'Coordenação de projetos de extensão aprovados oficialmente pela UFRPE',
						value: { pit: false, rit: false }
					},
					{
						text: 'Colaboração em projetos de extensão aprovados oficialmente pela UFRPE',
						value: { pit: false, rit: false }
					},
					{
						text: 'Colaboração em projeto de extensão de outra instituição',
						value: { pit: false, rit: false }
					},
					{
						text: 'Orientação principal de bolsista de projeto de extensão',
						value: { pit: false, rit: false }
					},
					{
						text: 'Coordenação, ministração ou participação em cursos de aperfeiçoamento  ou de outros cursos de curta duração',
						value: { pit: false, rit: false }
					},
					{
						text: 'Planejamento e organização de cursos, palestras, colóquios, simpósios, oficinas, minicursos, entre outros de interesse da instituição e da comunidade, na condição de Coordenador',
						value: { pit: false, rit: false }
					},
					{
						text: 'Planejamento e organização de cursos, palestras, colóquios, simpósios, oficinas, minicursos, entre outros de interesse da instituição e da comunidade, na condição de Colaborador',
						value: { pit: false, rit: false }
					},
					{
						text: 'Desenvolvimento de atividades contínuas de cunho esportivo, artístico e cultural no âmbito interno da instituição',
						value: { pit: false, rit: false }
					},
					{
						text: 'Planejamento e/ou organização de programas de qualificação profissional, programas comunitários de mobilização interna e externa, entre outros de interesse da instituição e da comunidade, na condição de Coordenador.',
						value: { pit: false, rit: false }
					},
					{
						text: 'Planejamento e/ou organização de programas de qualificação profissional, programas Comunitários de mobilização interna e externa, entre outros de interesse da instituição e da comunidade, na condição de Colaborador',
						value: { pit: false, rit: false }
					},
					{
						text: 'Atividades de consultoria, curadoria, assessoria, prestação de serviços, laudos técnicos, desde que não remuneradas, observando-se a legislação vigente, e devidamente autoriza- das pela instituição',
						value: { pit: false, rit: false }
					},
					{ text: 'Tutoria de empresas juniores', value: { pit: false, rit: false } },
					{
						text: 'Participação em bancas de concurso ou de formação acadêmica',
						value: { pit: false, rit: false }
					},
					{ text: 'Elaboração de relatórios de extensão', value: { pit: false, rit: false } },
					{
						text: 'Elaboração de escrita de artigos e capítulo de livros e a atuação como revisor',
						value: { pit: false, rit: false }
					},
					{ text: 'Avaliador de projetos de extensão', value: { pit: false, rit: false } },
					{
						text: 'Formação continuada em extensão na área de atuação ou afim',
						value: { pit: false, rit: false }
					},
					{ text: 'Outras atividades de extensão', value: { pit: false, rit: false } }
				].map((atividadeDeExtensao, index) => {
					return {
						...atividadeDeExtensao,
						value: { pit: pit.extensao[index], rit: rit.extensao[index] }
					};
				}),

				chExtensao: {
					text: 'Subtotal das Atividades de Extensão',
					value: { pit: pit.chExtensao, rit: rit.chExtensao },
					maximo: true
				},
				adm: [
					{
						text: 'Participação em comissões permanentes ou temporárias e colegiados institucionais como titular',
						value: { pit: false, rit: false }
					},
					{
						text: 'Participação em comissões permanentes ou temporárias e colegiados institucionais como suplente',
						value: { pit: false, rit: false }
					},
					{ text: 'Supervisão de área do conhecimento', value: { pit: false, rit: false } },
					{
						text: 'Cargos de coordenação de curso ou coordenação geral de cursos',
						value: { pit: false, rit: false }
					},
					{
						text: 'Cargos de coordenação administrativa (ex: execução de convênios, bases experimentais etc.)',
						value: { pit: false, rit: false }
					},
					{
						text: 'Reitoria, Vice-reitoria, Chefia de Gabinete, Pró-reitorias, Direção Geral e Acadêmica de Unidades Acadêmicas',
						value: { pit: false, rit: false }
					},
					{ text: 'Diretor de Departamento Acadêmico', value: { pit: false, rit: false } },
					{
						text: 'Assessorias e demais cargos de confiança nomeados pela Reitoria',
						value: { pit: false, rit: false }
					},
					{
						text: 'Representação em conselhos, comitês, sindicatos e outras organizações profissionais',
						value: { pit: false, rit: false }
					},
					{
						text: 'Formação continuada administrativa na área de atuação ou afim',
						value: { pit: false, rit: false }
					},
					{ text: 'Outras atividades administrativas', value: { pit: false, rit: false } }
				].map((atividadeAdministrativa, index) => {
					return {
						...atividadeAdministrativa,
						value: { pit: pit.adm[index], rit: rit.adm[index] }
					};
				}),

				chAdm: {
					text: 'Subtotal das Atividades Administrativas',
					value: { pit: pit.chAdm, rit: rit.chAdm },
					maximo: false
				}
			}
		};
	} catch (error) {
		return {
			error: true
		};
	}
}) satisfies PageServerLoad;
