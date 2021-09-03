using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalisadorSSIS.Modelo
{
    internal class Projeto
    {
        public Solucao Solucao { get; set; }
        public string NomeCompleto { get; set; }
        public string Nome => Path.GetFileName(NomeCompleto);
        public IList<Conexao> Conexoes { get; set; } = new List<Conexao>();
        public IList<Variavel> Variaveis { get; set; } = new List<Variavel>(); 

        public Projeto (Solucao solucao, string caminhoCompleto)
        {
            Solucao = solucao;
            NomeCompleto = caminhoCompleto;
        }
    }
}
