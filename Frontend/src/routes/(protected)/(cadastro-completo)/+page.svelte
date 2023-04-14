<script lang="ts">
	import AnoCard from '$/components/AnoCard.svelte';
	import { IconPlus } from '@tabler/icons-svelte';
	import type { PageData } from './$types';
	import CriarAnoPopup from '$/components/CriarAnoPopup.svelte';

	export let data: PageData;

	let isOpen: boolean = false;

	$: anos = data.anos?.sort((a, b) => a.ano - b.ano);
</script>

<div class="grid grid-cols-2 gap-4 md:grid-cols-2 lg:grid-cols-3 2xl:grid-cols-4">
	{#if data.session?.user}
		<CriarAnoPopup bind:isOpen userId={data.session?.user?.id} />
	{/if}
	{#if anos}
		{#if anos.length === 0}
			<div class="flex flex-col gap-4">
				<span
					>Você não possui nenhum pit ainda, crie o seu primeiro pit clicando no botão abaixo</span
				>
				<button
					on:click={() => (isOpen = true)}
					class="xs:py-10 w-full rounded-lg border-2 border-dashed border-red-500 py-16 text-red-500 transition-transform active:scale-90"
				>
					<IconPlus class="inline" size={100} />
				</button>
			</div>
		{:else}
			{#each anos as ano}
				<AnoCard ano={ano.ano} ritCriado={ano.rit} userId={data.session?.user?.id ?? ''} />
			{/each}
			<button
				on:click={() => (isOpen = true)}
				class="xs:py-10 w-full rounded-lg border-2 border-dashed border-red-500 py-16 text-red-500 transition-transform active:scale-90"
			>
				<IconPlus class="inline" size={100} />
			</button>
		{/if}
	{/if}
</div>
