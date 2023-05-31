module.exports = {
    content: [
        './**/*.{razor,html,cshtml}',
        './../FSharpComponents/**/*.fs',
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