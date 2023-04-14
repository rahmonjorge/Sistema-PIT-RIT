<script lang="ts">
	import type { PageData } from './$types';
	import { page } from '$app/stores';
	import { goto } from '$app/navigation';
	import SimpleModal from '$/components/SimpleModal.svelte';

	export let data: PageData;

	const horasTotais = data.userInfo?.regime === '20h' ? 20 : 40;
	$: totalMinistracaoRit =
		data.planilha?.minAula
			.map((linha) => Number(linha.value.rit))
			.reduce((acc, curr) => {
				return acc + curr;
			}, 0) ?? 0;

	$: totalMinistracaoPit =
		data.planilha?.minAula
			.map((linha) => Number(linha.value.pit))
			.reduce((acc, curr) => {
				return acc + curr;
			}, 0) ?? 0;

	$: finalTotal =
		totalMinistracaoRit +
		Number(data.planilha?.chEnsino.value.rit) +
		Number(data.planilha?.chPesquisa.value.rit) +
		Number(data.planilha?.chExtensao.value.rit) +
		Number(data.planilha?.chAdm.value.rit);

	$: isFinalValid = {
		value: finalTotal === horasTotais,
		text:
			finalTotal > horasTotais
				? `Você ultrapassou o limite de ${horasTotais} horas em ${finalTotal - horasTotais} horas`
				: `Você está com ${horasTotais - finalTotal} horas a menos`
	};

	$: invalidPitErrorMessage = `Não podemos gerar o PDF pois o PIT está inválido.\n${isFinalValid.text}.`;

	let isSuccessModalOpen = false;
	let isErrorModalOpen = false;

	async function handleGerarPDF() {
		await handleSalvar(false);
		const responseValidatePit = await fetch('/api/pit/validar', {
			method: 'POST',
			headers: {
				'Content-Type': 'application/json'
			},
			body: JSON.stringify({
				userId: $page.data.session?.user?.id as string,
				ano: Number($page.params.ano)
			})
		});

		const dataValidatePIT = await responseValidatePit.json();

		if (dataValidatePIT.valid) {
			const responseValidateRit = await fetch('/api/rit/validar', {
				method: 'POST',
				headers: {
					'Content-Type': 'application/json'
				},
				body: JSON.stringify({
					userId: $page.data.session?.user?.id as string,
					ano: Number($page.params.ano)
				})
			});

			const dataValidateRIT = await responseValidateRit.json();

			if (responseValidateRit.ok) {
				goto(`/rit/${$page.params.ano}/pdf`);
			} else {
				invalidPitErrorMessage = `Não podemos gerar o PDF pois o RIT está inválido.\n${dataValidateRIT.errors.join(
					', '
				)}.`;
				isErrorModalOpen = true;
			}
		} else {
			invalidPitErrorMessage = `Não podemos gerar o PDF pois o PIT está inválido.\n${dataValidatePIT.errors.join(
				', '
			)}.`;
			isErrorModalOpen = true;
		}
	}

	async function handleSalvar(showModal: boolean = true) {
		const body = {
			ano: Number($page.params.ano),
			userId: $page.data.session?.user?.id as string,
			sheet: {
				chGrad: Number(data.planilha?.minAula[0].value.rit),
				chPos: Number(data.planilha?.minAula[1].value.rit),
				ensino: data.planilha?.ensino.map((item) => item.value.rit) as boolean[],
				chEnsino: Number(data.planilha?.chEnsino.value.rit),
				pesquisa: data.planilha?.pesquisa.map((item) => item.value.rit) as boolean[],
				chPesquisa: Number(data.planilha?.chPesquisa.value.rit),
				extensao: data.planilha?.extensao.map((item) => item.value.rit) as boolean[],
				chExtensao: Number(data.planilha?.chExtensao.value.rit),
				adm: data.planilha?.adm.map((item) => item.value.rit) as boolean[],
				chAdm: Number(data.planilha?.chAdm.value.rit)
			}
		};

		try {
			const response = await fetch('/api/rit/salvar', {
				method: 'POST',
				headers: {
					'Content-Type': 'application/json'
				},
				body: JSON.stringify(body)
			});

			if (response.ok) {
				if (showModal) isSuccessModalOpen = true;
			}
		} catch (error) {
			console.error(error);
		}
	}
</script>

{#if data.error}
	<h1>Você não possui um RIT para esse ano...</h1>
	<a class="text-blue-400 underline" href="/">Voltar para tela inicial</a>
{:else if data.planilha}
	<SimpleModal
		description="PIT Salvo com sucesso!"
		isOpen={isSuccessModalOpen}
		title="PIT Salvo"
		cancel={{
			text: 'Continuar editando',
			action: () => {
				isSuccessModalOpen = false;
			}
		}}
		confirm={{
			text: 'Voltar para o início',
			action: () => {
				isSuccessModalOpen = false;
				goto('/');
			}
		}}
	/>
	<SimpleModal
		title="PIT Inválido!"
		description={invalidPitErrorMessage}
		isOpen={isErrorModalOpen}
		confirm={{
			text: 'OK',
			action: () => {
				isErrorModalOpen = false;
			}
		}}
		oneButton
	/>
	<div class="mx-auto flex w-full flex-col gap-8 md:w-4/5">
		<h1 class="text-6xl font-bold">RIT {$page.params.ano}</h1>

		<div class="flex flex-col gap-6">
			<h2 class="text-5xl font-bold">MINISTRAÇÃO DE AULA</h2>
			<div class="flex w-full flex-col items-start gap-4">
				<table class="h-full w-full border-collapse">
					<thead>
						<tr>
							<th />
							<th class="border border-black p-2">PIT</th>
							<th class="border border-black p-2">RIT</th>
						</tr>
					</thead>
					<tbody>
						{#each data.planilha.minAula as linha}
							<tr>
								<td class="h-full border border-black p-2">
									<span>
										{linha.text}
									</span>
								</td>
								<td
									class="h-full border border-black bg-orange-100 outline outline-transparent focus-within:outline-2 focus-within:outline-black {linha
										.value.pit < linha.minimo
										? 'bg-red-100 text-red-600'
										: ''}"
								>
									<span
										class="flex h-full max-w-[6.5rem] flex-col items-center justify-center px-4 py-2"
									>
										<span class="w-full bg-transparent text-center">
											{linha.value.pit}
										</span>
										{#if linha.minimo > -1}
											<span
												class="text-xs {linha.value.rit < linha.minimo
													? 'font-medium text-red-500'
													: ''}">mínimo {linha.minimo}h</span
											>
										{/if}
									</span>
								</td>
								<td
									class="h-full border border-black bg-orange-100 outline outline-transparent focus-within:outline-2 focus-within:outline-black {linha
										.value.rit < linha.minimo
										? 'bg-red-100 text-red-600'
										: ''}"
								>
									<label
										class="flex h-full max-w-[6.5rem] flex-col items-center justify-center px-4 py-2"
									>
										<input
											class="h-full w-full bg-transparent text-center text-xs outline-none"
											type="text"
											inputmode="numeric"
											pattern="[0-9]*"
											bind:value={linha.value.rit}
										/>
										{#if linha.minimo > -1}
											<span
												class="text-xs {linha.value.rit < linha.minimo
													? 'font-medium text-red-500'
													: ''}">mínimo {linha.minimo}h</span
											>
										{/if}
									</label>
								</td>
							</tr>
						{/each}
						<tr>
							<td class="p-1 text-right">
								<span class="text-xl font-semibold"> Subtotal (Ministração de Aula) </span>
							</td>
							<td class="relative p-1 text-center">
								<span
									class=" text-xl font-medium {totalMinistracaoPit >
										data.planilha.chMinAula.maximo ||
									totalMinistracaoPit < data.planilha.chMinAula.minimo
										? 'text-red-500'
										: ''}"
								>
									{totalMinistracaoPit.toFixed(1)}
								</span>
								{#if totalMinistracaoPit > data.planilha.chMinAula.maximo || totalMinistracaoPit < data.planilha.chMinAula.minimo}
									<span
										class="absolute top-8 right-1/2 w-full translate-x-1/2 text-xs text-red-500"
									>
										{#if data.planilha.chMinAula.minimo !== -1}
											Entre {data.planilha.chMinAula.minimo} e {data.planilha.chMinAula.maximo} horas
										{:else}
											Até {data.planilha.chMinAula.maximo} horas
										{/if}
									</span>
								{/if}
							</td>
							<td class="relative p-1 text-center">
								<span
									class=" text-xl font-medium {totalMinistracaoRit >
										data.planilha.chMinAula.maximo ||
									totalMinistracaoRit < data.planilha.chMinAula.minimo
										? 'text-red-500'
										: ''}"
								>
									{totalMinistracaoRit.toFixed(1)}
								</span>
								{#if totalMinistracaoRit > data.planilha.chMinAula.maximo || totalMinistracaoRit < data.planilha.chMinAula.minimo}
									<span
										class="absolute top-8 right-1/2 w-full translate-x-1/2 text-xs text-red-500"
									>
										{#if data.planilha.chMinAula.minimo !== -1}
											Entre {data.planilha.chMinAula.minimo} e {data.planilha.chMinAula.maximo} horas
										{:else}
											Até {data.planilha.chMinAula.maximo} horas
										{/if}
									</span>
								{/if}
							</td>
						</tr>
					</tbody>
				</table>
			</div>
		</div>

		<h2 class="text-5xl font-bold">OUTRAS ATIVIDADES DE ENSINO</h2>
		<div>
			<table class="h-full w-full table-auto">
				<thead>
					<tr>
						<th />
						<th class="border border-black p-2">PIT</th>
						<th class="border border-black p-2">RIT</th>
					</tr>
				</thead>
				<tbody>
					{#each data.planilha.ensino as linha}
						<tr>
							<td class="h-full border border-black p-2">
								<span>
									{linha.text}
								</span>
							</td>
							<td class="h-full border border-black bg-orange-100">
								<label class="flex h-full items-center justify-center px-4 py-2">
									<input type="checkbox" disabled bind:checked={linha.value.pit} class="" />
								</label>
							</td>
							<td class="h-full border border-black bg-orange-100">
								<label class="flex h-full items-center justify-center px-4 py-2">
									<input type="checkbox" bind:checked={linha.value.rit} />
								</label>
							</td>
						</tr>
					{/each}
					<tr class="h-full">
						<td class="p-1 text-right">
							<label for="sub-ensino" class="text-xl font-semibold">
								Subtotal (Outras atividades de ensino)
							</label>
						</td>
						<td class="border border-black bg-neutral-200">
							<span
								class="flex w-full max-w-[6.5rem] items-center justify-center bg-transparent text-center text-xl font-medium"
							>
								{data.planilha.chEnsino.value.pit}
							</span>
						</td>

						<td
							class="border border-black outline outline-transparent focus-within:outline-2 focus-within:outline-black"
						>
							<input
								id="sub-ensino"
								class="h-full w-full max-w-[6.5rem] bg-transparent text-center text-xl font-medium outline-none"
								bind:value={data.planilha.chEnsino.value.rit}
							/>
						</td>
					</tr>
				</tbody>
			</table>
		</div>
		<h2 class="text-5xl font-bold">ATIVIDADES DE PESQUISA</h2>
		<div>
			<table class="h-full w-full table-auto">
				<thead>
					<tr>
						<th />
						<th class="border border-black p-2">PIT</th>
						<th class="border border-black p-2">RIT</th>
					</tr>
				</thead>
				<tbody>
					{#each data.planilha.pesquisa as linha}
						<tr>
							<td class="border border-black p-2">
								<span>
									{linha.text}
								</span>
							</td>
							<td class="h-full border border-black bg-orange-100">
								<label class="flex h-full items-center justify-center px-4 py-2">
									<input type="checkbox" disabled bind:checked={linha.value.pit} class="" />
								</label>
							</td>
							<td class="h-full border border-black bg-orange-100">
								<label class="flex h-full items-center justify-center px-4 py-2">
									<input type="checkbox" bind:checked={linha.value.rit} />
								</label>
							</td>
						</tr>
					{/each}
					<tr class="h-full">
						<td class="p-1 text-right">
							<label for="sub-pesquisa" class="text-xl font-semibold"> Subtotal (Pesquisa) </label>
						</td>
						<td
							class="relative border border-black bg-neutral-200 {data.planilha.chPesquisa.value
								.rit > 20
								? 'bg-red-100 text-red-600'
								: ''}"
						>
							<span
								class="flex w-full max-w-[6.5rem] items-center justify-center bg-transparent text-center text-xl font-medium"
							>
								{data.planilha.chPesquisa.value.pit}
							</span>
							<span
								class="absolute top-10 right-1/2 w-full translate-x-1/2 text-xs {data.planilha
									.chPesquisa.value.rit > 20
									? 'text-red-500'
									: ''}"
							>
								Até 20 horas
							</span>
						</td>
						<td
							class="relative border border-black outline outline-transparent focus-within:outline-2 focus-within:outline-black"
						>
							<input
								id="sub-pesquisa"
								class="h-full w-full max-w-[6.5rem] bg-transparent text-center text-xl font-medium outline-none {data
									.planilha.chPesquisa.value.rit > 20
									? 'text-red-600'
									: ''}"
								bind:value={data.planilha.chPesquisa.value.rit}
							/>

							<span
								class="absolute top-10 right-1/2 w-full translate-x-1/2 text-xs {data.planilha
									.chPesquisa.value.rit > 20
									? 'text-red-500'
									: ''}"
							>
								Até 20 horas
							</span>
						</td>
					</tr>
				</tbody>
			</table>
		</div>
		<h2 class="text-5xl font-bold">ATIVIDADES DE EXTENSÃO</h2>
		<div>
			<table class="h-full w-full table-auto">
				<thead>
					<tr>
						<th />
						<th class="border border-black p-2">PIT</th>
						<th class="border border-black p-2">RIT</th>
					</tr>
				</thead>
				<tbody>
					{#each data.planilha.extensao as linha}
						<tr>
							<td class="border border-black p-2">
								<span>
									{linha.text}
								</span>
							</td>
							<td class="h-full border border-black bg-orange-100">
								<label class="flex h-full items-center justify-center px-4 py-2">
									<input type="checkbox" disabled bind:checked={linha.value.pit} class="" />
								</label>
							</td>
							<td class="h-full border border-black bg-orange-100">
								<label class="flex h-full items-center justify-center px-4 py-2">
									<input type="checkbox" bind:checked={linha.value.rit} />
								</label>
							</td>
						</tr>
					{/each}
					<tr class="h-full">
						<td class="p-1 text-right">
							<label for="sub-extensao" class="text-xl font-semibold"> Subtotal (Extensão) </label>
						</td>
						<td
							class="relative border border-black bg-neutral-200 {data.planilha.chExtensao.value
								.rit > 20
								? 'bg-red-100 text-red-600'
								: ''}"
						>
							<span
								class="flex w-full max-w-[6.5rem] items-center justify-center bg-transparent text-center text-xl font-medium"
							>
								{data.planilha.chExtensao.value.pit}
							</span>
							<span
								class="absolute top-10 right-1/2 w-full translate-x-1/2 text-xs {data.planilha
									.chExtensao.value.rit > 20
									? 'text-red-500'
									: ''}"
							>
								Até 20 horas
							</span>
						</td>
						<td
							class="relative border border-black outline outline-transparent focus-within:outline-2 focus-within:outline-black"
						>
							<input
								id="sub-pesquisa"
								class="h-full w-full max-w-[6.5rem] bg-transparent text-center text-xl font-medium outline-none {data
									.planilha.chExtensao.value.rit > 20
									? 'text-red-600'
									: ''}"
								bind:value={data.planilha.chExtensao.value.rit}
							/>

							<span
								class="absolute top-10 right-1/2 w-full translate-x-1/2 text-xs {data.planilha
									.chExtensao.value.rit > 20
									? 'text-red-500'
									: ''}"
							>
								Até 20 horas
							</span>
						</td>
					</tr>
				</tbody>
			</table>
		</div>
		<h2 class="text-5xl font-bold">ATIVIDADES ADMINISTRATIVAS</h2>
		<div>
			<table class="h-full w-full table-auto">
				<thead>
					<tr>
						<th />
						<th class="border border-black p-2">PIT</th>
						<th class="border border-black p-2">RIT</th>
					</tr>
				</thead>
				<tbody>
					{#each data.planilha.adm as linha}
						<tr>
							<td class="border border-black p-2">
								<span>
									{linha.text}
								</span>
							</td>
							<td class="h-full border border-black bg-orange-100">
								<label class="flex h-full items-center justify-center px-4 py-2">
									<input type="checkbox" disabled bind:checked={linha.value.pit} class="" />
								</label>
							</td>
							<td class="h-full border border-black bg-orange-100">
								<label class="flex h-full items-center justify-center px-4 py-2">
									<input type="checkbox" bind:checked={linha.value.rit} />
								</label>
							</td>
						</tr>
					{/each}
					<tr class="h-full">
						<td class="p-1 text-right">
							<label for="sub-adm" class="text-xl font-semibold"> Subtotal (Administrativa) </label>
						</td>
						<td class="border border-black bg-neutral-200">
							<span
								class="flex w-full max-w-[6.5rem] items-center justify-center bg-transparent text-center text-xl font-medium"
							>
								{data.planilha.chAdm.value.pit}
							</span>
						</td>
						<td class="border border-black ring-black focus-within:ring-2">
							<input
								id="sub-adm"
								class="h-full w-full max-w-[6.5rem] bg-transparent text-center text-xl font-medium outline-none"
								bind:value={data.planilha.chAdm.value.rit}
							/>
						</td>
					</tr>
				</tbody>
			</table>
		</div>
		<div class="ml-auto flex flex-col items-end">
			<span class="flex gap-4 text-2xl font-bold">
				Total <span class="{!isFinalValid.value ? 'text-red-500' : ''} font-medium"
					>{finalTotal} horas</span
				>
			</span>

			{#if !isFinalValid.value}
				<span class="text-red-500">{isFinalValid.text}</span>
			{/if}
		</div>
		<div class="flex w-full justify-between">
			<button
				type="button"
				class="rounded-lg bg-orange-500 px-12 py-5 text-2xl font-semibold text-white shadow-sm transition-colors duration-300 ease-in-out hover:bg-orange-600"
				on:click={() => handleSalvar()}>Salvar</button
			>

			<button
				type="button"
				class="rounded-lg bg-orange-500 px-12 py-5 text-2xl font-semibold text-white shadow-sm transition-colors duration-300 ease-in-out hover:bg-orange-600"
				on:click={handleGerarPDF}>Gerar PDF</button
			>
		</div>
	</div>
{/if}
