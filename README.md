## Desafio de Arquitetura de Soluções

Nesse repositório você encontrará o projeto proposto para o desafio de arquitetura de soluções e arquitetura de software. O desafio consiste na proposição de uma para atender à um comerciante que precisa controlar seu fluxo de caixa diário com lançamentos de débito e crédito, além de necessitar de um relatório diário de saldo consolidado.

## Sumário

- Antes de iniciarmos...
- Introdução (arquitetura de negócios)
- Arquitetura de Soluções
- Arquitetura de Tecnologia
- Arquitetura de Software
- Como executar esse projeto
- Débitos Técnicos e próximos passos

## Antes de iniciarmos...

Antes de iniciarmos a avaliação desse Code Challenger, eu gostaria de pedir desculpas pela demora na entrega do repositório. Além disso, infelizmente deixei alguns débitos técnicos na solução, além de pontos que foram solicitados, mas infelizmente, não tive tempo para construí-los da forma que considero ideal, tal como uma abordagem eficiente para os testes automatizados.

Peço, por gentileza, que avaliem meu projeto mesmo assim, caso possível.

Me encontro à disposição para conversarmos e debatermos as decisões técnicas que tomei ao longo do projeto e minhas alternativas para implementação dos itens faltantes. Acredito que tenho boas opções e técnicas para a conclusão do projeto.

Desde já agradeço imensamente pela paciência e pela oportunidade de realizar esse desafio.


## Introdução

Como não há muitas informações à cerca da situação atual da empresa para que se possa realizar um mapeamento AS-IS da arquitetura corporativa, foram assumidas as seguintes premissas para guiar a construção desse desafio:

-  Nosso cliente possui uma loja com uma operação in loco para venda de artigos de vestuário.
- Nosso cliente está ampliando sua operação com a abertura de novas lojas e realizará um processo de transformação digital do seu modelo negócios visando alcançar novos mercados por meio de vendas online.

Dessa forma, o mapeamento de capacidades de negócio do nosso cliente pode ser descrito da seguinte forma:

  - Estratégia
    - Desenvolvimento e implementação de estratégia de negócio
    - Transformação Digital
    - Gestão e capacitação de pessoas
  - Core
    - Gestão de Marketing
      - Seleção de Público Alvo
      - Divulgação da marca
      - Desenvolvimento de novos produtos
      - Monitoramento e desenvolvimento de novas oportunidades
    - Operação de Vendas
      - Conversão de Vendas
      - Organização do Layout
      - Atendimento aos clientes
    - Operação de Pós-vendas
      - Gerenciamento de devolução e troca de mercadorias
    - Operações Logísticas
      - Gerenciamento de estoque
      - Planejamento de Compras
      - Recebimento de Mercadorias
      - Segmentação de produtos
      - Armazenamento de produtos
      - Distribuição de produtos
    - Gerenciamento Financeiro
      - Gerenciamento de contas a pagar e receber
      - Planejamento de risco financeiro
    - Gestão de Tecnologia da Informação
      - Gerenciamento de inventário de ativos de tecnologia
      - Gerenciamento de infraestrutura de tecnologia
      - Gerenciamento de dados e aplicações de sistemas de informação
      - Gerenciamento de demandas de software e tecnologia
      - Gerenciamento de governança e segurança da informação
  - Suporte
      - Operações de contabilidade
      - Gestão de RH
      - Gerenciamento de Infraestrutura

Como não possui budget para investir inicialmente na construção da plataforma digital, a estratégia será focada na abertura de duas novas lojas, conforme mapeamento realizado pelo time de desenvolvimento de estratégias e pelo time marketing.

Nesse sentido, será necessário o desenvolvimento de novas capacidades de negócio, assim como a evolução de outras já existentes. Capacidades como "Gerenciamento Financeiro" que antes eram executadas por processos de negócio manuais, utilizando planilhas em MS Excel como ferramenta, precisarão ser automatizadas para que as demais lojas possam reportar em tempo hábil as operações de entrada e saída de caixa. Essa integração de informações financeiras de todas as lojas fornecerá mecanismos para auxiliar a tomada de decisão quanto à necessidade de ações para mitigação de riscos financeiros, além de permitir que nosso cliente realize melhores operações no mercado baseado na saúde financeira da empresa.

Na segunda fase do processo de expansão, capacidades estratégicas como "Transformação Digital" e capacidades Core como "Gestão de Tecnologia da Informação" precisarão ser implementadas com suas respectivas áreas e processos de negócio para que a estratégia de negócio seja executada e conclua a proposta de valor prevista pelo novo modelo de negócios do nosso cliente.

## Arquitetura de Soluções

Nessa seção eu apresento alteranativas para a implementação da solução proposta no desafio. Embora sejam apresentadas diferentes versões, isso não configura a definição de AT's (Arquiteturas de Transição). O objetivo de apresentar essas diferentes versões é apenas para demonstrar variabilidade de opções e "arsenal técnico".

Para uma proposta adequeda de arquiteturas de transição seria necessário, e ideal, o mapeamento do estado atual da arquitetura de soluções (aplicações e dados) e, a partir disso, propor uma arquitetura TO-BE, incluindo a definição de uma arquitetura Landscape. Então seria possível definir arquiteturas de transição partindo do princípio de executar àqueles que proporcionassem maior valor agregado à organização de forma imediata, considerando as integrações entre elas e a co-existência com sistemas e mecanismos legados durante a construção dessas transições.

Versão 1) A arquitetura de soluções proposta para a primeira versão desse projeto utiliza um load balancer externo que será responsável por rotear o tráfego das requisições feitas pela aplicação cliente para o cluster de aplicação que estará disponível em uma subrede privada. Esse load balancer será acionado por um proxy reverso que atua juntamente com uma camada de WAF e CDN.

![alt text](https://github.com/FSilote/gestao_fluxo_caixa/blob/main/assets/arq_solucoes_v3.png)

Versão 2) A segunda versão da arquitetura de soluções apresenta uma evolução da versão inicial. Nesse modelo, assumimos que nosso cliente atuará com diferentes aplicações clientes, tais como Mobile e Web. Nesse caso, substituímos o balanceador externo por um API Gateway e utilizamos o balanceador interno para receber as chamadas que forem repassadas pelo API Gateway.

![alt text](https://github.com/FSilote/gestao_fluxo_caixa/blob/main/assets/arq_solucoes_v2.png)

Versão 3) A versão número 3 apresenta uma proposta de utilização de BFF's para as aplicações cliente. Essa camada de BFF acionará um load balancer interno que é responsável por direcionar as requisições dos BFF aos serviços correspondentes.

![alt text](https://github.com/FSilote/gestao_fluxo_caixa/blob/main/assets/arq_solucoes_v1.png)

## Arquitetura de Tecnologia

A arquitetura de tecnologia proposta contempla a arquitetura de aplicações e a arquitetura de dados. Essas arquiteturas serão implementadas no provedor de Cloud AWS. Nessa solução, adotamos a SNS e a SQS como mecanismos de notificação e filas, mas outros serviços para mensageria poderiam ser utilizados, tais como AWS MQ, Kinesis ou mesmo um Kafka em modelo de IaaS. Para a implementação do código foi utilizado o RabbitMQ, para simplificar a execução do projeto em ambiente local.

![alt text](https://github.com/FSilote/gestao_fluxo_caixa/blob/main/assets/arq_tecnologia.png)

## Arquitetura de software

A solução de software construída se baseia em microserviços seguindo o padrão EDA (Event Driven Architecture) como estilo arquitetural para comunicação entre os serviços. Para tanto, utilizamos o RabbitMQ como broker de mensageria, em uma implementação simples que será evoluída no futuro par correponder à uma construção de Service Bus mais robusta. O RabbitMQ foi escolhido para demonstrar variabilidade técnica e de ferramentas, além de ser uma solução simples para excução em ambiente local. No entanto, outras opções de mensageria e Service Bus poderiam ter sido adotadas, como a SNS/SQS na AWS, Azure Service Bus ou mesmo implementações de Kafka, sejam em modelo PaaS ou IaaS.

O ecossistema desenvolvidos é composto por dois serviços independentes: lancamentos e Saldos. Cada um deles foi construído em .NET8, utilizando EntityFramework como ORM. Para a persistência de dados foi utilizado o EntityFramework in Memory, para simplificar a execução local dos serviços. Além disso, os serviços desenvolvidos adotam os seguintes padrões arquiteturais:

- Command Handler Pattern para orquestrar as operações de escrita na aplicação. Para tanto foi utilizado o pacote MediatR.
- Command and Query Responsibility Segregation. Nesse caso também é adotado o MeditR como pacote para a mediação. A diferença é que o implementação dos Handlers (QueryHandler) é realizada diretamente na camada de adaptares, o que nos permite alterar a implementação da Query sem impactar nas camadas Core (Domain e Application)
- Hexagonal Architecture, com a adoção de Portas e Adaptadores de infraestrutura.
- Clean Architecture, com a segregação e implementação de cada caso de uso específico


## Como executar esse projeto

1) Executar pelo Visual Studio (ou outra IDE)

- Faça clone desse projeto em seu computador
- Certifique-se de que possui o docker instalado
- Certifique-se de que possui o Visual Studio instalado, ou .NET SDK com runtime para NET8.
- Execute o comando a seguir para inicializer um container de RabbitMQ:
```
docker container run -d -it --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:4.0.5-management
```
- Abra o arquivo CodeChallenger.sln no Visual Studio
- Faça um Clean e Rebuild da solução
- Configure a solução para executar mais de um projeto ao mesmo tempo (Clique com o Botão direito na solução -> Configure Startup Projects -> Marque os dois projetos WebApi como iniciais).
- Execute a solução (F5)
- Identifique o Swagger da aplicação "Lançamentos"
- Faça Login utilizando o endpoint auth/login (user: gerente@teste.com ou atendente@teste.com | senha: teste)
- Autentique no Swagger utilizando o token recebido no passo anterior
- Execute as rotas de POST para realizar lançamentos (à vista ou parcelados)
- Identifique o Swagger da aplicação "Saldo"
- Execute as rotas de relatórios e de Saldos (utilize o mesmo token obtido anteriormente para autenticar)

## Débitos Técnicos e próximos passos

- Criação dos arquivos dockerfile para construção das imagens dos serviços (Lançamentos e Saldo)
- Criação do Docker Compose para simplificar a execução local e ser utilizado como base nos testes de integração e end-to-end
- Implementação dos testes unitários na camada Core (Domain e Application)
- Implementação dos brokers e validadores a serem utilizados nos testes de integração
- Implentação dos testes de integração na camada de Aplicação e Adaptadores
- Há muita cópia de código entre os projetos, tais como o mecanismo de autenticação e os adaptadores. A alteranativa será externalizar um novo serviço para autenticação e SSO (AuthService) e criar sdk's para itens comuns, como startup da aplicação e adapters e transformá-los em pacotes Nuget, para otimizar o reuso de código.
- Implementar auditoria, registrando o usuário que realizou cada operação
- Implementar DataAnnotations para validar os requests (commands de entrada)
- Corrigir o versionamento da API
- Configurar pipelines para CI/CD utilizando o Github Actions, incluindo steps para execução dos testes unitários e de integração.
- Evoluir a modelagem das entidades da aplicação (Domain) para seguir um modelo de herança, tendo a entidande "Operacao" como base e os tipos "Receita" e "Despesa" como especialistas, utilizando a estratégia de uma tabela por hierarquia. Com isso a categoria poderia ser subdividida para cada tipo específico de operação.
- Implementar as Queries utilizando Dapper
- Evoluir a implementação do RabbitMQ