﻿namespace scan
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.Pwd = new DevExpress.XtraEditors.TextEdit();
            this.checkButton1 = new DevExpress.XtraEditors.CheckButton();
            this.loginLoad = new DevExpress.XtraWaitForm.ProgressPanel();
            this.Usrid = new System.Windows.Forms.ComboBox();
            this.checkBoxXpw = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.Pwd.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            resources.ApplyResources(this.labelControl1, "labelControl1");
            this.labelControl1.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("labelControl1.Appearance.Font")));
            this.labelControl1.Appearance.ForeColor = ((System.Drawing.Color)(resources.GetObject("labelControl1.Appearance.ForeColor")));
            this.labelControl1.Name = "labelControl1";
            // 
            // labelControl2
            // 
            resources.ApplyResources(this.labelControl2, "labelControl2");
            this.labelControl2.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("labelControl2.Appearance.Font")));
            this.labelControl2.Appearance.ForeColor = ((System.Drawing.Color)(resources.GetObject("labelControl2.Appearance.ForeColor")));
            this.labelControl2.Name = "labelControl2";
            // 
            // Pwd
            // 
            resources.ApplyResources(this.Pwd, "Pwd");
            this.Pwd.Name = "Pwd";
            this.Pwd.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.Pwd.Properties.LookAndFeel.TouchUIMode = DevExpress.LookAndFeel.TouchUIMode.False;
            this.Pwd.Properties.PasswordChar = '*';
            this.Pwd.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Pwd_KeyUp);
            // 
            // checkButton1
            // 
            resources.ApplyResources(this.checkButton1, "checkButton1");
            this.checkButton1.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("checkButton1.Appearance.Font")));
            this.checkButton1.Appearance.Options.UseFont = true;
            this.checkButton1.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.checkButton1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.checkButton1.ImageLocation = DevExpress.XtraEditors.ImageLocation.BottomCenter;
            this.checkButton1.Name = "checkButton1";
            this.checkButton1.CheckedChanged += new System.EventHandler(this.checkButton1_CheckedChanged);
            // 
            // loginLoad
            // 
            this.loginLoad.Appearance.BackColor = ((System.Drawing.Color)(resources.GetObject("loginLoad.Appearance.BackColor")));
            this.loginLoad.Appearance.Options.UseBackColor = true;
            this.loginLoad.AppearanceCaption.Font = ((System.Drawing.Font)(resources.GetObject("resource.Font")));
            this.loginLoad.AppearanceCaption.Options.UseFont = true;
            this.loginLoad.AppearanceDescription.Font = ((System.Drawing.Font)(resources.GetObject("resource.Font1")));
            this.loginLoad.AppearanceDescription.Options.UseFont = true;
            resources.ApplyResources(this.loginLoad, "loginLoad");
            this.loginLoad.Name = "loginLoad";
            // 
            // Usrid
            // 
            this.Usrid.FormattingEnabled = true;
            resources.ApplyResources(this.Usrid, "Usrid");
            this.Usrid.Name = "Usrid";
            this.Usrid.SelectedIndexChanged += new System.EventHandler(this.Usrid_SelectedIndexChanged);
            // 
            // checkBoxXpw
            // 
            resources.ApplyResources(this.checkBoxXpw, "checkBoxXpw");
            this.checkBoxXpw.Name = "checkBoxXpw";
            this.checkBoxXpw.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.checkBoxXpw);
            this.Controls.Add(this.Usrid);
            this.Controls.Add(this.loginLoad);
            this.Controls.Add(this.checkButton1);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.Pwd);
            this.Controls.Add(this.labelControl2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Pwd.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit Pwd;
        private DevExpress.XtraEditors.CheckButton checkButton1;
        private DevExpress.XtraWaitForm.ProgressPanel loginLoad;
        private System.Windows.Forms.ComboBox Usrid;
        private System.Windows.Forms.CheckBox checkBoxXpw;
    }
}

