using DocumentManagement.Data;
using DocumentManagement.Dto;
using DocumentManagement.Mapping;
using DocumentManagement.Models;
using DocumentManagement.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eshop_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private readonly DocumentContext _context;
        private readonly IConfiguration _configuration;

        public DocumentController(DocumentContext context,
                        IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpGet()]
        public async Task<IActionResult> GetDocuments()
        {

            try
            {
                var documentService = new DocumentService(_context, _configuration);

                return Ok(await documentService.GetDocuments());
              
            }
            catch (Exception ex)
            {
                //500 - Internal Server Error
                return StatusCode(500, ex.Message);
            }

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDocument(int id)
        {

            try
            {

                var documentService = new DocumentService(_context, _configuration);

                if (await documentService.DeleteDocument(id))
                    //200 - OK
                    return Ok();
                else
                    return NotFound();
            }

            catch (Exception ex)
            {
                //500 - Internal Server Error
                return StatusCode(500, ex.Message);

            }

        }


        [HttpPost()]
        public async Task<IActionResult> AddDocument([FromForm] IFormFile file)
        {

            try
            {
                string contentType = file.ContentType;
                if (contentType.ToLower() != "application/pdf")
                    return BadRequest("The file must be in a pdf format.");


                long size = file.Length;
                if (size > 5000000)
                    return BadRequest("Max file size is 5MB.");

                //call serve to save file and add record to table.
                var documentService = new DocumentService(_context, _configuration);

                return Ok(await documentService.CreateDocument(file));

            }
            catch (Exception ex)
            {
                //500 - Internal Server Error
                return StatusCode(500, ex.Message);
            }


        }


    }
}
