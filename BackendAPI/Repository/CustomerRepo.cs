using BackendAPI.IRepository;
using BackendAPI.Models;

namespace BackendAPI.Repository
{
    public class CustomerRepo:ICustomer
    {
        private readonly DataContext _context;
        public CustomerRepo(DataContext context)
        {
            _context = context;
        }

        public bool AddCustomer(Customer customer)
        {
           customer.InsertedOn=DateTime.Now;
            customer.IsActive=true;

            var data = _context.Customer.Where(x => x.Id == customer.Id && x.OrgId == customer.OrgId).FirstOrDefault();
            if(data != null)
            {
                data.IsActive = true;
                data.InsertedOn = DateTime.Now;
                _context.Customer.Update(data);
                _context.SaveChanges();
                return true;
            }
            
           else
            {
                _context.Customer.Add(customer);
                int i = _context.SaveChanges();
                if (i > 0)
                {
                    return true;
                }
                else
                    return false;
            }
        }

        public bool DeleteCustomer(int id, int StaffId)
        {
            var data = _context.Customer.Where(x => x.Id == id).FirstOrDefault();
            if (data != null)
            {
                data.IsActive = false;
                data.Updated_By = StaffId;
                _context.Customer.Update(data);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public List<Customer> GetCustomer(int orgid)
        {
            var data = _context.Customer.Where(x => x.OrgId == orgid).ToList();
            return data;
        }

        public Customer GetCustomerById(int id)
        {
            var data = _context.Customer.Where(x => x.Id == id).FirstOrDefault();
            return data;
        }

        public bool UpdateCustomer(Customer customer)
        {
            var data = _context.Customer.Where(x => x.Id == customer.Id).FirstOrDefault();
            if (data != null)
            {
                data.Name = customer.Name;
                data.Email = customer.Email;
                data.Phone = customer.Phone;
                data.Updated_By= customer.Updated_By;
                _context.Customer.Update(data) ;
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
