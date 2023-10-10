using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesteApiNuvemShop.Objetos
{
    public class ProdutosIntegracao
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class Description
        {
            public string pt { get; set; }
        }

        public class Handle
        {
            public string pt { get; set; }
        }

        public class Name
        {
            public string pt { get; set; }
        }

        public class Root
        {
            public DateTime created_at { get; set; }

            //private string _createdAtString;
            //private string _updated_atgString;
            public int id { get; set; }
            public Name? name { get; set; }
            public Description? description { get; set; }
            public Handle? handle { get; set; }
            public List<object>? attributes { get; set; }
            public bool published { get; set; }
            public bool free_shipping { get; set; }
            public bool requires_shipping { get; set; }
            public string? canonical_url { get; set; }
            public object? video_url { get; set; }
            public SeoTitle? seo_title { get; set; }
            public SeoDescription? seo_description { get; set; }
            public object? brand { get; set; }
            //{
            //    get { return DateTimeOffset.ParseExact(_createdAtString, "yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture).DateTime; }
            //    set { _createdAtString = value.ToString("yyyy-MM-ddTHH:mm:ss"); }
            //}
            public DateTime updated_at { get; set; }
            //{
            //    get { return DateTimeOffset.ParseExact(_updated_atgString, "yyyy-MM-ddTHH:mm:sszzz", CultureInfo.InvariantCulture).DateTime; }
            //    set { _updated_atgString = value.ToString("yyyy-MM-ddTHH:mm:sszzz"); }
            //}
            public List<Variant>? variants { get; set; }
            public string? tags { get; set; }
            public List<object>? images { get; set; }
            public List<object>? categories { get; set; }
        }

        public class SeoDescription
        {
            public string? pt { get; set; }
        }

        public class SeoTitle
        {
            public string? pt { get; set; }
        }

        public class Variant
        {
            //private string _createdAtString;
            //private string _updated_atgString;
            public int id { get; set; }
            public object? image_id { get; set; }
            public int product_id { get; set; }
            public int position { get; set; }
            public string? price { get; set; }
            public string? compare_at_price { get; set; }
            public object? promotional_price { get; set; }
            public bool stock_management { get; set; }
            public int stock { get; set; }
            public string? weight { get; set; }
            public string? width { get; set; }
            public string? height { get; set; }
            public string? depth { get; set; }
            public object? sku { get; set; }
            public List<object>? values { get; set; }
            public object? barcode { get; set; }
            public object? mpn { get; set; }
            public object? age_group { get; set; }
            public object? gender { get; set; }
            public DateTime created_at { get; set; }
            //{
            //    get { return DateTimeOffset.ParseExact(_createdAtString, "yyyy-MM-ddTHH:mm:sszzz", CultureInfo.InvariantCulture).DateTime; }
            //    set { _createdAtString = value.ToString("yyyy-MM-ddTHH:mm:sszzz"); }
            //}
            public DateTime updated_at { get; set; }
            //{
            //    get { return DateTimeOffset.ParseExact(_updated_atgString, "yyyy-MM-ddTHH:mm:sszzz", CultureInfo.InvariantCulture).DateTime; }
            //    set { _updated_atgString = value.ToString("yyyy-MM-ddTHH:mm:sszzz"); }
            //}
            public string? cost { get; set; }
        }

    }
}
