<script lang="ts">
	export let number: number;
	export let title: string;
	export let image: string = '';
</script>

<div class="flex w-full flex-col gap-4 rounded-lg bg-blue-100 p-10">
	<div class="flex items-center gap-4">
		<span
			class="flex h-10 w-10 items-center justify-center rounded-full bg-neutral-50 text-xl font-semibold"
		>
			{number}
		</span>
		<span class="text-lg">
			{title}
		</span>
	</div>
	{#if image}
		<img src={image} alt="Passo {number}" class="w-full object-cover" />
	{/if}
</div>
