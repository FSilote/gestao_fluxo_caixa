﻿namespace CodeChallenger.Saldo.Domain.Entity
{
    public enum Categoria
    {
        ENTRADA_POR_VENDA_PRODUTO_A_VISTA,
        ENTRADA_POR_PARCELA_VENDA_PRODUTO,
        ENTRADA_POR_RENDIMENTO_INVESTIMENTO,
        ENTRADA_POR_VENDA_ATIVO,
        ENTRADA_POR_EMPRESTIMO_TOMADO,
        ENTRADA_POR_CHAMADA_CAPITAL_SOCIOS,

        SAIDA_PARA_PAGAMENTO_FORNECEDOR,
        SAIDA_PARA_PAGAMENTO_SALARIOS,
        SAIDA_PARA_PAGAMENTO_CONTA_AGUA,
        SAIDA_PARA_PAGAMENTO_CONTA_ENERGIA,
        SAIDA_PARA_PAGAMENTO_ALUGUEL,
        SAIDA_PARA_PAGAMENTO_IMPOSTOS_ENCARGOS,
        SAIDA_PARA_PAGAMENTO_SERVICOS_TERCEIROS,
        SAIDA_PARA_PAGAMENTO_JUROS_EMPRESTIMOS,
        SAIDA_PARA_PAGAMENTO_ATIVO_ADQUIRIDO,
        SAIDA_PARA_PAGAMENTO_LUCROS_SOCIOS
    }
}
