using System.Text.Json;
using Azure.Messaging.ServiceBus;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Minio;
using Minio.Exceptions;
using UploadFilesServer.Context;
using UploadFilesServer.Models;

namespace UploadFilesServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly DataContext _context;

        public ImagesController(DataContext context) => _context = context;

        [HttpGet]
        public async Task<IActionResult> GetAllImages()
        {
            try
            {
                var images = await _context.Images.ToListAsync();
                return Ok(images);      
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Submit([FromBody] Image image)
        {
            try
            {
                if (image is null)
                {
                    return BadRequest("Image is null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }

                image.Id = Guid.NewGuid();
                image.pullFlag = false;
                _context.Add(image);
                await _context.SaveChangesAsync();

                return StatusCode(201);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
    }
}
