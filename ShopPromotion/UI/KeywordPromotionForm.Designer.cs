namespace ShopPromotion.UI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(KeywordPromotionForm));
            this.ShopLink = new MetroFramework.Controls.MetroTextBox();
            this.ShopName = new MetroFramework.Controls.MetroLabel();
            this.button1 = new MetroFramework.Controls.MetroButton();
            this.PromotionList = new MetroFramework.Controls.MetroGrid();
            this.listingActionColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.linkDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.keyWordsDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.noteDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dateLastPromotionColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FoundOnPage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StatusColumn = new System.Windows.Forms.DataGridViewImageColumn();
            this.productsListDataSource = new System.Windows.Forms.BindingSource(this.components);
            this.Button_StartPromotion = new MetroFramework.Controls.MetroButton();
            this.RunModeComboBox = new MetroFramework.Controls.MetroComboBox();
            this.RumModeLabel = new MetroFramework.Controls.MetroLabel();
            this.MaxSearchPagesCount = new MetroFramework.Controls.MetroLabel();
            this.MaximumSearchPagesNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.SiteModeComboBox = new MetroFramework.Controls.MetroComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.PromotionList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.productsListDataSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MaximumSearchPagesNumericUpDown)).BeginInit();
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
            this.ShopLink.CustomButton.Location = new System.Drawing.Point(650, 2);
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
            this.ShopLink.Size = new System.Drawing.Size(668, 20);
            this.ShopLink.TabIndex = 0;
            this.ShopLink.UseSelectable = true;
            this.ShopLink.Visible = false;
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
            this.ShopName.Visible = false;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Enabled = false;
            this.button1.Location = new System.Drawing.Point(922, 63);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(192, 27);
            this.button1.TabIndex = 2;
            this.button1.Text = "Загрузить список товаров";
            this.button1.UseSelectable = true;
            this.button1.Visible = false;
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
            this.listingActionColumn,
            this.linkDataGridViewTextBoxColumn,
            this.keyWordsDataGridViewTextBoxColumn,
            this.noteDataGridViewTextBoxColumn,
            this.dateLastPromotionColumn,
            this.FoundOnPage,
            this.StatusColumn});
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
            this.PromotionList.Size = new System.Drawing.Size(1091, 419);
            this.PromotionList.TabIndex = 3;
            this.PromotionList.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.PromotionList_RowPostPaint);
            // 
            // listingActionColumn
            // 
            this.listingActionColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.listingActionColumn.DataPropertyName = "ItemAction";
            this.listingActionColumn.FillWeight = 75F;
            this.listingActionColumn.HeaderText = "Действие";
            this.listingActionColumn.Name = "listingActionColumn";
            this.listingActionColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.listingActionColumn.Width = 160;
            // 
            // linkDataGridViewTextBoxColumn
            // 
            this.linkDataGridViewTextBoxColumn.DataPropertyName = "Link";
            this.linkDataGridViewTextBoxColumn.FillWeight = 50F;
            this.linkDataGridViewTextBoxColumn.HeaderText = "Ссылка на товар";
            this.linkDataGridViewTextBoxColumn.Name = "linkDataGridViewTextBoxColumn";
            this.linkDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.linkDataGridViewTextBoxColumn.Width = 304;
            // 
            // keyWordsDataGridViewTextBoxColumn
            // 
            this.keyWordsDataGridViewTextBoxColumn.DataPropertyName = "KeyWords";
            this.keyWordsDataGridViewTextBoxColumn.HeaderText = "Ключевые слова(через ;)";
            this.keyWordsDataGridViewTextBoxColumn.Name = "keyWordsDataGridViewTextBoxColumn";
            this.keyWordsDataGridViewTextBoxColumn.ToolTipText = "Список ключевых слов по которым будет продвигаться товар(через ;)";
            this.keyWordsDataGridViewTextBoxColumn.Width = 240;
            // 
            // noteDataGridViewTextBoxColumn
            // 
            this.noteDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.noteDataGridViewTextBoxColumn.DataPropertyName = "Note";
            this.noteDataGridViewTextBoxColumn.FillWeight = 50F;
            this.noteDataGridViewTextBoxColumn.HeaderText = "Заметка";
            this.noteDataGridViewTextBoxColumn.Name = "noteDataGridViewTextBoxColumn";
            // 
            // dateLastPromotionColumn
            // 
            this.dateLastPromotionColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dateLastPromotionColumn.DataPropertyName = "DateLastPromotion";
            this.dateLastPromotionColumn.HeaderText = "Дата продвижения";
            this.dateLastPromotionColumn.Name = "dateLastPromotionColumn";
            this.dateLastPromotionColumn.ToolTipText = "Дата последнего продвижения товара";
            this.dateLastPromotionColumn.Width = 110;
            // 
            // FoundOnPage
            // 
            this.FoundOnPage.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.FoundOnPage.DataPropertyName = "FoundOnPage";
            this.FoundOnPage.HeaderText = "Найдено на странице";
            this.FoundOnPage.Name = "FoundOnPage";
            this.FoundOnPage.ReadOnly = true;
            this.FoundOnPage.Width = 87;
            // 
            // StatusColumn
            // 
            this.StatusColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.StatusColumn.HeaderText = "Статус";
            this.StatusColumn.Name = "StatusColumn";
            this.StatusColumn.ReadOnly = true;
            this.StatusColumn.Width = 45;
            // 
            // productsListDataSource
            // 
            this.productsListDataSource.DataSource = typeof(ShopPromotion.Promotion.Interfaces.KeyWordsListingInfo);
            // 
            // Button_StartPromotion
            // 
            this.Button_StartPromotion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_StartPromotion.Location = new System.Drawing.Point(537, 527);
            this.Button_StartPromotion.Name = "Button_StartPromotion";
            this.Button_StartPromotion.Size = new System.Drawing.Size(577, 54);
            this.Button_StartPromotion.TabIndex = 4;
            this.Button_StartPromotion.Text = "Запустить продвижение";
            this.Button_StartPromotion.UseSelectable = true;
            this.Button_StartPromotion.Click += new System.EventHandler(this.Button_StartPromotion_Click);
            // 
            // RunModeComboBox
            // 
            this.RunModeComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RunModeComboBox.FormattingEnabled = true;
            this.RunModeComboBox.ItemHeight = 23;
            this.RunModeComboBox.Location = new System.Drawing.Point(218, 527);
            this.RunModeComboBox.Name = "RunModeComboBox";
            this.RunModeComboBox.Size = new System.Drawing.Size(313, 29);
            this.RunModeComboBox.TabIndex = 9;
            this.RunModeComboBox.UseSelectable = true;
            // 
            // RumModeLabel
            // 
            this.RumModeLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.RumModeLabel.AutoSize = true;
            this.RumModeLabel.Location = new System.Drawing.Point(23, 533);
            this.RumModeLabel.Name = "RumModeLabel";
            this.RumModeLabel.Size = new System.Drawing.Size(189, 19);
            this.RumModeLabel.TabIndex = 8;
            this.RumModeLabel.Text = "Режим запуска продвижения";
            // 
            // MaxSearchPagesCount
            // 
            this.MaxSearchPagesCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.MaxSearchPagesCount.AutoSize = true;
            this.MaxSearchPagesCount.Location = new System.Drawing.Point(23, 562);
            this.MaxSearchPagesCount.Name = "MaxSearchPagesCount";
            this.MaxSearchPagesCount.Size = new System.Drawing.Size(430, 19);
            this.MaxSearchPagesCount.TabIndex = 10;
            this.MaxSearchPagesCount.Text = "Максимальное количество страниц на которых производится поиск";
            // 
            // MaximumSearchPagesNumericUpDown
            // 
            this.MaximumSearchPagesNumericUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MaximumSearchPagesNumericUpDown.Location = new System.Drawing.Point(460, 562);
            this.MaximumSearchPagesNumericUpDown.Maximum = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.MaximumSearchPagesNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.MaximumSearchPagesNumericUpDown.Name = "MaximumSearchPagesNumericUpDown";
            this.MaximumSearchPagesNumericUpDown.Size = new System.Drawing.Size(71, 20);
            this.MaximumSearchPagesNumericUpDown.TabIndex = 11;
            this.MaximumSearchPagesNumericUpDown.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // SiteModeComboBox
            // 
            this.SiteModeComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SiteModeComboBox.FormattingEnabled = true;
            this.SiteModeComboBox.ItemHeight = 23;
            this.SiteModeComboBox.Location = new System.Drawing.Point(407, 25);
            this.SiteModeComboBox.Name = "SiteModeComboBox";
            this.SiteModeComboBox.Size = new System.Drawing.Size(133, 29);
            this.SiteModeComboBox.TabIndex = 12;
            this.SiteModeComboBox.UseSelectable = true;
            // 
            // KeywordPromotionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1137, 590);
            this.Controls.Add(this.SiteModeComboBox);
            this.Controls.Add(this.MaximumSearchPagesNumericUpDown);
            this.Controls.Add(this.MaxSearchPagesCount);
            this.Controls.Add(this.RunModeComboBox);
            this.Controls.Add(this.RumModeLabel);
            this.Controls.Add(this.Button_StartPromotion);
            this.Controls.Add(this.PromotionList);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.ShopName);
            this.Controls.Add(this.ShopLink);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "KeywordPromotionForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Style = MetroFramework.MetroColorStyle.Green;
            this.Text = "Продвижение по ключевым словам";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.KeywordPromotion_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.KeywordPromotion_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.PromotionList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.productsListDataSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MaximumSearchPagesNumericUpDown)).EndInit();
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
        private System.Windows.Forms.DataGridViewComboBoxColumn listingActionColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn linkDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn keyWordsDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn noteDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dateLastPromotionColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn FoundOnPage;
        private System.Windows.Forms.DataGridViewImageColumn StatusColumn;
        private MetroFramework.Controls.MetroComboBox RunModeComboBox;
        private MetroFramework.Controls.MetroLabel RumModeLabel;
        private MetroFramework.Controls.MetroLabel MaxSearchPagesCount;
        private System.Windows.Forms.NumericUpDown MaximumSearchPagesNumericUpDown;
        private MetroFramework.Controls.MetroComboBox SiteModeComboBox;
    }
}