using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebbApi.Models.Categories
{
    public class SubCategoryModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoriesId { get; set; }
        public List<SubCategoryProductModel> Products { get; set; } = new();
    }
}
