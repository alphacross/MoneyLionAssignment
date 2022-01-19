using CoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MoneyLion.Controllers
{
    public class FeatureController : ApiController
    {
        [HttpGet]
        public IHttpActionResult feature(string email, string featureName)
        {
            var user = Users.GetUserByNameAndFeature(email, featureName);

			if (user != null)
			{
                return Ok(new { canAccess = user.Enable });

			}
			else
			{
                return StatusCode(HttpStatusCode.NoContent);
			}

        }

        [HttpPost]
        public IHttpActionResult feature(Users us)
		{
            var user = Users.GetUserByNameAndFeature(us.Email, us.FeatureName);
            var res = false;
			if (user != null)
            {
                user.Enable = us.Enable;
                res = user.UpdateUser();
			}
			else
			{
               res = us.AddUser();
			}
            return res ? StatusCode(HttpStatusCode.OK) : StatusCode(HttpStatusCode.NotModified);

        }
    }
}
