namespace BibliotekBoklusen.Client.Services
{
    public interface ICategoryManager
    {
        Task<List<Category>> GetAllCategoriesAsync();
        Task<Category> GetCategoryByIdAsync(int id);
        Task<string> AddCategory(Category category);
        Task UpdateCategory(Category category);
        Task DeleteCategoryAsync(int id);
    }
}
