using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
  public  class Barcode
    {
        private static DCF19Entities db = new DCF19Entities();




        /// <summary>
        /// 根据条码获取产品详情
        /// </summary>
        /// <param name="Barcode"></param>
        /// <returns></returns>
        public BarcodeDetial GetBarlist(string Barcode)
        {

            try
            {
               

                var list = (from u in db.t_BCMM_BarcodeFgItem
                            join g in db.t_BCMM_BarcodeItem
                            on u.fPackNo equals g.fPackNo
                            join j in db.t_BCMM_BarcodeMst
                            on u.fPackNo equals j.fPackNo
                            join k in db.t_BOMM_GoodsMst
                            on u.fGoodsID equals k.fGoodsID
                            where g.fBarcode == Barcode
                            select new
                            {
                                fPackNo = u.fPackNo,
                                fOrdNo = j.fOrdNo,
                                fBarcode = g.fBarcode,
                                fGoodsCode = k.fGoodsCode,
                                fGoodsName = k.fGoodsName,
                                fSizeDesc = k.fSizeDesc,
                                fQty=u.fQty
                            }).FirstOrDefault();
                if (list == null)
                {
                    return null;
                }

                return new BarcodeDetial()
                {

                    fBarcode = list.fBarcode,
                    fGoodsCode = list.fGoodsCode,
                    fGoodsName = list.fGoodsName,
                    fSizeDesc = list.fSizeDesc,
                    fOrdNo = list.fOrdNo,
                    fPackNo = list.fPackNo
                };
            }
            catch (System.Data.Entity.Core.EntityException)
            {
                throw new System.Data.Entity.Core.EntityException("请检查网络！");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            //BarcodeDetial n = new BarcodeDetial();
            //n.fBarcode= list.fBarcode,



        }

        //根据条码的包装编号获取条码的个数
        public int GetBarcodeCount(string fPackNo)
        {

           return db.t_BCMM_BarcodeItem.Where(u => u.fPackNo == fPackNo).Count();
        }









    }
}
