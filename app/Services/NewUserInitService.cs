using Ultra_Saver.Migrations;

namespace Ultra_Saver;

public static class NewUserInitService
{
    // Since we are doing authentication with Google, when a user signs in for the first time we need to initialize important properties in the database. This service collects all those properties in one place to make sure that all initializations happen in a single transaction.
    public static void init(AppDatabaseContext db, string email)
    {
        initializeNewProperties(db, email);

        db.SaveChanges(); // Push changes to db (Transaction)
    }

    private static void initializeNewProperties(AppDatabaseContext db, string email)
    {

        // Initialize a new User Properties row and add it to the table

        UserPropsModel props = new UserPropsModel();
        props.email = email;

        db.Properties.Add(props);
    }
}