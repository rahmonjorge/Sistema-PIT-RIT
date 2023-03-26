<script lang="ts">
	import { signIn, signOut } from '@auth/sveltekit/client';
	import { page } from '$app/stores';
	let loading = false;
</script>

<h1>Nome do Sistema</h1>
{#if $page.data.session}
	<button
		disabled={loading}
		on:click|once={function loginClick() {
			this.disabled = true;
			loading = true;
			signOut();
		}}
	>
		Sair
	</button>
{:else}
	<button
		disabled={loading}
		on:click|once={function loginClick() {
			this.disabled = true;
			loading = true;
			signIn('google', {
				redirect: true,
				callbackUrl: 'http://localhost:5173/completar-cadastro'
			});
		}}>Continuar com google</button
	>
{/if}
