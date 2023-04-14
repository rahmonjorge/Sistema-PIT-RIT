<script lang="ts">
	export let name: string;
	export let label: string;
	export let noLabel: boolean = false;
	export let value: string = '';
</script>

{#if !noLabel}
	<label class="text-2xl font-medium" for={name}>{label}</label>
{/if}

<input
	type="text"
	{...$$restProps}
	{name}
	id={name}
	bind:value
	class="w-full rounded-lg border border-neutral-400 bg-white px-2 py-3 text-base text-zinc-700 shadow-sm outline-none placeholder:text-neutral-400 focus:ring-1 focus:ring-zinc-600 sm:px-4 md:px-6 md:text-2xl"
/>
