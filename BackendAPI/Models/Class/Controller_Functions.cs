using BackendAPI.IRepository;
using BackendAPI.IRepository.Roles;
using BackendAPI.Models.Invoice;
using ClosedXML.Excel;
using System;
using System.Text;

namespace BackendAPI.Models.Class
{
    public class Controller_Functions
    {
        [Obsolete]
        private Microsoft.AspNetCore.Hosting.IHostingEnvironment _environment;
        private readonly DataContext _dataContext;
        private readonly IRoles _Roles;
        private readonly ICustomer _customer;
        private readonly IOut_Order _order;

        [Obsolete]
        public Controller_Functions(Microsoft.AspNetCore.Hosting.IHostingEnvironment environment, DataContext dataContext,IRoles Roles, IOrganisation_Info info, ICustomer customer, IOut_Order order)
        {
            _environment = environment;
            _dataContext = dataContext;
            _Roles = Roles;
            _customer = customer;
            _order = order;
        }

        [Obsolete]
        public void ImportExcel(int StaffId,String Sidebarname,IFormFile File)
        {
            if (Sidebarname == "Category")
            {
                var CheckRoleTypeData = _Roles.CheckStaffType(StaffId);
                if (CheckRoleTypeData.RoleType == "Admin")
                {
                    string xmlFile = Path.Combine(_environment.ContentRootPath, "Uploads");
                    if (!Directory.Exists(xmlFile))
                    {
                        Directory.CreateDirectory(xmlFile);
                    }

                    // Save the uploaded Excel file.
                    string fileName = Path.GetFileName(File.FileName);
                    string filePath = Path.Combine(xmlFile, fileName);
                    using (FileStream stream = new FileStream(filePath, FileMode.Create))
                    {
                        File.CopyTo(stream);
                    }

                    XLWorkbook xl = new XLWorkbook(filePath);
                    int row = 2;
                    while (xl.Worksheets.Worksheet(1).Cell(row, 1).GetString() != "")
                    {
                        string Cat_Name = xl.Worksheets.Worksheet(1).Cell(row, 1).GetString();
                        //string Phone_Number = xl.Worksheets.Worksheet(1).Cell(row, 3).GetString();
                        //string Email = xl.Worksheets.Worksheet(1).Cell(row, 4).GetString();
                        //string Food_Preference = xl.Worksheets.Worksheet(1).Cell(row, 5).GetString();

                        Category cus = new Category();
                        cus.Name = Cat_Name;
                        cus.IsActive = true;
                        cus.UpdatedBy = StaffId;
                        cus.InsertedOn = DateTime.Now;

                        _dataContext.Category.Add(cus);
                        _dataContext.SaveChanges();
                        row++;
                    }
                }

                else
                {
                    var AccessData = _Roles.CheckAccess(StaffId);
                    for (var i = 0; i < AccessData.Count; i++)
                    {
                        if (AccessData[i].Import == true)
                        {
                            string xmlFile = Path.Combine(_environment.ContentRootPath, "Uploads");
                            if (!Directory.Exists(xmlFile))
                            {
                                Directory.CreateDirectory(xmlFile);
                            }

                            // Save the uploaded Excel file.
                            string fileName = Path.GetFileName(File.FileName);
                            string filePath = Path.Combine(xmlFile, fileName);
                            using (FileStream stream = new FileStream(filePath, FileMode.Create))
                            {
                                File.CopyTo(stream);
                            }

                            XLWorkbook xl = new XLWorkbook(filePath);
                            int row = 2;
                            while (xl.Worksheets.Worksheet(1).Cell(row, 1).GetString() != "")
                            {
                                string Cat_Name = xl.Worksheets.Worksheet(1).Cell(row, 1).GetString();
                                //string Phone_Number = xl.Worksheets.Worksheet(1).Cell(row, 3).GetString();
                                //string Email = xl.Worksheets.Worksheet(1).Cell(row, 4).GetString();
                                //string Food_Preference = xl.Worksheets.Worksheet(1).Cell(row, 5).GetString();

                                Category cus = new Category();
                                cus.Name = Cat_Name;
                                cus.IsActive = true;
                                cus.UpdatedBy = StaffId;
                                cus.InsertedOn = DateTime.Now;

                                _dataContext.Category.Add(cus);
                                _dataContext.SaveChanges();
                                row++;
                            }
                        }
                    }
                }
            }

            else if (Sidebarname == "Item")
            {
                var CheckRoleTypeData = _Roles.CheckStaffType(StaffId);
                if (CheckRoleTypeData.RoleType == "Admin")
                {
                    string xmlFile = Path.Combine(_environment.ContentRootPath, "Uploads");
                    if (!Directory.Exists(xmlFile))
                    {
                        Directory.CreateDirectory(xmlFile);
                    }

                    // Save the uploaded Excel file.
                    string fileName = Path.GetFileName(File.FileName);
                    string filePath = Path.Combine(xmlFile, fileName);
                    using (FileStream stream = new FileStream(filePath, FileMode.Create))
                    {
                        File.CopyTo(stream);
                    }

                    XLWorkbook xl = new XLWorkbook(filePath);
                    int row = 2;
                    while (xl.Worksheets.Worksheet(1).Cell(row, 1).GetString() != "")
                    {
                        string Cat_Name = xl.Worksheets.Worksheet(1).Cell(row, 1).GetString();
                        string UnitType = xl.Worksheets.Worksheet(1).Cell(row, 2).GetString();
                        string Category = xl.Worksheets.Worksheet(1).Cell(row, 3).GetString();
                        int buyingPrice = Convert.ToInt32(xl.Worksheets.Worksheet(1).Cell(row, 4).GetString());
                        int SellingPrice = Convert.ToInt32(xl.Worksheets.Worksheet(1).Cell(row, 5).GetString());
                        int StockAlert = Convert.ToInt32(xl.Worksheets.Worksheet(1).Cell(row, 6).GetString());
                        int OpeningStock = Convert.ToInt32(xl.Worksheets.Worksheet(1).Cell(row, 7).GetString());
                        string Vendor = xl.Worksheets.Worksheet(1).Cell(row, 8).GetString();
                        //string Email = xl.Worksheets.Worksheet(1).Cell(row, 4).GetString();
                        //string Food_Preference = xl.Worksheets.Worksheet(1).Cell(row, 5).GetString();

                        //getting the id to for making sure that join works properly
                        int OrgId = GetOrgId(StaffId);
                        int UnitId = UnitTypeId(UnitType, StaffId, OrgId);
                        int CatId = CategoryId(Category, StaffId, OrgId);
                        int VendId = VendorId(Vendor, StaffId, OrgId);


                        Item item = new Item();
                        item.Name = Cat_Name;
                        item.Unit_Type_Id = UnitId;
                        item.Category_Id = CatId;
                        item.Vendor_Id = VendId;
                        item.Org_Id = OrgId;
                        item.Selling_Price = SellingPrice;
                        item.Buying_Price = buyingPrice;
                        item.Stock_Alert = StockAlert;
                        item.Opening_Stock = OpeningStock;
                        item.IsActive = true;
                        item.Updated_By = StaffId;
                        item.InsertedOn = DateTime.Now;

                        _dataContext.Items.Add(item);
                        _dataContext.SaveChanges();
                        row++;
                    }
                }
                else
                {
                    var AccessData = _Roles.CheckAccess(StaffId);
                    for (var i = 0; i < AccessData.Count; i++)
                    {
                        if (AccessData[i].Import == true)
                        {
                            string xmlFile = Path.Combine(_environment.ContentRootPath, "Uploads");
                            if (!Directory.Exists(xmlFile))
                            {
                                Directory.CreateDirectory(xmlFile);
                            }

                            // Save the uploaded Excel file.
                            string fileName = Path.GetFileName(File.FileName);
                            string filePath = Path.Combine(xmlFile, fileName);
                            using (FileStream stream = new FileStream(filePath, FileMode.Create))
                            {
                                File.CopyTo(stream);
                            }

                            XLWorkbook xl = new XLWorkbook(filePath);
                            int row = 2;
                            while (xl.Worksheets.Worksheet(1).Cell(row, 1).GetString() != "")
                            {
                                string Cat_Name = xl.Worksheets.Worksheet(1).Cell(row, 1).GetString();
                                string UnitType = xl.Worksheets.Worksheet(1).Cell(row, 2).GetString();
                                string Category = xl.Worksheets.Worksheet(1).Cell(row, 3).GetString();
                                int buyingPrice = Convert.ToInt32(xl.Worksheets.Worksheet(1).Cell(row, 4).GetString());
                                int SellingPrice = Convert.ToInt32(xl.Worksheets.Worksheet(1).Cell(row, 5).GetString());
                                int StockAlert = Convert.ToInt32(xl.Worksheets.Worksheet(1).Cell(row, 6).GetString());
                                int OpeningStock = Convert.ToInt32(xl.Worksheets.Worksheet(1).Cell(row, 7).GetString());
                                string Vendor = xl.Worksheets.Worksheet(1).Cell(row, 8).GetString();
                                //string Email = xl.Worksheets.Worksheet(1).Cell(row, 4).GetString();
                                //string Food_Preference = xl.Worksheets.Worksheet(1).Cell(row, 5).GetString();

                                //getting the id to for making sure that join works properly
                                int OrgId = GetOrgId(StaffId);
                                int UnitId = UnitTypeId(UnitType, StaffId, OrgId);
                                int CatId = CategoryId(Category, StaffId, OrgId);
                                int VendId = VendorId(Vendor, StaffId, OrgId);


                                Item item = new Item();
                                item.Name = Cat_Name;
                                item.Unit_Type_Id = UnitId;
                                item.Category_Id = CatId;
                                item.Vendor_Id = VendId;
                                item.Org_Id = OrgId;
                                item.Selling_Price = SellingPrice;
                                item.Buying_Price = buyingPrice;
                                item.Stock_Alert = StockAlert;
                                item.Opening_Stock = OpeningStock;
                                item.IsActive = true;
                                item.Updated_By = StaffId;
                                item.InsertedOn = DateTime.Now;

                                _dataContext.Items.Add(item);
                                _dataContext.SaveChanges();
                                row++;
                            }
                        }
                    }
                }
            }
        }






        //Function to get the orgid
        public int GetOrgId(int StaffId)
        {
            var orgId=_dataContext.Staff.Where(x=>x.Id== StaffId).FirstOrDefault();
            if (orgId==null)
            {
                return 0;
            }
            else
            {
                return orgId.OrgId;    
            }

        }


        //Function to get the id of unit type
        public int UnitTypeId(string UnitTypeName,int StaffId,int OrgId)
        {
            var UnitData = _dataContext.Unit_Type.Where(x => x.Name == UnitTypeName&&x.OrgId==OrgId).FirstOrDefault();
            if (UnitData != null)
            {
                UnitData.IsActive = true;
                UnitData.UpdatedBy = StaffId;
                UnitData.InsertedOn = DateTime.Now;
                _dataContext.Unit_Type.Update(UnitData);
                _dataContext.SaveChanges();
                return UnitData.Id;
            }
            else
            {
                UnitType unit=new UnitType();
                unit.Name = UnitTypeName;
                unit.IsActive = true;
                unit.UpdatedBy = StaffId;
                unit.InsertedOn = DateTime.Now;
                unit.OrgId=OrgId;
                _dataContext.Unit_Type.Add(unit);
                _dataContext.SaveChanges();
                return unit.Id;
            }
        }
        
        //Function to get The Category Id
        public int CategoryId(string CategoryTypeName,int StaffId,int OrgId)
        {
            var catdata=_dataContext.Category.Where(x=>x.Name == CategoryTypeName&&x.OrgId==OrgId).FirstOrDefault();
            if(catdata != null)
            {
                catdata.IsActive = true;
                catdata.UpdatedBy = StaffId;
                catdata.InsertedOn = DateTime.Now;
                _dataContext.Category.Update(catdata);
                _dataContext.SaveChanges();
                return catdata.Id;
            }
            else
            {
                Category Cat=new Category();
                Cat.Name = CategoryTypeName;
                Cat.IsActive = true;
                Cat.InsertedOn = DateTime.Now;
                Cat.UpdatedBy = StaffId;
                Cat.OrgId = OrgId;
                _dataContext.Category.Add(Cat);
                _dataContext.SaveChanges();
                return Cat.Id;
            }
        }

        //Function to get the Vendor Id
        public int VendorId(string VendorTypeName,int StaffId,int OrgId)
        {
            var Vendordata=_dataContext.Vendor.Where(x=>x.Name==VendorTypeName&&x.OrgId==OrgId).FirstOrDefault();
            if(Vendordata != null)
            {
                Vendordata.IsActive = true;
                Vendordata.UpdatedBy= StaffId;
                Vendordata.InsertedOn = DateTime.Now;
                _dataContext.Vendor.Update(Vendordata);
                _dataContext.SaveChanges();
                return Vendordata.Id;
            }
            else
            {
                Vendor vendor=new Vendor();
                vendor.Name = VendorTypeName;
                vendor.Email = "ABC@EXAMPLE.COM";
                vendor.Phone = "1234567891";
                vendor.IsActive = true;
                vendor.InsertedOn = DateTime.Now;
                vendor.UpdatedBy = StaffId;
                vendor.OrgId = OrgId;
                _dataContext.Vendor.Add(vendor);
                _dataContext.SaveChanges();
                return vendor.Id;
            }
        }





        public string GenerateHtmlInvoice(Organisation_Info info, List<object> orderData,Customer cust,Billing bill)
        {
            var currentDate = DateTime.Now.ToString("MM/dd/yyyy");

            var htmlContent = $@"
<!DOCTYPE html>
<html lang='en'>
<head>
    <meta charset='UTF-8'>
    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
    <title>Invoice</title>
    <style>
        body {{
            font-family: 'Arial', sans-serif;
            margin: 20px;
            line-height: 1.6;
        }}
        .invoice {{
            max-width: 600px;
            margin: 0 auto;
            background-color: #f8f9fa;
            padding: 20px;
            border: 1px solid #ced4da;
            border-radius: 10px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
        }}
        .header {{
            text-align: right;
            margin-bottom: 20px;
        }}
        .header p {{
            margin: 5px 0;
            font-size: 14px;
        }}
        h2 {{
            color: #343a40;
            border-bottom: 2px solid #007bff;
            padding-bottom: 10px;
            margin-bottom: 20px;
        }}
        table {{
            width: 100%;
            border-collapse: collapse;
            margin-bottom: 20px;
        }}
        th, td {{
            border: 1px solid #ced4da;
            padding: 10px;
            text-align: left;
        }}
        .total {{
            font-weight: bold;
            font-size: 18px;
            color: #343a40;
            margin-top: 20px;
        }}
    </style>
</head>
<body>
    <div class='invoice'>
        <header>
            <h1>Invoice</h1>
            <div class='company-info'>
                <img src='your_logo.png' alt='Your Logo'>
                <p>{info.Name}</p>
                <p>{info.Email}</p>
                <p>{info.PhoneNo}</p>
            </div>
        </header>
        <main>
            <div class='bill-to'>
                <h2>BILLED TO</h2>
                <p>{cust.Name}</p>
                <p>{cust.Email}</p>
                <p>{cust.Phone}</p>
                <p>{bill.BillingAddress}</p>
               
               
            </div>
            <div class='invoice-info'>
                <p><b>Invoice Number:</b> {bill.Invoice_No}</p>
                <p><b>Date of Issue:</b> {currentDate}</p>
            </div>
            <table class='items'>
                <thead>
                    <tr>
                        <th>Description</th>

                        <th>Qty</th>
                        <th>Unit Cost</th>
                        <th>Amount</th>
                    </tr>
                </thead>
                <tbody>
                    {GenerateOrderItemsHtml(orderData)}
                </tbody>
                <tfoot>
                    <tr>
                        <td colspan='2'>Subtotal</td>
                        <td></td>
                        <td>$0.00</td>
                    </tr>
                    <tr>
                        <td colspan='2'>Discount</td>
                        <td></td>
                        <td>$0.00</td>
                    </tr>
                    <tr>
                        <td colspan='2'>Tax rate</td>
                        <td></td>
                        <td>%</td>
                    </tr>
                    <tr>
                        <td colspan='2'>Tax</td>
                        <td></td>
                        <td>$0.00</td>
                    </tr>
                    <tr>
                        <td colspan='2'><b>INVOICE TOTAL</b></td>
                        <td></td>
                        <td><b>${CalculateTotalAmount(orderData)}</b></td>
                    </tr>
                </tfoot>
            </table>
            <div class='payment-terms'>
                <h4>TERMS</h4>
                <p>Please pay Invoice by MM/DD/YYYY</p>
                <p>Send money abroad with TransferWise.</p>
                <img src='transferwise_logo.png' alt='TransferWise'>
            </div>
        </main>
    </div>
</body>
</html>";

            return htmlContent;
        }



      
        private string GenerateOrderItemsHtml(List<object> orderData)
        {
            StringBuilder itemsHtml = new StringBuilder();

            foreach (var item in orderData)
            {
                itemsHtml.Append($@"
            <tr>
                <td>{item.GetType().GetProperty("ItemName").GetValue(item)}</td>
                <td>{item.GetType().GetProperty("Quantity").GetValue(item)}</td>
                <td>${item.GetType().GetProperty("ItemSellingPrice").GetValue(item)}</td>
                <td>${CalculateItemTotalPrice(item)}</td>
            </tr>");
            }

            return itemsHtml.ToString();
        }

        private decimal CalculateItemTotalPrice(object item)
        {
            decimal quantity = Convert.ToDecimal(item.GetType().GetProperty("Quantity").GetValue(item));
            decimal sellingPrice = (decimal)item.GetType().GetProperty("ItemSellingPrice").GetValue(item);
            return quantity * sellingPrice;
        }

        private decimal CalculateTotalAmount(List<object> orderData)
        {
            return orderData.Sum(item => CalculateItemTotalPrice(item));
        }








    }


}
