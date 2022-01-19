using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Core.Common;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.SQLite;
using System.Data.SQLite.EF6;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib
{
    public class DB
    {
        public class SQLiteConfiguration : DbConfiguration
        {
            public SQLiteConfiguration()
            {
                SetProviderFactory("System.Data.SQLite", SQLiteFactory.Instance);
                SetProviderFactory("System.Data.SQLite.EF6", SQLiteProviderFactory.Instance);
                SetProviderServices("System.Data.SQLite", (DbProviderServices)SQLiteProviderFactory.Instance.GetService(typeof(DbProviderServices)));
            }
        }

        public class DatabaseContext : DbContext
        {
			public DatabaseContext() :
				base(new SQLiteConnection()
				{
					ConnectionString = ConfigurationManager.ConnectionStrings["Default"].ToString()
				}, true)
			{

			}

			protected override void OnModelCreating(DbModelBuilder modelBuilder)
			{
				modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
				base.OnModelCreating(modelBuilder);
			}


			public DbSet<Users> User { get; set; }
        }
	}
}
