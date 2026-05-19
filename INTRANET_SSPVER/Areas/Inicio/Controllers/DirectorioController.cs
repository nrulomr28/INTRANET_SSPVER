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

                    page.Margin(1.3f, Unit.Centimetre);

                    page.DefaultTextStyle(x =>
                        x.FontFamily("Arial")
                         .FontSize(10)
                    );

                    // 🔥 HEADER
                    page.Header().Column(header =>
                    {
                        header.Item()
                            .Background("#7B1E2B")
                            .PaddingVertical(2)
                            .AlignCenter()
                            .Text("Directorio Telefónico SSP")
                            .FontColor("#FFFFFF")
                            .FontSize(20)
                            .Bold();

                        header.Item()
                            .PaddingTop(6)
                            .AlignCenter()
                            .Text("Secretaría de Seguridad Pública del Estado de Veracruz")
                            .FontSize(9)
                            .FontColor("#666666");
                    });

                    // 🔥 CONTENIDO
                    page.Content()
                        .PaddingTop(15)
                        .Column(column =>
                        {
                            string areaPrincipal = "";
                            string subAreaActual = "";

                            foreach (var item in modelo)
                            {
                                // 🔥 NIVEL PRINCIPAL (1 y 2)
                                if (item.Nivel == 1 || item.Nivel == 2)
                                {
                                    areaPrincipal = item.Area;
                                    subAreaActual = "";

                                    column.Item()
                                        .PaddingTop(12)
                                        .PaddingBottom(6)
                                        .Column(areaCol =>
                                        {
                                            areaCol.Item()
                                                .LineHorizontal(1)
                                                .LineColor("#7B1E2B");

                                            areaCol.Item()
                                                .PaddingVertical(4)
                                                .AlignCenter()
                                                .Text(areaPrincipal.ToUpper())
                                                .Bold()
                                                .FontSize(11)
                                                .FontColor("#7B1E2B");

                                            areaCol.Item()
                                                .LineHorizontal(1)
                                                .LineColor("#7B1E2B");
                                        });
                                }
                                else
                                {
                                    // 🔹 SUBÁREA
                                    if (subAreaActual != item.Area)
                                    {
                                        subAreaActual = item.Area;

                                        column.Item()
                                            .PaddingTop(8)
                                            .PaddingBottom(3)
                                            .Text(subAreaActual)
                                            .SemiBold()
                                            .FontSize(9)
                                            .FontColor("#666666");
                                    }
                                }

                                // 🔥 RESALTAR FILA NIVEL 1 Y 2
                                bool destacar = item.Nivel == 1 || item.Nivel == 2;

                                column.Item()
                                    .PaddingVertical(4)
                                    .Row(row =>
                                    {
                                        // 👤 NOMBRE
                                        row.RelativeItem(6)
                                            .Text(text =>
                                            {
                                                var span = text.Span(item.Nombre);

                                                if (destacar)
                                                {
                                                    span.Bold()
                                                        .FontColor("#000000")
                                                        .FontSize(10.5f);
                                                }
                                                else
                                                {
                                                    span.FontSize(10)
                                                        .FontColor("#333333");
                                                }
                                            });

                                        // 📞 EXTENSIÓN
                                        row.ConstantItem(80)
                                            .AlignRight()
                                            .Text(text =>
                                            {
                                                var ext = text.Span($"Ext. {item.Ext}");

                                                if (destacar)
                                                {
                                                    ext.Bold()
                                                       .FontColor("#000000")
                                                       .FontSize(10.5f);
                                                }
                                                else
                                                {
                                                    ext.FontSize(10)
                                                       .FontColor("#555555");
                                                }
                                            });
                                    });

                                // 🔹 LÍNEA DIVISORIA SUAVE
                                column.Item()
                                    .PaddingTop(2)
                                    .LineHorizontal(0.5f)
                                    .LineColor("#E5E5E5");
                            }
                        });

                    // 🔥 FOOTER
                    page.Footer()
                        .PaddingTop(10)
                        .AlignCenter()
                        .Text(text =>
                        {
                            text.Span("Documento generado automáticamente • SSP Veracruz")
                                .FontSize(8)
                                .FontColor("#888888");
                        });
                });
            });

            var pdfBytes = document.GeneratePdf();

            return File(
                pdfBytes,
                "application/pdf",
                $"DirectorioSSP_{DateTime.Now:yyyyMMdd}.pdf"
            );
        }








    }


}
