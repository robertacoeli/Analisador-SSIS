using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalisadorSSIS.Modelo
{
    internal class Solucao
    {
        public string nomeArquivoCompleto { get; set; }
        public string nomeArquivo => Path.GetFileName(nomeArquivoCompleto);
        public string diretorio => Path.GetDirectoryName(nomeArquivoCompleto);
        public List<string> projetos { get; set; } = new List<string>();

        public Solucao (string arquivoSolucao)
        {
            nomeArquivoCompleto = arquivoSolucao;
        }
    }
}
