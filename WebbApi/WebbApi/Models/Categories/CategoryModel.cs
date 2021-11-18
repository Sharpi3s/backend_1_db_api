using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace WebbApi.Models.Categories
{
    public class CategoryModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<SubCategoryModel> SubCategories { get; set; } = new();
        //public virtual ICollection<SubCategory> SubCategories { get; set; }
    }
}
