using System;
using System.ComponentModel.Design.Serialization;
using System.Data;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using TesteApiNuvemShop.AcessoDados;
using TesteApiNuvemShop.Negocio;
using TesteApiNuvemShop.Objetos;
using static TesteApiNuvemShop.Objetos.ProdutosIntegracao;

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
        //        // Criando os dados JSON para o novo produto
        //        string jsonData = @"{
        //        ""name"": ""Novo Produto x"",
        //        ""variants"":[ 
        //            {
        //                ""price"": 10.00,
        //                ""stock_management"": true,
        //                ""stock"": 12,
        //                ""weight"": ""2.00"",
        //                ""cost"": ""10.99""
        //            }]
        //        }";

        //        StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

        //        // Fazendo a solicitação POST para criar o novo produto
        //        HttpResponseMessage response = await httpClient.PostAsync("products", content);

        //        // Verificando se a solicitação foi bem-sucedida
        //        if (response.IsSuccessStatusCode)
        //        {
        //            // Lendo o conteúdo da resposta como uma string JSON
        //            string jsonResponse = await response.Content.ReadAsStringAsync();
        //            Console.WriteLine(DateTime.Now+" -- Novo Produto Cadastrado!\n\n"+jsonResponse +"\n");

        //            //ProdutosIntegracao.Root root = new();
        //            //ProdutosIntegracao.Root root = (ProdutosIntegracao.Root)AcessoJson.Desserializar(jsonResponse);                                           
        //        }
        //        else
        //        {
        //            Console.WriteLine($"Falha na solicitação!!. Código de status: {response.StatusCode}\n");
        //        }
        //    }, null, TimeSpan.Zero, TimeSpan.FromMinutes(1));          

        //    // Mantenha o programa em execução
        //    Console.WriteLine("Pressione Enter para sair...");
        //    Console.ReadLine();
        //}
        #endregion

        #region Get
        // HttpClient
        using (HttpClient httpClient = new HttpClient())
        {
            httpClient.BaseAddress = new Uri($"https://api.tiendanube.com/v1/{storeId}/");

            // cabeçalho de autenticação
            httpClient.DefaultRequestHeaders.Add("Authentication", "bearer " + accessToken);
            httpClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue(app, versao));


            // Fazendo GET
            HttpResponseMessage response = await httpClient.GetAsync("products");

            // Verificando status da solicitação
            if (response.IsSuccessStatusCode)
            {
                try
                {
                    // guardando a resposta como uma string
                    string responseContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(responseContent);

                    List<Root> listaProd = new List<Root>();

                    listaProd = ProdutosIntegracaoNegocio.DesserializarList(responseContent);

                    //var json = ProdutosIntegracaoNegocio.DesserializarList2(responseContent);


                    AcessoFirebird acessoFirebird = new();
                    acessoFirebird.LimparParametros();

                    foreach (var item in listaProd)
                    {
                        acessoFirebird.AdicionarParametros("id", item.id);
                        if (listaProd.Count == 1)
                        {
                            acessoFirebird.AdicionarParametros("sku", item.variants[0].sku);
                        }
                        else
                        {
                            for (int i = 0; i < listaProd.Count; i++)
                            {
                                acessoFirebird.AdicionarParametros("sku", item.variants[i].sku);
                            }
                        }
                        acessoFirebird.AdicionarParametros("origem", "NS");
                        acessoFirebird.ExecutarManipulacao(CommandType.Text, "INSERT INTO PRODUTOS_INTEGRACAO (ID, SKU, ORIGEM) VALUES (@id, @sku, @origem);");

                    }

                }
                catch (Exception e)
                {

                    throw new Exception(e.Message);
                }               
            }
            else
            {
                Console.WriteLine($"Falha na solicitação. Código de status: {response.StatusCode}");
            }
        }
        #endregion
    }
}
