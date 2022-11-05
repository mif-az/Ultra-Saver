/* eslint-disable no-nested-ternary */
const { createProxyMiddleware } = require('http-proxy-middleware');
const { env } = require('process');

const target = env.ASPNETCORE_HTTPS_PORT
  ? `https://localhost:${env.ASPNETCORE_HTTPS_PORT}`
  : env.ASPNETCORE_URLS
  ? env.ASPNETCORE_URLS.split(';')[0]
  : 'http://localhost:21932';

const context = [
  '/userinfo',
  '/fetchlogindata',
  '/energycost',
  '/recipe',
  '/chat/send',
  '/chat/messages'
];

module.exports = function (app) {
  const appProxy = createProxyMiddleware(context, {
    target,
    ws: true,
    changeOrigin: true,
    secure: false
  });

  app.use(appProxy);
};
