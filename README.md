### 📚 Refatoração de Código Legado — Arquitetura, Domínio Rico e Testabilidade

Este repositório consolida os aprendizados aplicados durante o curso de refatoração de código legado, com foco em desacoplamento, escalabilidade, testabilidade e melhoria de performance.

O projeto foi estruturado seguindo princípios de **Domain-Driven Design (DDD) e boas práticas de arquitetura**, priorizando organização de domínio, separação de responsabilidades e clareza de intenção no código.

## 🧱 Conceitos e Práticas Aplicadas
🔹 Modelagem de Domínio Rico

- Entidades com comportamento e regras de negócio encapsuladas.

- Eliminação de anemias no modelo de domínio.

- Garantia de invariantes dentro das próprias entidades.

🔹 Value Objects

- Objetos imutáveis.

- Igualdade baseada em valor.

- Representação explícita de conceitos do domínio.

🔹 Validações com Flunt

- Uso de notificações para tratamento de inconsistências.

- Centralização de regras de validação.

- Redução de exceções como fluxo de controle.

🔹 CQRS (Command Query Responsibility Segregation)

- Separação clara entre escrita (Commands) e leitura (Queries).

Implementação de:

- Command.

- Handler.

- ICommand.

- IHandler.

- Isolamento de regras de negócio dentro dos Handlers.

🔹 Testes Unitários

Testes de:

- Entidades.

- Value Objects.

- Commands (requests de entrada).

- Handlers.

- Validação de regras de negócio isoladamente.

- Garantia de previsibilidade e segurança em refatorações futuras.

## 🎯 Objetivos Técnicos Alcançados

- Redução de acoplamento.

- Aumento de coesão.

- Código orientado a comportamento.

- Melhor organização da camada de domínio.

- Facilidade de manutenção e evolução.

- Maior cobertura e confiabilidade através de testes.

🛠 Stack e Tecnologias

- C#

- .NET

- Flunt

- xUnit

- Princípios de DDD

- Padrão CQRS

Este projeto reforça a importância de modelar corretamente o domínio antes de pensar em infraestrutura, promovendo um código mais expressivo, sustentável e preparado para crescimento.
