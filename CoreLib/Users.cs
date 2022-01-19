using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib
{
	[Table("User")]
	public class Users
	{
		[Column("ID", TypeName = "INTEGER")]
		[Key]
		public int ID { get; set; }

		[Column("Email", TypeName = "VARCHAR")]
		public string Email { get; set; }

		[Column("FeatureName", TypeName = "VARCHAR")]
		public string FeatureName { get; set; }

		[Column("Enable", TypeName = "BIT")]
		public bool Enable { get; set; }

		public static Users GetUserByNameAndFeature(string email, string featureName)
		{
			try
			{
				using (var context = new DB.DatabaseContext())
				{
					var us = context.User.Where(x => x.Email == email && x.FeatureName == featureName);
					if (us.Any())
					{
						return us.First();
					}
				}

			}
			catch (Exception ex)
			{
				//log Error
			}
			return null;
		}

		public bool AddUser()
		{
			try
			{
				using (var context = new DB.DatabaseContext())
				{
					context.User.Add(this);
					var res = context.SaveChanges();
					return res == 1;
				}
			}
			catch (Exception ex)
			{
				//log error
			}
			return false;
		}

		public bool UpdateUser()
		{
			try
			{
				using (var context = new DB.DatabaseContext())
				{
					var us = context.User.Where(x => x.ID == ID).First();
					us.Enable = Enable;
					var res = context.SaveChanges();
					return res > 0;
				}
			}
			catch (Exception)
			{
				//log error
			}
			return false;
		}
	}
}
