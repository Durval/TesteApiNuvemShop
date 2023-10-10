using System.Text.Json;
using TesteApiNuvemShop.Objetos;

namespace TesteApiNuvemShop.AcessoDados
{
    public static class AcessoJson
    {
        public static string Serializar(Object obj)
        {
            return JsonSerializer.Serialize(obj, new JsonSerializerOptions { WriteIndented = true });
        }

        public static Object Desserializar(string json)
        {
            return JsonSerializer.Deserialize<Object>(json);
        }
    }
}
