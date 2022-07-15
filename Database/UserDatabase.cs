namespace Database;
public class UserDatabase
{
    private static int userCounter = 1;
    private static int profileCounter = 1;
    public List<User> Users{ get; }
    public List<UserProfile> UserProfiles{ get; }

    public UserDatabase()
    {
        Users = new List<User>();
        UserProfiles = new List<UserProfile>();
    }

    public void AddUser(string username, string password)
    {
        if(username == null && password == null)
            return;
        
        foreach(User user in Users)
            if(user.Username == username)
                throw new UserAlreadyExistsException();
          
        Users.Add(new User(userCounter, username, password)); 
        userCounter++;
    }

    public void AddUserProfile(string username, string password, string firstName, string lastName, string DOB)
    {
        int containsId = -1;

        if(username == null || password == null || firstName == null || lastName == null || DOB == null)
            return;

        foreach(User user in Users)
            if(username == user.Username && password == user.Password)
                containsId = user.Id;

        foreach(UserProfile userProfile in UserProfiles)
            if( containsId == userProfile.UserId )
                throw new UserProfileAlreadyExistsException();
        if( containsId == -1 )
                throw new UserNotFoundException();
        else
        {
            UserProfiles.Add(new UserProfile(profileCounter, containsId, firstName, lastName, DOB));
            profileCounter++;
        }

    }

    public List<User> FindUsersWithFirstName(string firstName)
    {
        List<int> containsId = new List<int>();
        List<User> usersWithFirstName = new List<User>();

        foreach(UserProfile userProfile in UserProfiles)
            if(userProfile.FirstName == firstName)
                containsId.Add(userProfile.UserId);

        foreach(User user in Users)
            if(containsId.Contains(user.Id))
                usersWithFirstName.Add(user);

        return usersWithFirstName;
    }

    public List<User> FindUsersWithLastName(string lastName)
    {
        List<int> containsId = new List<int>();
        List<User> usersWithLastName = new List<User>();

        foreach(UserProfile userProfile in UserProfiles)
            if(userProfile.LastName == lastName)
                containsId.Add(userProfile.UserId);
        foreach(User user in Users)
            if(containsId.Contains(user.Id))
                usersWithLastName.Add(user);

       return usersWithLastName;
    }

    public void UpdateProfile(string username, string password, string firstName, string lastName, string DOB)
    {
        int containsId = -1;
        int containsUserProfile = -1;
        int oldId = 0;

        if(username == null || password == null || firstName == null || lastName == null || DOB == null)
            return;

        foreach(User user in Users)
        {
            if(username == user.Username && password == user.Password)
            {
                containsId = user.Id;
                oldId = user.Id;
            }
        }

        if(containsId == -1) 
            throw new UserNotFoundException();  

        foreach(UserProfile userProfile in UserProfiles)
        {
            if( containsId == userProfile.Id )
            {
                containsUserProfile = containsId;
            }
        }

        if( containsUserProfile == -1 )
            throw new ProfileNotFoundException();
        else
        {
            UserProfiles.RemoveAll(UserProfile => UserProfile.UserId == containsId);
            UserProfiles.Add(new UserProfile(profileCounter, oldId, firstName, lastName, DOB));
            profileCounter++;
        } 
    }

    public void Delete(string username, string password)
    {
        int containsID = -1;
        int index = 0;
        int counter = 0;

        if(username == null && password == null)
            return;
        
        foreach(User user in Users)
        {
            if(user.Username == username && user.Password == password)
            {
                containsID = user.Id;
                index = counter;
            }
            counter++;
        } 

        if(containsID == -1) 
            throw new UserNotFoundException();  
        else
        {
            UserProfiles.RemoveAt(index);
            Users.RemoveAt(index);
            profileCounter--;
        }

    }
}