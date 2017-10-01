namespace Chess
{
    partial class Chess
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
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new System.Windows.Forms.ListViewItem.ListViewSubItem[] {
            new System.Windows.Forms.ListViewItem.ListViewSubItem(null, "1"),
            new System.Windows.Forms.ListViewItem.ListViewSubItem(null, "L*Pf3", System.Drawing.SystemColors.WindowText, System.Drawing.SystemColors.Window, new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)))),
            new System.Windows.Forms.ListViewItem.ListViewSubItem(null, "K*Te4", System.Drawing.SystemColors.WindowText, System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128))))), new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0))))}, -1);
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem(new string[] {
            "40",
            "subitem2"}, -1);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Chess));
            this.shapeContainer1 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.Chessbord = new Microsoft.VisualBasic.PowerPacks.RectangleShape();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setupGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.wedstrijdModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mensTegenComputerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.animationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.betaTesterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.michaelKoopmanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chessRulesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnBack = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.btnHint = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.btnForward = new System.Windows.Forms.Button();
            this.btnTest = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.contextMenuStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // shapeContainer1
            // 
            this.shapeContainer1.Location = new System.Drawing.Point(0, 0);
            this.shapeContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.shapeContainer1.Name = "shapeContainer1";
            this.shapeContainer1.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] {
            this.Chessbord});
            this.shapeContainer1.Size = new System.Drawing.Size(525, 420);
            this.shapeContainer1.TabIndex = 1;
            this.shapeContainer1.TabStop = false;
            // 
            // Chessbord
            // 
            this.Chessbord.BackgroundImage = global::Chess.Properties.Resources.bord;
            this.Chessbord.CornerRadius = 10;
            this.Chessbord.Location = new System.Drawing.Point(86, 36);
            this.Chessbord.Name = "rectangleShape1";
            this.Chessbord.SelectionColor = System.Drawing.Color.Red;
            this.Chessbord.Size = new System.Drawing.Size(318, 318);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(105, 26);
            // 
            // addToolStripMenuItem
            // 
            this.addToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pToolStripMenuItem});
            this.addToolStripMenuItem.Name = "addToolStripMenuItem";
            this.addToolStripMenuItem.Size = new System.Drawing.Size(104, 22);
            this.addToolStripMenuItem.Text = "&Add";
            // 
            // pToolStripMenuItem
            // 
            this.pToolStripMenuItem.Image = global::Chess.Properties.Resources.Chess_pl;
            this.pToolStripMenuItem.Name = "pToolStripMenuItem";
            this.pToolStripMenuItem.Size = new System.Drawing.Size(105, 22);
            this.pToolStripMenuItem.Text = "&Pion";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.optionsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(525, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadGameToolStripMenuItem,
            this.saveGameToolStripMenuItem,
            this.newGameToolStripMenuItem,
            this.setupGameToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // loadGameToolStripMenuItem
            // 
            this.loadGameToolStripMenuItem.Name = "loadGameToolStripMenuItem";
            this.loadGameToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.loadGameToolStripMenuItem.Text = "&Load game";
            this.loadGameToolStripMenuItem.Click += new System.EventHandler(this.loadGameToolStripMenuItem_Click);
            // 
            // saveGameToolStripMenuItem
            // 
            this.saveGameToolStripMenuItem.Name = "saveGameToolStripMenuItem";
            this.saveGameToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.saveGameToolStripMenuItem.Text = "&Save game";
            this.saveGameToolStripMenuItem.Click += new System.EventHandler(this.saveGameToolStripMenuItem_Click);
            // 
            // newGameToolStripMenuItem
            // 
            this.newGameToolStripMenuItem.Name = "newGameToolStripMenuItem";
            this.newGameToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.newGameToolStripMenuItem.Text = "&New game";
            this.newGameToolStripMenuItem.Click += new System.EventHandler(this.newGameToolStripMenuItem_Click);
            // 
            // setupGameToolStripMenuItem
            // 
            this.setupGameToolStripMenuItem.Name = "setupGameToolStripMenuItem";
            this.setupGameToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.setupGameToolStripMenuItem.Text = "&Setup game";
            this.setupGameToolStripMenuItem.Click += new System.EventHandler(this.setupGameToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.exitToolStripMenuItem.Text = "&Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.wedstrijdModeToolStripMenuItem,
            this.mensTegenComputerToolStripMenuItem,
            this.animationToolStripMenuItem});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
            this.optionsToolStripMenuItem.Text = "&Options";
            // 
            // wedstrijdModeToolStripMenuItem
            // 
            this.wedstrijdModeToolStripMenuItem.Name = "wedstrijdModeToolStripMenuItem";
            this.wedstrijdModeToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.wedstrijdModeToolStripMenuItem.Text = "Wedstrijd mode";
            this.wedstrijdModeToolStripMenuItem.Click += new System.EventHandler(this.wedstrijdModeToolStripMenuItem_Click);
            // 
            // mensTegenComputerToolStripMenuItem
            // 
            this.mensTegenComputerToolStripMenuItem.Checked = true;
            this.mensTegenComputerToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mensTegenComputerToolStripMenuItem.Name = "mensTegenComputerToolStripMenuItem";
            this.mensTegenComputerToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.mensTegenComputerToolStripMenuItem.Text = "Mens tegen Computer";
            this.mensTegenComputerToolStripMenuItem.Click += new System.EventHandler(this.mensTegenComputerToolStripMenuItem_Click);
            // 
            // animationToolStripMenuItem
            // 
            this.animationToolStripMenuItem.Name = "animationToolStripMenuItem";
            this.animationToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.animationToolStripMenuItem.Text = "Animatie";
            this.animationToolStripMenuItem.Click += new System.EventHandler(this.animationToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.betaTesterToolStripMenuItem,
            this.michaelKoopmanToolStripMenuItem,
            this.chessRulesToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // betaTesterToolStripMenuItem
            // 
            this.betaTesterToolStripMenuItem.Name = "betaTesterToolStripMenuItem";
            this.betaTesterToolStripMenuItem.Size = new System.Drawing.Size(205, 22);
            this.betaTesterToolStripMenuItem.Text = "Design: Rudy Meijer";
            // 
            // michaelKoopmanToolStripMenuItem
            // 
            this.michaelKoopmanToolStripMenuItem.Name = "michaelKoopmanToolStripMenuItem";
            this.michaelKoopmanToolStripMenuItem.Size = new System.Drawing.Size(205, 22);
            this.michaelKoopmanToolStripMenuItem.Text = "Tester: Michael Koopman";
            // 
            // chessRulesToolStripMenuItem
            // 
            this.chessRulesToolStripMenuItem.Name = "chessRulesToolStripMenuItem";
            this.chessRulesToolStripMenuItem.Size = new System.Drawing.Size(205, 22);
            this.chessRulesToolStripMenuItem.Text = "Chess Rules";
            this.chessRulesToolStripMenuItem.Click += new System.EventHandler(this.chessRulesToolStripMenuItem_Click);
            // 
            // btnBack
            // 
            this.btnBack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnBack.Location = new System.Drawing.Point(420, 332);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(30, 23);
            this.btnBack.TabIndex = 4;
            this.btnBack.Text = "<";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2});
            this.statusStrip1.Location = new System.Drawing.Point(0, 398);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(525, 22);
            this.statusStrip1.TabIndex = 5;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.AutoSize = false;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(510, 17);
            this.toolStripStatusLabel1.Spring = true;
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(0, 17);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "my game.chess";
            this.openFileDialog1.Filter = "Chess games|*.chess|All files|*.*";
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.FileName = "My game.chess";
            this.saveFileDialog1.Filter = "Chess games|*.chess|All files|*.*";
            // 
            // btnHint
            // 
            this.btnHint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnHint.Location = new System.Drawing.Point(420, 286);
            this.btnHint.Name = "btnHint";
            this.btnHint.Size = new System.Drawing.Size(94, 40);
            this.btnHint.TabIndex = 6;
            this.btnHint.Text = "aanzet";
            this.toolTip1.SetToolTip(this.btnHint, "Hint");
            this.btnHint.UseVisualStyleBackColor = true;
            this.btnHint.Click += new System.EventHandler(this.btnHint_Click);
            // 
            // listView1
            // 
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.listView1.GridLines = true;
            listViewItem1.StateImageIndex = 0;
            listViewItem1.UseItemStyleForSubItems = false;
            listViewItem2.UseItemStyleForSubItems = false;
            this.listView1.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2});
            this.listView1.Location = new System.Drawing.Point(420, 36);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(93, 244);
            this.listView1.TabIndex = 8;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "n";
            this.columnHeader1.Width = 15;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "wit";
            this.columnHeader2.Width = 33;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "zwart";
            this.columnHeader3.Width = 40;
            // 
            // btnForward
            // 
            this.btnForward.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnForward.Location = new System.Drawing.Point(484, 332);
            this.btnForward.Name = "btnForward";
            this.btnForward.Size = new System.Drawing.Size(29, 23);
            this.btnForward.TabIndex = 9;
            this.btnForward.Text = ">";
            this.btnForward.UseVisualStyleBackColor = true;
            this.btnForward.Click += new System.EventHandler(this.btnForward_Click);
            // 
            // btnTest
            // 
            this.btnTest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnTest.Location = new System.Drawing.Point(420, 361);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(75, 23);
            this.btnTest.TabIndex = 10;
            this.btnTest.Text = "Test";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Visible = false;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // toolTip1
            // 
            this.toolTip1.ShowAlways = true;
            // 
            // Chess
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(525, 420);
            this.Controls.Add(this.btnTest);
            this.Controls.Add(this.btnForward);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.btnHint);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.shapeContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Chess";
            this.Text = "Schaakspel";
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Chess_KeyPress);
            this.contextMenuStrip1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer1;
        private Microsoft.VisualBasic.PowerPacks.RectangleShape Chessbord;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
		private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pToolStripMenuItem;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem loadGameToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem saveGameToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
		private System.Windows.Forms.Button btnBack;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
		private System.Windows.Forms.ToolStripMenuItem newGameToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem setupGameToolStripMenuItem;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Button btnHint;
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.Button btnForward;
		private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ToolStripMenuItem betaTesterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem michaelKoopmanToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem wedstrijdModeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mensTegenComputerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem chessRulesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem animationToolStripMenuItem;
    }
}

