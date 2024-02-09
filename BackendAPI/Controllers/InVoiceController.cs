using BackendAPI.IRepository;
using BackendAPI.Models.Class;
using BackendAPI.Models.Invoice;
using ClosedXML;
using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InVoiceController : ControllerBase
    {
        private readonly IConverter _pdfConverter;
        private readonly Controller_Functions _invoiceRequest;
        private readonly IOrganisation_Info _orginfo;
        public InVoiceController(IConverter pdfConverter, Controller_Functions invoiceRequest,IOrganisation_Info info)
        {
            _pdfConverter = pdfConverter;
            _invoiceRequest = invoiceRequest;
            _orginfo = info;
        }

        [HttpPost]
        [Route("GenreateInVoice")]
        public IActionResult GenreateInVoice([FromBody]InvoiceRequest invoiceRequest,int OrgId)
        
        {
            var OrgData=_orginfo.GetOrgById(OrgId);
            var CustomerName = invoiceRequest.Name;
          var htmlContent = _invoiceRequest.GenerateHtmlInvoice(OrgData,CustomerName, invoiceRequest);
            var pdfBytes = _pdfConverter.Convert(new HtmlToPdfDocument
            {
                Objects =
            {
                new ObjectSettings { HtmlContent = htmlContent }
            }
            });

            return File(pdfBytes, "application/pdf", "invoice.pdf");
        }
    }
}
