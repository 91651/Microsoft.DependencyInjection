namespace Services
{
    public class User : IUser
    {
        public string[] GetAllUser()
        {
            var users = new string[] { "User1", "User2" };
            return users;
        }
    }
}
