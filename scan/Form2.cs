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
        public Form2( )
        {
            InitializeComponent();

        }
        public Form2(t_ADMM_UsrMst t)
        {
            InitializeComponent();
            lookUpEdit1.Properties.ValueMember = "fValue";   
            lookUpEdit1.Properties.DisplayMember = "fValueDesc";
            searchLookUpEdit1.Properties.NullText = "";
            lookUpEdit1.Properties.DataSource = n.GetStore(t);
            gridControl2.DataSource = barlist;
            gridView2.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.View_Common1_CustomDrawRowIndicator);



        }


        private void lookUpEdit1_EditValueChanged(object sender, EventArgs e)
        {
          List<t_INVD_StkOutLogMst> data= so.Query_fStoutLogNo(lookUpEdit1.EditValue.ToString());
            textEdit1.Text = "";
            textEdit2.Text = "";
            textEdit3.Text = "";
            searchLookUpEdit1.Properties.ValueMember = "fStkOutLogNo";
            searchLookUpEdit1.Properties.DisplayMember = "fStkOutLogNo";
            searchLookUpEdit1.Properties.DataSource = data;
            searchLookUpEdit1.Properties.NullText = "";

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
            List<fStoutLogNoList> data = so.Query_list(searchLookUpEdit1.EditValue.ToString());
            textEdit1.Text = data[0].fDlvNo;
            textEdit2.Text = data[0].fCCode;
            textEdit3.Text = data[0].fCName; ;
            List<V_INVD_StkOutLogItemSum> product = so.Query_product(searchLookUpEdit1.EditValue.ToString());
            gridControl1.DataSource = product;
            searchControl1.Enabled = false;

            LoadData(sf.Load(searchLookUpEdit1.EditValue.ToString()));




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
                        }
                        else if(data==null)
                        {
                            MessageBox.Show("条码错误");
                        }
                        else
                        {
                            List<V_INVD_StkOutLogItemSum> product = so.Query_product(searchLookUpEdit1.EditValue.ToString());

                            //查询是否有这个品号
                            V_INVD_StkOutLogItemSum result = product.Where(u => u.fGoodsCode.Equals(data.fGoodsCode)).FirstOrDefault();
                            if (data.fOrdNo.StartsWith("YC"))//条码订单号是yc开头
                            {
                               
                                if(result != null)
                                {
                                    barlist.Add(data);
                                    gridControl2.RefreshDataSource();
                                    MessageBox.Show("ok");
                                }
                                else
                                {
                                    MessageBox.Show("此单不包含品号"+data.fGoodsCode);
                                }
                            }
                            else
                            {
                                if (result != null)
                                {
                                    t_INVD_StkOutLogItem t = so.GetFordNo(searchLookUpEdit1.EditValue.ToString());

                                    if (data.fOrdNo.Equals(t.fOrdNo))
                                    {
                                        barlist.Add(data);
                                        gridControl2.RefreshDataSource();
                                       
                                    }
                                    else
                                    {
                                        MessageBox.Show("您的条码订单号为:"+data.fOrdNo+"  不匹配出库单的订单号");

                                    }
                                   


                                }
                                else
                                {
                                    MessageBox.Show("此单不包含品号" + data.fGoodsCode);
                                }


                            }





                               
                            
                            
                        }
                        

                    }
                    else
                    {
                        MessageBox.Show("不能为空！");
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
            catch (Exception)
            {
                MessageBox.Show("请检查网络");
            }
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
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
                }


            }
            else
            {
                MessageBox.Show("请选择出库单");
            }



        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            delbarlist(0);
        }

        private void gridControl2_Click(object sender, EventArgs e)
        {
          
        }

        private void 清空ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
                barlist.Clear();

                gridControl2.RefreshDataSource();
                barButtonItem8.Enabled = false;

           

        }

        private void barButtonItem8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {


                if (gridView2.RowCount > 0)
                {
                    List<DO_t_TemporaryScan> files = new List<DO_t_TemporaryScan>();
                    sf.del(searchLookUpEdit1.EditValue.ToString());
                    foreach (BarcodeDetial item in barlist)
                    {
                        DO_t_TemporaryScan ts = new DO_t_TemporaryScan()
                        {
                            fStOutLogNo = searchLookUpEdit1.EditValue.ToString(),
                            fPackNo = item.fPackNo,
                            fOrdNo = item.fOrdNo,
                            fBarcode = item.fBarcode,
                            fGoodsCode = item.fGoodsCode,
                            fSizeDesc = item.fSizeDesc,
                            fGoodsName = item.fGoodsName,
                            saveDate = DateTime.Now.ToString("d")
                        };
                        files.Add(ts);

                    }
                    int row = sf.Save(files);
                    if (row > 0)
                    {
                        MessageBox.Show("保存成功");
                    }

                }

            }
            catch (System.Data.Entity.Core.EntityException)
            {
                MessageBox.Show("请检查网络");
            }


            catch (Exception)
            {
                MessageBox.Show("已在别的出库单扫描过");
            }

        }

        private void 删除并ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            delbarlist(1);
        }


        /// <summary>
        /// 快捷菜单删除
        /// </summary>
        private void delbarlist(int flag)
        {

            int[] row = gridView2.GetSelectedRows();
            List<BarcodeDetial> del = new List<BarcodeDetial>();
            sf.del(searchLookUpEdit1.EditValue.ToString());
            foreach (int i in row)
            {
                string barcode = gridView2.GetRowCellValue(i, "fBarcode").ToString();
                foreach (BarcodeDetial item in barlist)
                {
                    if (item.fBarcode.Equals(barcode))
                    {
                        del.Add(item);
                    }
                }
            }
            barlist.RemoveAll(u => del.Contains(u));
            if (flag == 1)
            {
                List<DO_t_TemporaryScan> files = new List<DO_t_TemporaryScan>();
               
                foreach(BarcodeDetial b in barlist)
                {
                    DO_t_TemporaryScan ts = new DO_t_TemporaryScan()
                    {
                        fStOutLogNo = searchLookUpEdit1.EditValue.ToString(),
                        fPackNo = b.fPackNo,
                        fOrdNo = b.fOrdNo,
                        fBarcode = b.fBarcode,
                        fGoodsCode = b.fGoodsCode,
                        fSizeDesc = b.fSizeDesc,
                        fGoodsName = b.fGoodsName,

                        saveDate = DateTime.Now.ToString("d")
                    };
                    files.Add(ts);
                }
                sf.Save(files);
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
            barlist.Clear();
            sf.del(searchLookUpEdit1.EditValue.ToString());
            gridControl2.RefreshDataSource();
            barButtonItem8.Enabled = false;

        }

        private void 重新加载ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<DO_t_TemporaryScan> list = sf.Load(searchLookUpEdit1.EditValue.ToString());
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
            }

            gridControl2.RefreshDataSource();
        }

        private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            List<V_INVD_StkOutLogItemSum> product = so.Query_product(searchLookUpEdit1.EditValue.ToString());


            for(int i = 0; i < product.Count; i++)
            {

                int row = barlist.Count(u => u.fGoodsCode == product[i].fGoodsCode);
               
               if(row > (int)product[i].fPlanOutQty)
                {
                    MessageBox.Show("品号：" + product[i].fGoodsCode + "多出" + (row - (int)product[i].fPlanOutQty) + "套");
                    return;


                }else if(row < (int)product[i].fPlanOutQty)
                {
                    MessageBox.Show("品号：" + product[i].fGoodsCode + "差" + ( (int)product[i].fPlanOutQty-row) + "套");
                    return;
                }

                
            }

            MessageBox.Show("全部通过");




        }

        private void searchControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
