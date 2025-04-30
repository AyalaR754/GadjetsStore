namespace Entities;

    public class User
    {
        //public int counter { get; set; } = 0;
        public String? FirstName { get; set; }
        public String? LastName { get; set; }
        public String UserName { get; set; }

        public String Password { get; set; }
        public int userId { get; set; }


        public User(String firstName, String lastName, String userName, String password)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.UserName = userName;
            this.Password = password;
            // userId = counter++;
        }


    }

