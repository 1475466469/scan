namespace scan
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
            this.Usrid = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.Pwd = new DevExpress.XtraEditors.TextEdit();
            this.checkButton1 = new DevExpress.XtraEditors.CheckButton();
            ((System.ComponentModel.ISupportInitialize)(this.Usrid.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Pwd.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            resources.ApplyResources(this.labelControl1, "labelControl1");
            this.labelControl1.Name = "labelControl1";
            // 
            // Usrid
            // 
            resources.ApplyResources(this.Usrid, "Usrid");
            this.Usrid.Name = "Usrid";
            this.Usrid.Properties.LookAndFeel.TouchUIMode = DevExpress.LookAndFeel.TouchUIMode.False;
            this.Usrid.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Pwd_KeyUp);
            // 
            // labelControl2
            // 
            resources.ApplyResources(this.labelControl2, "labelControl2");
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
            this.checkButton1.Name = "checkButton1";
            this.checkButton1.CheckedChanged += new System.EventHandler(this.checkButton1_CheckedChanged);
            // 
            // Form1
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.checkButton1);
            this.Controls.Add(this.Pwd);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.Usrid);
            this.Controls.Add(this.labelControl1);
            this.Name = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Usrid.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Pwd.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit Usrid;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit Pwd;
        private DevExpress.XtraEditors.CheckButton checkButton1;
    }
}

