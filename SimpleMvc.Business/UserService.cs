using SimpleMvc.DAL;
using SimpleMvc.Entitys;

namespace SimpleMvc.Business
{
    public class UserService
    {
        public static User GetUserByToken(string token)
        {
            return new DbService().GetUserByToken(token);
        }

        public static bool IsInRole(string roles, object user)
        {
            return new DbService().IsInRole(roles, user);
        }

        public bool AddUser(User user)
        {
            return new DbService().AddUser(user);
        }
    }
}
