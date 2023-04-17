using NLog;

// See https://aka.ms/new-console-template for more information
string path = Directory.GetCurrentDirectory() + "\\nlog.config";

// create instance of Logger
var logger = LogManager.LoadConfiguration(path).GetCurrentClassLogger();
string movieFilePath = Directory.GetCurrentDirectory() + "\\movies.csv";

logger.Info("Program started");

MovieFile movieFile = new MovieFile(movieFilePath);

string choice = "";
do
{
  // display choices to user
  Console.WriteLine("1) Add Movie");
  Console.WriteLine("2) Display All Movies");
  Console.WriteLine("3) Search for Movie");
  Console.WriteLine("Enter to quit");
  // input selection
  choice = Console.ReadLine();
  logger.Info("User choice: {Choice}", choice);
  
  if (choice == "1")
  {
    // Add movie
        Movie movie = new Movie();
    // ask user to input movie title
    Console.WriteLine("Enter movie title");
    // input title
    movie.title = Console.ReadLine();
    // verify title is unique
    if (movieFile.isUniqueTitle(movie.title)){
    // input genres
      string input;
      do
      {
        // ask user to enter genre
        Console.WriteLine("Enter genre (or done to quit)");
        // input genre
        input = Console.ReadLine();
        // if user enters "done"
        // or does not enter a genre do not add it to list
        if (input != "done" && input.Length > 0)
        {
          movie.genres.Add(input);
        }
      } while (input != "done");
      // specify if no genres are entered
      if (movie.genres.Count == 0)
      {
        movie.genres.Add("(no genres listed)");
      }

    // asking for director
    Console.WriteLine("Who was the dirctor of this movie?");
    // director input
    movie.director = Console.ReadLine();

    // asking for runtime
    Console.WriteLine("What was the total runtime of the movie?(hh:mm:ss)");
    // director input
    movie.runningTime = TimeSpan.Parse(Console.ReadLine());
            // add movie
      movieFile.AddMovie(movie);
    }
    
  } else if (choice == "2")
  {
    // Display All Movies
    foreach(Movie m in movieFile.Movies)
    {
      Console.WriteLine(m.Display());
    }
  } else if (choice == "3")
  {
    // Asking for title of movie
    Console.WriteLine("Enter the title you would like to search for:");
    // User input
    string SearchTitle = Console.ReadLine();
    // LINQ - Where filter operator & Select projection operator & Contains quantifier operator
var titles = movieFile.Movies.Where(m => m.title.Contains(SearchTitle)).Select(m => m.title);
foreach(string t in titles)
{
    Console.WriteLine($"  {t}");
}
// LINQ - Count aggregation method
Console.WriteLine($"There are {titles.Count()} movies with {SearchTitle} in the title:");
  }
} while (choice == "1" || choice == "2" || choice == "3");

logger.Info("Program ended");
