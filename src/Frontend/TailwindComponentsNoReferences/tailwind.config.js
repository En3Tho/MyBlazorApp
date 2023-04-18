/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    './**/*.{razor,html,cshtml}',
  ],
  theme: {
    extend: {},
    animatedSettings: {
      animatedClassName: ""
    }
  },
  plugins: [
    require('tailwindcss-animatecss'),
  ],
  colors: {
  }
}