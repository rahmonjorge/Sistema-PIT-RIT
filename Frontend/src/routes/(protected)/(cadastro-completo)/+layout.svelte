<script>
	import Header from '$/components/Header.svelte';
	import { page } from '$app/stores';
	import { signOut } from '@auth/sveltekit/client';
	import defaultProfilePicture from '$lib/assets/default-profile-picture.png';
	import { IconHelpCircle, IconHome, IconLogout, IconSettings } from '@tabler/icons-svelte';
	import Footer from '$/components/Footer.svelte';
</script>

<Header layout="protected" />
<div class="grid min-h-screen grid-cols-5 grid-rows-2">
	{#if $page.data.session}
		<aside
			class="cos-span-1 row-span-2 hidden h-full bg-blue-500 pt-36 text-white sm:block lg:px-8"
		>
			<a href="/configuracoes" class="flex flex-col items-center gap-6 lg:flex-row ">
				<img
					src={$page.data.session.user?.image ?? defaultProfilePicture}
					alt="Foto de perfil de {$page.data.session.user?.name}"
					referrerpolicy="no-referrer"
					class="w-3/4 rounded-full border border-neutral-700 object-cover md:w-3/5 lg:w-16"
				/>
				<span class="hidden text-2xl font-semibold lg:inline"
					>{$page.data.session.user?.name?.split(' ')[0]}</span
				>
			</a>
			<nav class="mt-10 flex w-full flex-col items-center pb-8 lg:mt-20 lg:items-start lg:gap-8">
				<a
					href="/"
					class="flex w-full items-center justify-center gap-6 py-4 lg:justify-start lg:py-0"
				>
					<IconHome size={32} />
					<span class="hidden lg:inline">Início</span>
				</a>
				<div class="h-[1px] w-full bg-white" />
				<a
					href="/configuracoes"
					class="flex w-full items-center justify-center gap-6 py-4 lg:justify-start lg:py-0"
				>
					<IconSettings size={32} />
					<span class="hidden lg:inline">Configurações</span>
				</a>
				<div class="h-[1px] w-full bg-white" />
				<a
					href="/ajuda"
					class="flex w-full items-center justify-center gap-6 py-4 lg:justify-start lg:py-0"
				>
					<IconHelpCircle size={32} />
					<span class="hidden lg:inline">Ajuda</span>
				</a>
				<div class="h-[1px] w-full bg-white" />
				<button
					class="flex w-full items-center justify-center gap-6 py-4 lg:justify-start lg:py-0"
					on:click|once={() => signOut()}
				>
					<IconLogout size={32} />
					<span class="hidden lg:inline">Sair</span>
				</button>
			</nav>
		</aside>
	{/if}
	<main class="col-span-full row-span-2 w-full px-4 pt-32 pb-4 sm:col-span-4 lg:px-10">
		{#if $page.data.session}
			<slot />
		{/if}
	</main>
	<Footer />
</div>
