namespace Gma.System.MouseKeyHook.Demo
{
    partial class TestCommonWinFormsBehaviour
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private global::System.ComponentModel.IContainer components = null;

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
            this.clickableArea = new global::System.Windows.Forms.Label();
            this.typeArea = new global::System.Windows.Forms.TextBox();
            this.textBoxLog = new global::System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // clickableArea
            // 
            this.clickableArea.BackColor = global::System.Drawing.Color.Lime;
            this.clickableArea.BorderStyle = global::System.Windows.Forms.BorderStyle.FixedSingle;
            this.clickableArea.Location = new global::System.Drawing.Point(8, 9);
            this.clickableArea.Name = "clickableArea";
            this.clickableArea.Size = new global::System.Drawing.Size(104, 76);
            this.clickableArea.TabIndex = 0;
            this.clickableArea.Text = "Click here";
            this.clickableArea.TextAlign = global::System.Drawing.ContentAlignment.MiddleCenter;
            this.clickableArea.MouseClick += new global::System.Windows.Forms.MouseEventHandler(this.clickableArea_MouseClick);
            this.clickableArea.MouseDoubleClick += new global::System.Windows.Forms.MouseEventHandler(this.clickableArea_MouseDoubleClick);
            this.clickableArea.MouseDown += new global::System.Windows.Forms.MouseEventHandler(this.clickableArea_MouseDown);
            this.clickableArea.MouseUp += new global::System.Windows.Forms.MouseEventHandler(this.clickableArea_MouseUp);
            // 
            // typeArea
            // 
            this.typeArea.Anchor = ((global::System.Windows.Forms.AnchorStyles)(((global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Left) 
            | global::System.Windows.Forms.AnchorStyles.Right)));
            this.typeArea.Location = new global::System.Drawing.Point(118, 9);
            this.typeArea.Multiline = true;
            this.typeArea.Name = "typeArea";
            this.typeArea.Size = new global::System.Drawing.Size(518, 76);
            this.typeArea.TabIndex = 1;
            // 
            // textBoxLog
            // 
            this.textBoxLog.Anchor = ((global::System.Windows.Forms.AnchorStyles)(((global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Left) 
            | global::System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxLog.BackColor = global::System.Drawing.Color.White;
            this.textBoxLog.BorderStyle = global::System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxLog.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 8.25F, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxLog.Location = new global::System.Drawing.Point(8, 91);
            this.textBoxLog.Multiline = true;
            this.textBoxLog.Name = "textBoxLog";
            this.textBoxLog.ReadOnly = true;
            this.textBoxLog.ScrollBars = global::System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxLog.Size = new global::System.Drawing.Size(636, 378);
            this.textBoxLog.TabIndex = 6;
            this.textBoxLog.WordWrap = false;
            // 
            // TestCommonWinFormsBehaviour
            // 
            this.AutoScaleDimensions = new global::System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new global::System.Drawing.Size(648, 457);
            this.Controls.Add(this.textBoxLog);
            this.Controls.Add(this.typeArea);
            this.Controls.Add(this.clickableArea);
            this.Name = "TestCommonWinFormsBehaviour";
            this.Text = "TestCommonWinFormsBehaviour";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private global::System.Windows.Forms.Label clickableArea;
        private global::System.Windows.Forms.TextBox typeArea;
        private global::System.Windows.Forms.TextBox textBoxLog;
    }
}