# Sistema PIT-RIT

## Descrição

Este projeto tem como objetivo desenvolver um sistema que auxilie os professores da **Universidade Federal Rural de Pernambuco (UFRPE)**¹ na elaboração e gestão dos relatórios exigidos pelo **Processo de Avaliação RIT**. O RIT é um instrumento de avaliação institucional que visa acompanhar o desempenho dos docentes em relação às atividades de ensino, pesquisa, extensão e gestão². O sistema deve permitir que os professores cadastrem seus dados pessoais, acadêmicos e profissionais, registrem suas atividades realizadas no período avaliativo, gerem relatórios parciais e finais em formato PDF e acompanhem o resultado da avaliação.

## Funcionalidades

- Cadastro e login de usuários (professores)
- Registro e edição de dados pessoais e acadêmicos
- Registro e edição de atividades de ensino, pesquisa, extensão e gestão
- Geração de planilhas PIT em formato PDF
- Geração de relatórios parciais e finais em formato PDF
- Visualização do resultado da avaliação RIT

## Tecnologias

- Linguagens de programação: C#, JavaScript
- Framework web: Sveltekit
- Banco de dados: DjangoDB
- Comunicação entre módulos: gRPC

## Instalação

- Clone o repositório do projeto: `git clone <url>`
- Crie um ambiente virtual: `python -m venv venv`
- Ative o ambiente virtual: `source venv/bin/activate` (Linux) ou `venv\Scripts\activate` (Windows)
- Instale as dependências: `pip install -r requirements.txt`
- Crie as tabelas do banco de dados: `python manage.py migrate`
- Crie um usuário administrador: `python manage.py createsuperuser`
- Execute o servidor de desenvolvimento: `python manage.py runserver`

## Uso

- Acesse o sistema pelo navegador: `http://localhost:8000/`
- Faça login com o google ou crie um novo usuário
- Preencha os seus dados pessoais e acadêmicos
- Registre as suas atividades de ensino, pesquisa, extensão e gestão
- Gere os relatórios parciais e finais em formato PDF
- Acompanhe o resultado da avaliação RIT

## Licença

Este projeto está licenciado sob a licença <license> Veja o arquivo LICENSE para mais detalhes.

## Contribuição

Este projeto é um trabalho em andamento e aceita contribuições. Se você tem alguma sugestão, crítica ou dúvida, por favor, abra uma issue ou envie um pull request.
