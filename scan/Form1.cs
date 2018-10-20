﻿using BLL;
using DevExpress.XtraSplashScreen;
using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace scan
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
           


        }
        private loginBLL n = new loginBLL();
        private void checkButton1_CheckedChanged(object sender, EventArgs e)
        {


            try
            {
                login();
            }
            catch (System.Data.Entity.Core.EntityException ex)//捕获网络异常
            {
                loginLoad.Hide();
                MessageBox.Show(ex.Message);
            }catch(Exception ex)
            {
                loginLoad.Hide();
                MessageBox.Show(ex.Message);
            }
            
        }

        

       
        private void Pwd_KeyUp(object sender, KeyEventArgs e)
        {
           
                if (e.KeyCode.GetHashCode() == 13)
                {
                    
                    if (Usrid.Text.Trim() != "")
                    {
                        if (Pwd.Text.Trim() != "")
                        {
                            login();

                        }
                        else
                        {

                            MessageBox.Show("请输入密码！");
                        }
                    }
                    else
                    {
                        MessageBox.Show("请输入用户名！");
                    }
                }
            
        }

        private void login()
        {
            loginLoad.Show();
            if (Usrid.Text.Trim() != "" & Pwd.Text.Trim() != "")
            {

                

               t_ADMM_UsrMst admin = n.LoginHandle(Usrid.Text.Trim(), Pwd.Text.Trim());
                    if (admin != null)
                    {

                        this.Hide();
                        
                        new Form2(admin).ShowDialog();

                    }
                    else
                    {
                        
                        MessageBox.Show("用户名或密码错误");
                    }
                    loginLoad.Hide();

               
            }
            else
            {
                loginLoad.Hide();
                MessageBox.Show("用户名或密码不能为空");

            }
           
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
