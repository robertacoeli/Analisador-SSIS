using AnalisadorSSIS.Modelo.Enums;

namespace AnalisadorSSIS.Modelo
{
    internal class Variavel
    {
        public string Nome { get; set; }
        public string NomeReferencia => $"{Escopo}::{Nome}";
        public string Id { get; set; }
        public Escopo Escopo { get; set; }
        public string Conteudo { get; set; }
        public string ConteudoExpressao { get; set; }
    }
}
