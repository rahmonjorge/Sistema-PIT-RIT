const { fontFamily } = require('tailwindcss/defaultTheme');

/** @type {import('tailwindcss').Config} */
module.exports = {
	content: ['./src/**/*.{html,js,svelte,ts}'],
	theme: {
		extend: {
			fontSize: {
				pequeno: '0.5rem'
			},
			screens: {
				xs: '320px',
				lg: '1280px'
			},
			borderRadius: {
				'home-sm': '10rem',
				'home-md': '16rem'
			},
			width: {
				25: '6.25rem'
			},
			height: {
				25: '6.25rem'
			},
			minHeight: {
				'with-footer': 'calc(100vh - 7.25rem)'
			},
			fontFamily: {
				primary: ['Poppins', ...fontFamily.sans]
			},
			boxShadow: {
				sm: ' 0px 0px 4px rgba(0, 0, 0, 0.25)'
			},
			colors: {
				red: {
					50: '#F2D2D3',
					100: '#ECA6AA',
					200: '#E6797F',
					300: '#DF4E57',
					400: '#CF2B35',
					500: '#B61822',
					600: '#8F0F17',
					700: '#6E080F',
					800: '#530409',
					900: '#330104'
				},
				yellow: {
					50: '#FCEED7',
					100: '#FFE4B6',
					200: '#FCD48F',
					300: '#FAC770',
					400: '#FBBD51',
					500: '#F9B033',
					600: '#EAA020',
					700: '#CA8713',
					800: '#A96F0B',
					900: '#845708'
				},
				blue: {
					50: '#ECF0F8',
					100: '#B0BDD4',
					200: '#8B9CBD',
					300: '#6C80A7',
					400: '#51678F',
					500: '#354B72',
					600: '#24395E',
					700: '#1B2D4F',
					800: '#112344',
					900: '#081834'
				}
			}
		}
	},
	plugins: []
};
