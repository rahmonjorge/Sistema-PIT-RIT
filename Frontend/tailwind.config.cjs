const { fontFamily } = require('tailwindcss/defaultTheme');

/** @type {import('tailwindcss').Config} */
module.exports = {
	content: ['./src/**/*.{html,js,svelte,ts}'],
	theme: {
		extend: {
			fontFamily: {
				primary: ['Poppins', 'sans-serif']
			},
			boxShadow: {
				sm: ' 0px 0px 4px rgba(0, 0, 0, 0.25)'
			}
		}
	},
	plugins: []
};
