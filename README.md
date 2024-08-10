# API .NET Core com Redis

Este projeto demonstra como integrar o Redis em uma API .NET Core, incluindo a configuração do Redis, criação de endpoints para salvar e recuperar dados, e como prevenir a duplicação de registros ao salvar.
O Redis é usado como uma solução de cache em memória, ideal para operações rápidas e eficientes.

## Índice
- [Introdução](#introducao)
- [Instalação](#instalacao)
  - [Pré-requisitos](#pré-requisitos)
  - [Configuração do Redis](#configuracao-do-redis)
  - [Configuração do Projeto](#configuracao-do-projeto)
- [Uso](#uso)
  - [Salvar Usuário](#salvar-usuario)
  - [Recuperar Usuário](#recuperar-usuario)
- [Endpoints](#requisitos)
  - [POST /api/redis/save-user](#save-user)
  - [GET /api/redis/get-user/{id}](#get-user)
- [Contribuição](#contribuicao)

## Introdução
Este projeto é uma API simples em .NET Core que demonstra como usar o Redis para armazenar e recuperar informações de usuários. A API inclui endpoints para salvar e obter usuários, com verificação para evitar a duplicação de usuários com o mesmo ID.

## Instalação
### Pré-requisitos
- [.NET Core SDK](https://dotnet.microsoft.com/download)
- [Redis](https://redis.io/)
- [Docker](https://www.docker.com/get-started) (opcional, mas recomendado para rodar o Redis localmente)

## Configuração do Redis

### Executando Redis Localmente com Docker

Se você não tem o Redis instalado localmente, a maneira mais fácil de executá-lo é usando Docker. Execute os comandos abaixo para baixar a imagem do Redis e iniciá-lo:

```
docker pull redis
docker run --name redis-local -p 6379:6379 -d redis
```
Isso iniciará um container Docker com o Redis na porta padrão 6379.

### Instalando Redis Localmente:
- Windows: Memurai (Redis para Windows)
- Linux: `sudo apt install redis-server`
- macOS: `brew install redis`

## Configuração do Projeto
### Clone do repositorio
```
git clone https://github.com/inoccard/RedisSimple.git
cd RedisSimple
```

### Instale os pacotes necessários:
`dotnet restore`

### Atualize a string de conexão com o Redis no appsettings.json:
```
{
  "ConnectionStrings": {
    "Redis": "localhost:6379"
  }
}
```

### Executar o projeto: precione F5 ou
`dotnet watch run` no terminal

## Uso
### Salvar Usuário
Envie uma requisição POST para `/api/redis/save-user` com o corpo da requisição contendo o JSON do usuário:
```
{
  "id": "1",
  "name": "John Doe",
  "email": "johndoe@example.com"
}
```
### Recuperar Usuário
Envie uma requisição GET para `/api/redis/get-user/{id}` onde `{id}` é o ID do usuário que deseja recuperar.

## Endpoints
### POST /api/redis/save-user
Salva um novo usuário no Redis.

#### Requisição:

- Corpo (JSON):
```
Copiar código
{
  "id": "1",
  "name": "John Doe",
  "email": "johndoe@example.com"
}
```

#### Respostas:
- 200 OK: Usuário salvo com sucesso.
- 409 Conflict: Usuário com este ID já existe.

### GET /api/redis/get-user/{id}
Recupera um usuário do Redis pelo ID.

#### Requisição:
- Parâmetro de URL:
  - id: O ID do usuário a ser recuperado.

#### Respostas:
- 200 OK: Retorna o objeto do usuário.
- 404 Not Found: Usuário não encontrado.

## Contribuição
Contribuições são bem-vindas! Por favor, abra uma issue ou envie um pull request para melhorias.
