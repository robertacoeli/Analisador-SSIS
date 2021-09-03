using System.Collections.Generic;

namespace AnalisadorSSIS.Modelo
{
    internal class Pacote
    {
        public string Nome { get; set; }
        public string NomeArquivo { get; set; }
        public bool Habilitado { get; set; } // indica se o pacote está habilitado ou desabilitado
        public IList<Conexao> Conexoes { get; set; } = new List<Conexao>();
        public IList<Variavel> Variaveis { get; set; } = new List<Variavel>();
    }
}
