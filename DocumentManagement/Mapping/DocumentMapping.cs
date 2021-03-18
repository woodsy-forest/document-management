using DocumentManagement.Dto;
using DocumentManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagement.Mapping
{
    public class DocumentMapping
    {

        public DocumentDto Map(Document document)
        {
            DocumentDto documentDto = new DocumentDto();
            PropertyCopier<Document, DocumentDto>.Copy(document, documentDto);

            return documentDto;
        }

    }
}
