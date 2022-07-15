using Xunit;
using System.Collections.Generic;

namespace Database.Tests;

public class UnitTests
{
    UserDatabase db = new UserDatabase();
    
    public UnitTests()
    {
        db.AddUser("user1", "1234");
        db.AddUserProfile("user1", "1234", "John", "Doe", "1992/12/11");
        
        db.AddUser("user2", "abcd");
        db.AddUserProfile("user2", "abcd", "Jane", "Doe", "2000/01/02");
        
        db.AddUser("turtle", "896f");
        db.AddUserProfile("turtle", "896f", "Ka", "Roo", "2001/02/12");
        
        db.AddUser("mrmagoo", "achoo");
        db.AddUserProfile("mrmagoo", "achoo", "Jamal", "King", "2000/08/14");
        
        db.AddUser("mark", "hi mark");
        db.AddUserProfile("mark", "hi mark", "Marcus", "Aurelius", "121/4/26");
        
        db.AddUser("leo", "strong password");
        
        db.AddUser("brick", "wall");
        db.AddUserProfile("brick", "wall", "John", "Smith", "1982/7/10");
        
        db.AddUser("optimus", "prime");
        db.AddUserProfile("optimus", "prime", "Stephen", "King", "1972/10/18");
        
        db.AddUser("cls", "dnr");
        // Should not be added
        db.AddUserProfile("cls", "dnr", "", "", "1/1");
    }
    [Fact]
    public void AllUsersAddedTest()
    {
        Assert.Equal(9, db.Users.Count);
        Assert.Equal("user1", db.Users[0].Username);
        Assert.Equal(1, db.Users[0].Id);
        
        Assert.Equal("turtle", db.Users[2].Username);
        Assert.Equal("mrmagoo", db.Users[3].Username);
        Assert.Equal("achoo", db.Users[3].Password);
        Assert.Equal("leo", db.Users[5].Username);
    }
    
    [Fact]
    public void AllProfilesAddedTest()
    {
        Assert.Equal("Ka", db.UserProfiles[2].FirstName);
        Assert.Equal("Jamal", db.UserProfiles[3].FirstName);
        Assert.Equal("2000/01/02", db.UserProfiles[1].DOB);
        Assert.Equal(5, db.UserProfiles[4].Id);
        
        Assert.Equal("2|2:Jane,Doe born 2000/01/02", db.UserProfiles[1].ToString());
    }
    
    [Fact]
    public void FindUsersFirstNameTest()
    {
        List<User> users = db.FindUsersWithFirstName("John");
        Assert.Equal(2, users.Count);
        Assert.Equal("user1", users[0].Username);
        Assert.Equal("brick", users[1].Username);
    }

    [Fact]
    public void FindUsersLastNameTest()
    {
        List<User> users = db.FindUsersWithLastName("King");
        Assert.Equal(2, users.Count);
        Assert.Equal("mrmagoo", users[0].Username);
        Assert.Equal("optimus", users[1].Username);
    }

    [Fact]
    public void UpdateProfileTest()
    {
        db.UpdateProfile("user2", "abcd", "Samantha", "Johnson", "1991/10/08");
        Assert.Equal("8|2:Samantha,Johnson born 1991/10/08", db.UserProfiles.Find(p => p.FirstName == "Samantha")?.ToString());

        db.UpdateProfile("mark", "hi mark", "Mark", "Antony", "-83/01/14");
        Assert.Equal("9|5:Mark,Antony born -83/01/14", db.UserProfiles.Find(p => p.FirstName == "Mark")?.ToString());

        Assert.Equal(7, db.UserProfiles.Count);
    }

    [Fact]
    public void DeleteTest()
    {
        db.Delete("mark", "hi mark");
        Assert.Null(db.UserProfiles.Find(p => p.FirstName == "Marcus"));
        Assert.Null(db.Users.Find(u => u.Username == "mark"));
        Assert.Equal(6, db.UserProfiles.Count);
        Assert.Equal(8, db.Users.Count);
    }

    [Fact]
    public void AuthExceptionsTest()
    {
        Assert.Throws<UserNotFoundException>(() => db.AddUserProfile("mark", "bye mark", "Fname", "Lname", "5/5/5"));
        Assert.Throws<UserNotFoundException>(() => db.UpdateProfile("mrmagoo", "boiler", "Mr", "Magoo", "9-9-9"));
        Assert.Throws<UserNotFoundException>(() => db.Delete("mrgmagoo", "dog"));
    }

    [Fact]
    public void UserProfileExceptionTest()
    {
        Assert.Throws<ProfileNotFoundException>(() => db.UpdateProfile("cls", "dnr", "fname", "lname", "888"));
        Assert.Throws<UserProfileAlreadyExistsException>(() => db.AddUserProfile("optimus", "prime", "fname", "lname", "000"));
        Assert.Throws<UserAlreadyExistsException>(() => db.AddUser("mrmagoo", "1235"));
    }
}