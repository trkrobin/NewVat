using newvat.Services;
using newvat.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace newvat.Repository
{
    public class CustomerRepo
    {
        public List<CustomerGroupVM> DropDown()
        {
            try
            {
                return new CustomerDAL().DropDown();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<CustomerVM> SelectAll(string Id = null, string[] conditionFields = null, string[] conditionValues = null)
        {
            try
            {
                return new CustomerDAL().SelectAll(Id, conditionFields, conditionValues);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string[] Insert(CustomerVM vm)
        {
            try
            {
                return new CustomerDAL().Insert(vm);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string[] Update(CustomerVM vm)
        {
            try
            {
                return new CustomerDAL().Update(vm);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string[] Delete(string[] ids)
        {
            try
            {
                return new CustomerDAL().Delete(ids);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
