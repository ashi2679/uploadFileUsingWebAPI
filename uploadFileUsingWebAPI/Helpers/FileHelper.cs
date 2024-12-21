namespace uploadFileUsingWebAPI.Helpers
{
    public class FileHelper
    {
        public static string ConverFileSize(long fileSizeInBytes)
        {
            const long OneKb = 1024; //1KB
            const long OneMB = 1024 * 1024; //1MB

            return fileSizeInBytes switch
            {
                < OneKb => $"{fileSizeInBytes} Bytes",
                < OneMB => $"{fileSizeInBytes / OneKb} KB", 
                _ => $"{fileSizeInBytes / OneMB} MB" 
            };
        }
    }
}
