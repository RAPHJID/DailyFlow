using JournalApi.Models;

namespace JournalApi.Services.Interfaces
{
    public interface IJournalService
    {
        Task<IEnumerable<JournalEntry>> GetAllAsync();
        Task<JournalEntry?> GetByIdAsync(int id);
        Task<JournalEntry> CreateAsync(JournalEntry entry);
        Task<bool> UpdateAsync(JournalEntry entry);
        Task<bool> DeleteAsync(int id);
    }
}
