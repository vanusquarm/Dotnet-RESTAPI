using GemBox.Document;

namespace MerchantPortal.API.Services;


public class PdfService
{
    public static void GenerateProtectedPdf(string content, string outputFilePath, string password)
    {
        ComponentInfo.SetLicense("FREE-LIMITED-KEY");

        var document = new DocumentModel();
        document.Sections.Add(new Section(document, new Paragraph(document, content)));

        var options = new PdfSaveOptions()
        {
            DocumentOpenPassword = password,
            PermissionsPassword = "",
            Permissions = PdfPermissions.None
        };

        // Output path for the password-protected PDF
        string protectedFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, outputFilePath);

        document.Save(protectedFilePath, options);
    }

    public static void DeleteFile(string filePath)
    {
        try
        {
            string protectedFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filePath);

            // Check if the file exists before attempting to delete it
            if (File.Exists(protectedFilePath))
            {
                // Delete the file
                File.Delete(protectedFilePath);
            }
            else
            {
                Console.WriteLine($"File not found: {protectedFilePath}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error deleting file: {ex.Message}");
        }
    }
}




