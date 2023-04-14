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

		const response = await GRPCClient.database.gui.pit.getPit({
			ano,
			userId: user.id
		});

		const pit = response.response;

		return {
			userInfo: serializeNonPOJOs(userInfo),
			planilha: {
				minAula: [
					{
						text: 'Carga horária semanal de ministração de aulas teóricas, práticas, de laboratório ou de campo em cursos de graduação',
						value: pit.chGrad,
						minimo: userInfo.reducao === 'Sim (Art. 9º)' ? -1 : 4
					},
					{
						text: 'Carga horária semanal de ministração de aulas teóricas, práticas, de laboratório ou de campo em programas de pós-graduação',
						value: pit.chPos,
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
						value: true
					},
					{
						text: 'Participação no planejamento, na organização, na execução e na avaliação referentes ao ensino oferecido pela UFRPE (reuniões pedagógicas, reuniões de coordenação e reuniões de gestão)',
						value: true
					},
					{
						text: 'Orientação e/ou supervisão principal de alunos de graduação (programas de estímulo à docência, monografia, bolsa permanência, monitoria, tutorial de 1º ano, cooperação internacional, PAVI, TCC, ESO, ACC, incluindo estágios não obrigatórios), Turoria PET',
						value: false
					},
					{ text: 'Tutoria ou preceptoria de Programas de Residência (PR)', value: false },
					{
						text: 'Orientação principal de alunos de pós-graduação (Stricto sensu e Lato sensu)',
						value: true
					},
					{
						text: 'Co-orientação de alunos de pós-graduação (Stricto sensu e Lato sensu)',
						value: true
					},
					{
						text: 'Coordenação de núcleos e grupos de estudos institucionalizados em CTA',
						value: true
					},
					{ text: 'Coordenação de projeto de ensino com financiamento externo', value: false },
					{ text: 'Coordenação de projeto de ensino sem financiamento externo', value: true },
					{
						text: 'Colaboração em projeto de ensino com ou sem financiamento externo',
						value: true
					},
					{ text: 'Supervisão/orientação de residência multiprofissional', value: false },
					{
						text: 'Participação como membro titular de banca de trabalhos de conclusão de curso',
						value: true
					},
					{ text: 'Formação continuada em docência na área de atuação ou afim', value: true },
					{
						text: 'Carga horária semanal de ministração de aulas não remuneradas em cursos de graduação em outras instituições de ensino público',
						value: false
					},
					{
						text: 'Carga horária semanal de ministração de aulas não remuneradas em cursos de pós-graduação em outras instituições de ensino público',
						value: false
					},
					{ text: 'Outras atividades de ensino', value: false }
				].map((atividadeDeEnsino, index) => {
					return {
						...atividadeDeEnsino,
						value: pit.ensino[index]
					};
				}),
				chEnsino: {
					text: 'Subtotal outras atividades de ensino',
					value: pit.chEnsino,
					maximo: false
				},
				pesquisa: [
					{
						text: 'Coordenação de grupos de pesquisa reconhecidos oficialmente pela UFRPE',
						value: true
					},
					{
						text: 'Colaboração em grupos de pesquisa reconhecidos oficialmente pela UFRPE',
						value: false
					},
					{
						text: 'Coordenação de projeto de pesquisa com financiamento (exceto bolsa)',
						value: true
					},
					{ text: 'Coordenação de projeto de pesquisa sem financiamento', value: true },
					{
						text: 'Colaboração em projeto de pesquisa com ou sem financiamento externo',
						value: true
					},
					{ text: 'Supervisor de estágio pós-doutoral', value: true },
					{
						text: 'Participação em conselhos editoriais de revistas científicas, técnicas e culturais ou de instituições de capital público ou privadas',
						value: false
					},
					{ text: 'Participação em Comirês e Comissões Científicas', value: false },
					{ text: 'Editor de periódicos científicos', value: true },
					{
						text: 'Orientação principal de iniciação científica ou tecnológica (em programas oficiais da UFRPE ou outros órgãos de fomento)',
						value: true
					},
					{
						text: 'Coorientação de iniciação científica ou tecnológica (em programas oficiais da UFRPE ou outros órgãos de fomento)',
						value: true
					},
					{ text: 'Revisor de periódico científico', value: true },
					{
						text: 'Participação como titular em bancas de pós-graduação (sem ser orientador)',
						value: false
					},
					{
						text: 'Avaliador de programas (PIBIC, CIEPEX, etc.) e eventos técnico-científicos',
						value: true
					},
					{
						text: 'Desenvolvimento de aplicativos computacionais, registrados ou publicados em livros ou  revistas indexadas',
						value: true
					},
					{ text: 'Registro de patente', value: true },
					{
						text: 'Elaboração e submissão para publicação de livro (científico, didático, cultural ou técnico), produção de manual técnico e/ou didático',
						value: false
					},
					{
						text: 'Elaboração e submissão para publicação de capítulo de livro, artigo científico em revista indexada',
						value: false
					},
					{ text: 'Elaboração de parecer em agências de fomento', value: false },
					{ text: 'Revisão e elaboração de parecer Ad-hoc em artigos e projetos', value: false },
					{ text: 'Elaboração e publicação de relatórios técnicos', value: true },
					{
						text: 'Tradução de artigos científicos e livros científicos, didáticos, culturais ou técnicos',
						value: false
					},
					{
						text: 'Editoração, organização e/ou tradução de livro técnico-científico, didático, cultural ou técnico',
						value: true
					},
					{
						text: 'Produção cientifica em congressos, simpósios, workshops, seminários regionais, nacionais ou internacionais, como primeiro autor ou autor correspondente',
						value: false
					},
					{ text: 'Produção científica em periódicos nacionais e/ou internacionais', value: true },
					{
						text: 'Planejamento ou organização de eventos acadêmicos-científicos na condição de Coordenador',
						value: false
					},
					{
						text: 'Planejamento ou organização de eventos acadêmicos-científicos na condição de Colaborador',
						value: false
					},
					{ text: 'Formação continuada científica na área de atuação ou afim', value: true },
					{ text: 'Outras atividades de pesquisa', value: false }
				].map((atividadeDePesquisa, index) => {
					return {
						...atividadeDePesquisa,
						value: pit.pesquisa[index]
					};
				}),
				chPesquisa: {
					text: 'Subtotal das Atividades de Pesquisa',
					value: pit.chPesquisa,
					maximo: true
				},
				extensao: [
					{
						text: 'Coordenação de projetos de extensão aprovados oficialmente pela UFRPE',
						value: false
					},
					{
						text: 'Colaboração em projetos de extensão aprovados oficialmente pela UFRPE',
						value: false
					},
					{ text: 'Colaboração em projeto de extensão de outra instituição', value: true },
					{ text: 'Orientação principal de bolsista de projeto de extensão', value: true },
					{
						text: 'Coordenação, ministração ou participação em cursos de aperfeiçoamento  ou de outros cursos de curta duração',
						value: false
					},
					{
						text: 'Planejamento e organização de cursos, palestras, colóquios, simpósios, oficinas, minicursos, entre outros de interesse da instituição e da comunidade, na condição de Coordenador',
						value: false
					},
					{
						text: 'Planejamento e organização de cursos, palestras, colóquios, simpósios, oficinas, minicursos, entre outros de interesse da instituição e da comunidade, na condição de Colaborador',
						value: true
					},
					{
						text: 'Desenvolvimento de atividades contínuas de cunho esportivo, artístico e cultural no âmbito interno da instituição',
						value: true
					},
					{
						text: 'Planejamento e/ou organização de programas de qualificação profissional, programas comunitários de mobilização interna e externa, entre outros de interesse da instituição e da comunidade, na condição de Coordenador.',
						value: false
					},
					{
						text: 'Planejamento e/ou organização de programas de qualificação profissional, programas Comunitários de mobilização interna e externa, entre outros de interesse da instituição e da comunidade, na condição de Colaborador',
						value: false
					},
					{
						text: 'Atividades de consultoria, curadoria, assessoria, prestação de serviços, laudos técnicos, desde que não remuneradas, observando-se a legislação vigente, e devidamente autoriza- das pela instituição',
						value: false
					},
					{ text: 'Tutoria de empresas juniores', value: false },
					{ text: 'Participação em bancas de concurso ou de formação acadêmica', value: false },
					{ text: 'Elaboração de relatórios de extensão', value: false },
					{
						text: 'Elaboração de escrita de artigos e capítulo de livros e a atuação como revisor',
						value: false
					},
					{ text: 'Avaliador de projetos de extensão', value: false },
					{ text: 'Formação continuada em extensão na área de atuação ou afim', value: true },
					{ text: 'Outras atividades de extensão', value: false }
				].map((atividadeDeExtensao, index) => {
					return {
						...atividadeDeExtensao,
						value: pit.extensao[index]
					};
				}),

				chExtensao: {
					text: 'Subtotal das Atividades de Extensão',
					value: pit.chExtensao,
					maximo: true
				},
				adm: [
					{
						text: 'Participação em comissões permanentes ou temporárias e colegiados institucionais como titular',
						value: true
					},
					{
						text: 'Participação em comissões permanentes ou temporárias e colegiados institucionais como suplente',
						value: false
					},
					{ text: 'Supervisão de área do conhecimento', value: false },
					{ text: 'Cargos de coordenação de curso ou coordenação geral de cursos', value: false },
					{
						text: 'Cargos de coordenação administrativa (ex: execução de convênios, bases experimentais etc.)',
						value: false
					},
					{
						text: 'Reitoria, Vice-reitoria, Chefia de Gabinete, Pró-reitorias, Direção Geral e Acadêmica de Unidades Acadêmicas',
						value: false
					},
					{ text: 'Diretor de Departamento Acadêmico', value: false },
					{ text: 'Assessorias e demais cargos de confiança nomeados pela Reitoria', value: true },
					{
						text: 'Representação em conselhos, comitês, sindicatos e outras organizações profissionais',
						value: true
					},
					{ text: 'Formação continuada administrativa na área de atuação ou afim', value: true },
					{ text: 'Outras atividades administrativas', value: false }
				].map((atividadeAdministrativa, index) => {
					return {
						...atividadeAdministrativa,
						value: pit.adm[index]
					};
				}),

				chAdm: {
					text: 'Subtotal das Atividades Administrativas',
					value: pit.chAdm,
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
