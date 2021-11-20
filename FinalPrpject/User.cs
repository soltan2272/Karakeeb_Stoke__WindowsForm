using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalPrpject
{
    class User
    {
        public int ID { set; get; }
        public string Name { set; get; }
        public string Password { set; get; }
        public bool IsAdmin { set; get; }


        //public virtual List<Reposotry> Reposotries { set; get; }

        //public virtual List<Item> Items { set; get; }

        /*
                         c => new
                {
                    Reposotry_ID = c.Int(nullable: false),
                    Item_ID = c.Int(nullable: false),
                    Quantity = c.Int(nullable: false),
                    date = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => new { t.Reposotry_ID, t.Item_ID })
                .ForeignKey("dbo.Reposotries", t => t.Reposotry_ID, cascadeDelete: true)
                .ForeignKey("dbo.Items", t => t.Item_ID, cascadeDelete: true)
                .Index(t => t.Reposotry_ID)
                .Index(t => t.Item_ID);
                */
    }
}
