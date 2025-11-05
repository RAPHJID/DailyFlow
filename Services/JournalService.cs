using JournalApi.Data;
using JournalApi.Models;
using JournalApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace JournalApi.Services
{
    public class JournalService : IJournalService
    {
        private readonly JournalDbContext _context;

        public JournalService(JournalDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<JournalEntry>> GetAllAsync()
        {
            return await _context.JournalEntries
                .OrderByDescending(e => e.CreatedAt)
                .ToListAsync();
        }

        public async Task<JournalEntry?> GetByIdAsync(int id)
        {
            return await _context.JournalEntries.FindAsync(id);
        }

        public async Task<JournalEntry> CreateAsync(JournalEntry entry)
        {
            _context.JournalEntries.Add(entry);
            await _context.SaveChangesAsync();
            return entry;
        }

        public async Task<bool> UpdateAsync(JournalEntry entry)
        {
            _context.Entry(entry).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entry = await _context.JournalEntries.FindAsync(id);
            if (entry == null) return false;

            _context.JournalEntries.Remove(entry);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
