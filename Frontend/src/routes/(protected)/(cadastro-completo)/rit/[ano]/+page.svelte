<script lang="ts">
	import type { PageData } from './$types';

	export let data: PageData;
</script>

{#if data.error}
	<h1>Você não possui um PIT para esse ano...</h1>
	<a class="underline" href="/">Voltar para tela inicial</a>
{:else if data.planilha}
	<h2 class="text-4xl">MINISTRAÇÃO DE AULA</h2>
	<div class="flex w-full flex-col items-start gap-4">
		{#each data.planilha.ministracaoDeAula as min}
			<div class="flex gap-2">
				<p>{min.text}</p>
				<input type="number" value={min.value} />
			</div>
		{/each}
	</div>

	<h2 class="text-4xl">OUTRAS ATIVIDADES DE ENSINO</h2>
	<div>
		<table class="h-full w-full table-auto">
			<tbody>
				{#each data.planilha.outrasAtividadesDeEnsino as linha}
					<tr>
						<td class="border border-black p-2">
							<span>
								{linha.text}
							</span>
						</td>
						<td class="h-full border border-black bg-indigo-50">
							<label class="flex h-full items-center justify-center px-4 py-2">
								<input type="checkbox" bind:checked={linha.value} />
							</label>
						</td>
						<td class="border border-black bg-indigo-50">
							<div class="flex items-center justify-center px-2 py-1">
								{#if Math.random() > 0.5}
									<button class="w-full rounded bg-orange-500 px-1 py-1 text-white sm:py-2 sm:px-3">
										Editar
									</button>
								{:else}
									<button class="w-full rounded bg-orange-500 px-1 py-1 text-white sm:py-2 sm:px-3">
										Adicionar
									</button>
								{/if}
							</div>
						</td>
					</tr>
				{/each}
			</tbody>
		</table>
	</div>
	<h2 class="text-4xl">ATIVIDADE DE PESQUISA</h2>
	<div class="flex w-full flex-col items-start gap-4">
		{#each data.planilha.atividadesDePesquisa as pesquisa}
			<div class="flex gap-2">
				<p>{pesquisa.text}</p>
				<input type="checkbox" checked={pesquisa.value} />
			</div>
		{/each}
	</div>
	<h2 class="text-4xl">ATIVIDADE DE EXTENSÃO</h2>
	<div class="flex w-full flex-col items-start gap-4">
		{#each data.planilha.atividadesDeExtensao as extensao}
			<div class="flex gap-2">
				<p>{extensao.text}</p>
				<input type="checkbox" checked={extensao.value} />
			</div>
		{/each}
	</div>
	<h2 class="text-4xl">ATIVIDADE DE ADMINISTRATIVA</h2>
	<div class="flex w-full flex-col items-start gap-4">
		{#each data.planilha.atividadesAdministrativas as admistrativa}
			<div class="flex gap-2">
				<p>{admistrativa.text}</p>
				<input type="checkbox" checked={admistrativa.value} />
			</div>
		{/each}
	</div>
{/if}
