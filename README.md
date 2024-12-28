## Desafio de Arquitetura de Soluções

Nesse repositório você encontrará o projeto proposto para o desafio de arquitetura de soluções e arquitetura de software. O desafio consiste na proposição de uma para atender à um comerciante que precisa controlar seu fluxo de caixa diário com lançamentos de débito e crédito, além de necessitar de um relatório diário de saldo consolidado.

## Sumário

- Introdução (arquitetura de negócios)
- Arquitetura de Soluções
- Arquitetura de Tecnologia
- Arquitetura de Software
- Como executar esse projeto

## Introdução

Como não há muitas informações à cerca do situação atual da empresa para que pudesse ser traçado um mapeamento AS-IS da arquitetura corporativa, foram assumidas as seguintes premissas para guiar a construção desse desafio:

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

Versão 1) A arquitetura de soluções proposta para a primeira versão desse projeto utiliza um load balancer externo que será responsável por rotear o tráfego das requisições feitas pela aplicação cliente para o cluster de aplicação que estará disponível em uma subrede privada. Esse load balancer será acionado por um proxy reverso que atua juntamente com uma camada de WAF e CDN.

![alt text](https://github.com/FSilote/gestao_fluxo_caixa/blob/main/assets/arq_solucoes_v3.png)

Versão 2) A segunda versão da arquitetura de soluções apresenta uma evolução da versão inicial. Nesse modelo, assumimos que nosso cliente atuará com diferentes aplicações clientes, tais como Mobile e Web. Nesse caso, substituímos o balanceador externo por um API Gateway e utilizamos o balanceador interno para receber as chamadas que forem repassadas pelo API Gateway.

![alt text](https://github.com/FSilote/gestao_fluxo_caixa/blob/main/assets/arq_solucoes_v2.png)

Versão 3) A versão número 3 apresenta uma proposta de utilização de BFF's para as aplicações cliente. Essa camada de BFF acionará um load balancer interno que é responsável por direcionar as requisições dos BFF aos serviços correspondentes.

![alt text](https://github.com/FSilote/gestao_fluxo_caixa/blob/main/assets/arq_solucoes_v1.png)

## Arquitetura de Tecnologia

A arquitetura de tecnologia proposta contempla a arquitetura de aplicações e a arquitetura de dados. Essas arquiteturas serão implementadas no provedor de Cloud AWS.

![alt text](https://github.com/FSilote/gestao_fluxo_caixa/blob/main/assets/arq_tecnologia.png)

## Arquitetura de software

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
- Execute a solução (F5)
- Identifique o Swagger da aplicação "Lançamentos"
- Faça Login utilizando o endpoint auth/login (user: test@test.com | senha: 123456)
- Autentique no Swagger utilizando o token recebido no passo anterior
- Execute as rotas de POST para realizar lançamentos (à vista ou parcelados)
- Identifique o Swagger da aplicação "Saldo"
- Execute as rotas de relatório de Saldos

2) Docker Compose