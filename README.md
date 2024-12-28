## Desafio de Arquitetura de Soluções

Nesse repositório você encontrará o projeto proposto para o desafio de arquitetura de soluções e arquitetura de software. O desafio consiste na proposição de uma para atender à um comerciante que precisa controlar seu fluxo de caixa diário com lançamentos de débito e crédito, além de necessitar de um relatório diário de saldo consolidado.

## Sumário

- Introdução (arquitetura de negócios)
- Arquitetura de Soluções (e possíveis AT's)
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

```
![image info](./assets/arq_solucoes_v3)
```