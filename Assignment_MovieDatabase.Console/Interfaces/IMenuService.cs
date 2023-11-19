namespace Assignment_MovieDatabase.Console.Interfaces
{
    public interface IMenuService<T> where T : class
    {
        void Menu();
        void AddMenu();
        void ListAllMenu();
    }
}
