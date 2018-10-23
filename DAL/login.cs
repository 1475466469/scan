using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DAL
{
    public class Login
    {
        private DCF19Entities db = new DCF19Entities();
        private t_ADMM_UsrMst t = new t_ADMM_UsrMst();
       
        public Login()
        {
        }
        public Login(string Usrid,string Pwd)
        {
            this.t.fUsrID = Usrid;
            this.t.C_x_f010 = Pwd;
        }
        public Login(string Usrid)
        {
            this.t.fUsrID = Usrid;
        }
        /// <summary>
        /// 根据用户获取信息
        /// </summary>
        /// <returns></returns>
       public t_ADMM_UsrMst Query()
        {
            try
            {
                t_ADMM_UsrMst tum = db.t_ADMM_UsrMst.Where(u => u.fUsrID == t.fUsrID & u.C_x_f010==t.C_x_f010).FirstOrDefault();
                return tum;
            }catch(System.Data.Entity.Core.EntityException)
            {
                throw new System.Data.Entity.Core.EntityException("请检查网络！");
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        /// <summary>
        /// 获取所有仓库
        /// </summary>
        /// <param name="tum"></param>
        /// <returns></returns>
        public List<StoreCode> QueryStore(t_ADMM_UsrMst tum)
        {
            try
            {
                
                //select* from t_ADMM_UsrDataRightItem where fUsrID = '000099' and fItemCode = '13'
                var list = from u in db.t_ADMM_UsrDataRightItem
                            where u.fUsrID == tum.fUsrID & u.fItemCode == "13"
                            select new
                            {
                                fValue = u.fValue,
                                fValueDesc = u.fValueDesc
                            };
                if (list.Count() == 0)
                {

                    var list2 = (from u in db.t_ADMM_UsrItem
                                join g in db.t_ADMM_UsrDataRightMst on u.fGrpCode equals g.fUsrID
                                join k in db.t_ADMM_UsrDataRightItem on g.fUsrID equals k.fUsrID
                                where u.fUsrID == tum.fUsrID & k.fItemCode == "13"
                                select new
                                {
                                    fValue = k.fValue,
                                    fValueDesc = k.fValueDesc
                                }).ToList().Distinct();

                    List<StoreCode> data = new List<StoreCode>();
                    foreach (var item in list2)
                    {
                        StoreCode n = new StoreCode();
                        n.fValue = item.fValue;
                        n.fValueDesc = item.fValueDesc;
                        data.Add(n);
                    }
                    return data;
                }
                else
                {
                    List<StoreCode> data = new List<StoreCode>();
                    foreach (var item in list)
                    {
                        StoreCode n = new StoreCode();
                        n.fValue = item.fValue;
                        n.fValueDesc = item.fValueDesc;
                        data.Add(n);
                    }
                    return data;

                }





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



    }
}
