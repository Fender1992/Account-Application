const { env } = require('process');

const target = env.ASPNETCORE_HTTPS_PORT ? `https://localhost:${env.ASPNETCORE_HTTPS_PORT}` :
  env.ASPNETCORE_URLS ? env.ASPNETCORE_URLS.split(';')[0] : 'https://localhost:https://localhost:7178';

const PROXY_CONFIG = [
  {
    context: [
      "api/Accounts",
      "api/User",
    ],
    target: 'https://localhost:44377',
    secure: false
  }
]

module.exports = PROXY_CONFIG;
