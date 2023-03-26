<script>
	import { page } from '$app/stores';
	import { signOut } from '@auth/sveltekit/client';

	let loading = false;
</script>

<header>
	<h2>Nome do sistema</h2>
	<nav>
		<a href="/sobre">Sobre</a>
	</nav>
</header>

<main>
	{#if $page.data.session}
		<h1>Olá, {$page.data.session?.user?.name}</h1>
		Aqui fica os pits e rits
		<button
			on:click|once={function loginClick() {
				this.disabled = true;
				loading = true;
				signOut();
			}}
		>
			Sair
		</button>
	{/if}
</main>
