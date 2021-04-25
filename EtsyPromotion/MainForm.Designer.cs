namespace EtsyHacker
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
            this.OpenLink = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Button_AddItemsToCard = new MetroFramework.Controls.MetroButton();
            this.Button_CheckLocation = new MetroFramework.Controls.MetroButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.CurrentIP = new MetroFramework.Controls.MetroLink();
            this.addToCardDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.linkDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dateLastAddDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.addToCardDataGridViewCheckBoxColumn,
            this.linkDataGridViewTextBoxColumn,
            this.OpenLink,
            this.dateLastAddDataGridViewTextBoxColumn,
            this.noteDataGridViewTextBoxColumn});
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
            this.ItemsTable.Size = new System.Drawing.Size(776, 267);
            this.ItemsTable.TabIndex = 0;
            this.ItemsTable.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ItemsTable_CellContentClick);
            // 
            // OpenLink
            // 
            this.OpenLink.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.OpenLink.FillWeight = 75F;
            this.OpenLink.HeaderText = "Перейти по ссылке";
            this.OpenLink.Name = "OpenLink";
            this.OpenLink.Text = "Открыть";
            this.OpenLink.ToolTipText = "Открыть в браузере ссылку на товар";
            this.OpenLink.UseColumnTextForButtonValue = true;
            this.OpenLink.Width = 70;
            // 
            // Button_AddItemsToCard
            // 
            this.Button_AddItemsToCard.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_AddItemsToCard.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Button_AddItemsToCard.Location = new System.Drawing.Point(3, 3);
            this.Button_AddItemsToCard.Name = "Button_AddItemsToCard";
            this.Button_AddItemsToCard.Size = new System.Drawing.Size(508, 40);
            this.Button_AddItemsToCard.TabIndex = 2;
            this.Button_AddItemsToCard.Text = "Добавить выбранные товары в корзину";
            this.Button_AddItemsToCard.UseSelectable = true;
            this.Button_AddItemsToCard.Click += new System.EventHandler(this.Button_AddItemsToCard_Click);
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
            this.splitContainer1.Location = new System.Drawing.Point(12, 334);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.Button_CheckLocation);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.Button_AddItemsToCard);
            this.splitContainer1.Size = new System.Drawing.Size(776, 46);
            this.splitContainer1.SplitterDistance = 258;
            this.splitContainer1.TabIndex = 3;
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
            // addToCardDataGridViewCheckBoxColumn
            // 
            this.addToCardDataGridViewCheckBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.addToCardDataGridViewCheckBoxColumn.DataPropertyName = "AddToCard";
            this.addToCardDataGridViewCheckBoxColumn.FillWeight = 75F;
            this.addToCardDataGridViewCheckBoxColumn.HeaderText = "Добавлять в корзину";
            this.addToCardDataGridViewCheckBoxColumn.Name = "addToCardDataGridViewCheckBoxColumn";
            this.addToCardDataGridViewCheckBoxColumn.ToolTipText = "Добавлять в корзину";
            this.addToCardDataGridViewCheckBoxColumn.Width = 75;
            // 
            // linkDataGridViewTextBoxColumn
            // 
            this.linkDataGridViewTextBoxColumn.DataPropertyName = "Link";
            this.linkDataGridViewTextBoxColumn.FillWeight = 54.90196F;
            this.linkDataGridViewTextBoxColumn.HeaderText = "Ссылка на товар";
            this.linkDataGridViewTextBoxColumn.Name = "linkDataGridViewTextBoxColumn";
            // 
            // dateLastAddDataGridViewTextBoxColumn
            // 
            this.dateLastAddDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dateLastAddDataGridViewTextBoxColumn.DataPropertyName = "DateLastAdd";
            this.dateLastAddDataGridViewTextBoxColumn.FillWeight = 54.90196F;
            this.dateLastAddDataGridViewTextBoxColumn.HeaderText = "Дата последнего добавления";
            this.dateLastAddDataGridViewTextBoxColumn.Name = "dateLastAddDataGridViewTextBoxColumn";
            this.dateLastAddDataGridViewTextBoxColumn.ToolTipText = "Дата последнего добавления товара в корзину";
            this.dateLastAddDataGridViewTextBoxColumn.Width = 125;
            // 
            // noteDataGridViewTextBoxColumn
            // 
            this.noteDataGridViewTextBoxColumn.DataPropertyName = "Note";
            this.noteDataGridViewTextBoxColumn.FillWeight = 54.90196F;
            this.noteDataGridViewTextBoxColumn.HeaderText = "Заметка";
            this.noteDataGridViewTextBoxColumn.Name = "noteDataGridViewTextBoxColumn";
            this.noteDataGridViewTextBoxColumn.ToolTipText = "Любая заметка об этом элементе";
            // 
            // etsyLinkInfoBindingSource
            // 
            this.etsyLinkInfoBindingSource.DataSource = typeof(EtsyPromotion.EtsyLinkInfo);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = MetroFramework.Forms.MetroFormBorderStyle.FixedSingle;
            this.ClientSize = new System.Drawing.Size(797, 403);
            this.Controls.Add(this.CurrentIP);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.ItemsTable);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Style = MetroFramework.MetroColorStyle.Green;
            this.Text = "Продвижение Etsy.com";
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
        private MetroFramework.Controls.MetroButton Button_AddItemsToCard;
        private MetroFramework.Controls.MetroButton Button_CheckLocation;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private MetroFramework.Controls.MetroLink CurrentIP;
        private System.Windows.Forms.BindingSource etsyLinkInfoBindingSource;
        private System.Windows.Forms.DataGridViewCheckBoxColumn addToCardDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn linkDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewButtonColumn OpenLink;
        private System.Windows.Forms.DataGridViewTextBoxColumn dateLastAddDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn noteDataGridViewTextBoxColumn;
    }
}

