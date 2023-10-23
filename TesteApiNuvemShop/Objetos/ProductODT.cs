using Microsoft.VisualBasic;
using static TesteApiNuvemShop.Objetos.CategoryODT;
using static TesteApiNuvemShop.Objetos.ImageODT;
using static TesteApiNuvemShop.Objetos.VariantODT;

namespace TesteApiNuvemShop.Objetos
{
    public class ProductODT
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);

        public class Description
        {
            public string en { get; set; }
            public string es { get; set; }
            public string pt { get; set; }
        }

        public class Handle
        {
            public string en { get; set; }
            public string es { get; set; }
            public string pt { get; set; }
        }

        public class Name //Lista dos nomes do Produto, em todos os idiomas suportados pela loja
        {
            public string en { get; set; }
            public string es { get; set; }
            public string pt { get; set; }
        }

        public class Product
        {           
            public List<object> attributes { get; set; } //Lista dos nomes dos atributos cujos valores definem as variantes. Ex: Cor, Tamanho, etc. É importante que o número de attributesseja igual ao número de valuesdentro das variantes.
            public List<Category> categories { get; set; }
            public DateTime created_at { get; set; }
            public Description description { get; set; }
            public Handle handle { get; set; } //Lista das strings amigáveis ​​para URL geradas a partir dos nomes dos Produtos, em todos os idiomas suportados pela loja
            public int id { get; set; }
            public List<Image> images { get; set; }
            public Name name { get; set; }
            public string brand { get; set; } //A marca do produto
            public string video_url { get; set; }
            public string seo_title { get; set; }
            public string seo_description { get; set; }
            public bool published { get; set; }
            public bool free_shipping { get; set; }
            public List<Variant> variants { get; set; }
        }
    }
}
