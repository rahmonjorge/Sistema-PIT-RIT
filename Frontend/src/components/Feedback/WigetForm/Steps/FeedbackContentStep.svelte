<script lang="ts">
	import { IconArrowLeft } from '@tabler/icons-svelte';
	import { createEventDispatcher } from 'svelte';
	import { feedbackTypes, type FeedbackType } from '../WidgetForm';
	import CloseButton from '../../CloseButton.svelte';
	import Loading from '../../Loading.svelte';

	export let feedbackType: FeedbackType;
	const feedbackTypeInfo = feedbackTypes[feedbackType];

	export let name: string = '';
	export let email: string = '';
	let message = '';
	let isSendingFeedback = false;
	let error = '';

	const dispatch = createEventDispatcher();

	async function handleSubmit() {
		isSendingFeedback = true;
		try {
			const response = await fetch('/api/feedback/send', {
				method: 'POST',
				headers: {
					'Content-Type': 'application/json'
				},
				body: JSON.stringify({
					email: email === '' ? undefined : email,
					name: name === '' ? undefined : name,
					message,
					type: feedbackType
				})
			});

			if (response.ok) {
				isSendingFeedback = false;
				dispatch('feedbackSubmitted');
			} else {
				const data = await response.json();
				error = data.message;
				isSendingFeedback = false;
			}
		} catch (e) {
			console.error(e);
			error = e.message;
		}
	}
</script>

<header>
	<button
		type="button"
		class="absolute top-5 left-5 text-zinc-500 hover:text-zinc-800"
		on:click={() => dispatch('feedbackRestartRequested')}
	>
		<IconArrowLeft class="h-4 w-4" />
	</button>
	<span class="flex items-center gap-2 text-xl leading-6">
		<img src={feedbackTypeInfo.image.source} alt={feedbackTypeInfo.image.alt} class="h-6 w-6" />
		{feedbackTypeInfo.title}
	</span>

	<CloseButton />
</header>

<form on:submit|preventDefault={handleSubmit} class="my-4 flex w-full flex-col gap-2">
	<input
		type="text"
		bind:value={name}
		class="scrollbar-thumb-zinc-200 scrollbar-track-transparent scrollbar-thin w-full min-w-[304px] resize-none rounded-md border border-zinc-300 bg-transparent p-2 text-sm text-zinc-800 placeholder-zinc-500 focus:border-orange-500 focus:outline-none focus:ring-1 focus:ring-orange-500"
		placeholder="Nome (opcional)"
	/>
	<input
		type="email"
		bind:value={email}
		class="scrollbar-thumb-zinc-200 scrollbar-track-transparent scrollbar-thin w-full min-w-[304px]  resize-none rounded-md border border-zinc-300 bg-transparent p-2 text-sm text-zinc-800 placeholder-zinc-500 focus:border-orange-500 focus:outline-none focus:ring-1 focus:ring-orange-500"
		placeholder="E-mail (opcional)"
	/>
	<textarea
		class="scrollbar-thumb-zinc-200 scrollbar-track-transparent scrollbar-thin min-h-[112px] w-full min-w-[304px] resize-none rounded-md border border-zinc-300 bg-transparent p-2 text-sm text-zinc-800 placeholder-zinc-500 focus:border-orange-500 focus:outline-none focus:ring-1 focus:ring-orange-500"
		bind:value={message}
		placeholder="Conte com detalhes o que está acontecendo..."
	/>

	{#if error !== ''}
		<div class="text-red-500">{error}</div>
	{/if}

	<footer class="mt-2 flex gap-2">
		<button
			type="submit"
			disabled={message.length === 0 || isSendingFeedback}
			class="flex flex-1 items-center justify-center rounded-md border-transparent bg-orange-500 p-2 text-sm text-white transition-colors hover:bg-orange-300 focus:outline-none focus:ring-2 focus:ring-orange-500 focus:ring-offset-2 focus:ring-offset-white disabled:cursor-not-allowed disabled:opacity-50 disabled:hover:bg-orange-500 "
		>
			{#if isSendingFeedback}
				<Loading />
			{:else}
				Enviar Feedback
			{/if}
		</button>
	</footer>
</form>
