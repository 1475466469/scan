using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;

namespace DAL
{
  public  class StockOut
    {

        
        private DCF19Entities db = new DCF19Entities();
        public StockOut()
        {

        }
      
        /// <summary>
        /// 根据仓库代号获取所有的出库单号
        /// </summary>
        /// <param name="fStkCode"></param>
        /// <returns></returns>
        public List<t_INVD_StkOutLogMst> Query_fStoutLogNo(string fStkCode)
        {
            try
            {
                List<t_INVD_StkOutLogMst> list = db.t_INVD_StkOutLogMst.Where(u => u.fStkCode == fStkCode & u.fScanFlag=="0" & u.fIfPost == "0" & u.fIfCancel=="0").ToList();
                return list;
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }


        }
        public List<fStoutLogNoList> Query_list(string fStkOutLogNo)
        {

            try
            {
                var data = from u in db.t_INVD_StkOutLogMst
                           join g in db.t_COPD_DlvMst
                           on u.fOriNo equals g.fDlvNo
                           join k in db.t_CRMM_CstMst
                           on g.fCCode equals k.fCCode
                           where u.fStkOutLogNo == fStkOutLogNo
                           select new
                           {
                               fStkOutLogNo = u.fStkOutLogNo,
                               fDlvNo = g.fDlvNo,
                               fCCode = k.fCCode,
                               fCName = k.fCName
                           };
                List<fStoutLogNoList> result = new List<fStoutLogNoList>();
                foreach(var item in data)
                {
                    fStoutLogNoList n = new fStoutLogNoList();
                    n.fStkOutLogNo = item.fStkOutLogNo;
                    n.fDlvNo = item.fDlvNo;
                    n.fCCode = item.fCCode;
                    n.fCName = item.fCName;
                    result.Add(n);
                }


                return result;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 根据出库单号拿取品牌专享款的货品
        /// </summary>
        /// <param name="fStkOutLogNo"></param>
        /// <returns></returns>
          public List<V_INVD_StkOutLogItemSum> Query_product(string fStkOutLogNo)
        {
            try
            {
                List<V_INVD_StkOutLogItemSum> data = db.V_INVD_StkOutLogItemSum.Where(u => u.fStkOutLogNo == fStkOutLogNo & u.fStyleName == "品牌专享款").ToList();

                return data;
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            


        }





    }
}
