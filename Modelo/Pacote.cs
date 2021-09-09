using AnalisadorSSIS.Servicos.Extensoes;
using System.Collections.Generic;
using System.IO;

namespace AnalisadorSSIS.Modelo
{
    internal class Pacote
    {
        public string Nome { get; set; }
        public string NomeArquivo { get; set; }
        public bool Habilitado { get; set; } // indica se o pacote está habilitado ou desabilitado
        public IList<Conexao> Conexoes { get; set; } = new List<Conexao>();
        public IList<Variavel> Variaveis { get; set; } = new List<Variavel>();
        public IList<Executavel> Executaveis { get; set; } = new List<Executavel>();

        public Pacote(string nome, string nomeArquivo)
        {
            Nome = Path.GetFileNameWithoutExtension(nome);
            NomeArquivo = nomeArquivo;
        }
    }
}
