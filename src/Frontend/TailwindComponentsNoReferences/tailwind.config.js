/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    './**/*.{razor,html,cshtml}',
  ],
  theme: {
    extend: {},
    animatedSettings: {
    }
  },
  plugins: [
    require('tailwindcss-animatecss'),
  ],
  colors: {
  }
}