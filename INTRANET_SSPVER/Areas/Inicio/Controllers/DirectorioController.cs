using INTRANET_SSPVER.Models.Contexts;
using INTRANET_SSPVER.Models.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace INTRANET_SSPVER.Areas.Home.Controllers
{

    [Area("Inicio")]

    public class DirectorioController : Controller
    {

        private readonly IDirectorioService _service;

        public DirectorioController(IDirectorioService service)
        {
            _service = service;
        }


        public IActionResult Index()
        {
            var listado = _service.ObtenerDirectorio();
            return View("Index", listado);

        }


        public IActionResult ExportPdf()
        {
            var modelo = _service.ObtenerDirectorio();

            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);

                    
                    page.Header().Background("#6A1B1B").Padding(10).AlignCenter().Text("Directorio Telefónico SSP")
                        .FontSize(20).Bold().FontColor("#FFFFFF");

                    
                    page.Content().Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn(1); 
                            columns.RelativeColumn(3); 
                            columns.RelativeColumn(3); 
                            columns.RelativeColumn(2); 
                        });

                        // Encabezado 
                        table.Header(header =>
                        {
                            header.Cell().Background("#F2C94C").AlignCenter().Text("#").Bold().FontSize(12).FontColor("#6A1B1B");
                            header.Cell().Background("#F2C94C").AlignCenter().Text("Nombre").Bold().FontSize(12).FontColor("#6A1B1B");
                            header.Cell().Background("#F2C94C").AlignCenter().Text("Área").Bold().FontSize(12).FontColor("#6A1B1B");
                            header.Cell().Background("#F2C94C").AlignCenter().Text("Extensión").Bold().FontSize(12).FontColor("#6A1B1B");
                        });

                        // Filas con numeración y contenido centrado
                        int i = 1;
                        foreach (var item in modelo)
                        {
                            table.Cell().AlignCenter().Text(i++.ToString()).FontSize(10);
                            table.Cell().AlignCenter().Text(item.Nombre).FontSize(10);
                            table.Cell().AlignCenter().Text(item.Area).FontSize(10);
                            table.Cell().AlignCenter().Text(item.Ext).FontSize(10);
                        }
                    });

                    // Footer
                    page.Footer().AlignCenter().Text(text =>
                    {
                        text.Span("Secretaría de Seguridad Pública - Veracruz")
                            .FontSize(9).FontColor("#6A1B1B");
                    });
                });
            });

            var pdfBytes = document.GeneratePdf();
            return File(pdfBytes, "application/pdf", "DirectorioSSP.pdf");
        }


        //Revisar

    }


}
