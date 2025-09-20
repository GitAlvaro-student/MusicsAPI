# 📌 MusicSoundAPI

API desenvolvida em **.NET 8** com **Entity Framework Core**, focada em fornecer endpoints para gerenciamento de músicas, artistas.  
O projeto segue boas práticas de arquitetura e é ideal como base para estudos ou aplicações reais relacionadas ao domínio musical.

---

## 🛠 Tecnologias Utilizadas

- **.NET 8 (ASP.NET Core Web API)** → Framework moderno e performático para construção de APIs RESTful.  
- **Entity Framework Core** → ORM utilizado para mapeamento objeto-relacional e acesso ao banco de dados.  
- **LINQ** → Consultas mais legíveis e expressivas diretamente em C#.  
- **Dependency Injection (DI)** → Gerenciamento de serviços e repositórios com baixo acoplamento.  
- **Swagger / Swashbuckle** → Documentação interativa da API.  
- **Migrations (EF Core)** → Controle de versão do banco de dados.  

---

## ⚙️ Funcionalidades Principais

- 🔹 **Gerenciamento de músicas**: cadastro e listagem de músicas de acordo com o artista.  
- 🔹 **Controle de artistas**: cadastro e listagem de artistas.  
- 🔹 **Documentação via Swagger**: acesso rápido e fácil para testar os endpoints.  

---

## 📖 Estrutura do Projeto

```
MusicSoundAPI/
 ┣ Controllers/       → Endpoints da API
 ┣ Models/            → Entidades e DTOs
 ┣ Data/              → DbContext e configurações do EF Core
 ┣ Services/          → Regras de negócio
 ┣ Program.cs         → Configuração inicial da aplicação
 ┗ appsettings.json   → Configurações do projeto e banco de dados
```

---

## 🛠 Futuras Melhorias

- **Implementar Autenticação e Autorização** → Utilizar Pacotes da Microsoft de Autenticação para segurança.  
- **Adição de Logs** → Utilizar EventHub e BlobStorage para monitoramento da aplicação.
- **Visualização no Kibana** → Integrar os Logs da Azure ao Elasticsearch e monitorar a aplicação por meio de dashboards.  



🔗 **Autor:** [Álvaro](https://github.com/GitAlvaro-student)  
