using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace AnalisadorSSIS.Servicos.Extensoes
{
    internal static class XElementExtensions
    {
        // Verifica se atributo existe
        public static bool ExisteAtributo(this XElement elemento, string nomeAtributo)
        {
            return elemento.Attributes()
                           .Any(x => x.Name.LocalName.Equals(nomeAtributo, StringComparison.InvariantCultureIgnoreCase));
        }

        // Obtem os atributos do arquivo XML
        public static string ObterAtributo(this XElement elemento, string nomeAtributo)
        {
            if (elemento.ExisteAtributo(nomeAtributo))
                return elemento.Attributes()
                               .Where(x => x.Name.LocalName.Equals(nomeAtributo, StringComparison.InvariantCultureIgnoreCase))
                               .First()
                               .Value;
            else
                return string.Empty;
        }

        // Verifica se elemento existe
        public static bool ExisteElemento(this XElement elemento, string nomeElemento)
        {
            return elemento.Elements()
                           .Any(x => x.Name.LocalName.Equals(nomeElemento, StringComparison.InvariantCultureIgnoreCase));
        }

        // Verifica se existe um elemento com determinado atributo
        public static bool ExisteDescendenteComAtributo(this XElement elemento, string nomeElemento, string nomeAtributo, string valorAtributo)
        {
            var descendentes = elemento.ObterDescendentes(nomeElemento: nomeElemento);

            if (descendentes.Count() <= 0)
                return false;

            return descendentes.Any(x => x.Attributes()
                                          .Any(y => y.Name.LocalName.Equals(nomeAtributo, StringComparison.InvariantCultureIgnoreCase)
                                                    && y.Value.Equals(valorAtributo, StringComparison.InvariantCultureIgnoreCase)));
        }

        // Obtem o elemento descendente (imediatamente abaixo ao elemento na hierarquia)
        public static XElement ObterDescendente(this XElement elemento, string nomeElemento)
        {
            return elemento.Elements()
                           .Where(x => x.Name.LocalName.Equals(nomeElemento, StringComparison.InvariantCultureIgnoreCase))
                           .FirstOrDefault();
        }

        // Obtem o elemento descendente (imediatamente abaixo ao elemento na hierarquia)
        public static XElement ObterDescendenteContendo(this XElement elemento, string nomeContido)
        {
            return elemento.Elements()
                           .Where(x => x.Name.LocalName.IndexOf(nomeContido, StringComparison.InvariantCultureIgnoreCase) >= 0)
                           .FirstOrDefault();
        }

        // Obtem os elementos descendentes com determinado titulo
        public static IEnumerable<XElement> ObterDescendentes(this XElement elemento, string nomeElemento)
        {
            return elemento.Elements()
                           .Where(x => x.Name.LocalName.Equals(nomeElemento, StringComparison.InvariantCultureIgnoreCase));
        }

        // Obtem os elementos descendentes com determinado atributo
        public static XElement ObterDescendenteComAtributo(this XElement elemento, string nomeElemento, string nomeAtributo, string valorAtributo)
        {
            return elemento.ObterDescendentes(nomeElemento: nomeElemento)
                           .Where(x => x.Attributes()
                                        .Any(y => y.Name.LocalName.Equals(nomeAtributo, StringComparison.InvariantCultureIgnoreCase)
                                             && y.Value.Equals(valorAtributo, StringComparison.InvariantCultureIgnoreCase)))
                           .FirstOrDefault();
        }

        // Obtem os elementos descendentes cujo atributo contem determinado valor
        public static XElement ObterDescendenteCujoAtributoContem(this XElement elemento, string nomeElemento, string nomeAtributo, string valorContidoAtributo)
        {
            return elemento.ObterDescendentes(nomeElemento: nomeElemento)
                            .Where(x => x.Attributes()
                                         .Any(y => y.Name.LocalName.Equals(nomeAtributo, StringComparison.InvariantCultureIgnoreCase)
                                              && y.Value.IndexOf(valorContidoAtributo, StringComparison.InvariantCultureIgnoreCase) >= 0))
                            .FirstOrDefault();
        }
    }
}
