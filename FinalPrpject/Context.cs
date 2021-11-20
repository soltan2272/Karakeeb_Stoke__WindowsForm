using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace FinalPrpject
{
    class Context:DbContext
    {
        public Context() : base(@"Data Source=.;Initial Catalog=dd;Integrated Security=True")
        { }
        public DbSet<Reposotry> Reposotries { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ReposotryItem> ReposotryItem { get; set; }

        internal void SetInitializer<T>(object p)
        {
            throw new NotImplementedException();
        }

    }
}
