using BaseLibrary.Entities;
using BaseLibrary.Responses;

namespace ClientLibrary.Services.Interfaces
{
    public interface ISearchManager
    {
        event Action BooksChanged;
        List<Book> Books { get; set; }
        string Message { get; set; }
        /*if we have no categoryUrl given then we will display all the products or recive all the products
         * from the request(all the products from the server)
         */
        Task GetAllBooks();
        Task<ServiceResponse<Book>> GetBookById(int bookId);

        Task SearchBooks(string searchText);
        Task<List<string>> GetBookSearchSuggestions(string searchText);
        Task<List<User>> SearchMember(string searchText);


    }

}

