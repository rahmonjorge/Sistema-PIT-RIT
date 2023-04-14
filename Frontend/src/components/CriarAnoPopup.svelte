<script lang="ts">
	import { goto } from '$app/navigation';
	import { page } from '$app/stores';
	import { Dialog, DialogOverlay, DialogTitle } from '@rgossiaux/svelte-headlessui';
	import Input from './Input.svelte';
	export let isOpen: boolean;
	const anoPattern = '[0-9]{4}';

	let validationError = {
		error: false,
		message: ''
	};

	const handleCriar = async (event: SubmitEvent) => {
		const formData = new FormData(event.target as HTMLFormElement);

		if (!formData.get('ano')) {
			validationError = {
				error: true,
				message: 'O campo ano é obrigatório'
			};
			return;
		}

		const ano = Number(formData.get('ano'));
		const currentYear = new Date().getFullYear();

		if (ano < 2021 || ano > currentYear + 2) {
			validationError = {
				error: true,
				message: `O ano deve estar entre 2021 e ${currentYear + 2}`
			};
			return;
		}

		try {
			const response = await fetch('/api/pit/criar', {
				method: 'POST',
				headers: {
					'Content-Type': 'application/json'
				},
				body: JSON.stringify({ ano: formData.get('ano'), userId: $page.data.session?.user?.id })
			});

			if (response.ok) {
				goto('/pit/' + formData.get('ano'));
			}

			if (response.status === 409) {
				validationError = {
					error: true,
					message: 'Já existe um PIT para esse ano'
				};
			}
		} catch (error) {
			validationError = {
				error: true,
				message: error as string
			};
		}
	};
</script>

<Dialog
	open={isOpen}
	on:close={() => (isOpen = false)}
	class="flex flex-col gap-8 bg-white px-16 py-8"
>
	<DialogOverlay class="fixed inset-0 z-10 flex h-screen w-screen bg-black/60">
		<form
			class="m-auto flex flex-col items-center gap-8 rounded-lg bg-white px-16 py-8"
			on:submit|preventDefault={handleCriar}
		>
			<DialogTitle class="text-5xl font-bold">Criar PIT</DialogTitle>
			<div class="space-y-3">
				<Input
					label="Ano"
					placeholder="Digite o ano..."
					name="ano"
					required
					inputmode="numeric"
					pattern={anoPattern}
					input
					noLabel
				/>

				{#if validationError.error}
					<div class="mr-auto text-sm text-red-500">{validationError.message}</div>
				{/if}
			</div>
			<div class="flex w-full justify-between">
				<button
					type="button"
					on:click={() => {
						isOpen = false;
						validationError = {
							error: false,
							message: ''
						};
					}}
					class="rounded-lg bg-red-400 px-8 py-4 font-semibold text-white shadow-sm transition duration-300 hover:bg-red-500 active:scale-90"
					>Cancelar</button
				>
				<button
					type="submit"
					class="rounded-lg bg-orange-500 px-8 py-4 font-semibold text-white shadow-sm transition duration-300 hover:bg-orange-600 active:scale-90"
					>Criar PIT</button
				>
			</div>
		</form>
	</DialogOverlay>
</Dialog>
