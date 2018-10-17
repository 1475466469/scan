using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class loginBLL
    {
       
        
        public t_ADMM_UsrMst LoginHandle(string Usrid,string Pwd)
        {
            
            try
            {
                t_ADMM_UsrMst admin = new Login(Usrid, Pwd).Query();
                return admin;

            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
           
        }


        public List<StoreCode> GetStore(t_ADMM_UsrMst tsm)
        {

            return new Login().QueryStore(tsm);

        }
    }
}
