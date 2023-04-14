<script lang="ts">
	import {
		Listbox,
		ListboxButton,
		ListboxOptions,
		ListboxOption,
		ListboxLabel
	} from '@rgossiaux/svelte-headlessui';
	import { IconChevronDown } from '@tabler/icons-svelte';

	export let label: string = 'Selecione uma opção';
	export let options: any[] = [];
	export let selectedOption = options[0];
	export let placeholder: string = 'Selecione uma opção...';
	export let name: string;

	$: showPlaceholder = selectedOption === '' || selectedOption === undefined;
</script>

<Listbox
	value={selectedOption}
	on:change={(e) => (selectedOption = e.detail)}
	class="flex flex-col items-start"
	let:open
>
	<ListboxLabel class="text-2xl font-medium">{label}</ListboxLabel>
	<ListboxButton
		class="mt-2 flex w-full cursor-default items-center justify-between rounded-lg border border-neutral-400 bg-white bg-transparent px-2 py-3 text-left text-base text-zinc-700 shadow-sm outline-none focus:ring-1 focus:ring-zinc-600 sm:px-4 md:px-6 md:text-2xl
        {showPlaceholder ? 'text-zinc-400' : ''}
        {open ? 'rounded-b-none border-b-0' : ''}"
		>{showPlaceholder ? placeholder : selectedOption}

		<IconChevronDown size={24} class="text-neutral-400" />
	</ListboxButton>
	<div class="relative w-full">
		<ListboxOptions class="absolute z-20 w-full outline-none">
			{#each options as option, index}
				<ListboxOption value={option} class="w-full select-none" let:selected let:active>
					<p
						class="w-full border-x border-t border-neutral-400 bg-white bg-transparent px-2 py-3 text-base text-zinc-700 outline-none transition-colors duration-300 hover:bg-orange-100 focus:ring-1 focus:ring-zinc-600 sm:px-4 md:px-6 md:text-2xl {active
							? 'bg-orange-100'
							: ''} {selected ? 'bg-orange-200' : ''} {index === options.length - 1
							? 'rounded-b-lg border-b'
							: ''}"
					>
						{#if selected}
							<span class="mr-2 text-orange-600" aria-hidden="true">✓</span>
						{/if}
						{option}
					</p>
				</ListboxOption>
			{/each}
		</ListboxOptions>
	</div>
</Listbox>
<input type="hidden" bind:value={selectedOption} {name} />
