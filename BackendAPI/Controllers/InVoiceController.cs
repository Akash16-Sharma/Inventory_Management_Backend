using BackendAPI.IRepository;
using BackendAPI.IRepository.Invoice;
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
        private readonly ICustomer _customer;
        private readonly IOut_Order _order;
        private readonly IInvoice _invoice;
        public InVoiceController(IConverter pdfConverter, Controller_Functions invoiceRequest,IOrganisation_Info info, ICustomer customer, IOut_Order order,IInvoice invoice)
        {
            _pdfConverter = pdfConverter;
            _invoiceRequest = invoiceRequest;
            _orginfo = info;
            _customer = customer;
            _order = order;
            _invoice = invoice;
        }

        [HttpPost]
        [Route("GenreateInVoice")]
        public IActionResult GenreateInVoice([FromBody]Billing bill, int OrgId,string SalesOrderId)
        
        {
            var OrgData=_orginfo.GetOrgById(OrgId);
            var OrderData = _order.GetOutOrdersBySalesOrderID(SalesOrderId);
            var BillData = _invoice.AddBillingDetails(bill);
            var CustomerData = _customer.GetCustomerById(bill.Customer_Id);
           
            var htmlContent = _invoiceRequest.GenerateHtmlInvoice(OrgData, OrderData,CustomerData,BillData);
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
