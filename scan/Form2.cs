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


        private void lookUpEdit1_EditValueChanged(object sender, EventArgs e)
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
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                log.Wirtefile(admin.fUsrID + "切换仓库下拉框选择仓库引发异常：" + ex.Message);
            }
        }
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
               
                //if (data[0]._x_f008=="0")
                //{
                //    //MessageBox.Show("s申请中");
                //    barButtonItem2.Caption = "正在申请...";
                //    barButtonItem2.Enabled = false;
                //}
                //else if(data[0]._x_f008 == null)
                //{
                //    barButtonItem2.Caption = "申请特批";
                //    barButtonItem2.Enabled = true;
                //}
              
                product_2 = so.Query_product(searchLookUpEdit1.Text.Trim());
                gridControl1.DataSource = product_2;
                searchControl1.Enabled = false;

                LoadData(sf.Load(searchLookUpEdit1.Text.Trim()));

            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                log.Wirtefile(admin.fUsrID + "选择出库单引发异常:" + ex.Message);

            }



        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void searchControl1_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {

                if (e.KeyCode.GetHashCode() == 13)
                {
                    if (searchControl1.Text.Trim() != "")
                    {

                        BarcodeDetial data = bc.GetBarlist(searchControl1.Text);

                        if (barlist.Exists(u => u.fBarcode == searchControl1.Text))
                        {
                            MessageBox.Show("已经扫描过了");
                            log.Wirtefile(admin.fUsrID + "添加条码:" + searchControl1.Text+",提示已扫描");
                        }
                        else if(data==null)
                        {
                            MessageBox.Show("条码错误");
                            log.Wirtefile(admin.fUsrID + "添加条码:" + searchControl1.Text + ",提示条码错误");
                        }
                        else
                        {
                            List<V_INVD_StkOutLogItemSum> product = so.Query_product(searchLookUpEdit1.Text.Trim());

                            //查询是否有这个品号
                            V_INVD_StkOutLogItemSum result = product.Where(u => u.fGoodsCode.Equals(data.fGoodsCode)).FirstOrDefault();
                            if (data.fOrdNo.StartsWith("YC"))//条码订单号是yc开头
                            {
                               
                                if(result != null)
                                {
                                    log.Wirtefile(admin.fUsrID + "添加条码:" + searchControl1.Text + ".订单号为：" + data.fOrdNo);
                                    barlist.Add(data);
                                    gridControl2.RefreshDataSource();
                                 
                                }
                                else
                                {
                                    MessageBox.Show("此单不包含品号"+data.fGoodsCode);
                                    log.Wirtefile(admin.fUsrID + "添加条码:" + searchControl1.Text + "提示此单不包含品号："+ data.fGoodsCode);
                                }
                            }
                            else
                            {
                                if (result != null)
                                {
                                    t_INVD_StkOutLogItem t = so.GetFordNo(searchLookUpEdit1.Text.Trim());

                                    if (data.fOrdNo.Equals(t.fOrdNo))
                                    {
                                        log.Wirtefile(admin.fUsrID + "添加条码:" + searchControl1.Text + ".订单号为：" + data.fOrdNo);
                                        barlist.Add(data);
                                        gridControl2.RefreshDataSource();
                                       
                                    }
                                    else
                                    {
                                        MessageBox.Show("您的条码订单号为:"+data.fOrdNo+"  不匹配出库单的订单号");
                                        log.Wirtefile(admin.fUsrID + "添加条码:" + searchControl1.Text + "提示不匹配出库单的订单号：" + data.fOrdNo);

                                    }
                                   

                                }
                                else
                                {
                                    MessageBox.Show("此单不包含品号" + data.fGoodsCode);
                                   log.Wirtefile(admin.fUsrID + "添加条码:" + searchControl1.Text + "提示此单不包含品号：" + data.fGoodsCode);

                                }


                            }
                            
                            
                        }
                        

                    }
                    else
                    {
                        MessageBox.Show("请输入条码！");
                        log.Wirtefile(admin.fUsrID + "提示请输入条码！" );
                    }

                }

                if (barlist.Count > 0)
                {
                    barButtonItem8.Enabled = true;
                }
                else
                {
                    barButtonItem8.Enabled = false;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                log.Wirtefile(admin.fUsrID + "添加条码引发异常："+ex.Message);
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
                delbarlist(0);
                

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
                barlist.Clear();
                log.Wirtefile(admin.fUsrID + "清空扫描列表");
                gridControl2.RefreshDataSource();
                barButtonItem8.Enabled = false;
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
                delbarlist(1);

            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }


        /// <summary>
        /// 快捷菜单删除
        /// </summary>
        private void delbarlist(int flag)
        {

            int[] row = gridView2.GetSelectedRows();
            List<BarcodeDetial> del = new List<BarcodeDetial>();

           
            foreach (int i in row)
            {
                string barcode = gridView2.GetRowCellValue(i, "fBarcode").ToString();
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
            log.Wirtefile(admin.fUsrID + "选择删除成功" );

            if (flag == 1)
            {
                //删除出库单
                sf.del(searchLookUpEdit1.Text.Trim());
                List<DO_t_TemporaryScan> files = new List<DO_t_TemporaryScan>();
                foreach(BarcodeDetial b in barlist)
                {
                    DO_t_TemporaryScan ts = new DO_t_TemporaryScan()
                    {
                        fStOutLogNo = searchLookUpEdit1.Text.Trim(),
                        fPackNo = b.fPackNo,
                        fOrdNo = b.fOrdNo,
                        fBarcode = b.fBarcode,
                        fGoodsCode = b.fGoodsCode,
                        fSizeDesc = b.fSizeDesc,
                        fGoodsName = b.fGoodsName,

                        saveDate = DateTime.Now.ToString("d")
                    };
                    log.Wirtefile(admin.fUsrID + "添加保存临时文件：" + b.fBarcode);
                    files.Add(ts);
                    
                }
                sf.Save(files);
                log.Wirtefile(admin.fUsrID + "保存临时文件成功");
            }

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

        private void 重新加载ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {


                log.Wirtefile(admin.fUsrID + "点击重新加载");
                
                if (searchLookUpEdit1.Text.Trim() != "")
                {


                    List<DO_t_TemporaryScan> list = sf.Load(searchLookUpEdit1.Text.Trim());
                    if (list.Count == 0)
                    {
                        MessageBox.Show("你没有保存的历史数据");
                    }
                    else
                    {
                        LoadData(list);

                    }

                    if (barlist.Count > 0)
                    {
                        barButtonItem8.Enabled = true;
                    }
                    else
                    {
                        barButtonItem8.Enabled = false;
                    }
                }
                else
                {
                    MessageBox.Show("请先选择出库单");
                }
                }catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                log.Wirtefile(admin.fUsrID + "点击重新加载出现异常：" + ex.Message);

            }
        
        }



        
        private void LoadData(List<DO_t_TemporaryScan> list)
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

            gridControl2.RefreshDataSource();
        }

        private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (product_2.Count>0)
                {
                    if (barlist.Count > 0)
                    {
                        List<V_INVD_StkOutLogItemSum> product = so.Query_product(searchLookUpEdit1.Text.Trim());

                        for (int i = 0; i < product.Count; i++)
                        {
                            int row = barlist.Count(u => u.fGoodsCode == product[i].fGoodsCode);

                            if (row > (int)product[i].fPlanOutQty)
                            {
                                MessageBox.Show("品号：" + product[i].fGoodsCode + "多出" + (row - (int)product[i].fPlanOutQty) + "套");
                                return;
                            }
                            else if (row < (int)product[i].fPlanOutQty)
                            {
                                MessageBox.Show("品号：" + product[i].fGoodsCode + "差" + ((int)product[i].fPlanOutQty - row) + "套");
                                return;
                            }
                        }
                        List<Do_t_dious_Scan> ts = new List<Do_t_dious_Scan>();
                        foreach (BarcodeDetial item in barlist)
                        {
                         ts.Add(
                            new Do_t_dious_Scan()
                            {
                                fStkOutLogNo= product[0].fStkOutLogNo,
                                fOrdNo = item.fOrdNo,
                                fBarcode = item.fBarcode,
                                Date = DateTime.Now.ToString("d"),
                                fGoodsCode = item.fGoodsCode,
                                fGoodsName = item.fGoodsName,
                                fOrdSNo= so.GetFsno(so.GetFordNo(searchLookUpEdit1.Text.Trim()).fOrdNo, item.fGoodsName)
                            });
                        }
                        so.Save(ts);
                        MessageBox.Show("成功入库");
                        log.Wirtefile(admin.fUsrID + "出库单：" + searchLookUpEdit1.Text.Trim() + ",扫描完毕成功入库");
                        //更新出库单选择框
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
                        MessageBox.Show("您还没有扫描");
                    }
                }
                else
                {
                    MessageBox.Show("请选择需扫描的出库单");
                }
            }
            catch(Exception ex)
            {
                //throw ex;
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
    }
}
