namespace INVT_MNGMNT.Model.DataModels
{
    public class Item
    {
        public int Id { get; set; }
        public int Org_Id { get; set; }
        public int Unit_Type_Id { get; set; }
        public int Category_Id { get; set; }
        public string Name { get; set; }
        public int Buying_Price {  get; set; }
        public int Selling_Price {  get; set; }
        public int Stock_Alert {  get; set; }
        public int Opening_Stock {  get; set; }
        public int Vendor_Id { get; set; }
        public string Barcode {  get; set; }
        public int Updated_By {  get; set; }
        public Boolean IsActive { get; set; }
        public DateTime InsertedOn {  get; set; }

    }
}
