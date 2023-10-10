using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TesteApiNuvemShop.Objetos;

namespace TesteApiNuvemShop.Negocio
{
    public class ProdutosIntegracaoNegocio
    {
        public ProdutosIntegracao.Root ProdutosIntegracao { get; set; }


        public  static string Serializar(Object obj)
        {
            return JsonSerializer.Serialize(obj, new JsonSerializerOptions { WriteIndented = true });
        }

        public static ProdutosIntegracao.Root Desserializar(string json)
        {
            return JsonSerializer.Deserialize<ProdutosIntegracao.Root>(json);
        }

        public static List<ProdutosIntegracao.Root> DesserializarList(string json)
        {
            return JsonSerializer.Deserialize<List<ProdutosIntegracao.Root>>(json);
        }

        public static ProdutosIntegracao.Root DesserializarList2(string json)
        {
            JsonSerializerOptions options = new JsonSerializerOptions();
            options.Converters.Add(new DateTimeOffsetConverterUsingDateTimeParse());

            return JsonSerializer.Deserialize<ProdutosIntegracao.Root>(json, options);
        }


        //public partial class TaipeiTree
        //{
        //    public static TaipeiTree[] FromJson(string json)
        //    {
        //        JsonSerializerOptions options = new JsonSerializerOptions();
        //        options.Converters.Add(new DateTimeOffsetConverterUsingDateTimeParse());

        //        return JsonSerializer.Deserialize<TaipeiTree[]>(json, options);
        //    }
        //}

        internal class DateTimeOffsetConverterUsingDateTimeParse : JsonConverter<DateTimeOffset>
        {
            public override DateTimeOffset Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                return DateTimeOffset.Parse(reader.GetString());
            }

            public override void Write(Utf8JsonWriter writer, DateTimeOffset value, JsonSerializerOptions options)
            {
                writer.WriteStringValue(value.ToString());
            }
        }
    }
}
