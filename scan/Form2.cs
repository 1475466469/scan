using BLL;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using Model;
using System;
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
                            barlist.Add(data);
                            gridControl2.RefreshDataSource();
                        }


                    }
                    else
                    {
                        MessageBox.Show("不能为空！");
                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("请检查网络");
            }
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            MessageBox.Show("4");
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int[] row = gridView2.GetSelectedRows();
            

           
            foreach (int i in row)
            {
               string barcode= gridView2.GetRowCellValue(i, "fBarcode").ToString();
               foreach(BarcodeDetial item in barlist)
                {
                    if (item.fBarcode.Equals(barcode))
                    {
                        barlist.Remove(item);
                    }
                }
                

            }
            gridControl2.RefreshDataSource();
        }

        private void gridControl2_Click(object sender, EventArgs e)
        {
          
        }

        private void 清空ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            barlist.Clear();
            gridControl2.RefreshDataSource();

        }
    }
}
