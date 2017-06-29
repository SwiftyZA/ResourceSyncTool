namespace ResourceSyncTool
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
            this.components = new System.ComponentModel.Container();
            this.dgvResX = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnFilter = new System.Windows.Forms.Button();
            this.txtSearchPhrase = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cboState = new System.Windows.Forms.ComboBox();
            this.lblState = new System.Windows.Forms.Label();
            this.cboResxFiles = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.Tooltip = new System.Windows.Forms.ToolTip(this.components);
            this.lblTotalh = new System.Windows.Forms.Label();
            this.lblFilteredTotalh = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.lblFilteredTotal = new System.Windows.Forms.Label();
            this.grpbxControls = new System.Windows.Forms.GroupBox();
            this.btnClearOldDir = new System.Windows.Forms.Button();
            this.LblCurrentDir = new System.Windows.Forms.Label();
            this.lblPreviousDir = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblLang = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.btnCompareOldMainResXFiles = new System.Windows.Forms.Button();
            this.btnChangeLang = new System.Windows.Forms.Button();
            this.btnTranslateAll = new System.Windows.Forms.Button();
            this.btnTranslateSelectedRows = new System.Windows.Forms.Button();
            this.btnTranslateFilterResults = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ChangeMainResXLocationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Legend = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.panel7 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResX)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.grpbxControls.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.Legend.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvResX
            // 
            this.dgvResX.AllowUserToAddRows = false;
            this.dgvResX.AllowUserToDeleteRows = false;
            this.dgvResX.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvResX.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvResX.Location = new System.Drawing.Point(11, 204);
            this.dgvResX.Margin = new System.Windows.Forms.Padding(4);
            this.dgvResX.Name = "dgvResX";
            this.dgvResX.Size = new System.Drawing.Size(1494, 527);
            this.dgvResX.TabIndex = 0;
            this.dgvResX.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvResX_CellEndEdit);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnFilter);
            this.groupBox1.Controls.Add(this.txtSearchPhrase);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cboState);
            this.groupBox1.Controls.Add(this.lblState);
            this.groupBox1.Controls.Add(this.cboResxFiles);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(13, 33);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(408, 163);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Filter";
            // 
            // btnFilter
            // 
            this.btnFilter.Location = new System.Drawing.Point(289, 121);
            this.btnFilter.Margin = new System.Windows.Forms.Padding(4);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.Size = new System.Drawing.Size(100, 28);
            this.btnFilter.TabIndex = 6;
            this.btnFilter.Text = "Filter";
            this.btnFilter.UseVisualStyleBackColor = true;
            this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);
            // 
            // txtSearchPhrase
            // 
            this.txtSearchPhrase.Location = new System.Drawing.Point(115, 56);
            this.txtSearchPhrase.Margin = new System.Windows.Forms.Padding(4);
            this.txtSearchPhrase.Name = "txtSearchPhrase";
            this.txtSearchPhrase.Size = new System.Drawing.Size(273, 22);
            this.txtSearchPhrase.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 59);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "Value";
            // 
            // cboState
            // 
            this.cboState.FormattingEnabled = true;
            this.cboState.Location = new System.Drawing.Point(115, 90);
            this.cboState.Margin = new System.Windows.Forms.Padding(4);
            this.cboState.Name = "cboState";
            this.cboState.Size = new System.Drawing.Size(273, 24);
            this.cboState.TabIndex = 3;
            // 
            // lblState
            // 
            this.lblState.AutoSize = true;
            this.lblState.Location = new System.Drawing.Point(8, 93);
            this.lblState.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblState.Name = "lblState";
            this.lblState.Size = new System.Drawing.Size(98, 17);
            this.lblState.TabIndex = 2;
            this.lblState.Text = "Modified State";
            // 
            // cboResxFiles
            // 
            this.cboResxFiles.FormattingEnabled = true;
            this.cboResxFiles.Location = new System.Drawing.Point(116, 21);
            this.cboResxFiles.Margin = new System.Windows.Forms.Padding(4);
            this.cboResxFiles.Name = "cboResxFiles";
            this.cboResxFiles.Size = new System.Drawing.Size(273, 24);
            this.cboResxFiles.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 24);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Resource file";
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(1347, 739);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(153, 28);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Save Changes";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lblTotalh
            // 
            this.lblTotalh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTotalh.AutoSize = true;
            this.lblTotalh.Location = new System.Drawing.Point(17, 745);
            this.lblTotalh.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTotalh.Name = "lblTotalh";
            this.lblTotalh.Size = new System.Drawing.Size(44, 17);
            this.lblTotalh.TabIndex = 3;
            this.lblTotalh.Text = "Total:";
            // 
            // lblFilteredTotalh
            // 
            this.lblFilteredTotalh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblFilteredTotalh.AutoSize = true;
            this.lblFilteredTotalh.Location = new System.Drawing.Point(126, 745);
            this.lblFilteredTotalh.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFilteredTotalh.Name = "lblFilteredTotalh";
            this.lblFilteredTotalh.Size = new System.Drawing.Size(95, 17);
            this.lblFilteredTotalh.TabIndex = 4;
            this.lblFilteredTotalh.Text = "Filtered Total:";
            // 
            // lblTotal
            // 
            this.lblTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTotal.AutoSize = true;
            this.lblTotal.Location = new System.Drawing.Point(69, 745);
            this.lblTotal.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(16, 17);
            this.lblTotal.TabIndex = 5;
            this.lblTotal.Text = "0";
            // 
            // lblFilteredTotal
            // 
            this.lblFilteredTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblFilteredTotal.AutoSize = true;
            this.lblFilteredTotal.Location = new System.Drawing.Point(229, 745);
            this.lblFilteredTotal.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFilteredTotal.Name = "lblFilteredTotal";
            this.lblFilteredTotal.Size = new System.Drawing.Size(16, 17);
            this.lblFilteredTotal.TabIndex = 6;
            this.lblFilteredTotal.Text = "0";
            // 
            // grpbxControls
            // 
            this.grpbxControls.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpbxControls.Controls.Add(this.btnClearOldDir);
            this.grpbxControls.Controls.Add(this.LblCurrentDir);
            this.grpbxControls.Controls.Add(this.lblPreviousDir);
            this.grpbxControls.Controls.Add(this.label8);
            this.grpbxControls.Controls.Add(this.lblLang);
            this.grpbxControls.Controls.Add(this.button1);
            this.grpbxControls.Controls.Add(this.btnCompareOldMainResXFiles);
            this.grpbxControls.Controls.Add(this.btnChangeLang);
            this.grpbxControls.Controls.Add(this.btnTranslateAll);
            this.grpbxControls.Controls.Add(this.btnTranslateSelectedRows);
            this.grpbxControls.Controls.Add(this.btnTranslateFilterResults);
            this.grpbxControls.Location = new System.Drawing.Point(429, 32);
            this.grpbxControls.Margin = new System.Windows.Forms.Padding(4);
            this.grpbxControls.Name = "grpbxControls";
            this.grpbxControls.Padding = new System.Windows.Forms.Padding(4);
            this.grpbxControls.Size = new System.Drawing.Size(766, 164);
            this.grpbxControls.TabIndex = 7;
            this.grpbxControls.TabStop = false;
            this.grpbxControls.Text = "Controls";
            // 
            // btnClearOldDir
            // 
            this.btnClearOldDir.BackgroundImage = global::ResourceSyncTool.Properties.Resources.Red_Cross;
            this.btnClearOldDir.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClearOldDir.Location = new System.Drawing.Point(730, 121);
            this.btnClearOldDir.Name = "btnClearOldDir";
            this.btnClearOldDir.Size = new System.Drawing.Size(30, 30);
            this.btnClearOldDir.TabIndex = 6;
            this.btnClearOldDir.UseVisualStyleBackColor = true;
            this.btnClearOldDir.Click += new System.EventHandler(this.btnClearOldDir_Click);
            // 
            // LblCurrentDir
            // 
            this.LblCurrentDir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LblCurrentDir.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblCurrentDir.Location = new System.Drawing.Point(223, 88);
            this.LblCurrentDir.Name = "LblCurrentDir";
            this.LblCurrentDir.Size = new System.Drawing.Size(536, 28);
            this.LblCurrentDir.TabIndex = 5;
            this.LblCurrentDir.Text = "None Selected";
            this.LblCurrentDir.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblPreviousDir
            // 
            this.lblPreviousDir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPreviousDir.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblPreviousDir.Location = new System.Drawing.Point(223, 122);
            this.lblPreviousDir.Name = "lblPreviousDir";
            this.lblPreviousDir.Size = new System.Drawing.Size(502, 28);
            this.lblPreviousDir.TabIndex = 5;
            this.lblPreviousDir.Text = "None Selected";
            this.lblPreviousDir.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(8, 25);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(209, 17);
            this.label8.TabIndex = 3;
            this.label8.Text = "Translate Using Google";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblLang
            // 
            this.lblLang.AutoSize = true;
            this.lblLang.Location = new System.Drawing.Point(223, 60);
            this.lblLang.Name = "lblLang";
            this.lblLang.Size = new System.Drawing.Size(146, 17);
            this.lblLang.TabIndex = 3;
            this.lblLang.Text = "No language selected";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(7, 88);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(210, 28);
            this.button1.TabIndex = 2;
            this.button1.Text = "Change Location";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.ChangeMainResXLocationToolStripMenuItem_Click);
            // 
            // btnCompareOldMainResXFiles
            // 
            this.btnCompareOldMainResXFiles.Location = new System.Drawing.Point(6, 122);
            this.btnCompareOldMainResXFiles.Name = "btnCompareOldMainResXFiles";
            this.btnCompareOldMainResXFiles.Size = new System.Drawing.Size(210, 28);
            this.btnCompareOldMainResXFiles.TabIndex = 2;
            this.btnCompareOldMainResXFiles.Text = "Compare with Previous";
            this.btnCompareOldMainResXFiles.UseVisualStyleBackColor = true;
            this.btnCompareOldMainResXFiles.Click += new System.EventHandler(this.btnCompareOldMainResXFiles_Click);
            // 
            // btnChangeLang
            // 
            this.btnChangeLang.Location = new System.Drawing.Point(7, 54);
            this.btnChangeLang.Name = "btnChangeLang";
            this.btnChangeLang.Size = new System.Drawing.Size(210, 28);
            this.btnChangeLang.TabIndex = 1;
            this.btnChangeLang.Text = "Change Language";
            this.btnChangeLang.UseVisualStyleBackColor = true;
            this.btnChangeLang.Click += new System.EventHandler(this.btnChangeLang_Click);
            // 
            // btnTranslateAll
            // 
            this.btnTranslateAll.Location = new System.Drawing.Point(223, 19);
            this.btnTranslateAll.Margin = new System.Windows.Forms.Padding(4);
            this.btnTranslateAll.Name = "btnTranslateAll";
            this.btnTranslateAll.Size = new System.Drawing.Size(117, 28);
            this.btnTranslateAll.TabIndex = 0;
            this.btnTranslateAll.Text = "All";
            this.btnTranslateAll.UseVisualStyleBackColor = true;
            this.btnTranslateAll.Click += new System.EventHandler(this.btnGoogle_Click);
            // 
            // btnTranslateSelectedRows
            // 
            this.btnTranslateSelectedRows.Location = new System.Drawing.Point(473, 19);
            this.btnTranslateSelectedRows.Margin = new System.Windows.Forms.Padding(4);
            this.btnTranslateSelectedRows.Name = "btnTranslateSelectedRows";
            this.btnTranslateSelectedRows.Size = new System.Drawing.Size(117, 28);
            this.btnTranslateSelectedRows.TabIndex = 0;
            this.btnTranslateSelectedRows.Text = "Selected Items";
            this.btnTranslateSelectedRows.UseVisualStyleBackColor = true;
            this.btnTranslateSelectedRows.Click += new System.EventHandler(this.btnTranslateSelectedRows_Click);
            // 
            // btnTranslateFilterResults
            // 
            this.btnTranslateFilterResults.Location = new System.Drawing.Point(348, 19);
            this.btnTranslateFilterResults.Margin = new System.Windows.Forms.Padding(4);
            this.btnTranslateFilterResults.Name = "btnTranslateFilterResults";
            this.btnTranslateFilterResults.Size = new System.Drawing.Size(117, 28);
            this.btnTranslateFilterResults.TabIndex = 0;
            this.btnTranslateFilterResults.Text = "Filter Results";
            this.btnTranslateFilterResults.UseVisualStyleBackColor = true;
            this.btnTranslateFilterResults.Click += new System.EventHandler(this.btnTranslateFilterResults_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1517, 28);
            this.menuStrip1.TabIndex = 8;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ChangeMainResXLocationToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(44, 24);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // ChangeMainResXLocationToolStripMenuItem
            // 
            this.ChangeMainResXLocationToolStripMenuItem.Name = "ChangeMainResXLocationToolStripMenuItem";
            this.ChangeMainResXLocationToolStripMenuItem.Size = new System.Drawing.Size(195, 26);
            this.ChangeMainResXLocationToolStripMenuItem.Text = "Change Location";
            this.ChangeMainResXLocationToolStripMenuItem.Click += new System.EventHandler(this.ChangeMainResXLocationToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(195, 26);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // Legend
            // 
            this.Legend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Legend.Controls.Add(this.label10);
            this.Legend.Controls.Add(this.panel7);
            this.Legend.Controls.Add(this.label4);
            this.Legend.Controls.Add(this.label6);
            this.Legend.Controls.Add(this.panel3);
            this.Legend.Controls.Add(this.panel4);
            this.Legend.Controls.Add(this.label7);
            this.Legend.Controls.Add(this.panel2);
            this.Legend.Controls.Add(this.label5);
            this.Legend.Controls.Add(this.panel5);
            this.Legend.Controls.Add(this.label3);
            this.Legend.Controls.Add(this.panel1);
            this.Legend.Location = new System.Drawing.Point(1202, 33);
            this.Legend.Name = "Legend";
            this.Legend.Size = new System.Drawing.Size(303, 163);
            this.Legend.TabIndex = 11;
            this.Legend.TabStop = false;
            this.Legend.Text = "Legend";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(38, 59);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(155, 17);
            this.label10.TabIndex = 5;
            this.label10.Text = "Localized File Changed";
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.LightSalmon;
            this.panel7.Location = new System.Drawing.Point(6, 56);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(26, 25);
            this.panel7.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(238, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 17);
            this.label4.TabIndex = 1;
            this.label4.Text = "New";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(38, 24);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(138, 17);
            this.label6.TabIndex = 1;
            this.label6.Text = "Master File Changed";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Yellow;
            this.panel3.Location = new System.Drawing.Point(6, 21);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(26, 25);
            this.panel3.TabIndex = 0;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.LightGoldenrodYellow;
            this.panel4.Location = new System.Drawing.Point(206, 21);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(26, 25);
            this.panel4.TabIndex = 0;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(38, 93);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(146, 17);
            this.label7.TabIndex = 1;
            this.label7.Text = "Translated By Google";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Aqua;
            this.panel2.Location = new System.Drawing.Point(6, 90);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(26, 25);
            this.panel2.TabIndex = 0;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(38, 127);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(79, 17);
            this.label5.TabIndex = 1;
            this.label5.Text = "No Change";
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.White;
            this.panel5.Location = new System.Drawing.Point(6, 124);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(26, 25);
            this.panel5.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(238, 59);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 17);
            this.label3.TabIndex = 1;
            this.label3.Text = "Updated";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.LimeGreen;
            this.panel1.Location = new System.Drawing.Point(206, 56);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(26, 25);
            this.panel1.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1517, 777);
            this.Controls.Add(this.Legend);
            this.Controls.Add(this.grpbxControls);
            this.Controls.Add(this.lblFilteredTotal);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.lblFilteredTotalh);
            this.Controls.Add(this.lblTotalh);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dgvResX);
            this.Controls.Add(this.menuStrip1);
            this.Enabled = false;
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(1535, 824);
            this.Name = "MainForm";
            this.Text = "Resource Translation Tool";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.dgvResX)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.grpbxControls.ResumeLayout(false);
            this.grpbxControls.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.Legend.ResumeLayout(false);
            this.Legend.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvResX;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnFilter;
        private System.Windows.Forms.TextBox txtSearchPhrase;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboState;
        private System.Windows.Forms.Label lblState;
        private System.Windows.Forms.ComboBox cboResxFiles;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ToolTip Tooltip;
        private System.Windows.Forms.Label lblTotalh;
        private System.Windows.Forms.Label lblFilteredTotalh;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label lblFilteredTotal;
        private System.Windows.Forms.GroupBox grpbxControls;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ChangeMainResXLocationToolStripMenuItem;
        private System.Windows.Forms.Button btnChangeLang;
        private System.Windows.Forms.Button btnCompareOldMainResXFiles;
        private System.Windows.Forms.GroupBox Legend;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label lblLang;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label lblPreviousDir;
        private System.Windows.Forms.Label LblCurrentDir;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnTranslateAll;
        private System.Windows.Forms.Button btnTranslateSelectedRows;
        private System.Windows.Forms.Button btnTranslateFilterResults;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Button btnClearOldDir;
    }
}

