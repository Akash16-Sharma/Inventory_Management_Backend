using BackendAPI.Models;

namespace BackendAPI.IRepository
{
    public interface IGST_HSN_SAC
    {
         bool AddGST(Gst gST);
       List <Gst> GetGstInfo(int org_Id);

        bool Update_GST_INFO(Gst gst);
        bool Delete_GST_INFO(int id);
        Gst GetGST(int id);

        //now definig the functions for HSN

        List<object> GetHSNInfo(int Org_id);
        bool AddHSN(Hsn hSN);
        bool UpdateHSN(Hsn hSN);
        bool DeleteHSN(int id);
        Hsn GetHSN(int id);

    }
}
