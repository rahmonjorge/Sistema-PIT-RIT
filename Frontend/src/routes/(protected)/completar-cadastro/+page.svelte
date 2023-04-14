<script lang="ts">
	import { page } from '$app/stores';
	import { signOut } from '@auth/sveltekit/client';
	import type { ActionData, PageData } from './$types';
	import Select from '$/components/Select.svelte';
	import { superForm } from 'sveltekit-superforms/client';
	import { onMount } from 'svelte';
	import Input from '$/components/Input.svelte';
	import { goto } from '$app/navigation';

	export let data: PageData;
	export let actionData: ActionData;

	const vinculos: string[] = ['Efetivo', 'Substituto'];
	const regimes: string[] = ['20h', '40h com DE', '40h sem DE'];
	const reducoes: string[] = ['Não', 'Sim (Art. 9º)', 'Sim (Art. 10º)'];

	const { form, errors, enhance } = superForm(data.form);

	onMount(() => {
		$form.name = $page.data.session?.user?.name ?? '';
	});

	$: if (actionData?.success) {
		goto('/');
	}
</script>

<h1 class="mt-24 text-center text-6xl font-bold">Sistema PIT-RIT</h1>
<h2 class="mt-2 text-center text-xl">
	Crie seus <span class="font-semibold">PIT's</span> e <span class="font-semibold">RIT's</span> de uma
	forma simplificada
</h2>

<form
	use:enhance
	method="POST"
	class="mx-auto mt-10 flex w-11/12 max-w-3xl flex-col items-center gap-5 rounded-2xl p-9 pt-6"
>
	<div class="flex w-full flex-col gap-2 ">
		<Input
			bind:value={$form.name}
			label="Nome completo"
			placeholder="Digite o seu nome..."
			name="name"
			required
		/>

		{#if $errors.name}
			<span class="text-red-400">{$errors.name}</span>
		{/if}
	</div>

	<div class="flex w-full flex-col gap-2 ">
		<Input
			label="Matrícula SIAPE"
			placeholder="0000000"
			name="siape"
			bind:value={$form.siape}
			required
		/>
		{#if $errors.siape}
			<span class="text-red-400">{$errors.siape}</span>
		{/if}
	</div>
	<div class="flex w-full flex-col gap-2 ">
		<Input
			label="Departamento/Unidade Acadêmica"
			placeholder="Digite o Departamento ou a Unidade acadêmica..."
			name="dpto"
			bind:value={$form.dpto}
			required
		/>

		{#if $errors.dpto}
			<span class="text-red-400">{$errors.dpto}</span>
		{/if}
	</div>

	<div class="flex w-full flex-col gap-2">
		<Select
			options={vinculos}
			bind:selectedOption={$form.vinculo}
			label="Tipo de vínculo"
			placeholder="Selecione o tipo de vínculo"
			name="vinculo"
		/>
		{#if $errors.vinculo}
			<span class="text-red-400">{$errors.vinculo}</span>
		{/if}
	</div>
	<div class="flex w-full flex-col gap-2">
		<Select
			options={regimes}
			bind:selectedOption={$form.regime}
			label="Regime de trabalho"
			placeholder="Selecione o regime de trabalho"
			name="regime"
		/>
		{#if $errors.regime}
			<span class="text-red-400">{$errors.regime}</span>
		{/if}
	</div>
	<div class="flex w-full flex-col gap-2">
		<Select
			options={reducoes}
			bind:selectedOption={$form.reducao}
			label="Exerce Função com Redução de CH de Ensino"
			placeholder="Selecione a opção"
			name="reducao"
		/>
		{#if $errors.reducao}
			<span class="text-red-400">{$errors.reducao}</span>
		{/if}
	</div>
	<div class="mt-7 flex w-full justify-between">
		<button
			type="button"
			class="rounded-lg bg-red-400 px-12 py-5 text-2xl font-bold text-white shadow-sm transition-colors duration-300 ease-in-out hover:bg-red-500"
			on:click|once={() => signOut()}>Cancelar</button
		>

		<button
			type="submit"
			class="rounded-lg bg-orange-500 px-12 py-5 text-2xl font-bold text-white shadow-sm transition-colors duration-300 ease-in-out hover:bg-orange-600"
			>Continuar</button
		>
	</div>
</form>
