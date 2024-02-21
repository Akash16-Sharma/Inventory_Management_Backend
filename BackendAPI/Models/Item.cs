using System;
using System.Drawing;
using ZXing;
using ZXing.Common;
using ZXing.QrCode;
using ZXing.Windows.Compatibility;


namespace BackendAPI.Models
{
    public class Item
    {
        public int Id { get; set; }
        public int Org_Id { get; set; }
        public int Unit_Type_Id { get; set; }
        public int Category_Id { get; set; }
        public string Name { get; set; }

        public decimal Buying_Price { get; set; }
        public decimal Selling_Price { get; set; }
        public int Stock_Alert { get; set; }
        public int Opening_Stock { get; set; }
        public int Vendor_Id { get; set; }
        public string Barcode { get; set; }
        public int Updated_By { get; set; }
        public bool IsActive { get; set; }
        public DateTime InsertedOn { get; set; }

        public string GenerateAndSaveBarcode()
        {
           
            // Generate barcode and save it to the Barcode property
            Barcode = GenerateBarcode(Name);

            // Return the generated barcode
            return Barcode;
        }

        private string GenerateBarcode(string content, BarcodeFormat format = BarcodeFormat.QR_CODE, int width = 300, int height = 300)
        {
            BarcodeWriter<Bitmap> barcodeWriter = new BarcodeWriter<Bitmap>();
            barcodeWriter.Format = format;
            barcodeWriter.Options = new QrCodeEncodingOptions
            {
                Width = width,
                Height = height
            };

            // Set a renderer here (using the default renderer for now)
            //barcodeWriter.Renderer = new ZXing.Rendering.BitmapRenderer();
            barcodeWriter.Renderer = new BitmapRenderer();
            using (Bitmap barcodeBitmap = barcodeWriter.Write(content))
            {
                // Convert the barcode bitmap to base64 string and return
               return Convert.ToBase64String(BitmapToBytes(barcodeBitmap));
               
            }
        }

        private byte[] BitmapToBytes(Bitmap bitmap)
        {
            using (var stream = new System.IO.MemoryStream())
            {
                bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                return stream.ToArray();
            }
        }
    }
}
