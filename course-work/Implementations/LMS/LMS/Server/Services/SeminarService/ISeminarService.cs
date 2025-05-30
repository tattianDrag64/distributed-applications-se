namespace BibliotekBoklusen.Server.Services.SeminarService
{
    public interface ISeminarService
    {
        Task<List<Seminarium>> GetAllSeminars();
        Task<Seminarium> GetSeminarById(int id);
        Task<Seminarium> CreateSeminar(Seminarium seminarToAdd);
        Task<Seminarium> UpdateSeminar(int id, Seminarium seminarToUpdate);
        Task<string> DeleteSeminar(int id);
    }
}
