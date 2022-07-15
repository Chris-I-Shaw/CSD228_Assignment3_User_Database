namespace Database;
public class User
{
    public int Id {get; init;}
    public string Username {get; init;}
    public string Password {get; init;}
    
    public User(int Id, string Username, string Password)
    {
        this.Id = Id;
        this.Username = Username.ToLower();
        this.Password = Password;
    }
    
    public override string ToString()
    {
        return $"id:{Id},un:{Username},pw:{Password}";
    }
}