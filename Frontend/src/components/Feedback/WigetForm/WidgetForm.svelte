<script lang="ts">
	import FeedbackContentStep from './Steps/FeedbackContentStep.svelte';
	import FeedbackSuccessStep from './Steps/FeedbackSuccessStep.svelte';
	import FeedbackTypeStep from './Steps/FeedbackTypeStep.svelte';
	import type { FeedbackType } from './WidgetForm';

	export let user:
		| {
				[key: string]: any;
				name?: string | null;
				email?: string | null;
		  }
		| undefined;

	let feedbackType: FeedbackType | null = null;
	let feedbackSent = false;

	function handleRestartFeedback() {
		feedbackType = null;
		feedbackSent = false;
	}
</script>

<div
	class="relative mb-4 flex w-[calc(100vw-2rem)] flex-col items-center rounded-2xl bg-white p-4 shadow-lg md:w-auto"
>
	{#if feedbackSent}
		<FeedbackSuccessStep on:feedBackRestartRequested={handleRestartFeedback} />
	{:else if !feedbackType}
		<FeedbackTypeStep on:feedbackTypeChanged={({ detail }) => (feedbackType = detail.type)} />
	{:else}
		<FeedbackContentStep
			email={user?.email ?? ''}
			name={user?.name ?? ''}
			{feedbackType}
			on:feedbackRestartRequested={handleRestartFeedback}
			on:feedbackSubmitted={() => (feedbackSent = true)}
		/>
	{/if}
</div>
