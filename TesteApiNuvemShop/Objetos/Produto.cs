namespace TesteApiNuvemShop.Objetos
{
    public class Produto
    {
        public string name { get; set; }
        public List<Variant> variants { get; set; } = new List<Variant>();

        public class Variant
        {            
            public decimal price { get; set; }
            public bool stock_management { get; set; }
            public int stock { get; set; }
            public double weight { get; set; }
            public double cost { get; set; }
        }

    }
}
