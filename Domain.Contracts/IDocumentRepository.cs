using Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface IDocumentRepository
    {
        Task<IEnumerable<Document>> GetAllDocumentsAsync();
        Task<Document?> GetDocumentByIdAsync(int id);
        void CreateDocument(Document document);
        void DeleteDocument(Document document);
        Task<bool> SaveChangesAsync();
    }
}
