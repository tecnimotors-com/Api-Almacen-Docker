using ApiAlmacen.Repository.AlertaRepository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using ClosedXML.Excel;
using MailKit.Net.Smtp;
using MimeKit;

namespace ApiAlmacen.Controllers.Alerta
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AlertaController(IAlertaRepository IAlertaRepository, IConfiguration configuration) : ControllerBase
    {
        private readonly IAlertaRepository ialertarepository = IAlertaRepository;
        private readonly IConfiguration configuration = configuration;


        [HttpGet("BusquedaListadoAlerta/{Fecha_upload}")]
        public async Task<IActionResult> BusquedaListadoAlerta(string Fecha_upload)
        {
            var result = await ialertarepository.BusquedaListadoAlerta(Fecha_upload);
            return Ok(result);
        }

        [HttpGet("ListadoaAlertaInventario/{Fecha_upload}")]
        public async Task<IActionResult> ListadoaAlertaInventario(string Fecha_upload)
        {
            var result = await ialertarepository.ListadoaAlertaInventario(Fecha_upload);
            return Ok(result);
        }

        /*--------------------------------------------------------*/
        // Stock inventario promedio menor a 4 
        [HttpGet("StockAlertaInventario/{Fecha_upload}")]
        public async Task<IActionResult> StockAlertaInventario(string Fecha_upload)
        {
            try
            {
                var result = await ialertarepository.StockAlertaInventario(Fecha_upload);
                Thread.Sleep(100);
                var directory = Directory.GetCurrentDirectory() + "/ExcelImport/AlertaDiaria.xlsx";

                var format = "#,##0.00";

                var libro = new XLWorkbook();
                var workSheet = libro.Worksheets.Add("Reporte_Alerta");

                workSheet.Cell("A1").Value = "Codigo Interno";
                workSheet.Cell("B1").Value = "Codigo Equivalente";
                workSheet.Cell("C1").Value = "Descripcion";
                workSheet.Cell("D1").Value = "Total Stock";
                workSheet.Cell("E1").Value = "Promedio Venta 2024";

                for (int i = 0; i < result.Count(); i++)
                {
                    workSheet.Cell("A" + (i + 2).ToString()).Value = result.ToList()[i].Codigo_interno!.Trim();
                    workSheet.Cell("A" + (i + 2).ToString()).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    workSheet.Cell("B" + (i + 2).ToString()).Value = result.ToList()[i].Codigo_equivalente!.Trim();
                    workSheet.Cell("B" + (i + 2).ToString()).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
                    workSheet.Cell("C" + (i + 2).ToString()).Value = result.ToList()[i].Descripcion!.Trim();
                    workSheet.Cell("C" + (i + 2).ToString()).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;

                    workSheet.Cell("D" + (i + 2).ToString()).Value = int.Parse(result.ToList()[i].Total_stock!);
                    workSheet.Cell("D" + (i + 2).ToString()).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    //workSheet.Cell("D" + (i + 2).ToString()).Style.NumberFormat.Format = format;

                    workSheet.Cell("E" + (i + 2).ToString()).Value = float.Parse(result.ToList()[i].Prom_venta_2024!);
                    workSheet.Cell("E" + (i + 2).ToString()).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
                    workSheet.Cell("E" + (i + 2).ToString()).Style.NumberFormat.Format = format;
                }
                workSheet.Column("A").AdjustToContents();
                workSheet.Column("B").AdjustToContents();
                workSheet.Column("C").AdjustToContents();
                workSheet.Column("D").AdjustToContents();
                workSheet.Column("E").AdjustToContents();
                workSheet.SheetView.Freeze(1, 1);

                var resultsinventa = await ialertarepository.StockAlertaSinVenta(Fecha_upload);
                Thread.Sleep(100);
                var workSheet2 = libro.Worksheets.Add("Reporte_Alerta2");
                workSheet2.Cell("A1").Value = "Codigo Interno";
                workSheet2.Cell("B1").Value = "Codigo Equivalente";
                workSheet2.Cell("C1").Value = "Descripcion";
                workSheet2.Cell("D1").Value = "Total Stock";
                workSheet2.Cell("E1").Value = "Promedio Venta 2024";

                for (int i = 0; i < resultsinventa.Count(); i++)
                { 
                    workSheet2.Cell("A" + (i + 2).ToString()).Value = resultsinventa.ToList()[i].Codigo_interno!.Trim();
                    workSheet2.Cell("A" + (i + 2).ToString()).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    workSheet2.Cell("B" + (i + 2).ToString()).Value = resultsinventa.ToList()[i].Codigo_equivalente!.Trim();
                    workSheet2.Cell("B" + (i + 2).ToString()).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
                    workSheet2.Cell("C" + (i + 2).ToString()).Value = resultsinventa.ToList()[i].Descripcion!.Trim();
                    workSheet2.Cell("C" + (i + 2).ToString()).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;

                    //workSheet2["D" + (i + 2).ToString()].Value = float.Parse(resultsinventa.ToList()[i].Total_stock!);
                    workSheet2.Cell("D" + (i + 2).ToString()).Value = (resultsinventa.ToList()[i].Total_stock!).ToString();
                    workSheet2.Cell("D" + (i + 2).ToString()).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                    workSheet2.Cell("E" + (i + 2).ToString()).Value = int.Parse(resultsinventa.ToList()[i].Prom_venta_2024!);
                    workSheet2.Cell("E" + (i + 2).ToString()).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
                    //workSheet2.Cell("E" + (i + 2).ToString()).Style.NumberFormat.Format = format;
                }

                workSheet2.Column("A").AdjustToContents();
                workSheet2.Column("B").AdjustToContents();
                workSheet2.Column("C").AdjustToContents();
                workSheet2.Column("D").AdjustToContents();
                workSheet2.Column("E").AdjustToContents();
                workSheet2.SheetView.Freeze(1, 1);
                libro.SaveAs(directory);
                
                /*
                // Create new Excel WorkBook document.
                WorkBook workBook = WorkBook.Create(ExcelFileFormat.XLSX);
                workBook.Metadata.Author = "IronXL";
                // Add a blank WorkSheet
                WorkSheet workSheet = workBook.CreateWorkSheet("Reporte_Alerta");
                workSheet["A1"].Value = "Codigo Interno";
                workSheet["B1"].Value = "Codigo Equivalente";
                workSheet["C1"].Value = "Descripcion";
                workSheet["D1"].Value = "Total Stock";
                workSheet["E1"].Value = "Promedio Venta 2024";

                for (int i = 0; i < result.Count(); i++)
                {
                    workSheet["A" + (i + 2).ToString()].Value = result.ToList()[i].Codigo_interno!.Trim();
                    workSheet["A" + (i + 2).ToString()].Style.HorizontalAlignment = HorizontalAlignment.Center;
                    workSheet["B" + (i + 2).ToString()].Value = result.ToList()[i].Codigo_equivalente!.Trim();
                    workSheet["B" + (i + 2).ToString()].Style.HorizontalAlignment = HorizontalAlignment.Right;
                    workSheet["C" + (i + 2).ToString()].Value = result.ToList()[i].Descripcion!.Trim();
                    workSheet["C" + (i + 2).ToString()].Style.HorizontalAlignment = HorizontalAlignment.Right;

                    workSheet["D" + (i + 2).ToString()].Value = int.Parse(result.ToList()[i].Total_stock!);
                    workSheet["D" + (i + 2).ToString()].Style.HorizontalAlignment = HorizontalAlignment.Center;
                    workSheet["D" + (i + 2).ToString()].FormatString = BuiltinFormats.Number0;

                    workSheet["E" + (i + 2).ToString()].Value = float.Parse(result.ToList()[i].Prom_venta_2024!);
                    workSheet["E" + (i + 2).ToString()].Style.HorizontalAlignment = HorizontalAlignment.Right;
                    workSheet["E" + (i + 2).ToString()].FormatString = BuiltinFormats.Number2;
                }
                workSheet.AutoSizeColumn(0);
                workSheet.AutoSizeColumn(1);
                workSheet.AutoSizeColumn(2);
                workSheet.AutoSizeColumn(3);
                workSheet.AutoSizeColumn(4);
                workSheet.CreateFreezePane(1, 1);


                var resultsinventa = await ialertarepository.StockAlertaSinVenta();
                Thread.Sleep(100);
                WorkSheet workSheet2 = workBook.CreateWorkSheet("Reporte_Alerta2");
                workSheet2["A1"].Value = "Codigo Interno";
                workSheet2["B1"].Value = "Codigo Equivalente";
                workSheet2["C1"].Value = "Descripcion";
                workSheet2["D1"].Value = "Total Stock";
                workSheet2["E1"].Value = "Promedio Venta 2024";

                for (int i = 0; i < resultsinventa.Count(); i++)
                {
                    workSheet2["A" + (i + 2).ToString()].Value = resultsinventa.ToList()[i].Codigo_interno!.Trim();
                    workSheet2["A" + (i + 2).ToString()].Style.HorizontalAlignment = HorizontalAlignment.Center;
                    workSheet2["B" + (i + 2).ToString()].Value = resultsinventa.ToList()[i].Codigo_equivalente!.Trim();
                    workSheet2["B" + (i + 2).ToString()].Style.HorizontalAlignment = HorizontalAlignment.Right;
                    workSheet2["C" + (i + 2).ToString()].Value = resultsinventa.ToList()[i].Descripcion!.Trim();
                    workSheet2["C" + (i + 2).ToString()].Style.HorizontalAlignment = HorizontalAlignment.Right;

                    //workSheet2["D" + (i + 2).ToString()].Value = float.Parse(resultsinventa.ToList()[i].Total_stock!);
                    workSheet2["D" + (i + 2).ToString()].Value = (resultsinventa.ToList()[i].Total_stock!).ToString();
                    workSheet2["D" + (i + 2).ToString()].Style.HorizontalAlignment = HorizontalAlignment.Center;
                    workSheet2["D" + (i + 2).ToString()].FormatString = BuiltinFormats.Text;

                    workSheet2["E" + (i + 2).ToString()].Value = int.Parse(resultsinventa.ToList()[i].Prom_venta_2024!);
                    workSheet2["E" + (i + 2).ToString()].Style.HorizontalAlignment = HorizontalAlignment.Right;
                    workSheet2["E" + (i + 2).ToString()].FormatString = BuiltinFormats.Number0;
                }
                workSheet2.AutoSizeColumn(0);
                workSheet2.AutoSizeColumn(1);
                workSheet2.AutoSizeColumn(2);
                workSheet2.AutoSizeColumn(3);
                workSheet2.AutoSizeColumn(4);
                workSheet2.CreateFreezePane(1, 1);
                // Save the excel file
                workBook.SaveAs(directory);
                */

                /*------------------------------------------------------------------------*/

                
                var email = new MimeMessage();

                email.From.Add(new MailboxAddress("",
                    configuration.GetValue<string>("MailSettings:Email")!.ToString()));
                //Sender Name
                email.To.Add(new MailboxAddress("",
                    configuration.GetValue<string>("EmailSettings:Recipients")!.ToString()));
                //Receiver Name

                email.Subject = "Envio Reporte de Alerta";

                var builder = new BodyBuilder
                {
                    TextBody = "Se envia Reporte de alerta Stock y ventas menores del 20%" +
                                "y Reporte de alerta Stock mayor a cero sin Ventas"
                };

                byte[] fileBytes;
                FileStream file = new(directory, FileMode.Open, FileAccess.Read);
                using (var ms = new MemoryStream())
                {
                    file.CopyTo(ms);
                    fileBytes = ms.ToArray();
                }
                builder.Attachments.Add("AlertaDiaria.xlsx", fileBytes, ContentType.Parse("application/octet-stream"));
                email.Body = builder.ToMessageBody();

                using (var smtp = new SmtpClient())
                {
                    smtp.Connect(
                        configuration.GetValue<string>("MailSettings:Host"),
                       int.Parse(configuration.GetValue<string>("MailSettings:Port")!),
                        false);

                    // Note: only needed if the SMTP server requires authentication
                    smtp.Authenticate(
                        configuration.GetValue<string>("MailSettings:Email")!.ToString(),
                        configuration.GetValue<string>("MailSettings:Password")!.ToString());

                    smtp.Send(email);
                    smtp.Disconnect(true);
                    libro.Dispose();
                }

                return Ok("Guardado");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
