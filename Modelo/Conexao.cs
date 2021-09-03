using AnalisadorSSIS.Modelo.Enums;

namespace AnalisadorSSIS.Modelo
{
    internal class Conexao
    {
        public string NomeArquivo { get; set; }
        public string Nome { get; set; }
        public string Id { get; set; }
        public string ConnectionString { get; set; }
        public string TipoConexao { get; set; } // pode virar enum --> OLEDB, ADO.NET (Ingres), ODBC, EXCEL, etc
        public Escopo Escopo { get; set; }
    }
}
