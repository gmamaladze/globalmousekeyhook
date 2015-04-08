using MouseKeyboardActivityMonitor.Controls;

namespace MouseKeyboardActivityMonitor.Demo
{
    partial class TestFormComponent
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
            this.textBoxLog = new System.Windows.Forms.TextBox();
            this.labelWheel = new System.Windows.Forms.Label();
            this.labelMousePosition = new System.Windows.Forms.Label();
            this.m_KeyMouseEventProvider1 = new MouseKeyEventProvider();
            this.SuspendLayout();
            // 
            // textBoxLog
            // 
            this.textBoxLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxLog.Location = new System.Drawing.Point(0, 0);
            this.textBoxLog.Multiline = true;
            this.textBoxLog.Name = "textBoxLog";
            this.textBoxLog.ReadOnly = true;
            this.textBoxLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxLog.Size = new System.Drawing.Size(404, 437);
            this.textBoxLog.TabIndex = 8;
            this.textBoxLog.WordWrap = false;
            // 
            // labelWheel
            // 
            this.labelWheel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelWheel.AutoSize = true;
            this.labelWheel.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelWheel.Location = new System.Drawing.Point(270, 25);
            this.labelWheel.Name = "labelWheel";
            this.labelWheel.Size = new System.Drawing.Size(106, 16);
            this.labelWheel.TabIndex = 9;
            this.labelWheel.Text = "Wheel={0:####}";
            // 
            // labelMousePosition
            // 
            this.labelMousePosition.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelMousePosition.AutoSize = true;
            this.labelMousePosition.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelMousePosition.Location = new System.Drawing.Point(214, 9);
            this.labelMousePosition.Name = "labelMousePosition";
            this.labelMousePosition.Size = new System.Drawing.Size(162, 16);
            this.labelMousePosition.TabIndex = 7;
            this.labelMousePosition.Text = "x={0:####}; y={1:####}";
            // 
            // m_KeyMouseEventProvider1
            // 
            this.m_KeyMouseEventProvider1.Enabled = true;
            this.m_KeyMouseEventProvider1.HookType = HookType.Global;
            this.m_KeyMouseEventProvider1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.HookManager_MouseMove);
            this.m_KeyMouseEventProvider1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.HookManager_MouseClick);
            this.m_KeyMouseEventProvider1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.HookManager_MouseDown);
            this.m_KeyMouseEventProvider1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.HookManager_MouseUp);
            this.m_KeyMouseEventProvider1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.HookManager_MouseDoubleClick);
            this.m_KeyMouseEventProvider1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.HookManager_KeyPress);
            this.m_KeyMouseEventProvider1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.HookManager_KeyUp);
            this.m_KeyMouseEventProvider1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.HookManager_KeyDown);
            // 
            // TestFormComponent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(404, 437);
            this.Controls.Add(this.labelWheel);
            this.Controls.Add(this.labelMousePosition);
            this.Controls.Add(this.textBoxLog);
            this.Name = "TestFormComponent";
            this.Text = "Test for the component GlobelEventProvider";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxLog;
        private System.Windows.Forms.Label labelWheel;
        private System.Windows.Forms.Label labelMousePosition;
        private MouseKeyEventProvider m_KeyMouseEventProvider1;
    }
}