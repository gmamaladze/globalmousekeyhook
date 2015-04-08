namespace MouseKeyboardActivityMonitor.Demo
{
    partial class TestCommonWinFormsBehaviour
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
            this.clickableArea = new System.Windows.Forms.Label();
            this.typeArea = new System.Windows.Forms.TextBox();
            this.textBoxLog = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // clickableArea
            // 
            this.clickableArea.BackColor = System.Drawing.Color.Lime;
            this.clickableArea.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.clickableArea.Location = new System.Drawing.Point(8, 9);
            this.clickableArea.Name = "clickableArea";
            this.clickableArea.Size = new System.Drawing.Size(104, 76);
            this.clickableArea.TabIndex = 0;
            this.clickableArea.Text = "Click here";
            this.clickableArea.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.clickableArea.MouseClick += new System.Windows.Forms.MouseEventHandler(this.clickableArea_MouseClick);
            this.clickableArea.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.clickableArea_MouseDoubleClick);
            this.clickableArea.MouseDown += new System.Windows.Forms.MouseEventHandler(this.clickableArea_MouseDown);
            this.clickableArea.MouseUp += new System.Windows.Forms.MouseEventHandler(this.clickableArea_MouseUp);
            // 
            // typeArea
            // 
            this.typeArea.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.typeArea.Location = new System.Drawing.Point(118, 9);
            this.typeArea.Multiline = true;
            this.typeArea.Name = "typeArea";
            this.typeArea.Size = new System.Drawing.Size(518, 76);
            this.typeArea.TabIndex = 1;
            // 
            // textBoxLog
            // 
            this.textBoxLog.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxLog.BackColor = System.Drawing.Color.White;
            this.textBoxLog.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxLog.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxLog.Location = new System.Drawing.Point(8, 91);
            this.textBoxLog.Multiline = true;
            this.textBoxLog.Name = "textBoxLog";
            this.textBoxLog.ReadOnly = true;
            this.textBoxLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxLog.Size = new System.Drawing.Size(636, 378);
            this.textBoxLog.TabIndex = 6;
            this.textBoxLog.WordWrap = false;
            // 
            // TestCommonWinFormsBehaviour
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(648, 457);
            this.Controls.Add(this.textBoxLog);
            this.Controls.Add(this.typeArea);
            this.Controls.Add(this.clickableArea);
            this.Name = "TestCommonWinFormsBehaviour";
            this.Text = "TestCommonWinFormsBehaviour";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label clickableArea;
        private System.Windows.Forms.TextBox typeArea;
        private System.Windows.Forms.TextBox textBoxLog;
    }
}