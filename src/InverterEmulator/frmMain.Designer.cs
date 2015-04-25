namespace InverterEmulator
{
    partial class frmMain
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
            this.cmdRefreshComPorts = new System.Windows.Forms.Button();
            this.comComPorts = new System.Windows.Forms.ComboBox();
            this.cmdOpenComPort = new System.Windows.Forms.Button();
            this.lstCommands = new System.Windows.Forms.ListBox();
            this.txtCommand = new System.Windows.Forms.TextBox();
            this.tlpControls = new System.Windows.Forms.FlowLayoutPanel();
            this.tabMessages = new System.Windows.Forms.TabControl();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabMessages.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdRefreshComPorts
            // 
            this.cmdRefreshComPorts.Location = new System.Drawing.Point(341, 11);
            this.cmdRefreshComPorts.Name = "cmdRefreshComPorts";
            this.cmdRefreshComPorts.Size = new System.Drawing.Size(67, 21);
            this.cmdRefreshComPorts.TabIndex = 0;
            this.cmdRefreshComPorts.Text = "Refresh";
            this.cmdRefreshComPorts.UseVisualStyleBackColor = true;
            this.cmdRefreshComPorts.Click += new System.EventHandler(this.cmdRefreshComPorts_Click);
            // 
            // comComPorts
            // 
            this.comComPorts.FormattingEnabled = true;
            this.comComPorts.Location = new System.Drawing.Point(12, 12);
            this.comComPorts.Name = "comComPorts";
            this.comComPorts.Size = new System.Drawing.Size(323, 21);
            this.comComPorts.TabIndex = 1;
            this.comComPorts.Text = "COM9";
            this.comComPorts.SelectedIndexChanged += new System.EventHandler(this.comComPorts_SelectedIndexChanged);
            // 
            // cmdOpenComPort
            // 
            this.cmdOpenComPort.Location = new System.Drawing.Point(414, 11);
            this.cmdOpenComPort.Name = "cmdOpenComPort";
            this.cmdOpenComPort.Size = new System.Drawing.Size(67, 21);
            this.cmdOpenComPort.TabIndex = 2;
            this.cmdOpenComPort.Text = "Open";
            this.cmdOpenComPort.UseVisualStyleBackColor = true;
            this.cmdOpenComPort.Click += new System.EventHandler(this.cmdOpenComPort_Click);
            // 
            // lstCommands
            // 
            this.lstCommands.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstCommands.FormattingEnabled = true;
            this.lstCommands.Location = new System.Drawing.Point(12, 381);
            this.lstCommands.Name = "lstCommands";
            this.lstCommands.Size = new System.Drawing.Size(769, 108);
            this.lstCommands.TabIndex = 3;
            // 
            // txtCommand
            // 
            this.txtCommand.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCommand.Location = new System.Drawing.Point(11, 355);
            this.txtCommand.Name = "txtCommand";
            this.txtCommand.Size = new System.Drawing.Size(770, 20);
            this.txtCommand.TabIndex = 1;
            // 
            // tlpControls
            // 
            this.tlpControls.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tlpControls.AutoScroll = true;
            this.tlpControls.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tlpControls.Location = new System.Drawing.Point(11, 74);
            this.tlpControls.Name = "tlpControls";
            this.tlpControls.Size = new System.Drawing.Size(769, 275);
            this.tlpControls.TabIndex = 0;
            this.tlpControls.Resize += new System.EventHandler(this.tlpControls_Resize);
            // 
            // tabMessages
            // 
            this.tabMessages.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabMessages.Controls.Add(this.tabPage4);
            this.tabMessages.Controls.Add(this.tabPage5);
            this.tabMessages.Controls.Add(this.tabPage1);
            this.tabMessages.Controls.Add(this.tabPage2);
            this.tabMessages.Location = new System.Drawing.Point(12, 52);
            this.tabMessages.Name = "tabMessages";
            this.tabMessages.SelectedIndex = 0;
            this.tabMessages.Size = new System.Drawing.Size(766, 27);
            this.tabMessages.TabIndex = 0;
            this.tabMessages.SelectedIndexChanged += new System.EventHandler(this.tabMessages_SelectedIndexChanged);
            this.tabMessages.TabIndexChanged += new System.EventHandler(this.tabMessages_TabIndexChanged);
            // 
            // tabPage4
            // 
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(758, 1);
            this.tabPage4.TabIndex = 0;
            this.tabPage4.Tag = "QPIRI";
            this.tabPage4.Text = "Ratings";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // tabPage5
            // 
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(758, 1);
            this.tabPage5.TabIndex = 1;
            this.tabPage5.Tag = "QPIGS";
            this.tabPage5.Text = "Status";
            this.tabPage5.UseVisualStyleBackColor = true;
            this.tabPage5.Click += new System.EventHandler(this.tabPage5_Click);
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(758, 1);
            this.tabPage1.TabIndex = 2;
            this.tabPage1.Tag = "QMOD";
            this.tabPage1.Text = "Mode";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(758, 1);
            this.tabPage2.TabIndex = 3;
            this.tabPage2.Tag = "QPIWS";
            this.tabPage2.Text = "Faults";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(790, 519);
            this.Controls.Add(this.tlpControls);
            this.Controls.Add(this.tabMessages);
            this.Controls.Add(this.txtCommand);
            this.Controls.Add(this.lstCommands);
            this.Controls.Add(this.cmdOpenComPort);
            this.Controls.Add(this.comComPorts);
            this.Controls.Add(this.cmdRefreshComPorts);
            this.Name = "frmMain";
            this.Text = "Inverter Emulator";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.tabMessages.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdRefreshComPorts;
        private System.Windows.Forms.ComboBox comComPorts;
        private System.Windows.Forms.Button cmdOpenComPort;
        private System.Windows.Forms.ListBox lstCommands;
        private System.Windows.Forms.FlowLayoutPanel tlpControls;
        private System.Windows.Forms.TextBox txtCommand;
        private System.Windows.Forms.TabControl tabMessages;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
    }
}

