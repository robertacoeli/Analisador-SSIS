using System.Collections.Generic;
using System.IO;

namespace AnalisadorSSIS.Modelo
{
    internal class Projeto
    {
        public Solucao Solucao { get; set; }
        public string CaminhoCompleto { get; set; }
        public string Nome => Path.GetFileNameWithoutExtension(CaminhoCompleto);
        public IList<Conexao> Conexoes { get; set; } = new List<Conexao>();
        public IList<Variavel> Variaveis { get; set; } = new List<Variavel>(); 
        public IList<Pacote> Pacotes { get; set; } = new List<Pacote>(); 

        public Projeto (Solucao solucao, string caminhoCompleto)
        {
            Solucao = solucao;
            CaminhoCompleto = caminhoCompleto;
        }
    }
}
