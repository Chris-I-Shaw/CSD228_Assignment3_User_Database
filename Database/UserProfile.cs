namespace Database;
public class UserProfile
{
    public int Id {get; init;}
    public int UserId {get; init;}
    public string? FirstName {get; init;}
    public string? LastName {get; init;}
    public string? DOB {get; init;}

    public UserProfile(int id, int userId, string FirstName, string LastName, string DOB)
    {
        this.Id = id;
        this.UserId = userId;
        this.FirstName = FirstName;
        this.LastName = LastName;
        this.DOB = DOB;
    }

    public override string ToString()
    {
        return $"{Id}|{UserId}:{FirstName},{LastName} born {DOB}";
    }

}