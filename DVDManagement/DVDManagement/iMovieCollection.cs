namespace DVDManagement
{
    public interface iMovieCollection
    {
        int MovieCount { get; }
        void AddMovie(Movie movie);
        void RemoveMovie(string title, int copies);
        Movie FindMovie(string title);
        Movie[] GetAllMovies();
        void BorrowMovie(string title);
        void ReturnMovie(string title);
        Movie[] GetMoviesInDictionaryOrder();
        (string Title, int Count)[] GetTop3Movies();
        void SaveMovies();
        void LoadMovies();
    }
}
