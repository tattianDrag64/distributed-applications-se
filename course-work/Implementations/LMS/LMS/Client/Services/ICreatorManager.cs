namespace BibliotekBoklusen.Client.Services
{
    public interface ICreatorManager
    {
        Task<List<Creator>> GetAllCreatorsAsync();
        Task<Creator> GetCreatorByIdAsync(int id);
        Task<string> AddCreator(Creator creatorToAdd);
        Task UpdateCreator(Creator category);
        Task DeleteCreatorAsync(int id);
    }
}
