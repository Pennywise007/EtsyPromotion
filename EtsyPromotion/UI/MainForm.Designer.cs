namespace EtsyPromotion.UI
{
    partial class MainForm
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
            if (disposing)
            {
                components?.Dispose();
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.ItemsTable = new MetroFramework.Controls.MetroGrid();
            this.Button_RunPromotion = new MetroFramework.Controls.MetroButton();
            this.Button_CheckLocation = new MetroFramework.Controls.MetroButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.Button_KeyWordPromotion = new MetroFramework.Controls.MetroButton();
            this.CurrentIP = new MetroFramework.Controls.MetroLink();
            this.openLinkColumn = new System.Windows.Forms.DataGridViewButtonColumn();
            this.PromotionStatusColumn = new System.Windows.Forms.DataGridViewImageColumn();
            this.listingActionColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.linkDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lastPromotionDateColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.noteDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.etsyLinkInfoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.ItemsTable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.etsyLinkInfoBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // ItemsTable
            // 
            this.ItemsTable.AllowUserToOrderColumns = true;
            this.ItemsTable.AllowUserToResizeRows = false;
            this.ItemsTable.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ItemsTable.AutoGenerateColumns = false;
            this.ItemsTable.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.ItemsTable.BackgroundColor = System.Drawing.Color.White;
            this.ItemsTable.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ItemsTable.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.ItemsTable.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.PeachPuff;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ItemsTable.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.ItemsTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ItemsTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.listingActionColumn,
            this.linkDataGridViewTextBoxColumn,
            this.openLinkColumn,
            this.lastPromotionDateColumn,
            this.noteDataGridViewTextBoxColumn,
            this.PromotionStatusColumn});
            this.ItemsTable.DataSource = this.etsyLinkInfoBindingSource;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.ItemsTable.DefaultCellStyle = dataGridViewCellStyle2;
            this.ItemsTable.EnableHeadersVisualStyles = false;
            this.ItemsTable.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.ItemsTable.GridColor = System.Drawing.Color.White;
            this.ItemsTable.Location = new System.Drawing.Point(12, 61);
            this.ItemsTable.Name = "ItemsTable";
            this.ItemsTable.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ItemsTable.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.ItemsTable.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.ItemsTable.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ItemsTable.Size = new System.Drawing.Size(776, 348);
            this.ItemsTable.TabIndex = 0;
            this.ItemsTable.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ItemsTable_CellContentClick);
            // 
            // Button_RunPromotion
            // 
            this.Button_RunPromotion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_RunPromotion.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Button_RunPromotion.Location = new System.Drawing.Point(12, 415);
            this.Button_RunPromotion.Name = "Button_RunPromotion";
            this.Button_RunPromotion.Size = new System.Drawing.Size(776, 40);
            this.Button_RunPromotion.TabIndex = 2;
            this.Button_RunPromotion.Text = "Запустить продвижение";
            this.Button_RunPromotion.UseSelectable = true;
            this.Button_RunPromotion.Click += new System.EventHandler(this.Button_AddItemsToCard_Click);
            // 
            // Button_CheckLocation
            // 
            this.Button_CheckLocation.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_CheckLocation.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Button_CheckLocation.Location = new System.Drawing.Point(3, 3);
            this.Button_CheckLocation.Name = "Button_CheckLocation";
            this.Button_CheckLocation.Size = new System.Drawing.Size(252, 40);
            this.Button_CheckLocation.TabIndex = 1;
            this.Button_CheckLocation.Text = "Проверить текущий ip и местоположение";
            this.Button_CheckLocation.UseSelectable = true;
            this.Button_CheckLocation.Click += new System.EventHandler(this.Button_CheckLocation_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.CausesValidation = false;
            this.splitContainer1.Location = new System.Drawing.Point(12, 461);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.Button_CheckLocation);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.Button_KeyWordPromotion);
            this.splitContainer1.Size = new System.Drawing.Size(776, 46);
            this.splitContainer1.SplitterDistance = 258;
            this.splitContainer1.TabIndex = 3;
            // 
            // Button_KeyWordPromotion
            // 
            this.Button_KeyWordPromotion.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_KeyWordPromotion.BackColor = System.Drawing.Color.White;
            this.Button_KeyWordPromotion.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Button_KeyWordPromotion.Location = new System.Drawing.Point(3, 3);
            this.Button_KeyWordPromotion.Name = "Button_KeyWordPromotion";
            this.Button_KeyWordPromotion.Size = new System.Drawing.Size(508, 40);
            this.Button_KeyWordPromotion.TabIndex = 0;
            this.Button_KeyWordPromotion.Text = "Продвижение по ключевым словам";
            this.Button_KeyWordPromotion.UseSelectable = true;
            this.Button_KeyWordPromotion.Click += new System.EventHandler(this.Button_KeyWordPromotion_Click);
            // 
            // CurrentIP
            // 
            this.CurrentIP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CurrentIP.AutoSize = true;
            this.CurrentIP.Location = new System.Drawing.Point(535, 33);
            this.CurrentIP.Name = "CurrentIP";
            this.CurrentIP.Size = new System.Drawing.Size(252, 23);
            this.CurrentIP.TabIndex = 5;
            this.CurrentIP.Text = "Текущий IP:";
            this.CurrentIP.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.CurrentIP.UseSelectable = true;
            this.CurrentIP.Click += new System.EventHandler(this.CurrentIP_Click);
            // 
            // openLinkColumn
            // 
            this.openLinkColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.openLinkColumn.FillWeight = 75F;
            this.openLinkColumn.HeaderText = "Перейти по ссылке";
            this.openLinkColumn.Name = "openLinkColumn";
            this.openLinkColumn.Text = "Открыть";
            this.openLinkColumn.ToolTipText = "Открыть в браузере ссылку на товар";
            this.openLinkColumn.UseColumnTextForButtonValue = true;
            this.openLinkColumn.Width = 70;
            // 
            // PromotionStatusColumn
            // 
            this.PromotionStatusColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.PromotionStatusColumn.Description = "Состояние каждого листинга";
            this.PromotionStatusColumn.HeaderText = "Статус";
            this.PromotionStatusColumn.Name = "PromotionStatusColumn";
            this.PromotionStatusColumn.Width = 45;
            // 
            // listingActionColumn
            // 
            this.listingActionColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.listingActionColumn.DataPropertyName = "ItemAction";
            this.listingActionColumn.FillWeight = 75F;
            this.listingActionColumn.HeaderText = "Действие";
            this.listingActionColumn.Name = "listingActionColumn";
            this.listingActionColumn.Width = 160;
            // 
            // linkDataGridViewTextBoxColumn
            // 
            this.linkDataGridViewTextBoxColumn.DataPropertyName = "Link";
            this.linkDataGridViewTextBoxColumn.FillWeight = 54.90196F;
            this.linkDataGridViewTextBoxColumn.HeaderText = "Ссылка на товар";
            this.linkDataGridViewTextBoxColumn.Name = "linkDataGridViewTextBoxColumn";
            // 
            // lastPromotionDateColumn
            // 
            this.lastPromotionDateColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.lastPromotionDateColumn.DataPropertyName = "DateLastPromotion";
            this.lastPromotionDateColumn.FillWeight = 153.1002F;
            this.lastPromotionDateColumn.HeaderText = "Последнее продвижение";
            this.lastPromotionDateColumn.Name = "lastPromotionDateColumn";
            this.lastPromotionDateColumn.ToolTipText = "Дата последнего продвижения товара";
            this.lastPromotionDateColumn.Width = 125;
            // 
            // noteDataGridViewTextBoxColumn
            // 
            this.noteDataGridViewTextBoxColumn.DataPropertyName = "Note";
            this.noteDataGridViewTextBoxColumn.FillWeight = 28.35188F;
            this.noteDataGridViewTextBoxColumn.HeaderText = "Заметка";
            this.noteDataGridViewTextBoxColumn.Name = "noteDataGridViewTextBoxColumn";
            this.noteDataGridViewTextBoxColumn.ToolTipText = "Любая заметка об этом элементе";
            // 
            // etsyLinkInfoBindingSource
            // 
            this.etsyLinkInfoBindingSource.DataSource = typeof(EtsyPromotion.Promotion.Interfaces.ListingInfo);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = MetroFramework.Forms.MetroFormBorderStyle.FixedSingle;
            this.ClientSize = new System.Drawing.Size(797, 530);
            this.Controls.Add(this.Button_RunPromotion);
            this.Controls.Add(this.CurrentIP);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.ItemsTable);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Style = MetroFramework.MetroColorStyle.Green;
            this.Text = "Продвижение Etsy.com";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ItemsTable)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.etsyLinkInfoBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroGrid ItemsTable;
        private MetroFramework.Controls.MetroButton Button_RunPromotion;
        private MetroFramework.Controls.MetroButton Button_CheckLocation;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private MetroFramework.Controls.MetroLink CurrentIP;
        private System.Windows.Forms.BindingSource etsyLinkInfoBindingSource;
        private MetroFramework.Controls.MetroButton Button_KeyWordPromotion;
        private System.Windows.Forms.DataGridViewComboBoxColumn listingActionColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn linkDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewButtonColumn openLinkColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn lastPromotionDateColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn noteDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewImageColumn PromotionStatusColumn;
    }
}

