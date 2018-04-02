using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using shehryar.ModelClass;
using shehryar.ReposoitryModel;

namespace shehryar.API
{

    public class TestingController : ApiController
    {
        User userMethod = new User();  
        
        public List<users> get() {
            return userMethod.GetClasses();
        }

        public users get(int id)
        {
            return userMethod.GetUserByID(id);
        }

        public String delete(int id) {
            return userMethod.Delete(id);
        }

        [HttpPost]
        public String Post(users user)
        {
            return userMethod.AddUsers(user.userID,user.userName);
        }

        [HttpPut]
        public String Put(users user)
        {
            return userMethod.update(user.userID, user.userName);
        }
    }
}
