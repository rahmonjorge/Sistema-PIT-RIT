<script lang="ts">
	import ufrpeImage from '$lib/assets/ufrpelogo.png';
	export let layout: 'protected' | 'public' = 'public';
	export let loggedIn: boolean = false;
</script>

{#if layout === 'public'}
	<header
		class="sticky top-0 z-10 flex w-full justify-center rounded-bl-[150px] bg-blue-700 text-white"
	>
		<div class="flex w-3/4 max-w-5xl items-center justify-between">
			<a href="/" class="text-2xl font-bold"> Início </a>
			<nav class="flex gap-4 text-base lg:text-xl">
				{#if !loggedIn}
					<a
						class="relative transition-colors
				after:absolute after:bottom-0 after:left-0 after:h-0.5 after:w-full after:rounded-full after:bg-blue-600 after:opacity-0 after:transition-opacity  after:duration-300 after:ease-in after:content-[''] hover:after:opacity-100"
						href="/login">Login</a
					>
				{/if}
				<a
					class="relative transition-colors
				after:absolute after:bottom-0 after:left-0 after:h-0.5 after:w-full after:rounded-full after:bg-blue-600 after:opacity-0 after:transition-opacity  after:duration-300 after:ease-in after:content-[''] hover:after:opacity-100"
					href="/sobre">Sobre</a
				>
				<a
					class="relative transition-colors
				after:absolute after:bottom-0 after:left-0 after:h-0.5 after:w-full after:rounded-full after:bg-blue-600 after:opacity-0 after:transition-opacity  after:duration-300 after:ease-in after:content-[''] hover:after:opacity-100"
					href="/contato">Contato</a
				>
			</nav>
			<a href="https://ufrpe.br" target="_blank" rel="external noopener noreferrer">
				<img
					src={ufrpeImage}
					alt="Link para página inicial da UFRPE"
					class="h-25 w-25 object-contain"
				/>
			</a>
		</div>
	</header>
{:else}
	<header
		class="fixed top-0 right-0 z-10 flex w-full justify-center rounded-bl-[150px] bg-blue-700 text-white sm:w-11/12"
	>
		<div class="flex w-3/4 max-w-5xl items-center justify-between">
			<a href="/" class="text-2xl font-bold"> Início </a>
			<nav class="flex gap-4 text-base lg:text-xl">
				<a
					class="relative transition-colors
			after:absolute after:bottom-0 after:left-0 after:h-0.5 after:w-full after:rounded-full after:bg-blue-600 after:opacity-0 after:transition-opacity  after:duration-300 after:ease-in after:content-[''] hover:after:opacity-100"
					href="/sobre">Sobre</a
				>
				<a
					class="relative transition-colors
			after:absolute after:bottom-0 after:left-0 after:h-0.5 after:w-full after:rounded-full after:bg-blue-600 after:opacity-0 after:transition-opacity  after:duration-300 after:ease-in after:content-[''] hover:after:opacity-100"
					href="/contato">Contato</a
				>
			</nav>
			<a href="https://ufrpe.br" target="_blank" rel="external noopener noreferrer">
				<img
					src={ufrpeImage}
					alt="Link para página inicial da UFRPE"
					class="h-25 w-25 object-cover"
				/>
			</a>
		</div>
	</header>
{/if}
