<script lang="ts">
	import CloseButton from '../../CloseButton.svelte';
	import { feedbackTypes, type FeedbackType } from '../WidgetForm';
	import { createEventDispatcher } from 'svelte';

	const dispatch = createEventDispatcher();
	function dispatchFeedbackTypeChanged(type: string) {
		dispatch('feedbackTypeChanged', {
			type
		});
	}
</script>

<header>
	<span class="text-xl leading-6">Deixe o seu Feedback</span>

	<CloseButton />
</header>

<div class="flex w-full gap-2 py-8">
	{#each Object.entries(feedbackTypes) as [key, value]}
		<button
			class="flex w-24 flex-1 flex-col items-center gap-2 rounded-lg border-2 border-transparent bg-zinc-100 py-5 hover:border-orange-500 focus:border-orange-500 focus:outline-none"
			on:click={() => dispatchFeedbackTypeChanged(key)}
		>
			<img src={value.image.source} alt={value.image.alt} />
			<span>{value.title}</span>
		</button>
	{/each}
</div>
