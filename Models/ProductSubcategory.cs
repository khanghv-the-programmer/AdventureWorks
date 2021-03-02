using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace AdventureWorksBeta.Models
{
    public partial class ProductSubcategory
    {
        public ProductSubcategory()
        {
            Product = new HashSet<Product>();
        }

        public int ProductSubcategoryId { get; set; }
        public int ProductCategoryId { get; set; }
        public string Name { get; set; }
        public Guid Rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }
        [JsonIgnore]
        public virtual ProductCategory ProductCategory { get; set; }
        [JsonIgnore]
        public virtual ICollection<Product> Product { get; set; }
    }
}
