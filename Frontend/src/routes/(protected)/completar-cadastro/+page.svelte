<script lang="ts">
	import Input from '$/components/Input.svelte';
	import { page } from '$app/stores';

	const vinculos: { value: string; label: string }[] = [
		{
			value: '',
			label: 'Escolha o tipo de vínculo'
		},
		{
			value: '1',
			label: 'Efetivo'
		},
		{
			value: '2',
			label: 'Substituto'
		}
	];

	const regimes: { value: string; label: string }[] = [
		{
			value: '',
			label: 'Escolha o regime de trabalho'
		},
		{
			value: '1',
			label: '20h'
		},
		{
			value: '2',
			label: '40h com DE'
		},
		{
			value: '2',
			label: '40h sem DE'
		}
	];

	const reducoes: { value: string; label: string }[] = [
		{
			value: '1',
			label: 'Não'
		},
		{
			value: '2',
			label: 'Sim (Art. 9º)'
		},
		{
			value: '3',
			label: 'Sim (Art. 10º)'
		}
	];
</script>

<div class="h-full w-full bg-slate-800 py-10">
	<a href="/">
		<h1 class="text-center text-6xl font-bold text-white">Nome do Sistema</h1>
	</a>
	<form action="" class="mx-auto mt-10 flex max-w-3xl flex-col gap-5 rounded-2xl bg-white p-9 pt-6">
		<h2 class="text-4xl font-bold">Complete o seu cadastro</h2>
		<label for="nome" class="flex flex-col text-xl font-medium">
			Nome completo
			<Input
				type="text"
				name="nome"
				id="nome"
				value={$page.data.session?.user?.name}
				required
				placeholder="Digite o seu nome..."
			/>
		</label>
		<label for="siape" class="flex flex-col text-xl font-medium">
			Matrícula SIAPE
			<Input type="text" name="siape" id="siape" required placeholder="0000000" />
		</label>
		<label for="dep" class="flex flex-col text-xl font-medium">
			Departamento/Unidade Acadêmica
			<Input
				type="text"
				name="dep"
				id="dep"
				required
				placeholder="Digite o Departamento ou a Unidade acadêmica..."
			/>
		</label>
		<label for="vinculo" class="flex flex-col text-xl font-medium">
			Tipo de vínculo
			<select
				id="vinculo"
				name="vinculo"
				value=""
				class="w-full rounded-lg border border-zinc-600 px-4 py-2 text-xl text-zinc-700 shadow-sm"
			>
				{#each vinculos as vinculo}
					<option value={vinculo.value} disabled={vinculo.value === ''}>{vinculo.label}</option>
				{/each}
			</select>
		</label>
		<label for="regime" class="flex flex-col text-xl font-medium">
			Regime de Trabalho
			<select
				id="regime"
				name="regime"
				value=""
				class="w-full rounded-lg border border-zinc-600 px-4 py-2 text-xl text-zinc-700 shadow-sm"
			>
				{#each regimes as regime}
					<option value={regime.value} disabled={regime.value === ''}>{regime.label}</option>
				{/each}
			</select>
		</label>
		<label for="reducao" class="flex flex-col text-xl font-medium">
			Exerce Função com Redução de CH de Ensino
			<select
				id="reducao"
				name="reducao"
				value="1"
				class="w-full rounded-lg border border-zinc-600 px-4 py-2 text-xl text-zinc-700 shadow-sm"
			>
				{#each reducoes as regime}
					<option value={regime.value}>{regime.label}</option>
				{/each}
			</select>
		</label>
		<button class="w-full rounded-lg bg-slate-500 py-2 text-xl font-bold text-white shadow-sm"
			>Continuar</button
		>
	</form>
</div>
