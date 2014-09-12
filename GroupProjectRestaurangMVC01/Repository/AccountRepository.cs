using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GroupProjectRestaurangMVC01.Models;

namespace GroupProjectRestaurangMVC01.Repository
{
    public class AccountRepository
    {
        public UserProfile GetUserProfileByUserId(int userId)
        {
            using (UsersContext userDb = new UsersContext())
            {
                UserProfile result = userDb.UserProfiles.FirstOrDefault(c => c.UserId.Equals(userId));
                return result;
            }
        }
    }
}