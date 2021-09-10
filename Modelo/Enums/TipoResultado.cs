using System.ComponentModel;

namespace AnalisadorSSIS.Modelo.Enums
{
    internal enum TipoResultado
    {
        [Description("Conteúdo")]
        Conteudo,
        [Description("Expressão")]
        Expressao,
        [Description("Título do Componente")]
        Titulo
    }
}
