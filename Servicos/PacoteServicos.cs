using AnalisadorSSIS.Modelo;
using AnalisadorSSIS.Modelo.Enums;
using AnalisadorSSIS.Servicos.Extensoes;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;

namespace AnalisadorSSIS.Servicos
{
    internal static class PacoteServicos
    {
        // Analisa SQL tasks
        private static void AnalisarExecutavelSqlTask(this XElement elemento, ref Executavel executavel)
        {
            var sqlTaskData = elemento.ObterSqlTaskData();
            executavel.IdConexao = sqlTaskData.ObterConexaoTarefa();
            executavel.Conteudo = sqlTaskData.ObterSqlStatementSource().FormatarCaracteresSql();
            executavel.ConteudoExpressao = executavel.Parametrizado ? elemento.ObterSqlStatementSourceExpressao() : string.Empty;
        }

        // Analisa a tarefa de fluxo de dados/pipeline
        private static void AnalisarPipeline(this XElement elemento, ref Executavel executavel)
        {
            foreach (var c in elemento.ObterComponentes())
            {
                Executavel componente = new Executavel(nome: string.Format("{0}/{1}", executavel.Nome, c.ObterNomeParametro()), tipo: c.ObterTipoComponente());
                componente.Id = executavel.Id + "_componente";
                componente.Habilitado = c.ExecutavelEstaHabilitado() && executavel.Habilitado;

                if (componente.Tipo == TipoExecutavel.ComponenteDados)
                {
                    componente.IdConexao = c.ObterConexaoComponente();
                    componente.Conteudo = c.ObterConteudoComponente();
                    componente.ConteudoExpressao = c.ObterConteudoParametrizadoComponente();
                    componente.Parametrizado = !string.IsNullOrEmpty(componente.ConteudoExpressao);
                }

                executavel.Descendentes.Add(componente);
            }
        }

        // Analisa a tarefa de execucao de pacote
        private static void AnalisarExecutarPacote(this XElement elemento, ref Executavel executavel)
        {
            XElement tarefa = elemento.ObterTarefaExecucaoPacote();
            executavel.Conteudo = tarefa.ObterNomePacoteExecucao();
            executavel.ConteudoExpressao = tarefa.ObterParametrosPacoteExecucao();
        }

        // Analisa o script task
        private static void AnalisarScriptTask(this XElement elemento, ref Executavel executavel)
        {
            XElement tarefa = elemento.ObterScriptProject();
            executavel.ConteudoExpressao = tarefa.ObterVariaveisScript();
        }

        // Analisa a tarefa de expressao
        private static void AnalisarExpressionTask(this XElement elemento, ref Executavel executavel)
        {
            executavel.ConteudoExpressao = elemento.ObterExpressionTask().ObterExpressaoVariavelUsuario();
            executavel.Parametrizado = true;
        }

        // Analisa a tarefa de sequencia (sequence container)
        private static void AnalisarSequenceContainer(this XElement elemento, ref Executavel executavel)
        {
            executavel.Descendentes = elemento.AnalisarExecutaveis();
        }

        // Analisa o executavel (dados gerais)
        private static Executavel AnalisarBasicoExecutavel(this XElement elemento)
        {
            Executavel executavel = new Executavel(nome: elemento.ObterNome(), tipo: elemento.ObterTipoExecutavel());
            executavel.Id = elemento.ObterId();
            executavel.Parametrizado = elemento.ExecutavelTemExpressao();
            executavel.Habilitado = elemento.ExecutavelEstaHabilitado();
            return executavel;
        }

        // Analisa o executavel por tipos
        private static Executavel AnalisarExecutavel(this XElement elemento)
        {
            Executavel executavel = elemento.AnalisarBasicoExecutavel();

            switch (executavel.Tipo)
            {
                case TipoExecutavel.SqlTask:
                    elemento.AnalisarExecutavelSqlTask(ref executavel);
                    break;
                case TipoExecutavel.Pipeline:
                    elemento.AnalisarPipeline(ref executavel);
                    break;
                case TipoExecutavel.Pacote:
                    elemento.AnalisarExecutarPacote(ref executavel);
                    break;
                case TipoExecutavel.ScriptTask:
                    elemento.AnalisarScriptTask(ref executavel);
                    break;
                case TipoExecutavel.Expressao:
                    elemento.AnalisarExpressionTask(ref executavel);
                    break;
                case TipoExecutavel.Sequencia:
                    elemento.AnalisarSequenceContainer(ref executavel);
                    break;
                default: // outro -- tipos nao analisados
                    break;
            }

            return executavel;
        }

        // Analisa as tarefas para execucao no pacote
        private static List<Executavel> AnalisarExecutaveis(this XElement elemento)
        {
            List<Executavel> lista = new List<Executavel>();

            foreach (XElement e in elemento.ObterExecutaveis())
                lista.Add(e.AnalisarExecutavel());

            return lista;
        }

        // Analisa as variaveis do pacote
        private static List<Variavel> AnalisarVariaveis(this XElement elemento)
        {
            List<Variavel> lista = new List<Variavel>();

            // parametros do pacote
            foreach (XElement p in elemento.ObterParametrosPacote())
            {
                Variavel variavel = new Variavel();
                variavel.Nome = p.ObterNome();
                variavel.Escopo = Escopo.Package;
                variavel.Id = p.ObterId();
                variavel.Conteudo = p.ObterValorParametroPacote();
                lista.Add(variavel);
            }

            // variaveis de usuario
            foreach (XElement v in elemento.ObterVariaveis())
            {
                Variavel variavel = new Variavel();
                variavel.Nome = v.ObterNome();
                variavel.Escopo = Escopo.User;
                variavel.Id = v.ObterId();
                variavel.Conteudo = v.ObterValorVariavelUsuario();
                variavel.ConteudoExpressao = v.ObterExpressaoVariavelUsuario();
                lista.Add(variavel);
            }

            return lista;
        }

        // Analisa as conexoes do pacote
        private static List<Conexao> AnalisarConexoes(this XElement elemento)
        {
            List<Conexao> conexoes = new List<Conexao>();

            foreach (var c in elemento.ObterConnectionManagers())
                conexoes.Add(c.ObterConexao());

            return conexoes;
        }

        // Analisa o pacote
        public static Pacote Executar(string nomeArquivo)
        {
            XElement arquivo = XElement.Load(Path.Combine(nomeArquivo));

            Pacote pacote = new Pacote(arquivo.ObterNome(), nomeArquivo);
            pacote.Habilitado = arquivo.ExecutavelEstaHabilitado();
            pacote.Conexoes = arquivo.AnalisarConexoes();
            pacote.Variaveis = arquivo.AnalisarVariaveis();
            pacote.Executaveis = arquivo.AnalisarExecutaveis();

            return pacote;
        }
    }
}
