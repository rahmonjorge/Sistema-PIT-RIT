<script lang="ts">
	import { goto } from '$app/navigation';
	import SimpleModal from './SimpleModal.svelte';

	export let ano: number;
	export let ritCriado: boolean;
	export let userId: string;

	let isErrorModalOpen = false;
	let invalidPitErrorMessage = 'Não podemos gerar o PDF o PIT pois o mesmo não está válido.';

	async function handleGerarRitPDF() {
		const responseValidatePit = await fetch('/api/pit/validar', {
			method: 'POST',
			headers: {
				'Content-Type': 'application/json'
			},
			body: JSON.stringify({
				userId: userId,
				ano: ano
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
					userId: userId,
					ano: ano
				})
			});

			const dataValidateRIT = await responseValidateRit.json();

			if (responseValidateRit.ok) {
				goto(`/rit/${ano}/pdf`);
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

	async function handleGerarPitPDF() {
		try {
			const response = await fetch('/api/pit/validar', {
				method: 'POST',
				headers: {
					'Content-Type': 'application/json'
				},
				body: JSON.stringify({
					userId,
					ano
				})
			});

			const data = await response.json();

			if (data.valid) {
				goto(`/pit/${ano}/pdf`);
			} else {
				invalidPitErrorMessage = `Não podemos gerar o PDF do PIT pois o mesmo não está válido. ${data.errors.join(
					', '
				)}`;
				isErrorModalOpen = true;
			}
		} catch (error) {
			console.error(error);
		}
	}

	const handleCriarRit = async () => {
		try {
			const response = await fetch('/api/rit/criar', {
				method: 'POST',
				headers: {
					'Content-Type': 'application/json'
				},
				body: JSON.stringify({ ano: ano, userId: userId })
			});

			if (response.ok) {
				goto('/rit/' + ano);
			}
		} catch (error) {
			console.error(error);
		}
	};
</script>

<div class="flex flex-col items-center gap-3 rounded-lg bg-blue-600 p-3 text-white shadow-md">
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
	<span class="text-4xl font-bold">{ano}</span>
	<div class="flex w-full flex-col items-center gap-3 p-2.5 sm:flex-row sm:justify-between">
		<span class="text-2xl font-semibold">PIT</span>
		<div class="flex gap-3 text-sm font-medium sm:text-base">
			<a
				href="/pit/{ano}"
				class="rounded bg-orange-500 py-1 px-3 transition duration-300 hover:bg-orange-600 active:scale-90"
			>
				Editar
			</a>
			<button
				on:click={() => handleGerarPitPDF()}
				class="rounded bg-orange-500 py-1 px-3 transition duration-300 hover:bg-orange-600 active:scale-90"
			>
				Gerar PDF
			</button>
		</div>
	</div>
	<div class="flex w-full flex-col items-center gap-3 p-2.5 sm:flex-row sm:justify-between">
		<span class="text-2xl font-semibold">RIT</span>
		{#if ritCriado}
			<div class="flex gap-3 text-sm font-medium xs:text-base">
				<a
					href="/rit/{ano}"
					class="rounded bg-orange-500 py-1 px-3 font-medium transition duration-300 active:scale-90"
				>
					Editar
				</a>
				<button
					class="rounded bg-orange-500 py-1 px-3 font-medium transition duration-300 active:scale-90"
					on:click={() => handleGerarRitPDF()}
				>
					Gerar PDF
				</button>
			</div>
		{:else}
			<button
				class="rounded bg-orange-500 py-1 px-3 font-medium transition duration-300 hover:bg-orange-600 active:scale-90"
				on:click={() => handleCriarRit()}
			>
				Criar RIT</button
			>
		{/if}
	</div>
</div>
