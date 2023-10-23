namespace TesteApiNuvemShop.Objetos
{
    public class ImageODT
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class Image
        {
            public string filename { get; set; }
            public int position { get; set; }
            public string attachment { get; set; }
        }
    }
}
