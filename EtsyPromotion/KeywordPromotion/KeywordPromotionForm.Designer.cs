namespace EtsyPromotion.KeywordPromotion
{
    partial class KeywordPromotionForm
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
            this.ShopLink = new MetroFramework.Controls.MetroTextBox();
            this.ShopName = new MetroFramework.Controls.MetroLabel();
            this.button1 = new MetroFramework.Controls.MetroButton();
            this.PromotionList = new MetroFramework.Controls.MetroGrid();
            this.enableDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.linkDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.keyWordsDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dateLastAddDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.noteDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.productsListDataSource = new System.Windows.Forms.BindingSource(this.components);
            this.Button_StartPromotion = new MetroFramework.Controls.MetroButton();
            ((System.ComponentModel.ISupportInitialize)(this.PromotionList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.productsListDataSource)).BeginInit();
            this.SuspendLayout();
            // 
            // ShopLink
            // 
            this.ShopLink.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.ShopLink.CustomButton.Image = null;
            this.ShopLink.CustomButton.Location = new System.Drawing.Point(606, 2);
            this.ShopLink.CustomButton.Name = "";
            this.ShopLink.CustomButton.Size = new System.Drawing.Size(15, 15);
            this.ShopLink.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.ShopLink.CustomButton.TabIndex = 1;
            this.ShopLink.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.ShopLink.CustomButton.UseSelectable = true;
            this.ShopLink.CustomButton.Visible = false;
            this.ShopLink.Enabled = false;
            this.ShopLink.Lines = new string[0];
            this.ShopLink.Location = new System.Drawing.Point(248, 67);
            this.ShopLink.MaxLength = 32767;
            this.ShopLink.Name = "ShopLink";
            this.ShopLink.PasswordChar = '\0';
            this.ShopLink.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.ShopLink.SelectedText = "";
            this.ShopLink.SelectionLength = 0;
            this.ShopLink.SelectionStart = 0;
            this.ShopLink.ShortcutsEnabled = true;
            this.ShopLink.Size = new System.Drawing.Size(624, 20);
            this.ShopLink.TabIndex = 0;
            this.ShopLink.UseSelectable = true;
            this.ShopLink.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.ShopLink.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // ShopName
            // 
            this.ShopName.AutoSize = true;
            this.ShopName.Location = new System.Drawing.Point(23, 67);
            this.ShopName.Name = "ShopName";
            this.ShopName.Size = new System.Drawing.Size(219, 19);
            this.ShopName.TabIndex = 1;
            this.ShopName.Text = "Ссылка на продвигаемый магазин";
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Enabled = false;
            this.button1.Location = new System.Drawing.Point(878, 63);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(192, 27);
            this.button1.TabIndex = 2;
            this.button1.Text = "Загрузить список товаров";
            this.button1.UseSelectable = true;
            // 
            // PromotionList
            // 
            this.PromotionList.AllowUserToOrderColumns = true;
            this.PromotionList.AllowUserToResizeRows = false;
            this.PromotionList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PromotionList.AutoGenerateColumns = false;
            this.PromotionList.BackgroundColor = System.Drawing.Color.White;
            this.PromotionList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.PromotionList.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.PromotionList.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.PeachPuff;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.PromotionList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.PromotionList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.PromotionList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.enableDataGridViewCheckBoxColumn,
            this.linkDataGridViewTextBoxColumn,
            this.keyWordsDataGridViewTextBoxColumn,
            this.dateLastAddDataGridViewTextBoxColumn,
            this.noteDataGridViewTextBoxColumn});
            this.PromotionList.DataSource = this.productsListDataSource;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.PromotionList.DefaultCellStyle = dataGridViewCellStyle2;
            this.PromotionList.EnableHeadersVisualStyles = false;
            this.PromotionList.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.PromotionList.GridColor = System.Drawing.Color.White;
            this.PromotionList.Location = new System.Drawing.Point(23, 102);
            this.PromotionList.Name = "PromotionList";
            this.PromotionList.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.PromotionList.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.PromotionList.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.PromotionList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.PromotionList.Size = new System.Drawing.Size(1047, 411);
            this.PromotionList.TabIndex = 3;
            // 
            // enableDataGridViewCheckBoxColumn
            // 
            this.enableDataGridViewCheckBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.enableDataGridViewCheckBoxColumn.DataPropertyName = "Enable";
            this.enableDataGridViewCheckBoxColumn.FillWeight = 75F;
            this.enableDataGridViewCheckBoxColumn.HeaderText = "Продвигать";
            this.enableDataGridViewCheckBoxColumn.Name = "enableDataGridViewCheckBoxColumn";
            this.enableDataGridViewCheckBoxColumn.ToolTipText = "Продвигать товар";
            this.enableDataGridViewCheckBoxColumn.Width = 75;
            // 
            // linkDataGridViewTextBoxColumn
            // 
            this.linkDataGridViewTextBoxColumn.DataPropertyName = "Link";
            this.linkDataGridViewTextBoxColumn.FillWeight = 50F;
            this.linkDataGridViewTextBoxColumn.HeaderText = "Ссылка на товар";
            this.linkDataGridViewTextBoxColumn.Name = "linkDataGridViewTextBoxColumn";
            this.linkDataGridViewTextBoxColumn.Width = 304;
            // 
            // keyWordsDataGridViewTextBoxColumn
            // 
            this.keyWordsDataGridViewTextBoxColumn.DataPropertyName = "KeyWords";
            this.keyWordsDataGridViewTextBoxColumn.HeaderText = "Ключевые слова(через ;)";
            this.keyWordsDataGridViewTextBoxColumn.Name = "keyWordsDataGridViewTextBoxColumn";
            this.keyWordsDataGridViewTextBoxColumn.ToolTipText = "Список ключевых слов по которым будет продвигаться товар(через ;)";
            this.keyWordsDataGridViewTextBoxColumn.Width = 250;
            // 
            // dateLastAddDataGridViewTextBoxColumn
            // 
            this.dateLastAddDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dateLastAddDataGridViewTextBoxColumn.DataPropertyName = "DateLastAdd";
            this.dateLastAddDataGridViewTextBoxColumn.HeaderText = "Дата последнего добавления";
            this.dateLastAddDataGridViewTextBoxColumn.Name = "dateLastAddDataGridViewTextBoxColumn";
            this.dateLastAddDataGridViewTextBoxColumn.ToolTipText = "Дата последнего добавления товара в корзину";
            this.dateLastAddDataGridViewTextBoxColumn.Width = 125;
            // 
            // noteDataGridViewTextBoxColumn
            // 
            this.noteDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.noteDataGridViewTextBoxColumn.DataPropertyName = "Note";
            this.noteDataGridViewTextBoxColumn.FillWeight = 50F;
            this.noteDataGridViewTextBoxColumn.HeaderText = "Заметка";
            this.noteDataGridViewTextBoxColumn.Name = "noteDataGridViewTextBoxColumn";
            this.noteDataGridViewTextBoxColumn.Width = 250;
            // 
            // productsListDataSource
            // 
            this.productsListDataSource.DataSource = typeof(EtsyPromotion.KeywordPromotion.ProductsListItem);
            // 
            // Button_StartPromotion
            // 
            this.Button_StartPromotion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_StartPromotion.Location = new System.Drawing.Point(23, 519);
            this.Button_StartPromotion.Name = "Button_StartPromotion";
            this.Button_StartPromotion.Size = new System.Drawing.Size(1047, 48);
            this.Button_StartPromotion.TabIndex = 4;
            this.Button_StartPromotion.Text = "Запустить продвижение";
            this.Button_StartPromotion.UseSelectable = true;
            this.Button_StartPromotion.Click += new System.EventHandler(this.Button_StartPromotion_Click);
            // 
            // KeywordPromotionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1093, 590);
            this.Controls.Add(this.Button_StartPromotion);
            this.Controls.Add(this.PromotionList);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.ShopName);
            this.Controls.Add(this.ShopLink);
            this.Name = "KeywordPromotionForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Style = MetroFramework.MetroColorStyle.Green;
            this.Text = "Продвижение по ключевым словам";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.KeywordPromotion_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.KeywordPromotion_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.PromotionList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.productsListDataSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroTextBox ShopLink;
        private MetroFramework.Controls.MetroLabel ShopName;
        private MetroFramework.Controls.MetroButton button1;
        private System.Windows.Forms.BindingSource productsListDataSource;
        private MetroFramework.Controls.MetroGrid PromotionList;
        private MetroFramework.Controls.MetroButton Button_StartPromotion;
        private System.Windows.Forms.DataGridViewCheckBoxColumn enableDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn linkDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn keyWordsDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dateLastAddDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn noteDataGridViewTextBoxColumn;
    }
}