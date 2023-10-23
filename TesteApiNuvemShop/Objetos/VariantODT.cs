namespace TesteApiNuvemShop.Objetos
{
    public class VariantODT
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class Variant
        {
            public int id { get; set; }
            public object image_id { get; set; }
            public decimal promotional_price { get; set; }
            public DateTime created_at { get; set; }
            public decimal depth { get; set; }
            public decimal height { get; set; }
            public List<Value> values { get; set; }
            public decimal price { get; set; }
            public int product_id { get; set; }
            public bool stock_management { get; set; }
            public int stock { get; set; }
            public string sku { get; set; }
            //O número da peça do fabricante (MPN) do produto
            public string mpn { get; set; }
            //Atributo para definir o grupo demográfico para o qual o produto foi projetado. É opcional e suporta apenas estes valores:
            //"recém-nascido", "infantil", "criança", "crianças" e "adulto".
            public string age_group { get; set; }
            //Atributo para especificar o gênero para o qual seu produto foi projetado.É opcional e suporta apenas os valores: “feminino”, “masculino” e “unissex”
            public string gender { get; set; }
            public DateTime updated_at { get; set; }
            public decimal weight { get; set; }
            public decimal width { get; set; }
            public decimal cost { get; set; }
        }

        public class Value
        {
            public string pt { get; set; }
        }
    }
}
