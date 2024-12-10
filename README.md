# Crud Produto
## Descrição do Projeto
<p align="center">🚀 O projeto trata-se de uma api que permite realizar gerenciamento de um produto, podendo criar, obter atualizar e apagar um produto</p>

### 🛠 Objetivo

Demostrar a organização de projeto que realiza um CRUD de produto utilizando uma api rest c# .net 8,  o projeto é um crud de produto onde salva em um banco 
sql server utilizando o orm Ef Core, onion architecture, clean code, testes unitarios e etc. 

#### Fluxo do projeto
![Alt text](/Assets/diagramaProjeto.png?raw=true "Fluxo")

### 🛠 Como usar
 1. Baixe o projeto
 2. rode o comando no powershell na pasta [docker](https://github.com/Lucas-Sampaio/CrudProduto/tree/master/Docker) -> ```docker-compose up --build -d```
 isso irá subir os serviços necessario pro projeto api e banco de dados.
 4. O projeto gera a documentação da api automatica pelo swagger e pode ser acessado pelo url http://localhost:5000/swagger/index.html

### 🛠 Tecnologias

As seguintes ferramentas foram usadas na construção do projeto:

- [.Net 8](https://github.com/dotnet)
- [Ef Core Sql Server](https://github.com/dotnet/efcore)
- Testes [Moq](https://github.com/devlooped/moq?tab=readme-ov-file) e [Xuni.net](https://xunit.net/)
- [FluentValidation](https://docs.fluentvalidation.net/en/latest/)
- [Mediator](https://github.com/jbogard/MediatR)
- [Sql server 2022](https://www.microsoft.com/pt-br/sql-server/sql-server-2022)
  
