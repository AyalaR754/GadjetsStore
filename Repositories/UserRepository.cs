using Entities;
using System.Text.Json;



namespace Repositories
{
    public class UserRepository
    {
        public User Login(string userName, string password)
        {
            string filePath = "C:\\Users\\user\\Desktop\\תהילה\\API\\GadjetsStore\\user.txt";
            using (StreamReader reader = System.IO.File.OpenText(filePath))
            {
                string? currentUserInFile;
                while ((currentUserInFile = reader.ReadLine()) != null)
                {
                    User? user1 = JsonSerializer.Deserialize<User>(currentUserInFile);
                    if (user1?.Password == password && user1?.UserName == userName)
                    {
                        return user1;
                    }

                }
                return null;
            }
        }
        public User Register(User user)
        {
            try
            {

                string filePath = "C:\\Users\\user\\Desktop\\תהילה\\API\\GadjetsStore\\user.txt";
                if (!System.IO.File.Exists(filePath))
                {
                    System.IO.File.Create(filePath).Close();
                }
                int numberOfUsers = System.IO.File.ReadLines(filePath).Count();
                user.userId = numberOfUsers + 1;
                string userJson = JsonSerializer.Serialize(user);
                System.IO.File.AppendAllText(filePath, userJson + Environment.NewLine);

                return user;
            }
            catch (Exception)
            {
                return null;
            }

        }
        public List<User> Get()
        {
            const string FilePath = "C:\\Users\\user\\Desktop\\תהילה\\API\\GadjetsStore\\user.txt";

            List<User> users = System.IO.File.Exists(FilePath) ? System.IO.File.ReadLines("user.txt").Select(line => JsonSerializer.Deserialize<User>(line)).ToList() : new List<User>();


            //using (StreamReader reader = System.IO.File.OpenText("users.txt"))
            //{
            //    string? currentUserInFile;
            //    while ((currentUserInFile = reader.ReadLine()) != null)
            //    {
            //        User? user = JsonSerializer.Deserialize<User>(currentUserInFile);
            //        if (user != null)
            //            users.Add(user);
            //    }

            return users;
        }
        public void UpDate(User userToUpdate, int id)
        {

            string filePath = "C:\\Users\\user\\Desktop\\תהילה\\API\\GadjetsStore\\user.txt";
            string textToReplace = string.Empty;
            using (StreamReader reader = System.IO.File.OpenText(filePath))
            {
                string? currentUserInFile;
                while ((currentUserInFile = reader.ReadLine()) != null)
                {
                    User? user = JsonSerializer.Deserialize<User>(currentUserInFile);
                    if (user?.userId == id)
                        textToReplace = currentUserInFile;
                }
            }
            if (textToReplace != string.Empty)
            {
                string text = System.IO.File.ReadAllText(filePath);
                text = text.Replace(textToReplace, JsonSerializer.Serialize(userToUpdate));
                System.IO.File.WriteAllText(filePath, text);
            }
            // return user;







        }
    }
}
