import bugImageUrl from '../../../lib/assets/bug.svg';
import ideaImageUrl from '../../../lib/assets/idea.svg';
import thoughtImageUrl from '../../../lib/assets/thought.svg';

export const feedbackTypes = {
	BUG: {
		title: 'Problema',
		image: {
			source: bugImageUrl,
			alt: 'Imagem de um inseto'
		}
	},

	FEATURE: {
		title: 'Ideia',
		image: {
			source: ideaImageUrl,
			alt: 'Imagem de uma lâmpada'
		}
	},

	QUESTION: {
		title: 'Pergunta',
		image: {
			source: thoughtImageUrl,
			alt: 'Imagem de um balão de pensamento'
		}
	}
};

export type FeedbackType = keyof typeof feedbackTypes;
