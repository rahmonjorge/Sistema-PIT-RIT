<script lang="ts">
	import {
		Dialog,
		DialogDescription,
		DialogOverlay,
		DialogTitle
	} from '@rgossiaux/svelte-headlessui';

	export let title: string;
	export let isOpen: boolean;
	export let description: string;
	export let oneButton: boolean = false;
	export let confirm: {
		text: string;
		action: () => void;
	} = {
		text: 'OK',
		action: () => {
			isOpen = false;
		}
	};
	export let cancel: {
		text: string;
		action: () => void;
		red?: boolean;
	} = {
		text: 'Cancelar',
		action: () => {
			isOpen = false;
		},
		red: true
	};
</script>

<Dialog
	open={isOpen}
	on:close={() => (isOpen = false)}
	class="flex flex-col gap-8 bg-white px-16 py-8"
>
	<DialogOverlay class="fixed inset-0 z-10 flex h-screen w-screen bg-black/60">
		<div
			class="m-auto flex max-w-[600px] flex-col items-center gap-8 rounded-lg bg-white px-12 py-8"
		>
			<DialogTitle class="text-5xl font-bold">{title}</DialogTitle>
			<DialogDescription>
				{description}
			</DialogDescription>
			{#if oneButton}
				<div class="flex w-full justify-center gap-8">
					<button
						class="rounded-lg 
					bg-orange-500 px-8 py-4 font-semibold text-white shadow-sm transition duration-300 hover:bg-orange-600 active:scale-90"
						on:click={confirm.action}
					>
						{confirm.text}
					</button>
				</div>{:else}
				<div class="flex w-full justify-between gap-8">
					<button
						type="button"
						on:click={cancel.action}
						class="max-w-[150px] rounded-lg {cancel.red
							? 'bg-red-400 hover:bg-red-500'
							: 'bg-orange-500 hover:bg-orange-600'} px-6 py-3 font-semibold text-white shadow-sm transition duration-300  active:scale-90"
						>{cancel.text}
					</button>

					<button
						class="max-w-[150px] rounded-lg
					bg-orange-500 px-6 py-3 font-semibold text-white shadow-sm transition duration-300 hover:bg-orange-600 active:scale-90"
						on:click={confirm.action}
					>
						{confirm.text}
					</button>
				</div>
			{/if}
		</div>
	</DialogOverlay>
</Dialog>
