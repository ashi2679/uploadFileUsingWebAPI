
using OfficeOpenXml;
using uploadFileUsingWebAPI.Helpers;


namespace uploadFileUsingWebAPI.Services
{
    public interface IFileProcessingService
    {
        Task<object> ProcessExcelFile(string filePath);
        Task<object> ProcessPdfFile(string filePath);
        Task<object> ProcessWordFile(string filePath);
    }
    public class FileProcessingServce : IFileProcessingService
    {
        public async Task<object> ProcessExcelFile(string filePath)
        {
            try
            {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                using (var package = new ExcelPackage(new FileInfo(filePath)))
                {
                    var sheet = package.Workbook.Worksheets[0];
                    var rows = sheet.Dimension.Rows;
                    var cols = sheet.Dimension.Columns;

                    var data = new List<Dictionary<string, object>>();
                    for (int i = 1; i <= 6 && i <= rows; i++)
                    {
                        var row = new Dictionary<string, object>();
                        for (int j = 1; j <= cols; j++)
                        {
                            if (i != 1)
                                row[$"{sheet.Cells[1, j].Text}"] = sheet.Cells[i, j].Text;                           
                        }
                        data.Add(row);
                    }
                    return await Task.FromResult(new { filetype = "Excel", data });
                }
            }
            catch (Exception ex)
            {
                return new { error = ex.Message };
            }
        }
        public async Task<object> ProcessPdfFile(string filePath )
        {
            var pdfInfo = new FileInfo(filePath);
            try
            {
                using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    using (var pdfDoc = new Spire.Pdf.PdfDocument(filePath))
                    {
                        return await Task.FromResult(new
                        {
                            fileType = "PDF",                           
                            fileSizeInMB = FileHelper.ConverFileSize(pdfInfo.Length),                          
                            pageCount = pdfDoc.Pages.Count
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                return new { error = ex.Message };
            }
        }
        public async Task<object> ProcessWordFile(string filePath)
        {
            var docInfo = new FileInfo(filePath);
            try
            {
                using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    using (var Doc = new Spire.Doc.Document(filePath))
                    {
                        return await Task.FromResult(new
                        {
                            fileType = "Word",
                            fileSizeInMB = FileHelper.ConverFileSize(docInfo.Length),
                            pageCount = Doc.PageCount
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                return new { error = ex.Message };
            }
        }   
    }
}
