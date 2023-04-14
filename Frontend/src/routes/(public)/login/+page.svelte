<script lang="ts">
	import { signIn } from '@auth/sveltekit/client';
	import googleGLogo from '$lib/assets/google-g-logo.svg';
	let loading = false;
	import preencherPitImage from '$lib/assets/preencher_pit.png';
	import pdfImage from '$lib/assets/pdf.png';
</script>

<div class="flex flex-col items-center py-20">
	<h1 class="text-6xl font-bold">Sistema PIT-RIT</h1>
	<h2 class="mt-2 text-xl">
		Crie seus <span class="font-semibold">PIT's</span> e <span class="font-semibold">RIT's</span> de
		uma forma simplificada
	</h2>
	<div class="mt-20 flex w-[26.625rem] flex-col items-center gap-9 rounded-xl ">
		<h3 class="text-center text-3xl font-semibold">Conecte-se para começar a usar o sistema</h3>

		<button
			disabled={loading}
			class="mx-auto flex items-center gap-5 rounded-lg border bg-white py-6 px-8 text-xl font-semibold shadow-lg disabled:bg-neutral-100 disabled:text-neutral-600"
			on:click|once={function loginClick() {
				this.disabled = true;
				loading = true;
				signIn('google', {
					redirect: true,
					callbackUrl: 'http://localhost:5173/completar-cadastro'
				});
			}}
		>
			<img src={googleGLogo} alt="" class="h-7 w-7" />
			{#if loading}
				<span>Carregando...</span>
			{:else}
				<span>Continuar com Google</span>
			{/if}
		</button>
	</div>
	<div class="mt-24 flex gap-16">
		<div class="flex w-80 flex-col gap-2">
			<img src={preencherPitImage} alt="" class="h-44 w-80 object-cover" />
			<p>Marque suas tarefas e gere o seu <b>PIT</b> de maneira fácil</p>
		</div>
		<div class="flex w-80 flex-col gap-2">
			<img src={pdfImage} alt="" class="h-44 w-80 object-cover" />
			<p>Uma vez que seu <b>PIT</b> está válido, gere o PDF na mesma hora</p>
		</div>
		<!-- <div class="flex w-80 flex-col gap-2">
			<img src="" alt="" class="h-44 w-80" />
			<p>Marque suas tarefas e gere o seu <b>PIT</b> de maneira fácil</p>
		</div> -->
	</div>
</div>
