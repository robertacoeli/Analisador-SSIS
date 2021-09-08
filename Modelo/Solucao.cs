using System.Collections.Generic;
using System.IO;

namespace AnalisadorSSIS.Modelo
{
    internal class Solucao
    {
        public string NomeArquivoCompleto { get; set; }
        public string NomeArquivo => Path.GetFileNameWithoutExtension(NomeArquivoCompleto);
        public string Diretorio => Path.GetDirectoryName(NomeArquivoCompleto);
        public List<string> Projetos { get; set; } = new List<string>();

        public Solucao (string arquivoSolucao)
        {
            NomeArquivoCompleto = arquivoSolucao;
        }
    }
}
