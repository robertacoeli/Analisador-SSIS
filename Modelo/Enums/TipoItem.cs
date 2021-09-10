using System.ComponentModel;

namespace AnalisadorSSIS.Modelo.Enums
{
    internal enum TipoItem
    {
        [Description("Executável")]
        Executavel,
        [Description("Variável")]
        Variavel,
        [Description("Pacote")]
        Pacote,
        [Description("Conexão")]
        Conexao,
        [Description("Parâmetro de Projeto")]
        ParametroProjeto,
        [Description("Conexão de Projeto")]
        ConexaoProjeto
    }
}
