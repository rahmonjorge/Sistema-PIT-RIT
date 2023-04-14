<script lang="ts">
	import { goto } from '$app/navigation';
	import type { PageData } from './$types';
	import { onMount } from 'svelte';
	import { page } from '$app/stores';

	let PdfViewer: any;
	export let data: PageData;

	onMount(async () => {
		const module = await import('svelte-pdf');
		PdfViewer = module.default;
	});

	function baixarPDF() {
		const linkSource = `data:application/pdf;base64,${data.pdf}`;
		const downloadLink = document.createElement('a');
		const fileName = `PIT-${$page.params.ano}.pdf`;
		downloadLink.target = '_blank';
		downloadLink.href = linkSource;
		downloadLink.download = fileName;
		downloadLink.click();
	}
</script>

<div class="mx-10 flex justify-between">
	<button
		class="rounded-lg bg-orange-500 px-8 py-4 text-white shadow-sm hover:bg-orange-600"
		on:click={() => goto(`/pit/${$page.params.ano}`)}>Editar</button
	>
	<button
		class="rounded-lg bg-orange-500 px-8 py-4 text-white shadow-sm hover:bg-orange-600"
		on:click={baixarPDF}>Baixar</button
	>
</div>

<svelte:component
	this={PdfViewer}
	class="w-11/12"
	data={Buffer.from(data.pdf)}
	showButtons={['navigation', 'zoom', 'rotate', 'pageInfo']}
	scale={1.8}
/>
