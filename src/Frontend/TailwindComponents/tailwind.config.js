module.exports = {
    content: [
        './**/*.{razor,html,cshtml}',
        './../FSharpComponents/**/*.fs',
    ],
    theme: {
        extend: {},
        animatedSettings: {
            animatedClassName: ""
        },
        gridTemplateColumns: {
            '16': 'repeat(16, minmax(0, 1fr))',
            '32': 'repeat(32, minmax(0, 1fr))',
            '64': 'repeat(64, minmax(0, 1fr))',
            '128': 'repeat(128, minmax(0, 1fr))',
            '256': 'repeat(256, minmax(0, 1fr))',
            '512': 'repeat(512, minmax(0, 1fr))'
        }
    },
    plugins: [
        require('tailwindcss-animatecss'),
    ],
    colors: {
    }
}