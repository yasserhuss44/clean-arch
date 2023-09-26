using Core.Interfaces;
using DinkToPdf;
using DinkToPdf.Contracts;

namespace Core.Utilities;

public class DinkPDFfService : IPdfService
{
    private readonly IConverter converter;

    public DinkPDFfService(IConverter converter)
        => this.converter = converter;

    public byte[] ConvertHtmlToPDF(
        string htmlContent)
    {
        var globalSettings = new GlobalSettings()
        {
            ColorMode = ColorMode.Color ,
            Orientation = Orientation.Landscape ,
            PaperSize = PaperKind.A4 ,
            Margins = new MarginSettings
            {
                Top = 10 ,
                Bottom = 1
            } ,
        };

        var objectSettings = new ObjectSettings()
        {
            PagesCount = true ,
            HtmlContent = htmlContent
        };

        var webSettings = new WebSettings()
        {
            DefaultEncoding = "utf-8" ,
            MinimumFontSize = 16
        };

        objectSettings.WebSettings = webSettings;

        var htmlToPdfDocument = new HtmlToPdfDocument()
        {
            GlobalSettings = globalSettings ,
            Objects = { objectSettings } ,
        };

        return converter.Convert(htmlToPdfDocument);
    }
}