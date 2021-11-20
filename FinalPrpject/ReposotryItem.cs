using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalPrpject
{
    class ReposotryItem
    {
        public int ID { get; set; }
        public int Quantity { get; set; }
        public DateTime date { get; set; }
        public virtual Item Items { set; get; }
        public virtual Reposotry Reposotry { set; get; }

    }
}
