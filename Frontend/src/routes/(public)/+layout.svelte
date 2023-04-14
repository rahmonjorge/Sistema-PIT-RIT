<script>
	import { page } from '$app/stores';
	import Footer from '$/components/Footer.svelte';
	import Header from '$/components/Header.svelte';
</script>

<div class="min-h-screen pb-20">
	<Header loggedIn={!!$page.data.session?.user} />
	<div
		class="-mt-[100px] w-full bg-blue-100 pt-28 sm:rounded-bl-home-sm md:rounded-bl-home-md xl:rounded-bl-[400px]"
	>
		<slot />
	</div>
</div>
<Footer />
