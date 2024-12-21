using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using OfficeOpenXml;
using uploadFileUsingWebAPI.Services;
using iText.Kernel.Pdf;
using uploadFileUsingWebAPI.Helpers;


namespace uploadFileUsingWebAPI.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class UploadFileController : ControllerBase
    {
        private readonly IFileProcessingService fileProcessingService;

        public UploadFileController(IFileProcessingService _fileProcessingService)
        {
            fileProcessingService = _fileProcessingService;
        }

        [HttpPost("uploadFile")]        
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            #region Validation

            // Checking null or empty
            if (file == null || file.Length == 0)
            {
                return BadRequest(new { status = "Failure", message = "No file uploaded." });
            }
            // Validate file type and size
            var ValidExtensions = new[] { ".xlsx", ".xls", ".pdf", ".docx" };
            var extension = Path.GetExtension(file.FileName).ToLower();  
            var fileName = Path.GetFileNameWithoutExtension(file.FileName);
            if (!ValidExtensions.Contains(extension))
            {
                return BadRequest(new { status = "Failure", message = "Invalid file type." });
            }
            // 10 MB limit
            if (file.Length > 10 * 1024 * 1024)  
            {
                return BadRequest(new { status = "Failure", message = "File size exceeds limit of 10 MB." });
            }
            #endregion

            // Process the file based on its type
            try
            {
                string filePath = Path.Combine("Files", fileName + Guid.NewGuid().ToString() + extension);
                Directory.CreateDirectory("Files");
                // Save file to server
                using (var stream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                {
                    await file.CopyToAsync(stream);
                }
                object fileDetails = extension switch
                {
                    ".xlsx" => await fileProcessingService.ProcessExcelFile(filePath),
                    ".xls" => await fileProcessingService.ProcessExcelFile(filePath),
                    ".pdf" => await fileProcessingService.ProcessPdfFile(filePath),
                    ".docx" => await fileProcessingService.ProcessWordFile(filePath)
                    //_ => null
                };
                return Ok(new { status = "Success", message = "File uploaded successfully", fileDetails });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { status = "Failure", message = "Error processing file.", details = ex.Message });
            }
        }

    }
}
