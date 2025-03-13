# teste-tecnico-c--13-03-2025

# Autenticação da Aplicação

A aplicação possui rotas protegidas por autenticação.

## Login

Para acessar as rotas protegidas, é necessário autenticar-se utilizando a seguinte rota:

POST http://localhost:5044/api/Auth/login

### Credenciais Padrão

Utilize as seguintes credenciais para autenticação:

```json
{
  "username": "admin",
  "password": "senha"
}
