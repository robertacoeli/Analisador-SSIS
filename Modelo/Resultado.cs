using AnalisadorSSIS.Modelo.Enums;

namespace AnalisadorSSIS.Modelo
{
    internal class Resultado
    {
        public Pacote Pacote { get; set; }
        public string Nome { get; set; }
        public TipoItem TipoItem { get; set; }
        public TipoResultado TipoResultado { get; set; }
        public bool Habilitado { get; set; }
        public string IdConexao { get; set; }
        public string ConteudoCorrespondente { get; set; }
    }
}
