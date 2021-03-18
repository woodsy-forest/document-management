using DocumentManagement.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DocumentManagement.Models;
using System.Net.Mail;
using System.Net;
using Microsoft.AspNetCore.Http;
using System.IO;
using DocumentManagement.Dto;
using DocumentManagement.Mapping;
using Microsoft.AspNetCore.StaticFiles;

namespace DocumentManagement.Services
{
    public class DocumentService
    {
        private readonly IConfiguration _configuration;
        private readonly DocumentContext _context;

        public DocumentService(DocumentContext context,
            IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<List<DocumentDto>> GetDocuments()
        {

            var documents = await _context.Documents
                                .OrderBy(n => n.Name)
                                .ToListAsync();

            var documentMapping = new DocumentMapping();
            var documentDtos = new List<DocumentDto>();

            foreach (var document in documents)
                documentDtos.Add(documentMapping.Map(document));

            //returns an empty list when nothing is found.
            return documentDtos;
        }

        public async Task<bool> DeleteDocument(int id)
        {

            var document = await _context.Documents
                         .Where(d => d.Id == id)
                         .FirstOrDefaultAsync();

            if (document != null)
            {
                var location = document.Location;
                File.Delete(location);
            }
            else
                return false;

            _context.Documents.Remove(document);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<int> CreateDocument(IFormFile file)
        {

            if (!Directory.Exists(_configuration["DocumentFullPath"]))
            {
                Directory.CreateDirectory(_configuration["DocumentFullPath"]);
            }

            var location = _configuration["DocumentFullPath"] + DateTime.Now.ToString("yyyyMMdd_HH_mm_ss") + "_" + file.FileName;
            using (FileStream fileStream = System.IO.File.Create(location))
            {
                file.CopyTo(fileStream);
                fileStream.Flush();
            }

            var document = new Document();

            document.FileSize = file.Length;
            document.Name = file.FileName;
            document.Location = location;

            await _context.Documents.AddAsync(document);

            await _context.SaveChangesAsync();

            return document.Id;

        }
    }
}
