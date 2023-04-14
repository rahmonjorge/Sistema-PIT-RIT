<script>
	import { PopoverButton } from '@rgossiaux/svelte-headlessui';
	import { IconX } from '@tabler/icons-svelte';
</script>

<PopoverButton
	class="absolute top-5 right-5 text-zinc-500 hover:text-zinc-800"
	title="Fechar formulário"
>
	<IconX class="h-4 w-4" />
</PopoverButton>
