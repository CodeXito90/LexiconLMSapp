using AutoMapper;
using Domain.Contracts;
using Domain.Models.Entities;
using LMS.Shared.DTOs;
using Services.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LMS.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly IDocumentRepository _repository;
        private readonly IMapper _mapper;

        public DocumentService(IDocumentRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DocumentDto>> GetAllDocumentsAsync()
        {
            var documents = await _repository.GetAllDocumentsAsync();
            return _mapper.Map<IEnumerable<DocumentDto>>(documents);
        }

        public async Task<DocumentDto> GetDocumentByIdAsync(int id)
        {
            var document = await _repository.GetDocumentByIdAsync(id);
            return document != null ? _mapper.Map<DocumentDto>(document) : null;
        }

        public async Task CreateDocumentAsync(DocumentDto documentDto)
        {
            var document = _mapper.Map<Document>(documentDto);
            _repository.CreateDocument(document);
            await _repository.SaveChangesAsync();
        }

        public async Task<bool> UpdateDocumentAsync(int id, DocumentDto documentDto)
        {
            var document = await _repository.GetDocumentByIdAsync(id);
            if (document == null) return false;

            _mapper.Map(documentDto, document);
            return await _repository.SaveChangesAsync();
        }

        public async Task<bool> DeleteDocumentAsync(int id)
        {
            var document = await _repository.GetDocumentByIdAsync(id);
            if (document == null) return false;

            _repository.DeleteDocument(document);
            return await _repository.SaveChangesAsync();
        }
    }

}
