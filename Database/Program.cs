namespace Database;
public class Program
{
    public static void Main(string[] args)
    {
        var db = new UserDatabase();
        db.AddUser("user1", "1234");
        db.AddUser("user2", "1234");
        db.AddUser("user3", "1234");
        // calling this again should throw an exception
        // db.AddUser("user3", "9871");

        db.AddUserProfile("user1", "1234", "John", "Doe", "1997/11/10");
        db.AddUserProfile("user2", "1234", "Jane", "Doe", "1987/09/02");
        db.AddUserProfile("user3", "1234", "Allan", "Poe", "1999/03/25");
        // should throw an exception
        // db.AddUserProfile("user3", "1234", "Allan", "Poe", "1999/03/25"); 
        // should throw an exception
        // db.AddUserProfile("user3", "password", "Allan", "Poe", "1999/03/25"); 

        db.Users.ForEach(Console.WriteLine);
        db.UserProfiles.ForEach(Console.WriteLine);

        db.FindUsersWithFirstName("John").ForEach(Console.WriteLine);
    }
}
