using BackendAPI.IRepository.Roles;
using ClosedXML.Excel;
using System;

namespace BackendAPI.Models.Class
{
    public class Controller_Functions
    {
        private Microsoft.AspNetCore.Hosting.IHostingEnvironment _environment;
        private readonly DataContext _dataContext;
        private readonly IRoles _Roles;

        public Controller_Functions(Microsoft.AspNetCore.Hosting.IHostingEnvironment environment, DataContext dataContext,IRoles Roles)
        {
            _environment = environment;
            _dataContext = dataContext;
            _Roles = Roles;
        }

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
                      //  item.Vendor_Id = VendId;
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
                               // item.Vendor_Id = VendId;
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
            var UnitData = _dataContext.Unit_Type.Where(x => x.Name == UnitTypeName).FirstOrDefault();
            if (UnitData != null)
            {
                UnitData.IsActive = true;
                
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
               
                unit.InsertedOn = DateTime.Now;
               
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
        
    }
}
