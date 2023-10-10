using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirebirdSql.Data.FirebirdClient;

namespace TesteApiNuvemShop.AcessoDados
{
    public class AcessoFirebird
    {
        //private static readonly AcessoFirebird instanciaFirebird = new AcessoFirebird();
        //public static AcessoFirebird pegarInstancia()
        //{
        //    return instanciaFirebird;
        //}

        string strConexao = @"DataSource=localhost; Database=C:\Dados\store.FDB; username= SYSDBA; password = masterkey";

        #region Conexao com banco
        private FbConnection CriarConexao()
        {
            return new FbConnection(strConexao);
        }
        #endregion

        #region Parametros que vão para o banco
        private FbParameterCollection fbParameterCollection = new FbCommand().Parameters;
        #endregion

        #region Limpar parametros
        public void LimparParametros()
        {
            fbParameterCollection.Clear();
        }
        #endregion

        #region Adicionar parametros
        public void AdicionarParametros(string nomeParametro, object valorParametro)
        {
            fbParameterCollection.Add(new FbParameter(nomeParametro, valorParametro));
        }
        #endregion

        #region Persistencia de dados (Inserir, alterar, excluir)
        public object ExecutarManipulacao(CommandType commandType, string nomeProcOuTextoSql)
        {
            try
            {
                //Criar conexao
                using (FbConnection fbConnection = CriarConexao())
                {
                    //Abre conexao
                    fbConnection.Open();
                    //Cria o comando
                    FbCommand fbCommand = fbConnection.CreateCommand();
                    //Coloca o Tipo de comando e nome do comando
                    fbCommand.CommandType = commandType;
                    fbCommand.CommandText = nomeProcOuTextoSql;
                    fbCommand.CommandTimeout = 7200; //segundos
                                                     //Adiciona os paramentros
                    foreach (FbParameter fbParameter in fbParameterCollection)
                    {
                        fbCommand.Parameters.Add(new FbParameter(fbParameter.ParameterName, fbParameter.Value));
                    }
                    //executa o comando e retorna                 
                    return fbCommand.ExecuteScalar();
                }
            }
            catch (FbException error)
            {
                throw new Exception(error.Message);
            }
            catch (Exception error)
            {
                throw new Exception(error.Message);
            }
        }
        #endregion

        #region Consultar dados no banco
        public DataTable ExecutarConsultar(CommandType commandType, string nomeProcOuTextoSql)
        {
            try
            {
                using (FbConnection fbConnection = CriarConexao())
                {
                    fbConnection.Open();
                    FbCommand fbCommand = fbConnection.CreateCommand();
                    fbCommand.CommandType = commandType;
                    fbCommand.CommandText = nomeProcOuTextoSql;
                    fbCommand.CommandTimeout = 7200;

                    foreach (FbParameter fbParameter in fbParameterCollection)
                    {
                        fbCommand.Parameters.Add(new FbParameter(fbParameter.ParameterName, fbParameter.Value));
                    }
                    // Cria um adaptador
                    FbDataAdapter fbDataAdapter = new FbDataAdapter(fbCommand);
                    // Cria uma tabela de dados vazia
                    DataTable dataTable = new DataTable();
                    //Preenche o datatable
                    fbDataAdapter.Fill(dataTable);

                    return dataTable;
                }
            }
            catch (FbException error)
            {
                throw new Exception(error.Message);
            }
            catch (Exception error)
            {
                throw new Exception(error.Message);
            }
        }
        #endregion

        public Object ExecutarConsulta2(CommandType commandType, string nomeProcOuTextoSql)
        {
            try
            {
                using (FbConnection fbConnection = CriarConexao())
                {
                    fbConnection.Open();
                    FbCommand fbCommand = fbConnection.CreateCommand();
                    fbCommand.CommandType = commandType;
                    fbCommand.CommandText = nomeProcOuTextoSql;
                    fbCommand.CommandTimeout = 7200;

                    foreach (FbParameter fbParameter in fbParameterCollection)
                    {
                        fbCommand.Parameters.Add(new FbParameter(fbParameter.ParameterName, fbParameter.Value));
                    }
                    return fbCommand.ExecuteScalar();
                }
            }
            catch (Exception error)
            {
                throw new Exception(error.Message);
            }
        }
    }
}
