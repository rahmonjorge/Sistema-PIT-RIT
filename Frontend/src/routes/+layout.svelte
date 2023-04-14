<script>
	import { page } from '$app/stores';
	import '../app.css';
	import Feedback from '../components/Feedback/Widget.svelte';
	const user = $page.data.session?.user;
</script>

<div class="relative">
	<Feedback {user} />

	<slot />
</div>
