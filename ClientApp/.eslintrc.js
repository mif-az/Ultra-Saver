module.exports = {
  env: {
    browser: true,
    es2021: true
  },
  extends: ['airbnb', 'prettier'],
  overrides: [],
  parserOptions: {
    ecmaVersion: 'latest',
    sourceType: 'module'
  },
  plugins: ['prettier'],
  rules: {
    'prettier/prettier': ['error', { endOfLine: 'auto' }],
    'react/react-in-jsx-scope': 'off'
  }
};
