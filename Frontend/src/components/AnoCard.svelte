<script lang="ts">
	import { goto } from '$app/navigation';
	import SimpleModal from './SimpleModal.svelte';

	export let ano: number;
	export let ritCriado: boolean;
	export let userId: string;

	let isErrorModalOpen = false;
	let invalidPitErrorMessage = 'Não podemos gerar o PDF o PIT pois o mesmo não está válido.';

	async function handleGerarPDF() {
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
				on:click={() => handleGerarPDF()}
				class="rounded bg-orange-500 py-1 px-3 transition duration-300 hover:bg-orange-600 active:scale-90"
			>
				Gerar PDF
			</button>
		</div>
	</div>
	<div class="flex w-full flex-col items-center gap-3 p-2.5 sm:flex-row sm:justify-between">
		<span class="text-2xl font-semibold">RIT</span>
		{#if ritCriado}
			<div class="xs:text-base flex gap-3 text-sm font-medium">
				<button
					class="rounded bg-orange-500 py-1 px-3 font-medium transition duration-300 active:scale-90"
				>
					Editar
				</button>
				<button
					class="rounded bg-orange-500 py-1 px-3 font-medium transition duration-300 active:scale-90"
				>
					Gerar PDF
				</button>
			</div>
		{:else}
			<button
				class="rounded bg-orange-500 py-1 px-3 font-medium transition duration-300 hover:bg-orange-600 active:scale-90"
			>
				Criar RIT</button
			>
		{/if}
	</div>
</div>
