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
            components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(KeywordPromotionForm));
            ShopName = new MetroFramework.Controls.MetroLabel();
            ShopLink = new System.Windows.Forms.TextBox();
            ButtonDownloadShopListings = new MetroFramework.Controls.MetroButton();
            PromotionList = new MetroFramework.Controls.MetroGrid();
            listingActionColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            linkDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            keyWordsDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            noteDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            dateLastPromotionColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            FoundOnPage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            StatusColumn = new System.Windows.Forms.DataGridViewImageColumn();
            productsListDataSource = new System.Windows.Forms.BindingSource(components);
            Button_StartPromotion = new MetroFramework.Controls.MetroButton();
            RunModeComboBox = new MetroFramework.Controls.MetroComboBox();
            RumModeLabel = new MetroFramework.Controls.MetroLabel();
            MaxSearchPagesCount = new MetroFramework.Controls.MetroLabel();
            SiteModeComboBox = new MetroFramework.Controls.MetroComboBox();
            LabelStatus = new System.Windows.Forms.Label();
            MaximumSearchPagesNumericUpDown = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)PromotionList).BeginInit();
            ((System.ComponentModel.ISupportInitialize)productsListDataSource).BeginInit();
            ((System.ComponentModel.ISupportInitialize)MaximumSearchPagesNumericUpDown).BeginInit();
            SuspendLayout();
            // 
            // ShopName
            // 
            ShopName.AutoSize = true;
            ShopName.Location = new System.Drawing.Point(27, 77);
            ShopName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            ShopName.Name = "ShopName";
            ShopName.Size = new System.Drawing.Size(219, 19);
            ShopName.TabIndex = 1;
            ShopName.Text = "Ссылка на продвигаемый магазин";
            // 
            // ShopLink
            // 
            ShopLink.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            ShopLink.Location = new System.Drawing.Point(254, 73);
            ShopLink.Name = "ShopLink";
            ShopLink.Size = new System.Drawing.Size(813, 23);
            ShopLink.TabIndex = 13;
            // 
            // ButtonDownloadShopListings
            // 
            ButtonDownloadShopListings.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            ButtonDownloadShopListings.Location = new System.Drawing.Point(1074, 73);
            ButtonDownloadShopListings.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            ButtonDownloadShopListings.Name = "ButtonDownloadShopListings";
            ButtonDownloadShopListings.Size = new System.Drawing.Size(224, 23);
            ButtonDownloadShopListings.TabIndex = 2;
            ButtonDownloadShopListings.Text = "Загрузить список товаров";
            ButtonDownloadShopListings.UseSelectable = true;
            ButtonDownloadShopListings.Click += ButtonDownloadShopListings_Click;
            // 
            // PromotionList
            // 
            PromotionList.AllowUserToOrderColumns = true;
            PromotionList.AllowUserToResizeRows = false;
            PromotionList.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            PromotionList.AutoGenerateColumns = false;
            PromotionList.BackgroundColor = System.Drawing.Color.White;
            PromotionList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            PromotionList.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            PromotionList.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.PeachPuff;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(17, 17, 17);
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            PromotionList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            PromotionList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            PromotionList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { listingActionColumn, linkDataGridViewTextBoxColumn, keyWordsDataGridViewTextBoxColumn, noteDataGridViewTextBoxColumn, dateLastPromotionColumn, FoundOnPage, StatusColumn });
            PromotionList.DataSource = productsListDataSource;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(17, 17, 17);
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            PromotionList.DefaultCellStyle = dataGridViewCellStyle2;
            PromotionList.EnableHeadersVisualStyles = false;
            PromotionList.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            PromotionList.GridColor = System.Drawing.Color.White;
            PromotionList.Location = new System.Drawing.Point(27, 118);
            PromotionList.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            PromotionList.Name = "PromotionList";
            PromotionList.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(17, 17, 17);
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            PromotionList.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            PromotionList.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            PromotionList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            PromotionList.Size = new System.Drawing.Size(1271, 443);
            PromotionList.TabIndex = 3;
            PromotionList.RowPostPaint += PromotionList_RowPostPaint;
            // 
            // listingActionColumn
            // 
            listingActionColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            listingActionColumn.DataPropertyName = "ItemAction";
            listingActionColumn.FillWeight = 75F;
            listingActionColumn.HeaderText = "Действие";
            listingActionColumn.Name = "listingActionColumn";
            listingActionColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            listingActionColumn.Width = 160;
            // 
            // linkDataGridViewTextBoxColumn
            // 
            linkDataGridViewTextBoxColumn.DataPropertyName = "Link";
            linkDataGridViewTextBoxColumn.FillWeight = 50F;
            linkDataGridViewTextBoxColumn.HeaderText = "Ссылка на товар";
            linkDataGridViewTextBoxColumn.Name = "linkDataGridViewTextBoxColumn";
            linkDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            linkDataGridViewTextBoxColumn.Width = 304;
            // 
            // keyWordsDataGridViewTextBoxColumn
            // 
            keyWordsDataGridViewTextBoxColumn.DataPropertyName = "KeyWords";
            keyWordsDataGridViewTextBoxColumn.HeaderText = "Ключевые слова(через ;)";
            keyWordsDataGridViewTextBoxColumn.Name = "keyWordsDataGridViewTextBoxColumn";
            keyWordsDataGridViewTextBoxColumn.ToolTipText = "Список ключевых слов по которым будет продвигаться товар(через ;)";
            keyWordsDataGridViewTextBoxColumn.Width = 240;
            // 
            // noteDataGridViewTextBoxColumn
            // 
            noteDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            noteDataGridViewTextBoxColumn.DataPropertyName = "Note";
            noteDataGridViewTextBoxColumn.FillWeight = 50F;
            noteDataGridViewTextBoxColumn.HeaderText = "Заметка";
            noteDataGridViewTextBoxColumn.Name = "noteDataGridViewTextBoxColumn";
            // 
            // dateLastPromotionColumn
            // 
            dateLastPromotionColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dateLastPromotionColumn.DataPropertyName = "DateLastPromotion";
            dateLastPromotionColumn.HeaderText = "Дата продвижения";
            dateLastPromotionColumn.Name = "dateLastPromotionColumn";
            dateLastPromotionColumn.ToolTipText = "Дата последнего продвижения товара";
            dateLastPromotionColumn.Width = 110;
            // 
            // FoundOnPage
            // 
            FoundOnPage.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            FoundOnPage.DataPropertyName = "FoundOnPage";
            FoundOnPage.HeaderText = "Найдено на странице";
            FoundOnPage.Name = "FoundOnPage";
            FoundOnPage.ReadOnly = true;
            FoundOnPage.Width = 87;
            // 
            // StatusColumn
            // 
            StatusColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            StatusColumn.HeaderText = "Статус";
            StatusColumn.Name = "StatusColumn";
            StatusColumn.ReadOnly = true;
            StatusColumn.Width = 45;
            // 
            // productsListDataSource
            // 
            productsListDataSource.DataSource = typeof(Promotion.Interfaces.KeyWordsListingInfo);
            // 
            // Button_StartPromotion
            // 
            Button_StartPromotion.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            Button_StartPromotion.Location = new System.Drawing.Point(627, 581);
            Button_StartPromotion.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Button_StartPromotion.MinimumSize = new System.Drawing.Size(58, 0);
            Button_StartPromotion.Name = "Button_StartPromotion";
            Button_StartPromotion.Size = new System.Drawing.Size(671, 62);
            Button_StartPromotion.TabIndex = 4;
            Button_StartPromotion.Text = "Запустить продвижение";
            Button_StartPromotion.UseSelectable = true;
            Button_StartPromotion.Click += Button_StartPromotion_Click;
            // 
            // RunModeComboBox
            // 
            RunModeComboBox.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            RunModeComboBox.FormattingEnabled = true;
            RunModeComboBox.ItemHeight = 23;
            RunModeComboBox.Location = new System.Drawing.Point(254, 581);
            RunModeComboBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            RunModeComboBox.Name = "RunModeComboBox";
            RunModeComboBox.Size = new System.Drawing.Size(364, 29);
            RunModeComboBox.TabIndex = 9;
            RunModeComboBox.UseSelectable = true;
            // 
            // RumModeLabel
            // 
            RumModeLabel.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            RumModeLabel.AutoSize = true;
            RumModeLabel.Location = new System.Drawing.Point(27, 585);
            RumModeLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            RumModeLabel.Name = "RumModeLabel";
            RumModeLabel.Size = new System.Drawing.Size(189, 19);
            RumModeLabel.TabIndex = 8;
            RumModeLabel.Text = "Режим запуска продвижения";
            // 
            // MaxSearchPagesCount
            // 
            MaxSearchPagesCount.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            MaxSearchPagesCount.AutoSize = true;
            MaxSearchPagesCount.Location = new System.Drawing.Point(27, 622);
            MaxSearchPagesCount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            MaxSearchPagesCount.Name = "MaxSearchPagesCount";
            MaxSearchPagesCount.Size = new System.Drawing.Size(430, 19);
            MaxSearchPagesCount.TabIndex = 10;
            MaxSearchPagesCount.Text = "Максимальное количество страниц на которых производится поиск";
            // 
            // SiteModeComboBox
            // 
            SiteModeComboBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            SiteModeComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            SiteModeComboBox.FormattingEnabled = true;
            SiteModeComboBox.ItemHeight = 23;
            SiteModeComboBox.Location = new System.Drawing.Point(475, 29);
            SiteModeComboBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            SiteModeComboBox.MinimumSize = new System.Drawing.Size(81, 0);
            SiteModeComboBox.Name = "SiteModeComboBox";
            SiteModeComboBox.Size = new System.Drawing.Size(152, 29);
            SiteModeComboBox.TabIndex = 12;
            SiteModeComboBox.UseSelectable = true;
            // 
            // LabelStatus
            // 
            LabelStatus.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            LabelStatus.Location = new System.Drawing.Point(27, 649);
            LabelStatus.Name = "LabelStatus";
            LabelStatus.Size = new System.Drawing.Size(1270, 24);
            LabelStatus.TabIndex = 14;
            LabelStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // MaximumSearchPagesNumericUpDown
            // 
            MaximumSearchPagesNumericUpDown.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            MaximumSearchPagesNumericUpDown.Location = new System.Drawing.Point(529, 620);
            MaximumSearchPagesNumericUpDown.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            MaximumSearchPagesNumericUpDown.Maximum = new decimal(new int[] { 300, 0, 0, 0 });
            MaximumSearchPagesNumericUpDown.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            MaximumSearchPagesNumericUpDown.Name = "MaximumSearchPagesNumericUpDown";
            MaximumSearchPagesNumericUpDown.Size = new System.Drawing.Size(89, 23);
            MaximumSearchPagesNumericUpDown.TabIndex = 11;
            MaximumSearchPagesNumericUpDown.Value = new decimal(new int[] { 100, 0, 0, 0 });
            // 
            // KeywordPromotionForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1324, 683);
            Controls.Add(LabelStatus);
            Controls.Add(ShopLink);
            Controls.Add(SiteModeComboBox);
            Controls.Add(MaximumSearchPagesNumericUpDown);
            Controls.Add(MaxSearchPagesCount);
            Controls.Add(RunModeComboBox);
            Controls.Add(RumModeLabel);
            Controls.Add(Button_StartPromotion);
            Controls.Add(PromotionList);
            Controls.Add(ButtonDownloadShopListings);
            Controls.Add(ShopName);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Name = "KeywordPromotionForm";
            Padding = new System.Windows.Forms.Padding(23, 69, 23, 23);
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Style = MetroFramework.MetroColorStyle.Green;
            Text = "Продвижение по ключевым словам";
            FormClosing += KeywordPromotion_FormClosing;
            FormClosed += KeywordPromotion_FormClosed;
            ((System.ComponentModel.ISupportInitialize)PromotionList).EndInit();
            ((System.ComponentModel.ISupportInitialize)productsListDataSource).EndInit();
            ((System.ComponentModel.ISupportInitialize)MaximumSearchPagesNumericUpDown).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TextBox ShopLink;
        private MetroFramework.Controls.MetroLabel ShopName;
        private MetroFramework.Controls.MetroButton ButtonDownloadShopListings;
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
        private MetroFramework.Controls.MetroComboBox SiteModeComboBox;
        private System.Windows.Forms.Label LabelStatus;
        private System.Windows.Forms.NumericUpDown MaximumSearchPagesNumericUpDown;
    }
}