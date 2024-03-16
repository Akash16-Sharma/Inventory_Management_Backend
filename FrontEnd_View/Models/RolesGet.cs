namespace FrontEnd_View.Models
{
    public class RolesGet
    {
            public string staffName { get; set; }
            public string staffEamil { get; set; }
            public string staffRoleTypr { get; set; }
            public List<AccessRight> accessValue { get; set; }

        public class AccessRight
        {
            public int Id { get; set; }
            public string sideBarName { get; set; }
            public bool isCreate { get; set; }
            public bool isShow { get; set; }
            public bool isModify { get; set; }
            public bool import { get; set; }
            public bool export { get; set; }
            public DateTime insertedOn { get; set; }
            public bool isActive { get; set; }
            public int updated_By { get; set; }
            public int staffId { get; set; }
        }
    }
}
