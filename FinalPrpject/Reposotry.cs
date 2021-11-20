using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalPrpject
{
    class Reposotry
    {
        public int ID { set; get; }
        public string Name { set; get; }
        public virtual List<ReposotryItem> reposotryItem { set; get; }
        public override string ToString()
        {
            return $"{Name}";
        }
        //public virtual List<Item> items { set; get; }

        //public virtual List<User> users { set; get; }
        //public virtual List<Category> Category { set; get; }
    }
}
