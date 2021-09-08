using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AnalisadorSSIS.Servicos.Extensoes
{
    internal static class SSISXmlExtensions
    {
        // Obtem o nome de determinado elemento do XML do pacote
        public static string ObterNome(this XElement elemento)
        {
            return elemento.ObterAtributo(nomeAtributo: "ObjectName");
        }

        // Obtem o nome do parametro
        public static string ObterNomeParametro(this XElement elemento)
        {
            return elemento.ObterAtributo(nomeAtributo: "Name");
        }

        // Obtem o id de determinado elemento do XML do pacote
        public static string ObterId(this XElement elemento)
        {
            return elemento.ObterAtributo(nomeAtributo: "DTSID");
        }

        // Obtem o tipo de conexao via string
        public static string ObterTipoConexao(this XElement elemento)
        {
            return elemento.ObterAtributo(nomeAtributo: "CreationName");
        }

        // Obtem id da variavel/parametro do projeto
        public static string ObterIdParametroProjeto(this XElement elemento)
        {
            return elemento.ObterPropriedades().ObterPropriedade(nomePropriedade: "Name", valorPropriedade: "ID");
        }

        // Verifica se executavel esta desabilitado
        public static bool ExecutavelEstaHabilitado(this XElement elemento)
        {
            string desabilitado = elemento.ObterAtributo(nomeAtributo: "Disabled");
            return !string.IsNullOrEmpty(desabilitado) ? !Convert.ToBoolean(desabilitado) : true; // padrao: executavel habilitado
        }

        // Obtem conteudo da variavel do projeto
        public static string ObterValorParametroProjeto(this XElement elemento)
        {
            return elemento.ObterPropriedades().ObterPropriedade(nomePropriedade: "Name", valorPropriedade: "Value");
        }

        // Obtem o conteudo do parametro do pacote
        public static string ObterValorParametroPacote(this XElement elemento)
        {
            return elemento.ObterPropriedade(nomePropriedade: "Name", valorPropriedade: "ParameterValue");
        }

        // Obtem o valor da variavel do pacote
        public static string ObterValorVariavelUsuario(this XElement elemento)
        {
            return elemento.ObterDescendente(nomeElemento: "VariableValue").Value;
        }

        // Obtem a expressao da variavel
        public static string ObterExpressaoVariavelUsuario(this XElement elemento)
        {
            return elemento.ObterAtributo(nomeAtributo: "Expression");
        }

        // Obtem a descricao do tipo de executavel
        public static string ObterTipoExecutavel(this XElement elemento)
        {
            return elemento.ObterAtributo(nomeAtributo: "ExecutableType");
        }

        // Obtem o objeto de coleta de dados
        public static XElement ObterObjetoDados(this XElement elemento)
        {
            return elemento.ObterDescendente(nomeElemento: "ObjectData");
        }

        // Verifica se possui descendente com elemento PropertyExpression 
        public static bool ExecutavelTemExpressao(this XElement elemento)
        {
            return elemento.ExisteElemento(nomeElemento: "PropertyExpression");
        }

        // Obtem o elemento de conexao
        public static XElement ObterConnectionManager(this XElement elemento)
        {
            return elemento.ObterObjetoDados()
                           .ObterDescendenteContendo(nomeContido: "ConnectionManager");
        }

        // Obtem string de conexao
        public static string ObterConnectionString(this XElement elemento)
        {
            return elemento.ObterAtributo(nomeAtributo: "ConnectionString");
        }

        // Obtem id de conexao da tarefa
        public static string ObterConexaoTarefa(this XElement elemento)
        {
            return elemento.ObterAtributo(nomeAtributo: "Connection");
        }

        // Obtem as conexoes do componente
        public static XElement ObterConexoesComponente(this XElement elemento)
        {
            return elemento.ObterDescendente(nomeElemento: "connections");
        }

        // Obtem a conexao do componente
        public static string ObterConexaoComponente(this XElement elemento)
        {
            return elemento.ObterConexoesComponente()
                           .ObterDescendenteCujoAtributoContem(nomeElemento: "connection", nomeAtributo: "name", valorContidoAtributo: "DbConnection")
                           .ObterAtributo(nomeAtributo: "connectionManagerID");
        }

        // Obtem o conteudo parametrizado do componente, o qual sera composto por: OpenRowsetVariable e SqlCommandVariable
        public static string ObterConteudoParametrizadoComponente(this XElement elemento)
        {
            string conteudo = "";
            XElement propriedades = elemento.ObterPropriedades();

            conteudo += propriedades.ObterPropriedade("name", "SqlCommandVariable").FormatarConteudoElemento();
            conteudo += propriedades.ObterPropriedade("name", "OpenRowsetVariable").FormatarConteudoElemento(ehTabela: true);

            return conteudo;
        }

        // Obtem o conteudo do componente, o qual sera composto por: SqlCommand, TableOrViewName e OpenRowset, quando estes existirem
        public static string ObterConteudoComponente(this XElement elemento)
        {
            string conteudo = "";
            XElement propriedades = elemento.ObterPropriedades();

            conteudo += propriedades.ObterPropriedade("name", "SqlCommand").FormatarConteudoElemento();
            conteudo += propriedades.ObterPropriedade("name", "TableOrViewName").FormatarConteudoElemento(ehTabela: true);
            conteudo += propriedades.ObterPropriedade("name", "OpenRowset").FormatarConteudoElemento(ehTabela: true);

            return conteudo;
        }

        // Obtem a SQL Task
        public static XElement ObterSqlTaskData(this XElement elemento)
        {
            return elemento.ObterObjetoDados()
                           .ObterDescendente(nomeElemento: "SqlTaskData");
        }

        // Obtem query SQL no atributo SqlStatementSource
        public static string ObterSqlStatementSource(this XElement elemento)
        {
            return elemento.ObterAtributo(nomeAtributo: "SqlStatementSource");
        }

        // Obtem tarefa de execucao do pacote
        public static XElement ObterTarefaExecucaoPacote(this XElement elemento)
        {
            return elemento.ObterObjetoDados().ObterDescendente(nomeElemento: "ExecutePackageTask");
        }

        // Obtem o script project
        public static XElement ObterScriptProject(this XElement elemento)
        {
            return elemento.ObterObjetoDados().ObterDescendente(nomeElemento: "ScriptProject");
        }

        // Obtem as variaveis do script
        public static string ObterVariaveisScript(this XElement elemento)
        {
            string variaveis = "";

            variaveis += elemento.ObterAtributo(nomeAtributo: "ReadOnlyVariables");
            variaveis += ("," + elemento.ObterAtributo(nomeAtributo: "ReadWriteVariables"));

            return variaveis;
        }

        // Obtem nome do pacote executado
        public static string ObterNomePacoteExecucao(this XElement elemento)
        {
            return elemento.ObterDescendente(nomeElemento: "PackageName").Value;
        }

        // Obtem a tarefa de expressao
        public static XElement ObterExpressionTask(this XElement elemento)
        {
            return elemento.ObterObjetoDados().ObterDescendente(nomeElemento: "ExpressionTask");
        }

        // Obtem conteudo em string dos parametros do pacote execucao
        public static string ObterParametrosPacoteExecucao(this XElement elemento)
        {
            string conteudo = "";

            foreach (var parametro in elemento.ObterDescendentes(nomeElemento: "ParameterAssignment"))
            {
                conteudo += string.Format("{0} = {1} ; ", parametro.ObterDescendente("ParameterName").Value, parametro.ObterDescendente("BindedVariableOrParameterName").Value);
            }

            return conteudo;
        }

        // Obtem parametros
        public static IEnumerable<XElement> ObterParametros(this XElement elemento)
        {
            return elemento.ObterDescendentes(nomeElemento: "Parameter");
        }

        // Obtem os executaveis
        public static IEnumerable<XElement> ObterExecutaveis(this XElement elemento)
        {
            return elemento.ObterDescendente(nomeElemento: "Executables")
                           .Elements();
        }

        // Obtem as variaveis
        public static IEnumerable<XElement> ObterVariaveis(this XElement elemento)
        {
            return elemento.ObterDescendente(nomeElemento: "Variables")
                           .Elements();
        }

        // Obtem os parametros de pacote 
        public static IEnumerable<XElement> ObterParametrosPacote(this XElement elemento)
        {
            if (elemento.ExisteElemento(nomeElemento: "PackageParameters"))
                return elemento.ObterDescendente(nomeElemento: "PackageParameters")
                               .Elements();
            else
                return Enumerable.Empty<XElement>();
        }

        // Obtem o pipeline
        public static XElement ObterPipeline(this XElement elemento)
        {
            return elemento.ObterObjetoDados()
                           .ObterDescendente(nomeElemento: "pipeline");
        }

        // Obtem os componentes (pipeline)
        public static IEnumerable<XElement> ObterComponentes(this XElement elemento)
        {
            var componentes = elemento.ObterPipeline()
                                      .ObterDescendente(nomeElemento: "components");

            if (componentes == null)
                return new List<XElement>();

            return componentes.Elements();
        }

        // Obtem o tipo do componente (pipeline)
        public static string ObterTipoComponente(this XElement elemento)
        {
            return elemento.ObterAtributo(nomeAtributo: "componentClassID");
        }

        // Obtem as conexoes
        public static IEnumerable<XElement> ObterConnectionManagers(this XElement elemento)
        {
            return elemento.ExisteElemento(nomeElemento: "ConnectionManagers") ? elemento.ObterDescendente(nomeElemento: "ConnectionManagers").Elements() : Enumerable.Empty<XElement>();
        }

        // Obtem o sql statement source colocado como expressao
        public static string ObterSqlStatementSourceExpressao(this XElement elemento)
        {
            return elemento.ObterPropriedadeExpressao(nomePropriedade: "Name", valorPropriedade: "SqlStatementSource").Value;
        }

        // Obtem as propriedades
        public static XElement ObterPropriedades(this XElement elemento)
        {
            return elemento.ObterDescendente(nomeElemento: "properties");
        }

        // Obtem propriedade
        public static string ObterPropriedade(this XElement elemento, string nomePropriedade, string valorPropriedade)
        {
            string propriedade = elemento.ExistePropriedade(nomePropriedade: nomePropriedade, valorPropriedade: valorPropriedade) ?
                                 elemento.ObterDescendenteComAtributo(nomeElemento: "property", nomeAtributo: nomePropriedade, valorAtributo: valorPropriedade).Value.Trim()
                                 : string.Empty;

            return propriedade;
        }

        // Verifica propriedade
        public static bool ExistePropriedade(this XElement elemento, string nomePropriedade, string valorPropriedade)
        {
            return elemento.ExisteDescendenteComAtributo(nomeElemento: "property", nomeAtributo: nomePropriedade, valorAtributo: valorPropriedade);
        }

        // Obtem propriedade de expressao
        public static XElement ObterPropriedadeExpressao(this XElement elemento, string nomePropriedade, string valorPropriedade)
        {
            return elemento.ObterDescendenteComAtributo(nomeElemento: "PropertyExpression", nomeAtributo: nomePropriedade, valorAtributo: valorPropriedade);
        }
    }
}
