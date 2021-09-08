using AnalisadorSSIS.Modelo.Enums;
using System.Collections.Generic;

namespace AnalisadorSSIS.Modelo
{
    internal class Executavel
    {
        public string Nome { get; set; }
        public string Id { get; set; }
        public TipoExecutavel Tipo { get; set; }
        public string IdConexao { get; set; }
        public bool Habilitado { get; set; }
        public bool Parametrizado { get; set; }
        public string Conteudo { get; set; }
        public string ConteudoExpressao { get; set; }
        public IList<Executavel> Descendentes { get; set; } = new List<Executavel>();

        public Executavel(string nome, string tipo)
        {
            Nome = nome;
            Tipo = ObterTipo(tipo);
        }

        private TipoExecutavel ObterTipo(string tipoDescricao)
        {
            switch (tipoDescricao)
            {
                case "Microsoft.Package":
                case "Microsoft.ExecutePackageTask":
                    return TipoExecutavel.Pacote;
                case "Microsoft.ScriptTask":
                    return TipoExecutavel.ScriptTask;
                case "Microsoft.ExpressionTask":
                    return TipoExecutavel.Expressao;
                case "Microsoft.ExecuteSQLTask":
                    return TipoExecutavel.SqlTask;
                case "STOCK:SEQUENCE":
                    return TipoExecutavel.Sequencia;
                case "Microsoft.Pipeline":
                    return TipoExecutavel.Pipeline;
                case "Microsoft.ManagedComponentHost":
                case "Microsoft.OLEDBDestination":
                case "Microsoft.OLEDBSource":
                    return TipoExecutavel.ComponenteDados;
                case "Microsoft.DataConvert":
                    return TipoExecutavel.ComponenteFormatacao;
                default:
                    return TipoExecutavel.Indefinido;
            }
        }
    }
}
