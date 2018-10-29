using BLL;
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
    public partial class Form3 : Form
    {
        private t_ADMM_UsrMst admin;
        private loginBLL n = new loginBLL();
        public Form3()
        {
            InitializeComponent();
        }
        public Form3(t_ADMM_UsrMst t)
        {
            this.admin = t;
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {


                if (textEdit1.Text.Trim() != "")
                {
                    if (textEdit2.Text.Trim() != "")
                    {
                        if (textEdit2.Text.Trim() == textEdit3.Text.Trim())
                        {

                            n.UpdatePwd(admin.fUsrID, textEdit1.Text.Trim(), textEdit2.Text.Trim());
                            MessageBox.Show("密码修改成功");
                            this.Close();


                        }
                        else
                        {
                            MessageBox.Show("两次密码输入不一致");
                        }
                    }
                    else
                    {
                        MessageBox.Show("请输入新密码");
                    }
                }
                else
                {

                    MessageBox.Show("请输入原密码");
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
