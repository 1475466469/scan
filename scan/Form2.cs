using BLL;
using DevExpress.XtraEditors;
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
    }
}
