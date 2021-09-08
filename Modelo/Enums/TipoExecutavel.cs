using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalisadorSSIS.Modelo.Enums
{
    internal enum TipoExecutavel
    {
        Indefinido, // O atributo nao existe no elemento
        ComponenteFormatacao, // Serve para todos os executaveis que apenas cumprem o papel de formatar os dados ou converte-los, de forma que nao indicam tabelas alteradas
        ComponenteDados, // Indica um componente de dados do fluxo de dados/pipeline (origem ou destino de dados)
        Pacote,   // Pacote (o proprio ou outro sendo executado dentro do pacote analisado)
        SqlTask, // Tarefa de Execucao de SQL
        ScriptTask, // Tarefa de Execucao de Script
        Pipeline, // Pipeline = Fluxo de Dados (Data Flow)
        Sequencia, // Sequence Container
        Expressao // Tarefa de expressao
    }
}
