using BLL;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace scan
{
    public partial class Form2 : Form
    {
        private loginBLL n = new loginBLL();
        private StockOutBLL so = new StockOutBLL();
        private BarcodeBLL bc = new BarcodeBLL();
        private ScanfileBLL sf = new ScanfileBLL();
        private List<BarcodeDetial> barlist = new List<BarcodeDetial>();
        private List<V_INVD_StkOutLogItemSum> product_2;
        private Log log = new Log();
        private t_ADMM_UsrMst admin;
        public Form2( )
        {
            InitializeComponent();

        }
        public Form2(t_ADMM_UsrMst t)
        {
            try
            {
                admin = t;

                InitializeComponent();
                log.Wirtefile(t.fUsrID + "登陆成功打开主页面");
                lookUpEdit1.Properties.ValueMember = "fValue";
                lookUpEdit1.Properties.DisplayMember = "fValueDesc";
                lookUpEdit1.Properties.NullText = "";
                searchLookUpEdit1.Properties.NullText = "";
                lookUpEdit1.Properties.DataSource = n.GetStore(t);
                log.Wirtefile(t.fUsrID + "成功获取仓库");
                gridControl2.DataSource = barlist;
                gridView2.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.View_Common1_CustomDrawRowIndicator);
               

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                log.Wirtefile(t.fUsrID + "登陆打开主页面引发异常:"+ex.Message);
            }


        }


        //private void lookUpEdit1_EditValueChanged(object sender, EventArgs e)
        //{
        //    try
        //    {


        //        List<t_INVD_StkOutLogMst> data = so.Query_fStoutLogNo(lookUpEdit1.EditValue.ToString());
        //        log.Wirtefile(admin.fUsrID + "切换仓库下拉框选择仓库：" + lookUpEdit1.EditValue.ToString());
        //        textEdit1.Text = "";
        //        textEdit2.Text = "";
        //        textEdit3.Text = "";
        //        searchLookUpEdit1.Properties.ValueMember = "fStkOutLogNo";
        //        searchLookUpEdit1.Properties.DisplayMember = "fStkOutLogNo";
        //        searchLookUpEdit1.Properties.DataSource = data;
        //        searchLookUpEdit1.Properties.NullText = "";
        //    }catch(Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //        log.Wirtefile(admin.fUsrID + "切换仓库下拉框选择仓库引发异常：" + ex.Message);
        //    }
        //}
        private void View_Common1_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }
        private void searchLookUpEdit1_EditValueChanged(object sender, EventArgs e)
        {
            try
            {

                List<fStoutLogNoList> data = so.Query_list(searchLookUpEdit1.Text.Trim());
                log.Wirtefile(admin.fUsrID + "选择出库单:" + searchLookUpEdit1.Text.Trim());
                textEdit1.Text = data[0].fDlvNo;
                textEdit2.Text = data[0].fCCode;
                textEdit3.Text = data[0].fCName;

                product_2 = so.Query_product(searchLookUpEdit1.Text.Trim());
                gridControl1.DataSource = product_2;
                searchControl1.Enabled = false;
               
              List<BarcodeDetial> result= LoadData(sf.Load(searchLookUpEdit1.Text.Trim()));
              gridControl2.RefreshDataSource();

                //拿取备份文件重新计算已扫描数据
               List<string>arr = new List<string>();
                foreach(BarcodeDetial item in result)
                {
                    BarcodeDetial code = bc.GetBarlist(item.fBarcode);//获取到条码详情
                    int count = bc.GetBarcodeCount(code.fPackNo);//获取包装编号的条码个数
                    V_INVD_StkOutLogItemSum pr = product_2.Where(u => u.fGoodsCode == item.fGoodsCode).FirstOrDefault();//获取在左边的数据
                    int row = product_2.IndexOf(pr);  //取出行数

                    if (count > 1)
                    {

                        if (result.Where(u => u.fPackNo == code.fPackNo).Count() == count)
                        {
                            if (!arr.Contains(code.fPackNo))
                            {
                                arr.Add(code.fPackNo);
                                gridView1.SetRowCellValue(row, "mun", Convert.ToInt32(gridView1.GetRowCellValue(row, "mun").ToString()) + Convert.ToInt32(Decimal.Parse(code.fQty)));

                            }
                           




                        }
                        

                    }
                    else
                    {
                        gridView1.SetRowCellValue(row, "mun", Convert.ToInt32(gridView1.GetRowCellValue(row, "mun").ToString()) + Convert.ToInt32(Decimal.Parse(code.fQty)));
                      // gridView1.set
                    }



                }












            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                log.Wirtefile(admin.fUsrID + "选择出库单引发异常:" + ex.Message);

            }



        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }



        //条码扫描回车事件
        private void searchControl1_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.GetHashCode() == 13)
                {

                    if (searchControl1.Text.Trim() != "")
                    {
                        BarcodeDetial data = bc.GetBarlist(searchControl1.Text);//获取到条码详情
                        if (data != null)//判断条码是否有误
                        {
                            List<V_INVD_StkOutLogItemSum> pr = so.Query_product(searchLookUpEdit1.Text.Trim());//查询出库单的所有明细
                            List<fPackNoList> list = bc.GetBarcodeList(data.fPackNo);//拿到所有条码明细
                            if (data.fOrdNo.StartsWith("YC"))
                            {
                                foreach (fPackNoList item in list) //循环明细列表
                                {
                                    if (pr.Where(u => u.fGoodsCode == item.fGoodsCode).Count() > 0)
                                    {
                                        MessageBox.Show("条码的")

                                    }
                                    else
                                    {

                                    }

                                }


                            }
                            else
                            {




                            }








                        }
                        else
                        {
                            MessageBox.Show("条码错误");
                        }


                    }
                    else
                    {
                        MessageBox.Show("请输入条码");
                    }

                }













                //if (e.KeyCode.GetHashCode() == 13)
                //{
                //    if (searchControl1.Text.Trim() != "")
                //    {
                //        BarcodeDetial data = bc.GetBarlist(searchControl1.Text);//获取到条码详情
                //        if(data != null)//判断条码是否有误
                //        {
                        //List<V_INVD_StkOutLogItemSum> product = so.Query_product(searchLookUpEdit1.Text.Trim());//查询出库单的所有明细
                        //    V_INVD_StkOutLogItemSum result = product.Where(u => u.fGoodsCode.Equals(data.fGoodsCode)).FirstOrDefault();//查询是否有这个品号
                        //    //if (data.fOrdNo.StartsWith("YC"))  //判断条码类型
                            //{
                               
                                //if(result != null)
                                //{
                                   
                                    //查询这个条码的包装编号有多少个条码
                                    //if (item.mun < item.fPlanOutQty)
                                    //{
                                        //int count = bc.GetBarcodeCount(data.fPackNo);//获取包装编号的条码个数
                                        //if (count > 1) //大于一个的条码
                                        //{










                                            //if (!barlist.Exists(u => u.fBarcode == data.fBarcode))
                                            //{

                                            //    if (count - barlist.Where(u => u.fPackNo == data.fPackNo).Count() == 1)
                                            //    {

                                            //             List<fPackNoList> list = bc.GetBarcodeList(data.fPackNo);
                                            //           foreach(fPackNoList i in list)//循环所有的品号
                                            //      {
                                            //    if (item.fPlanOutQty < Convert.ToInt32(Decimal.Parse(data.fQty)))
                                            //    {
                                            //        MessageBox.Show("条码包含的数量大于计划扫描数不允许扫描");
                                            //        return;
                                            //    }
                                            //    if ((Convert.ToInt32(Decimal.Parse(data.fQty)) + item.mun) > item.fPlanOutQty)
                                            //    {
                                            //        MessageBox.Show("条码包含的数量加上已扫描数超出了计划数不允许扫描");
                                            //        return;
                                            //    }
                                                        
                                            //     List<V_INVD_StkOutLogItemSum> d= (List < V_INVD_StkOutLogItemSum >) gridControl1.DataSource;
                                            //     if( d.Where(u => u.fGoodsCode == i.fGoodsCode).Count() > 0)//判断是否存在此品号
                                            //     {
                                                    
                                                    




                                            //     }
                                            //     else
                                            //    {
                                            //                MessageBox.Show("出库单不包含品号：" + i.fGoodsCode + ",此条码无效");
                                            //                return;
                                            //    }






                                            //}



                                            //        int row = product_2.IndexOf(item);
                                            //        gridView1.FocusedRowHandle = row;
                                            //        gridView1.SetFocusedRowCellValue("mun", item.mun + Convert.ToInt32(Decimal.Parse(data.fQty)));
                                            //        barlist.Add(data);
                                            //        gridControl2.RefreshDataSource();
                                            //        searchControl1.Text = "";
                                            //        if (barlist.Count > 0)
                                            //        {
                                            //            barButtonItem8.Enabled = true;
                                            //        }
                                            //        else
                                            //        {
                                            //            barButtonItem8.Enabled = false;
                                            //        }

                                            //    }
                                            //    else
                                            //    {
                                            //        barlist.Add(data);
                                            //        gridControl2.RefreshDataSource();
                                            //        searchControl1.Text = "";
                                            //        if (barlist.Count > 0)
                                            //        {
                                            //            barButtonItem8.Enabled = true;
                                            //        }
                                            //        else
                                            //        {
                                            //            barButtonItem8.Enabled = false;
                                            //        }
                                            //    }
                                            //}
                                            //else
                                            //{
                                            //    MessageBox.Show("不要重复扫描");
                                            //}

                    //                    }
                    //                    else
                    //                    {
                    //                        if (!barlist.Exists(u => u.fBarcode == data.fBarcode)) //判断是否有相同条码
                    //                        {
                    //                            if(item.fPlanOutQty< Convert.ToInt32(Decimal.Parse(data.fQty)))
                    //                            {
                    //                                MessageBox.Show("条码包含的数量大于计划扫描数不允许扫描");
                    //                                return;
                    //                            }
                    //                            if ((Convert.ToInt32(Decimal.Parse(data.fQty)) + item.mun) > item.fPlanOutQty)
                    //                            {
                    //                                MessageBox.Show("条码包含的数量加上已扫描数超出了计划数不允许扫描");
                    //                                return;
                    //                            }

                    //                            //标准条码
                    //                            //V_INVD_StkOutLogItemSum item = product_2.Where(u => u.fGoodsCode == data.fGoodsCode).FirstOrDefault();
                    //                            int row = product_2.IndexOf(item);
                    //                            gridView1.FocusedRowHandle = row;

                    //                            //判断条码已扫描数与是否与订单实际数相同
                                                     
                    //                                gridView1.SetFocusedRowCellValue("mun", item.mun + Convert.ToInt32(Decimal.Parse(data.fQty)));
                    //                                barlist.Add(data);
                    //                                gridControl2.RefreshDataSource();
                    //                                searchControl1.Text = "";
                    //                            if (barlist.Count > 0)
                    //                            {
                    //                                barButtonItem8.Enabled = true;
                    //                            }
                    //                            else
                    //                            {
                    //                                barButtonItem8.Enabled = false;
                    //                            }




                    //                        }
                    //                        else
                    //                        {
                    //                            MessageBox.Show("请不要重复扫描");
                    //                        }





                    //                    }
                    //                }
                    //                else
                    //                {
                    //                    MessageBox.Show("已经满足计划数不需要再扫描了");
                    //                }
                    //            }
                    //            else
                    //            {
                    //                MessageBox.Show("不包含此品号");
                    //            }

                    //        }
                    //        else if(data.fOrdNo.StartsWith("XS"))  //销售订单条码
                    //        {
                    //            if (result != null)
                    //            {

                    //                if (data.fOrdNo == so.GetFordNo(searchLookUpEdit1.Text.Trim()).fOrdNo)   //订单号一致
                    //                {
                    //                    int count = bc.GetBarcodeCount(data.fPackNo);//获取包装编号的条码个数

                    //                    if (count > 1) //大于一个的条码
                    //                    {
                    //                        if (!barlist.Exists(u => u.fBarcode == data.fBarcode))
                    //                        {

                    //                            if (count - barlist.Where(u => u.fPackNo == data.fPackNo).Count() == 1)
                    //                            {
                    //                                V_INVD_StkOutLogItemSum item = product_2.Where(u => u.fGoodsCode == data.fGoodsCode).FirstOrDefault();
                    //                                int row = product_2.IndexOf(item);
                    //                                gridView1.FocusedRowHandle = row;
                    //                                gridView1.SetFocusedRowCellValue("mun", item.mun + Convert.ToInt32(Decimal.Parse(data.fQty)));
                    //                                barlist.Add(data);
                    //                                gridControl2.RefreshDataSource();
                    //                                searchControl1.Text = "";
                    //                                barButtonItem8.Enabled = false;
                    //                            }
                    //                            else
                    //                            {
                    //                                barlist.Add(data);
                    //                                gridControl2.RefreshDataSource();
                    //                                searchControl1.Text = "";
                    //                                barButtonItem8.Enabled = false;
                    //                            }
                    //                        }
                    //                        else
                    //                        {
                    //                            MessageBox.Show("不要重复扫描");
                    //                        }

                    //                    }
                    //                    else
                    //                    {
                    //                        if (!barlist.Exists(u => u.fBarcode == data.fBarcode)) //判断是否有相同条码
                    //                        {
                    //                            //标准条码
                    //                            V_INVD_StkOutLogItemSum item = product_2.Where(u => u.fGoodsCode == data.fGoodsCode).FirstOrDefault();
                    //                            int row = product_2.IndexOf(item);
                    //                            gridView1.FocusedRowHandle = row;

                    //                            //判断条码已扫描数与是否与订单实际数相同
                    //                            if (item.mun < item.fPlanOutQty)
                    //                            {
                    //                                gridView1.SetFocusedRowCellValue("mun", item.mun + Convert.ToInt32(Decimal.Parse(data.fQty)));
                    //                                barlist.Add(data);
                    //                                gridControl2.RefreshDataSource();
                    //                                searchControl1.Text = "";
                    //                                barButtonItem8.Enabled = false;

                    //                            }
                    //                            else
                    //                            {
                    //                                MessageBox.Show("数量不能多出计划出库数");
                    //                            }



                    //                        }
                    //                        else
                    //                        {
                    //                            MessageBox.Show("请不要重复扫描");
                    //                        }





                    //                    }











                    //                }
                    //                else
                    //                {
                    //                    MessageBox.Show("此条码订单号为：" + data.fOrdNo + "与出库单订单号不匹配");
                    //                }

                    //            }
                    //            else
                    //            {
                    //                MessageBox.Show("出库单没有这个品号");
                    //            }




                    //        }




                    //    }
                    //    else
                    //    {
                    //        MessageBox.Show("条码错误");
                    //    }
                       
                      


                    //}
                    //else
                    //{
                    //    MessageBox.Show("请输入条码");
                    //}
               // }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
            
            
            
            
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            log.Wirtefile(admin.fUsrID + "点击开始扫描按钮");

            if (searchLookUpEdit1.Text.Trim() != "")
            {
                if (gridView1.RowCount > 0)
                {
                    searchControl1.Enabled = true;
                    searchControl1.Focus();
                }
                else
                {
                    MessageBox.Show("此单不需要扫描");
                    log.Wirtefile(admin.fUsrID + "系统提示不需要扫描");
                }


            }
            else
            {
                MessageBox.Show("请选择出库单");
                log.Wirtefile(admin.fUsrID + "系统提示请选择出库单");
            }



        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                log.Wirtefile(admin.fUsrID + "点击删除快捷菜单");
                delbarlist();
                

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                log.Wirtefile(admin.fUsrID + "删除快捷菜单引发异常："+ex.Message);
            }
            
        }

        private void gridControl2_Click(object sender, EventArgs e)
        {
          
        }

        private void 清空ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //清空列表重置已扫描数
                barlist.Clear();
                gridControl2.RefreshDataSource();
                product_2 = so.Query_product(searchLookUpEdit1.Text.Trim());
                List<V_INVD_StkOutLogItemSum> product = new List<V_INVD_StkOutLogItemSum>();
                foreach(V_INVD_StkOutLogItemSum item in product_2)
                {
                    item.mun = 0;
                    product.Add(item);

                }
                gridControl1.DataSource = product;
                gridControl1.RefreshDataSource();
                log.Wirtefile(admin.fUsrID + "清空扫描列表重置扫描数量");
                
              
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                log.Wirtefile(admin.fUsrID + "点击清空扫描列表出现异常："+ex.Message);
            }
               

           

        }

        private void barButtonItem8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {


                if (gridView2.RowCount > 0)
                {
                    List<DO_t_TemporaryScan> files = new List<DO_t_TemporaryScan>();
                    sf.del(searchLookUpEdit1.Text.Trim());
                    foreach (BarcodeDetial item in barlist)
                    {
                        DO_t_TemporaryScan ts = new DO_t_TemporaryScan()
                        {
                            fStOutLogNo = searchLookUpEdit1.Text.Trim(),
                            fPackNo = item.fPackNo,
                            fOrdNo = item.fOrdNo,
                            fBarcode = item.fBarcode,
                            fGoodsCode = item.fGoodsCode,
                            fSizeDesc = item.fSizeDesc,
                            fGoodsName = item.fGoodsName,
                            saveDate = DateTime.Now.ToString("d")
                        };

                        log.Wirtefile(admin.fUsrID + "保存条码："+ts.fBarcode);
                        files.Add(ts);

                    }
                    int row = sf.Save(files);
                    if (row > 0)
                    {
                        MessageBox.Show("保存成功");
                        log.Wirtefile(admin.fUsrID + "保存临时文件共：" + barlist.Count+"行");
                    }
                }

            }
            catch (System.Data.Entity.Core.EntityException ex)
            {
                MessageBox.Show("请检查网络");
                log.Wirtefile(admin.fUsrID + "保存临时文件出现异常："+ex.Message );
            }


            catch (Exception ex)
            {
                MessageBox.Show("已在别的出库单扫描过");
                log.Wirtefile(admin.fUsrID + "保存临时文件出现异常2：" + ex.Message);
            }

        }

        private void 删除并ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
               // delbarlist();

            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }


        /// <summary>
        /// 快捷菜单删除
        /// </summary>
        private void delbarlist()
        {

            int[] row = gridView2.GetSelectedRows();
            List<BarcodeDetial> del = new List<BarcodeDetial>();

           
            foreach (int i in row)
            {
                string barcode = gridView2.GetRowCellValue(i, "fBarcode").ToString();
                BarcodeDetial data = bc.GetBarlist(barcode);//获取到条码详情
                int count = bc.GetBarcodeCount(data.fPackNo);//获取包装编号的条码个数

                if (count > 1)
                {
                    //混合条码
                    V_INVD_StkOutLogItemSum item = product_2.Where(u => u.fGoodsCode == data.fGoodsCode).FirstOrDefault(); 
                    int r = product_2.IndexOf(item);
                    gridView1.FocusedRowHandle = r;
                    if (barlist.Where(u => u.fPackNo == data.fPackNo).Count() == count)
                    {//如果相同则减去
                        gridView1.SetFocusedRowCellValue("mun", item.mun - Convert.ToInt32(Decimal.Parse(data.fQty)));
                        searchControl1.Text = "";
                    }
                    else
                    {

                    }

                }
                else
                {
                    V_INVD_StkOutLogItemSum item = product_2.Where(u => u.fGoodsCode == data.fGoodsCode).FirstOrDefault();
                    int r = product_2.IndexOf(item);
                    gridView1.FocusedRowHandle = r;
                    gridView1.SetFocusedRowCellValue("mun", item.mun - Convert.ToInt32(Decimal.Parse(data.fQty)));
                    searchControl1.Text = "";

                }

            
                foreach (BarcodeDetial item in barlist)
                {

                    if (item.fBarcode.Equals(barcode))
                    {
                        del.Add(item);
                        log.Wirtefile(admin.fUsrID + "添加选择删除：" + item.fBarcode);
                    }
                    
                }


            }
            barlist.RemoveAll(u => del.Contains(u));
          

            gridControl2.RefreshDataSource();
            if (barlist.Count > 0)
            {
                barButtonItem8.Enabled = true;
            }
            else
            {
                barButtonItem8.Enabled = false;
            }

        }

        private void 清空并更新ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {


                log.Wirtefile(admin.fUsrID + "点击清除并更新按钮");
                barlist.Clear();
                sf.del(searchLookUpEdit1.Text.Trim());
                log.Wirtefile(admin.fUsrID + "点击清除并更新按钮更新了临时表");
                gridControl2.RefreshDataSource();
                barButtonItem8.Enabled = false;
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                log.Wirtefile(admin.fUsrID + "点击清空并更新出现异常：" + ex.Message);
            }
        }

  



        
        private List<BarcodeDetial> LoadData(List<DO_t_TemporaryScan> list)
        {
            barlist.Clear();
            foreach (DO_t_TemporaryScan item in list)
            {
                BarcodeDetial n = new BarcodeDetial()
                {
                    fPackNo = item.fPackNo,
                    fOrdNo = item.fOrdNo,
                    fBarcode = item.fBarcode,
                    fGoodsCode = item.fGoodsCode,
                    fGoodsName = item.fGoodsName,
                    fSizeDesc = item.fSizeDesc
                };
                barlist.Add(n);
                log.Wirtefile(admin.fUsrID + "加载到条码："+n.fBarcode);
            }
            return barlist;
           
        }

        private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (gridView1.RowCount > 0 & gridView2.RowCount > 0)
                {
                    List<V_INVD_StkOutLogItemSum> vi = (List<V_INVD_StkOutLogItemSum>)gridControl1.DataSource;
                    foreach (V_INVD_StkOutLogItemSum item in vi)
                    {
                        if (Convert.ToInt32(item.mun) != Convert.ToInt32(Decimal.Parse(item.fPlanOutQty.ToString())))
                        {
                            MessageBox.Show("品号：" + item.fGoodsCode + "已扫描数量不符合");
                            return;
                        }
                    }
                    List<Do_t_dious_Scan> ts = new List<Do_t_dious_Scan>();
                    foreach (BarcodeDetial item in barlist)
                    {
                        ts.Add(
                           new Do_t_dious_Scan()
                           {
                               fStkOutLogNo = searchLookUpEdit1.Text.Trim(),
                               fOrdNo = item.fOrdNo,
                               fBarcode = item.fBarcode,
                               Date = DateTime.Now.ToString("d"),
                               fGoodsCode = item.fGoodsCode,
                               fGoodsName = item.fGoodsName,
                               fOrdSNo = so.GetFsno(so.GetFordNo(searchLookUpEdit1.Text.Trim()).fOrdNo, item.fGoodsName)
                           });
                    }
                    so.Save(ts);
                    MessageBox.Show("成功入库");
                    sf.del(searchLookUpEdit1.Text.Trim());
                    log.Wirtefile(admin.fUsrID + "出库单：" + searchLookUpEdit1.Text.Trim() + ",扫描完毕成功入库");
                    List<t_INVD_StkOutLogMst> data = so.Query_fStoutLogNo(lookUpEdit1.EditValue.ToString());
                    textEdit1.Text = "";
                    textEdit2.Text = "";
                    textEdit3.Text = "";
                    searchLookUpEdit1.Properties.ValueMember = "fStkOutLogNo";
                    searchLookUpEdit1.Properties.DisplayMember = "fStkOutLogNo";
                    searchLookUpEdit1.Properties.DataSource = data;
                    searchLookUpEdit1.Properties.NullText = "";
                    //清空扫描结果
                    barlist.Clear();
                    gridControl2.RefreshDataSource();
                    //清空参考产品
                    product_2.Clear();
                    gridControl1.RefreshDataSource();

                }
                else
                {
                    MessageBox.Show("请扫描条码");
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                log.Wirtefile(admin.fUsrID + "点击完成扫描出现异常：" + ex.Message);
            }

           
        }
        private void searchControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {


            try
            {
                if (searchLookUpEdit1.Text.Trim() != "")
                {

                    DialogResult result = XtraMessageBox.Show("确定要特批吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (result == DialogResult.Cancel)
                    {
                       
                    }
                    else
                    {


                        so.updateStats(searchLookUpEdit1.Text.Trim());
                        MessageBox.Show("成功特批！");
                        log.Wirtefile(admin.fUsrID + "成功特批出库单：" + searchLookUpEdit1.Text.Trim());
                        List<t_INVD_StkOutLogMst> data = so.Query_fStoutLogNo(lookUpEdit1.EditValue.ToString());
                        textEdit1.Text = "";
                        textEdit2.Text = "";
                        textEdit3.Text = "";
                        searchLookUpEdit1.Properties.ValueMember = "fStkOutLogNo";
                        searchLookUpEdit1.Properties.DisplayMember = "fStkOutLogNo";
                        searchLookUpEdit1.Properties.DataSource = data;
                        searchLookUpEdit1.Properties.NullText = "";
                    }

                }
                else
                {
                    MessageBox.Show("请选择出库单");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                log.Wirtefile(admin.fUsrID + "申请特批时出现异常：" + ex.Message);
            }
        }

        private void barButtonItem9_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            
        }
        //窗口关闭事件
        protected override void OnClosing(CancelEventArgs e)
        {

          DialogResult result=  XtraMessageBox.Show("确定要退出吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.Cancel)
            {
                e.Cancel = true;
            }
            else
            {

                log.Wirtefile(admin.fUsrID + "退出程序");
                Application.Exit();
            }

        }

        private void lookUpEdit1_EditValueChanged_1(object sender, EventArgs e)
        {
            try
            {


                List<t_INVD_StkOutLogMst> data = so.Query_fStoutLogNo(lookUpEdit1.EditValue.ToString());
                log.Wirtefile(admin.fUsrID + "切换仓库下拉框选择仓库：" + lookUpEdit1.EditValue.ToString());
                textEdit1.Text = "";
                textEdit2.Text = "";
                textEdit3.Text = "";
                searchLookUpEdit1.Properties.ValueMember = "fStkOutLogNo";
                searchLookUpEdit1.Properties.DisplayMember = "fStkOutLogNo";
                searchLookUpEdit1.Properties.DataSource = data;
                searchLookUpEdit1.Properties.NullText = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                log.Wirtefile(admin.fUsrID + "切换仓库下拉框选择仓库引发异常：" + ex.Message);
            }

        }

        private void barButtonItem10_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            new Form3(admin).ShowDialog();
        }

        private void barButtonItem11_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Hide();
            new Form1().ShowDialog();

        }
        private void barButtonItem12_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Application.Exit();
        }



        //取消扫描
        private void barButtonItem13_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                //清空列表重置已扫描数
                barlist.Clear();
                sf.del(searchLookUpEdit1.Text.Trim());
                gridControl2.RefreshDataSource();
                product_2 = so.Query_product(searchLookUpEdit1.Text.Trim());
                List<V_INVD_StkOutLogItemSum> product = new List<V_INVD_StkOutLogItemSum>();
                foreach (V_INVD_StkOutLogItemSum item in product_2)
                {
                    item.mun = 0;
                    product.Add(item);

                }
                gridControl1.DataSource = product;
                gridControl1.RefreshDataSource();
                log.Wirtefile(admin.fUsrID + "清空扫描列表重置扫描数量");


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                log.Wirtefile(admin.fUsrID + "点击清空扫描列表出现异常：" + ex.Message);
            }



          

        }
    }
}
