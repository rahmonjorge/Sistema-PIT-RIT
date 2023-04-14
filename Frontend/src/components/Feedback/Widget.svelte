<script lang="ts">
	import { Popover, PopoverButton, PopoverPanel } from '@rgossiaux/svelte-headlessui';
	import WidgetForm from './WigetForm/WidgetForm.svelte';
	import { IconMessageDots } from '@tabler/icons-svelte';

	export let user:
		| {
				[key: string]: any;
				name?: string | null;
				email?: string | null;
		  }
		| undefined;
</script>

<Popover class="fixed bottom-4 right-4 flex flex-col items-end md:bottom-8 md:right-8">
	<PopoverPanel>
		<WidgetForm {user} />
	</PopoverPanel>
	<PopoverButton class="group flex h-12 items-center rounded-full bg-orange-500 px-3 text-white">
		<IconMessageDots class="h-6 w-6" />
		<span
			class="max-w-0 overflow-hidden transition-all duration-500 ease-linear group-hover:max-w-xs"
		>
			<span class="pl-2" />
			Feedback
		</span>
	</PopoverButton>
</Popover>
