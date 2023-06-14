using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModsenOnlineStore.Store.Domain.Entities
{
    public class ProductType
    {
        public int Id { get; set; }
        public string TypeName { get; set; }
        
        public ProductType(string type) {
            TypeName = type;
        }
        
        public ProductType() {}
    }
}
