
namespace AtelierView
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.СправочникиtoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonCreateOrder = new System.Windows.Forms.Button();
            this.buttonTakeOrder = new System.Windows.Forms.Button();
            this.buttonOrderReady = new System.Windows.Forms.Button();
            this.buttonIssuedOrder = new System.Windows.Forms.Button();
            this.buttonRef = new System.Windows.Forms.Button();
            this.КомпонентыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ИзделияToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView
            // 
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Location = new System.Drawing.Point(12, 27);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.Size = new System.Drawing.Size(776, 371);
            this.dataGridView.TabIndex = 1;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.СправочникиtoolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1024, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // СправочникиtoolStripMenuItem
            // 
            this.СправочникиtoolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.КомпонентыToolStripMenuItem,
            this.ИзделияToolStripMenuItem});
            this.СправочникиtoolStripMenuItem.Name = "СправочникиtoolStripMenuItem";
            this.СправочникиtoolStripMenuItem.Size = new System.Drawing.Size(94, 20);
            this.СправочникиtoolStripMenuItem.Text = "Справочники";
            // 
            // buttonCreateOrder
            // 
            this.buttonCreateOrder.Location = new System.Drawing.Point(841, 79);
            this.buttonCreateOrder.Name = "buttonCreateOrder";
            this.buttonCreateOrder.Size = new System.Drawing.Size(134, 23);
            this.buttonCreateOrder.TabIndex = 3;
            this.buttonCreateOrder.Text = "Создать заказ";
            this.buttonCreateOrder.UseVisualStyleBackColor = true;
            this.buttonCreateOrder.Click += new System.EventHandler(this.buttonCreateOrder_Click);
            // 
            // buttonTakeOrder
            // 
            this.buttonTakeOrder.Location = new System.Drawing.Point(841, 123);
            this.buttonTakeOrder.Name = "buttonTakeOrder";
            this.buttonTakeOrder.Size = new System.Drawing.Size(134, 23);
            this.buttonTakeOrder.TabIndex = 4;
            this.buttonTakeOrder.Text = "Отдать на выполнение";
            this.buttonTakeOrder.UseVisualStyleBackColor = true;
            this.buttonTakeOrder.Click += new System.EventHandler(this.buttonTakeOrder_Click);
            // 
            // buttonOrderReady
            // 
            this.buttonOrderReady.Location = new System.Drawing.Point(841, 170);
            this.buttonOrderReady.Name = "buttonOrderReady";
            this.buttonOrderReady.Size = new System.Drawing.Size(134, 23);
            this.buttonOrderReady.TabIndex = 5;
            this.buttonOrderReady.Text = "Заказ готов";
            this.buttonOrderReady.UseVisualStyleBackColor = true;
            this.buttonOrderReady.Click += new System.EventHandler(this.buttonOrderReady_Click);
            // 
            // buttonIssuedOrder
            // 
            this.buttonIssuedOrder.Location = new System.Drawing.Point(841, 218);
            this.buttonIssuedOrder.Name = "buttonIssuedOrder";
            this.buttonIssuedOrder.Size = new System.Drawing.Size(134, 23);
            this.buttonIssuedOrder.TabIndex = 6;
            this.buttonIssuedOrder.Text = "Заказ выдан";
            this.buttonIssuedOrder.UseVisualStyleBackColor = true;
            this.buttonIssuedOrder.Click += new System.EventHandler(this.buttonIssuedOrder_Click);
            // 
            // buttonRef
            // 
            this.buttonRef.Location = new System.Drawing.Point(841, 266);
            this.buttonRef.Name = "buttonRef";
            this.buttonRef.Size = new System.Drawing.Size(134, 23);
            this.buttonRef.TabIndex = 7;
            this.buttonRef.Text = "Обновить список";
            this.buttonRef.UseVisualStyleBackColor = true;
            this.buttonRef.Click += new System.EventHandler(this.buttonRef_Click);
            // 
            // КомпонентыToolStripMenuItem
            // 
            this.КомпонентыToolStripMenuItem.Name = "КомпонентыToolStripMenuItem";
            this.КомпонентыToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.КомпонентыToolStripMenuItem.Text = "Компоненты";
            this.КомпонентыToolStripMenuItem.Click += new System.EventHandler(this.КомпонентыToolStripMenuItem_Click);
            // 
            // ИзделияToolStripMenuItem
            // 
            this.ИзделияToolStripMenuItem.Name = "ИзделияToolStripMenuItem";
            this.ИзделияToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.ИзделияToolStripMenuItem.Text = "Изделия";
            this.ИзделияToolStripMenuItem.Click += new System.EventHandler(this.ИзделияToolStripMenuItem_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1024, 396);
            this.Controls.Add(this.buttonRef);
            this.Controls.Add(this.buttonIssuedOrder);
            this.Controls.Add(this.buttonOrderReady);
            this.Controls.Add(this.buttonTakeOrder);
            this.Controls.Add(this.buttonCreateOrder);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormMain";
            this.Text = "Ателье";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem СправочникиtoolStripMenuItem;
        private System.Windows.Forms.Button buttonCreateOrder;
        private System.Windows.Forms.Button buttonTakeOrder;
        private System.Windows.Forms.Button buttonOrderReady;
        private System.Windows.Forms.Button buttonIssuedOrder;
        private System.Windows.Forms.Button buttonRef;
        private System.Windows.Forms.ToolStripMenuItem КомпонентыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ИзделияToolStripMenuItem;
    }
}