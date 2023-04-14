<script>
	import Step from '$/components/Step.svelte';
	import ajuda1 from '$lib/assets/ajuda/1.jpg';
	import ajuda2 from '$lib/assets/ajuda/2.jpg';
	import ajuda3 from '$lib/assets/ajuda/3.jpg';
	import ajuda4 from '$lib/assets/ajuda/4.jpg';
	import ajuda5 from '$lib/assets/ajuda/5.jpg';
	import ajuda6 from '$lib/assets/ajuda/6.jpg';
	import ajuda7 from '$lib/assets/ajuda/7.png';
	import ajuda8 from '$lib/assets/ajuda/8.jpg';
	import { Disclosure, DisclosureButton, DisclosurePanel } from '@rgossiaux/svelte-headlessui';
</script>

<h1 class="text-6xl font-bold">Ajuda</h1>

<div class="mt-8 w-full">
	<Disclosure class="w-full bg-neutral-50">
		<DisclosureButton class="flex w-full justify-start rounded-lg bg-blue-200 p-4"
			>Como criar e gerar o PDF de um PIT?</DisclosureButton
		>
		<DisclosurePanel class="flex flex-col gap-4">
			<Step number={1} title="Clique no botão de adicionar" image={ajuda1} />
			<Step number={2} title="Clique em digite o ano..." image={ajuda2} />
			<Step number={3} title="Digite o ano que você quer criar o PIT" />
			<Step number={4} title="Clique em criar PIT" image={ajuda3} />
			<Step number={5} title="Preencha os dados" image={ajuda4} />
			<Step number={6} title="Clique no subtotal para preencher as horas" image={ajuda5} />
			<Step
				number={7}
				title="Quando tudo estiver preenchido com valores válidos, clique em Gerar PDF no fim da página"
				image={ajuda6}
			/>
			<Step number={8} title="Aguarde um pouco até o PDF ser gerado." />
			<Step number={9} title="Visualize o seu PDF" image={ajuda7} />
			<Step number={8} title="Se estiver tudo certo baixe-o" image={ajuda8} />
		</DisclosurePanel>
	</Disclosure>
</div>

<p class="mt-8 text-xl">
	Se precisar de mais alguma ajuda, clique no botão de feedback no canto inferior direito da tela e
	nos envie uma mensagem!
</p>
