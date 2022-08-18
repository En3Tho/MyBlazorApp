module.exports = {
    plugins: [
        require('postcss-nested'),
        require('postcss-import'),
        require('autoprefixer'),
        require('tailwindcss/nesting'),
        require('tailwindcss'),
        require('postcss-custom-media'),
    ],
}