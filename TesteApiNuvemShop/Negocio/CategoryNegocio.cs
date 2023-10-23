using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TesteApiNuvemShop.AcessoDados;
using TesteApiNuvemShop.Objetos;

namespace TesteApiNuvemShop.Negocio
{
    public class CategoryNegocio
    {
        //public CategoryODT.Category category  { get; set; }

        public CategoryColecaoODT EnviaCategory()
        {
            AcessoFirebird acessoFirebird = new();
            acessoFirebird.LimparParametros();
            DataTable cat = acessoFirebird.ExecutarConsultarDataTable(CommandType.Text, "select a.codigo, coalesce(a.cod_integracao,0) as id,  trim(a.nome) as name , 0 as parent, (trim(a.nome) || ' google') as google_shopping_category  from ctprod_atalho_titulo a\r\nwhere a.codigo_pai = 0");

            CategoryColecaoODT categoryODTs = new();

            for (int i = 0; i < cat.Rows.Count; i++)
            {
                CategoryODT.Category category = new();
                category.codigo = cat.Rows[i]["codigo"].ToString();
                category.id = Convert.ToInt32(cat.Rows[i]["id"]) == 0 ? null : Convert.ToInt32(cat.Rows[i]["id"]);
                category.name.pt = cat.Rows[i]["name"].ToString();
                //category.name.en = cat.Rows[i]["nome"].ToString();
                //category.name.es = cat.Rows[i]["nome"].ToString();
                category.parent = Convert.ToInt32(cat.Rows[i]["parent"]) == 0 ? null : Convert.ToInt32(cat.Rows[i]["parent"]);
                //category.subcategories = ;
                category.google_shopping_category = cat.Rows[i]["google_shopping_category"].ToString();
                categoryODTs.Add(category);
            }
            return categoryODTs;
        }

        public CategoryColecaoODT EnviaSubCategory()
        {
            AcessoFirebird acessoFirebird = new();
            acessoFirebird.LimparParametros();
            DataTable cat = acessoFirebird.ExecutarConsultarDataTable(CommandType.Text, "select a.codigo, coalesce(a.cod_integracao,0) as id,  trim(a.nome) as name , coalesce(b.cod_integracao,0) as parent, (trim(a.nome) || ' google') as google_shopping_category  from ctprod_atalho_titulo a\r\njoin (select b.codigo, b.cod_integracao from ctprod_atalho_titulo b where b.codigo_pai = 0) b on (b.codigo = a.codigo_pai) where a.codigo_pai != 0");

            CategoryColecaoODT categoryODTs = new();

            for (int i = 0; i < cat.Rows.Count; i++)
            {
                CategoryODT.Category category = new();
                category.codigo = cat.Rows[i]["codigo"].ToString();
                category.id = Convert.ToInt32(cat.Rows[i]["id"]) == 0 ? null : Convert.ToInt32(cat.Rows[i]["id"]);
                category.name.pt = cat.Rows[i]["name"].ToString();
                //category.name.en = cat.Rows[i]["nome"].ToString();
                //category.name.es = cat.Rows[i]["nome"].ToString();
                category.parent = Convert.ToInt32(cat.Rows[i]["parent"]) == 0 ? null : Convert.ToInt32(cat.Rows[i]["parent"]);
                //category.subcategories = ;
                category.google_shopping_category = cat.Rows[i]["google_shopping_category"].ToString();
                categoryODTs.Add(category);
            }
            return categoryODTs;
        }

        public void RetCategory(CategoryODT.Category category, string codigo)
        {
            AcessoFirebird acessoFirebird = new();
            acessoFirebird.LimparParametros();
            acessoFirebird.AdicionarParametros("CODIGO", codigo);
            acessoFirebird.AdicionarParametros("Cod_integracao", category.id);
            acessoFirebird.ExecutarManipulacao(CommandType.Text, "UPDATE ctprod_atalho_titulo SET cod_integracao = @Cod_integracao WHERE CODIGO = @CODIGO");
        }

        public static CategoryODT.Category Desserializar(string json)
        {
            return JsonSerializer.Deserialize<CategoryODT.Category>(json);
        }
    }
}
