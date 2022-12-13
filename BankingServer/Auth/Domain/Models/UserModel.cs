using BCrypt.Net;

namespace BankingServer.Auth.Domain.Models;

public class UserModel
{
    public int Id { get; }
    public string Username { get; set; }
    private string Password { get; set; }
    public string Email { get; set; }
    private string Salt { get; set; } = Guid.NewGuid().ToString();

    public UserModel(
        int id,
        string password,
        string email,
        string username = "No username"
    )
    {
        Id = id;
        Password = password;
        Email = email;
        Username = username;
    }

    //Static method to create a new user with username and email only
    public static UserModel CreateNewUser(string username, string email, string password)
    {
        var u = new UserModel(0, password, email, username);
        u.SetPassword(password);
        return u;
    }

    /// <summary>
    /// Set the password of the user
    /// Using a hash function to hash the password
    /// </summary>
    /// <param name="password"></param>
    public void SetPassword(string password)
    {
        Password = BCrypt.Net.BCrypt.HashPassword(password);
    }

    /// <summary>
    /// Check if the password is correct
    /// </summary>
    /// <param name="password"></param>
    /// <returns>
    /// True if the password is correct
    ///  False if the password is incorrect
    /// </returns>
    public bool CheckPassword(string password)
    {
        try
        {
            return BCrypt.Net.BCrypt.Verify(password, Password);
        }
        catch (SaltParseException e)
        {
            Console.WriteLine(e);
            return false;
        }

    }

    //Generate a mock user
    public static UserModel GenerateMockUser()
    {
        return new UserModel(1, "password", "random@user.test", "mock user");

    }
}


//Create a model for Qna with the following properties
// Question: string, Answer: string, Confidence: string, prompts: List<string>


public  class  QnaModel
{
    public  string  Question { get;  set ; }
    public  string  Answer { get;  set ; }
    public  string  Confidence { get;  set ; }
    public  string  Source { get;  set ; }
    public  List < string > Prompts { get;  set ; }

    public  QnaModel( string  question,  string  answer,  string  confidence, string source, List < string > prompts)
    {
        Question = question;
        Answer = answer;
        Source = source;
        Confidence = confidence;
        Prompts = prompts;
    }
}