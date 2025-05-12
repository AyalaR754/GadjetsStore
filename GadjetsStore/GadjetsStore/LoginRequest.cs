namespace GadjetsStore
{
    public class LoginRequest
    {

        public String UserName { get; set; }

        public String Password { get; set; }

        public LoginRequest( String userName, String password)
        {
            
            this.UserName = userName;
            this.Password = password;
            // userId = counter++;
        }
    }
}
