<script lang="ts">
	import Footer from '$/components/Footer.svelte';
	import Header from '$/components/Header.svelte';
</script>

<div class="min-h-with-footer pb-20">
	<div
		class="h-full w-full bg-blue-100 pb-20 sm:rounded-bl-home-sm md:rounded-bl-home-md xl:rounded-bl-[400px]"
	>
		<Header />
		<slot />
	</div>
</div>
<Footer />
