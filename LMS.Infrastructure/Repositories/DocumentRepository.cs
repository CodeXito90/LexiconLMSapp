using Domain.Contracts;
using Domain.Models.Entities;
using LMS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LMS.Infrastructure.Repositories
{
    public class DocumentRepository : IDocumentRepository
    {
        private readonly LmsContext _context;

        public DocumentRepository(LmsContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Document>> GetAllDocumentsAsync()
        {
            return await _context.Documents.Include(d => d.ApplicationUser)
                                            .Include(d => d.Course)
                                            .Include(d => d.Module)
                                            .Include(d => d.Activity)
                                            .ToListAsync();
        }

        public async Task<Document?> GetDocumentByIdAsync(int id)
        {
            return await _context.Documents.Include(d => d.ApplicationUser)
                                            .Include(d => d.Course)
                                            .Include(d => d.Module)
                                            .Include(d => d.Activity)
                                            .FirstOrDefaultAsync(d => d.DocumentId == id);
        }

        public void CreateDocument(Document document)
        {
            _context.Documents.Add(document);
        }

        public void DeleteDocument(Document document)
        {
            _context.Documents.Remove(document);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
