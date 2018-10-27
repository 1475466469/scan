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
        public List<V_INVD_StkOutLogItemSum> Query_product(string fStkOutLogNo)
        {
            return so.Query_product(fStkOutLogNo);
        }
        public t_INVD_StkOutLogItem GetFordNo(string fStkOutLogNo)
        {
            return so.GetFordNo(fStkOutLogNo);
        }
        public void Save(List<Do_t_dious_Scan> list)
        {
            so.Save(list);
        }
        public void updateStats(string fStkOutLogNo)
        {
            so.updateStats(fStkOutLogNo);
        }
        public int GetFsno(string fOrdNo, string fGoodsName)
        {
          return  so.GetFsno(fOrdNo, fGoodsName);
        }
    }
}
