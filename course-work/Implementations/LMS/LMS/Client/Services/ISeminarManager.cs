namespace BibliotekBoklusen.Client.Services
{
    public interface ISeminarManager
    {
        Task<List<Seminarium>> GetAllSeminars();
        Task<Seminarium> GetSeminarById(int id);
        Task<string> CreateSeminar(Seminarium seminar);
        Task DeleteSeminar(int id);
        Task UpdateSeminar(int id, Seminarium seminar);
    }
}