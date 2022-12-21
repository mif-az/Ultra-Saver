using Ultra_Saver.Models;

namespace Ultra_Saver;

public interface INewUserInitService
{
    void Init(AppDatabaseContext db, string email);
}

public class NewUserInitService : INewUserInitService
{
    private readonly ILogger<NewUserInitService> _logger;

    public NewUserInitService(ILogger<NewUserInitService> logger)
    {
        _logger = logger;
    }

    // Since we are doing authentication with Google, when a user signs in for the first time we need to initialize important properties in the database. This service collects all those properties in one place to make sure that all initializations happen in a single transaction.
    public void Init(AppDatabaseContext db, string email)
    {
        InitializeNewUser(db, email);

        db.SaveChanges(); // Push changes to db (Transaction)

        _logger.LogInformation("Saved new user to database");
    }

    private void InitializeNewUser(AppDatabaseContext db, string email)
    {

        // Initialize a new User Properties row and add it to the table

        UserModel user = new UserModel();
        user.Email = email;

        db.User.Add(user);
    }
}