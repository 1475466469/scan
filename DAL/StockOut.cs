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
                List<t_INVD_StkOutLogMst> list = db.t_INVD_StkOutLogMst.Where(u => u.fStkCode == fStkCode & u.fScanFlag!="1" & u.fIfPost == "0" & u.fIfCancel=="0" & u.C_x_f008!="1").ToList();
                return list;
            }
            catch (System.Data.Entity.Core.EntityException)
            {
                throw new System.Data.Entity.Core.EntityException("请检查网络！");
            }
            catch (Exception ex)
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
                               fCName = k.fCName,
                               _x_f008=u.C_x_f008
                           };
                List<fStoutLogNoList> result = new List<fStoutLogNoList>();
                foreach(var item in data)
                {
                    fStoutLogNoList n = new fStoutLogNoList();
                    n.fStkOutLogNo = item.fStkOutLogNo;
                    n.fDlvNo = item.fDlvNo;
                    n.fCCode = item.fCCode;
                    n.fCName = item.fCName;
                    n._x_f008 = item._x_f008;
                    result.Add(n);
                }


                return result;
            }
            catch (System.Data.Entity.Core.EntityException)
            {
                throw new System.Data.Entity.Core.EntityException("请检查网络！");
            }
            catch (Exception ex)
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
            }
            catch (System.Data.Entity.Core.EntityException)
            {
                throw new System.Data.Entity.Core.EntityException("请检查网络！");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        //

        public t_INVD_StkOutLogItem GetFordNo(string fStkOutLogNo)
        {
            try
            {
                t_INVD_StkOutLogItem list = db.t_INVD_StkOutLogItem.Where(u => u.fStkOutLogNo == fStkOutLogNo).FirstOrDefault();
                return list;
            }
            catch (System.Data.Entity.Core.EntityException)
            {
                throw new System.Data.Entity.Core.EntityException("请检查网络！");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        
        /// <summary>
        /// 扫描完成后插入表
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public void Save(List<Do_t_dious_Scan> list)
        {
            try
            {
                foreach (Do_t_dious_Scan item in list)
                {
                    db.Do_t_dious_Scan.Add(item);
                }
                string fStOutLogNo = list[0].fStkOutLogNo;
                DCF19Entities db2 = new DCF19Entities();
                t_INVD_StkOutLogMst tsm = db2.t_INVD_StkOutLogMst.Where(u => u.fStkOutLogNo== fStOutLogNo).FirstOrDefault();
                tsm.fScanFlag = "1";
                tsm.fScanDate = DateTime.Now;
                db.SaveChanges();
                db2.SaveChanges();


            }
            catch (System.Data.Entity.Core.EntityException)
            {
                throw new System.Data.Entity.Core.EntityException("请检查网络！");
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException)
            {
                throw new System.Data.Entity.Infrastructure.DbUpdateException("请不要重复扫描");

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


        }
        //出库单的申请特批


        public void updateStats(string fStkOutLogNo)
        {

            try
            {
                t_INVD_StkOutLogMst tsm = db.t_INVD_StkOutLogMst.Where(u => u.fStkOutLogNo == fStkOutLogNo).FirstOrDefault();
                tsm.C_x_f008 = "1";
                tsm.C_x_f009 = DateTime.Now;
                db.SaveChanges();
            }
            catch (System.Data.Entity.Core.EntityException)
            {
                throw new System.Data.Entity.Core.EntityException("请检查网络！");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        //根据出库单号和品号获取订单行号

        public int GetFsno(string fOrdNo, string fGoodsName)
        {

            t_INVD_StkOutLogItem tsm = db.t_INVD_StkOutLogItem.Where(u => u.fOrdNo == fOrdNo & u.fGoodsName == fGoodsName).FirstOrDefault();

            return tsm.fOrdSNo;
        }



    }
}
