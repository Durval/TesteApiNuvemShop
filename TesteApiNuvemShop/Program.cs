using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using TesteApiNuvemShop.AcessoDados;
using TesteApiNuvemShop.Negocio;
using TesteApiNuvemShop.Objetos;

class Program
{
    static async Task Main(string[] args)
    {
        // token de acesso real obtido durante o processo de autenticação
        string accessToken = "41a1a62c43ef0e8862099be377d4ad57d06ac2a3";
        // ID da loja para a qual você deseja criar o produto
        int storeId = 1203492;
        // no do app e versão criado na NS
        string app = "AppFlima";
        string versao = "1.0";


        #region
        // Configuração do HttpClient
        using (HttpClient httpClient = new HttpClient())
        {
            httpClient.BaseAddress = new Uri($"https://api.tiendanube.com/v1/{storeId}/");
            httpClient.DefaultRequestHeaders.Add("Authentication", "bearer " + accessToken);
            httpClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue(app, versao));

            // Configurar o Timer para executar a cada 5 minutos
            var timer = new Timer(async (state) =>
            {
                CategoryNegocio categoryNegocio = new();
                var cat = categoryNegocio.EnviaCategory();

                for (int i = 0; i < cat.Count; i++)
                {
                   string codigo = cat[i].codigo.ToString();
                    string nome = cat[i].name.pt.ToString();
                   string jsonData = AcessoJson.Serializar(cat[i]);

                   StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                    if (string.IsNullOrEmpty(cat[i].id.ToString()))
                    {
                        // Fazendo a solicitação POST para criar o novo produto
                        HttpResponseMessage response = await httpClient.PostAsync("categories", content);
                        // Verificando se a solicitação foi bem-sucedida
                        if (response.IsSuccessStatusCode)
                        {
                            // Lendo o conteúdo da resposta como uma string JSON
                            string jsonResponse = await response.Content.ReadAsStringAsync();
                            Console.WriteLine(DateTime.Now + " -- Novo Categoria Cadastrada! - " + nome);

                            //Gravando o retorno 
                            CategoryODT.Category retCategory = new();
                            retCategory = CategoryNegocio.Desserializar(jsonResponse);
                            categoryNegocio.RetCategory(retCategory, codigo);
                        }
                        else
                        {
                            Console.WriteLine($"Falha na solicitação!!. Código de status: {response.StatusCode}\n");
                        }
                    }
                    else
                    {
                        // Fazendo a solicitação POST para criar o novo produto
                        HttpResponseMessage response = await httpClient.PutAsync("categories/" + cat[i].id, content);
                        // Verificando se a solicitação foi bem-sucedida
                        if (response.IsSuccessStatusCode)
                        {
                            // Lendo o conteúdo da resposta como uma string JSON
                            string jsonResponse = await response.Content.ReadAsStringAsync();
                            Console.WriteLine(DateTime.Now + " -- Categoria Atualizada! - " + nome);

                            ////Gravando o retorno 
                            //CategoryODT.Category retCategory = new();
                            //retCategory = CategoryNegocio.Desserializar(jsonResponse);
                            //categoryNegocio.RetCategory(retCategory, codigo);
                        }
                        else
                        {
                            Console.WriteLine($"Falha na solicitação!!. Código de status: {response.StatusCode}\n");
                        }

                    }
                }
            }, null, TimeSpan.Zero, TimeSpan.FromMinutes(1));

            // Mantenha o programa em execução
            Console.WriteLine("Pressione Enter para sair...");
            Console.ReadLine();
        }
        #endregion

        #region Post

        //// Configuração do HttpClient
        //using (HttpClient httpClient = new HttpClient())
        //{
        //    httpClient.BaseAddress = new Uri($"https://api.tiendanube.com/v1/{storeId}/");
        //    httpClient.DefaultRequestHeaders.Add("Authentication", "bearer " + accessToken);
        //    httpClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue(app, versao));

        //    // Configurar o Timer para executar a cada 5 minutos
        //    var timer = new Timer(async (state) =>
        //    {
        //        AcessoFirebird acessoFirebird = new();
        //        acessoFirebird.LimparParametros();
        //        acessoFirebird.AdicionarParametros("CODIGO", "100");

        //        DataTable produto1 = acessoFirebird.ExecutarConsultarDataTable(CommandType.Text, "SELECT NOME, 9.99 AS PRICE, false as STOCK_MANAGEMENT, 23 AS STOCK, 1.12 AS WEIGHT, 1.33 AS COST FROM CTPROD WHERE CODIGO = @CODIGO;");


        //        Produto produto = new();
        //        Produto.Variant variant = new();
        //        produto.name = produto1.Rows[0]["NOME"].ToString();
        //        Console.WriteLine("i=");

        //        foreach (DataRow row in produto1.Rows)
        //        {
        //            variant.price = Convert.ToDecimal(row[1]);
        //            variant.stock_management = Convert.ToBoolean(row[2]);
        //            variant.stock = Convert.ToInt32(row[3]);
        //            variant.weight = Convert.ToDouble(row[4]);
        //            variant.cost = Convert.ToDouble(row[5]);

        //            produto.variants.Add(variant);
        //            Console.WriteLine("j");

        //        }

        //        string jsonData = AcessoJson.Serializar(produto);

        //        ////Criando os dados JSON para o novo produto

                ////string jsonData = @"{
                ////""name"": ""nome produto"",
                ////""variants"":[ 
                ////    {
                ////        ""price"": 10.00,
                ////        ""stock_management"": true,
                ////        ""stock"": 12,
                ////        ""weight"": 2.00,
                ////        ""cost"": 10.99
                ////    }]
                ////}";

        //        StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

        //            // Fazendo a solicitação POST para criar o novo produto
        //            HttpResponseMessage response = await httpClient.PostAsync("products", content);

        //            // Verificando se a solicitação foi bem-sucedida
        //            if (response.IsSuccessStatusCode)
        //            {
        //                // Lendo o conteúdo da resposta como uma string JSON
        //                string jsonResponse = await response.Content.ReadAsStringAsync();
        //                Console.WriteLine(DateTime.Now + " -- Novo Produto Cadastrado!\n\n" + jsonResponse + "\n");

        //                //ProdutosIntegracao.Root root = new();
        //                //ProdutosIntegracao.Root root = (ProdutosIntegracao.Root)AcessoJson.Desserializar(jsonResponse);                                           
        //            }
        //            else
        //            {
        //                Console.WriteLine($"Falha na solicitação!!. Código de status: {response.StatusCode}\n");
        //            }
        //    }, null, TimeSpan.Zero, TimeSpan.FromMinutes(1));

        //    // Mantenha o programa em execução
        //    Console.WriteLine("Pressione Enter para sair...");
        //    Console.ReadLine();
        //}
        #endregion




        #region Get
        //// HttpClient
        //using (HttpClient httpClient = new HttpClient())
        //{
        //    httpClient.BaseAddress = new Uri($"https://api.tiendanube.com/v1/{storeId}/");

        //    // cabeçalho de autenticação
        //    httpClient.DefaultRequestHeaders.Add("Authentication", "bearer " + accessToken);
        //    httpClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue(app, versao));


        //    // Configurar o Timer para executar a cada 5 minutos
        //    var timer = new Timer(async (state) =>
        //    {
        //        // Fazendo GET
        //        HttpResponseMessage response = await httpClient.GetAsync("products");

        //        // Verificando status da solicitação
        //        if (response.IsSuccessStatusCode)
        //        {
        //            try
        //            {
        //                // guardando a resposta como uma string
        //                string responseContent = await response.Content.ReadAsStringAsync();
        //                Console.WriteLine(responseContent);

        //                //List<Root> listaProd = JsonSerializer.Deserialize<List<ProdutosIntegracao.Root>>(responseContent);

        //                List<Root> listaProd = ProdutosIntegracaoNegocio.DesserializarList(responseContent);


        //                AcessoFirebird acessoFirebird = new();

        //                foreach (var item in listaProd)
        //                {
        //                    for (int i = 0; i < item.variants.Count; i++)
        //                    {
        //                        acessoFirebird.LimparParametros();
        //                        acessoFirebird.AdicionarParametros("ID", item.variants[i].product_id);
        //                        if (item.variants[i].sku != null)
        //                        {
        //                            acessoFirebird.AdicionarParametros("SKU", item.variants[i].sku.ToString());
        //                        }
        //                        else
        //                        {
        //                            acessoFirebird.AdicionarParametros("SKU", "Não informado");
        //                        }

        //                        acessoFirebird.AdicionarParametros("ORIGEM", "NS");
        //                        var ok = acessoFirebird.ExecutarConsultaScalar(CommandType.Text, "SELECT COUNT(*) FROM PRODUTOS_INTEGRACAO WHERE ID = @ID AND SKU = @SKU AND ORIGEM = @ORIGEM;");
        //                        if (ok.ToString() != "1")
        //                        {
        //                            acessoFirebird.ExecutarManipulacao(CommandType.Text, "INSERT INTO PRODUTOS_INTEGRACAO (ID, SKU, ORIGEM) VALUES (@ID, @SKU, @ORIGEM);");
        //                        }
        //                    }
        //                }
        //            }
        //            catch (NullReferenceException e)
        //            {
        //                throw new Exception(e.Message);
        //            }
        //            catch (Exception e)
        //            {
        //                throw new Exception(e.Message);
        //            }
        //        }
        //        else
        //        {
        //            Console.WriteLine($"Falha na solicitação. Código de status: {response.StatusCode}");
        //        }

        //    }, null, TimeSpan.Zero, TimeSpan.FromMinutes(1));

        //    // Mantenha o programa em execução
        //    Console.WriteLine("Pressione Enter para sair...");
        //    Console.ReadLine();        
        //}
        #endregion
    }
}
