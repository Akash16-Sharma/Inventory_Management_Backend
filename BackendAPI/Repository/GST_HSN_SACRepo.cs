using BackendAPI.IRepository;
using BackendAPI.Models;
using DocumentFormat.OpenXml.Office2010.Excel;

namespace BackendAPI.Repository
{
    public class GST_HSN_SACRepo:IGST_HSN_SAC
    {
        private readonly DataContext _context;
        public GST_HSN_SACRepo(DataContext context)
        {
            _context = context;
        }

        public bool AddGST(Gst gST)
        {
           gST.Inserted_On=DateTime.Now;
            _context.Gst.Add(gST);
            _context.SaveChanges(); 
            return true;
        }

        public bool AddHSN(Hsn hSN)
        {
           hSN.Inserted_On=DateTime.Now;
            _context.HSN_SAC.Add(hSN);
            _context.SaveChanges();
            return true;
        }

        public bool DeleteHSN(int id)
        {
            var data=_context.HSN_SAC.Where(x=>x.Id==id).FirstOrDefault();
            if (data!=null)
            {
                _context.HSN_SAC.Remove(data);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool Delete_GST_INFO(int id)
        {
            var data = _context.Gst.Where(x => x.Id == id).FirstOrDefault();
            if (data!=null)
            {
                _context.Gst.Remove(data);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public Gst GetGST(int id)
        {
            var data = _context.Gst.Where(x => x.Id == id).FirstOrDefault();
            if (data!=null)
            {
                return data;
            }
            return null;
        }

        public List <Gst> GetGstInfo(int org_Id)
        {
            var data = _context.Gst.Where(x => x.OrgId == org_Id).ToList();
            return data;
        }

        public Hsn GetHSN(int id)
        {
            var data = _context.HSN_SAC.Where(x => x.Id == id).FirstOrDefault();
            return data;
        }

        public List<object> GetHSNInfo(int Org_id)
        {
            var data = (from hsn in _context.HSN_SAC
                        join gst in _context.Gst on hsn.GSTID equals gst.Id
                        where hsn.OrgId==Org_id
                        select new
                        {
                            hsn.Id,
                            hsn.HSNCODE,
                            gst.TaxName,
                            gst.Tax_Percent,
                        }).ToList<object>();
            return data;
        }

        public bool UpdateHSN(Hsn hSN)
        {
            var data = _context.HSN_SAC.Where(x => x.Id == hSN.Id).FirstOrDefault();
            if(data != null)
            {
                data.HSNCODE=hSN.HSNCODE;
                data.GSTID = hSN.GSTID;
                data.Updated_By=hSN.Updated_By;
                data.Inserted_On=DateTime.Now;
                _context.HSN_SAC.Update(data);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool Update_GST_INFO(Gst gst)
        {
            var data = _context.Gst.Where(x => x.Id == gst.Id).FirstOrDefault();
            if(data != null)
            {
                data.TaxName=gst.TaxName;
                data.Tax_Percent=gst.Tax_Percent;
                data.Updated_By=gst.Updated_By;
                data.Inserted_On=DateTime.Now;
                _context.Gst.Update(data);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
