using AnalisadorSSIS.Modelo.Enums;
using AnalisadorSSIS.Modelo.Extensoes;

namespace AnalisadorSSIS.Modelo
{
    internal class Resultado
    {
        public Pacote Pacote { get; set; }
        public string Nome { get; set; }
        public TipoItem TipoItem { get; set; }
        public string TipoItemStr => TipoItem.GetDescription();
        public TipoResultado TipoResultado { get; set; }
        public string TipoResultadoStr => TipoResultado.GetDescription();
        public bool Habilitado { get; set; }
        public string IdConexao { get; set; }
        public string ConteudoCorrespondente { get; set; }
    }
}
