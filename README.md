A simple ASP.NET C# web application allowing users to register, log in, and explore a vast collection of movies, directors, and actors. The application includes an admin account with predefined credentials in the SeedData file for managing the content.

**FEATURES**:

**User Registration & Login:** Users can sign up and log in to the platform to access movie content.

**Admin Access:** Admins can manage the collection of movies, directors, and actors.

**Movie Collection:** Users can explore a list of movies with detailed information about each movie, director, and cast.

**Responsive Design:** Designed with Bootstrap for a responsive, user-friendly interface.


**TECH STACK**:

**Frontend:** HTML, CSS, JavaScript, jQuery, Bootstrap

**Backend:** ASP.NET (C#)

**Database:** MySQL with Microsoft SQL Server

**Other Libraries:** Entity Framework (for data management)


**INSTALLATION**:

**Clone the Repository:** git clone https://github.com/veenaharshitha11/MoviesProject.git

**Navigate to the Project Directory:** cd MoviesProject

**Install Dependencies:** Make sure you have the required .NET dependencies installed by running: dotnet restore


**DATABASE SETUP**

**Set Up MySQL Database:** Create a new database for this project (e.g., movies_project_db).

**Update Connection Strings:** Go to appsettings.json in your project and update the connection string for your MySQL database.

**Run Migrations:** Use the following command to apply migrations and seed initial data:

  dotnet ef database update

Note: The application uses a SeedData file to add an initial admin user and sample data.


**USAGE**:

**Run the Application:** dotnet run


**Explore the Application:**

Register as a new user or log in using your credentials.

Admin Access: Log in as the admin (credentials provided below) to manage movie collections.

The application has a seeded admin account for testing purposes
