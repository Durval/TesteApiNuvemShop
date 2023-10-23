using System.Text.Json.Serialization;

namespace TesteApiNuvemShop.Objetos
{
    public class CategoryODT
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class Name
        {
            //public string en { get; set; }
            //public string es { get; set; }
            public string pt { get; set; }
        }

        public class Category
        {
            [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
            public string codigo { get; set; }
            [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
            public int? id { get; set; }
            public Name name { get; set; } = new Name();
            public int? parent { get; set; } //ID do pai da categoria. nulo se não tiver pai
           // public List<int> subcategories { get; set; } //Os ids das subcategorias de primeiro nível da categoria
            public string google_shopping_category { get; set; }
        }

    }
}
