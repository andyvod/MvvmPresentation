namespace MvvmPresentation.App
{
    partial class CustomerOrdersView
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CustomerOrdersView));
            tableLayoutPanel1 = new TableLayoutPanel();
            stackPanel1 = new DevExpress.Utils.Layout.StackPanel();
            label1 = new Label();
            comboBox1 = new ComboBox();
            simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            gridControl1 = new DevExpress.XtraGrid.GridControl();
            gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            stackPanel2 = new DevExpress.Utils.Layout.StackPanel();
            simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            mvvmContext1 = new DevExpress.Utils.MVVM.MVVMContext(components);
            tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)stackPanel1).BeginInit();
            stackPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridControl1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)stackPanel2).BeginInit();
            stackPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)mvvmContext1).BeginInit();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.Controls.Add(stackPanel1, 0, 0);
            tableLayoutPanel1.Controls.Add(gridControl1, 0, 1);
            tableLayoutPanel1.Controls.Add(stackPanel2, 0, 2);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 3;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
            tableLayoutPanel1.Size = new Size(800, 450);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // stackPanel1
            // 
            stackPanel1.Controls.Add(label1);
            stackPanel1.Controls.Add(comboBox1);
            stackPanel1.Controls.Add(simpleButton1);
            stackPanel1.Dock = DockStyle.Fill;
            stackPanel1.Location = new Point(3, 3);
            stackPanel1.Name = "stackPanel1";
            stackPanel1.Padding = new Padding(3);
            stackPanel1.Size = new Size(794, 54);
            stackPanel1.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(6, 20);
            label1.Name = "label1";
            label1.Size = new Size(79, 13);
            label1.TabIndex = 0;
            label1.Text = "Пользователи";
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(91, 16);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(210, 21);
            comboBox1.TabIndex = 1;
            // 
            // simpleButton1
            // 
            simpleButton1.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            simpleButton1.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("simpleButton1.ImageOptions.SvgImage");
            simpleButton1.Location = new Point(324, 11);
            simpleButton1.Margin = new Padding(20, 3, 3, 3);
            simpleButton1.Name = "simpleButton1";
            simpleButton1.Size = new Size(32, 32);
            simpleButton1.TabIndex = 2;
            simpleButton1.Text = "simpleButton1";
            // 
            // gridControl1
            // 
            gridControl1.Dock = DockStyle.Fill;
            gridControl1.Location = new Point(3, 63);
            gridControl1.MainView = gridView1;
            gridControl1.Name = "gridControl1";
            gridControl1.Size = new Size(794, 324);
            gridControl1.TabIndex = 0;
            gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridView1 });
            // 
            // gridView1
            // 
            gridView1.GridControl = gridControl1;
            gridView1.Name = "gridView1";
            gridView1.OptionsView.ShowFooter = true;
            gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // stackPanel2
            // 
            stackPanel2.Controls.Add(simpleButton2);
            stackPanel2.Dock = DockStyle.Fill;
            stackPanel2.Location = new Point(3, 393);
            stackPanel2.Name = "stackPanel2";
            stackPanel2.Padding = new Padding(10);
            stackPanel2.Size = new Size(794, 54);
            stackPanel2.TabIndex = 2;
            // 
            // simpleButton2
            // 
            simpleButton2.Location = new Point(13, 15);
            simpleButton2.Name = "simpleButton2";
            simpleButton2.Size = new Size(75, 23);
            simpleButton2.TabIndex = 0;
            simpleButton2.Text = "Добавить";
            // 
            // mvvmContext1
            // 
            mvvmContext1.ContainerControl = this;
            // 
            // CustomerOrdersView
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(tableLayoutPanel1);
            Name = "CustomerOrdersView";
            Text = "Покупки клиентов";
            tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)stackPanel1).EndInit();
            stackPanel1.ResumeLayout(false);
            stackPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)gridControl1).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)stackPanel2).EndInit();
            stackPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)mvvmContext1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.Utils.Layout.StackPanel stackPanel1;
        private Label label1;
        private ComboBox comboBox1;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.Utils.Layout.StackPanel stackPanel2;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.Utils.MVVM.MVVMContext mvvmContext1;
    }
}
