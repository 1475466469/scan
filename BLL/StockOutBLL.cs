using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
   public class StockOutBLL
    {
        private StockOut so = new StockOut();

        public List<t_INVD_StkOutLogMst> Query_fStoutLogNo(string fStkCode)
        {

            return so.Query_fStoutLogNo(fStkCode);

        }
        public List<fStoutLogNoList> Query_list(string fStkOutLogNo)
        {
            return so.Query_list(fStkOutLogNo);
        }


    }
}
