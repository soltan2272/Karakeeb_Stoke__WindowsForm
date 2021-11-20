using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalPrpject
{
    class Category
    {
        public int ID { set; get; }
        public string Name { set; get; }
        public string Description { set; get; }


        public virtual List<Item> Items { set; get; }
        public override string ToString()
        {
            return $"{Name}";
        }
        //public virtual List<Reposotry> Reposotries { set; get; }


    }
}
