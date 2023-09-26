namespace Core.Interfaces;
public interface IPdfService
{
    byte[] ConvertHtmlToPDF(string htmlContent);
}
