using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalPrpject
{
    class Item
    {
        public int ID { set; get; }
        public string Name { set; get; }
        public double price { set; get; }

        public virtual List<ReposotryItem> reposotryItem { set; get; }
        public virtual Category Cattegory { set; get; }
        public override string ToString()
        {
            return $"{Name}";
        }
        //public virtual List<User> Users { set; get; }

    }
}
