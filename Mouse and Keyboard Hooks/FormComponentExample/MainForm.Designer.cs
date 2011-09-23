namespace FormComponentExample
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
            this.labelPosition = new System.Windows.Forms.Label();
            this.labelDelta = new System.Windows.Forms.Label();
            this.inputEventsProvider = new MouseKeyboardActivityMonitor.Controls.MouseKeyEventProvider();
            this.txtOutput = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // labelPosition
            // 
            this.labelPosition.AutoSize = true;
            this.labelPosition.Location = new System.Drawing.Point(367, 9);
            this.labelPosition.Name = "labelPosition";
            this.labelPosition.Size = new System.Drawing.Size(79, 13);
            this.labelPosition.TabIndex = 1;
            this.labelPosition.Text = "Mouse Position";
            this.labelPosition.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelDelta
            // 
            this.labelDelta.AutoSize = true;
            this.labelDelta.Location = new System.Drawing.Point(367, 22);
            this.labelDelta.Name = "labelDelta";
            this.labelDelta.Size = new System.Drawing.Size(67, 13);
            this.labelDelta.TabIndex = 2;
            this.labelDelta.Text = "Mouse Delta";
            // 
            // inputEventsProvider
            // 
            this.inputEventsProvider.Enabled = true;
            this.inputEventsProvider.HookType = MouseKeyboardActivityMonitor.Controls.HookType.Global;
            this.inputEventsProvider.MouseUp += new System.Windows.Forms.MouseEventHandler(this.inputEventsProvider_MouseUp);
            this.inputEventsProvider.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.inputEventsProvider_MouseDoubleClick);
            this.inputEventsProvider.MouseMoveExt += new System.EventHandler<MouseKeyboardActivityMonitor.MouseEventExtArgs>(this.inputEventsProvider_MouseMoveExt);
            this.inputEventsProvider.MouseClickExt += new System.EventHandler<MouseKeyboardActivityMonitor.MouseEventExtArgs>(this.inputEventsProvider_MouseClickExt);
            this.inputEventsProvider.MouseDownExt += new System.EventHandler<MouseKeyboardActivityMonitor.MouseEventExtArgs>(this.inputEventsProvider_MouseDownExt);
            this.inputEventsProvider.MouseWheel += new System.EventHandler<System.Windows.Forms.MouseEventArgs>(this.inputEventsProvider_MouseWheel);
            this.inputEventsProvider.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.inputEventsProvider_KeyPress);
            this.inputEventsProvider.KeyUp += new System.Windows.Forms.KeyEventHandler(this.inputEventsProvider_KeyUp);
            this.inputEventsProvider.KeyDown += new System.Windows.Forms.KeyEventHandler(this.inputEventsProvider_KeyDown);
            // 
            // txtOutput
            // 
            this.txtOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtOutput.Location = new System.Drawing.Point(0, 0);
            this.txtOutput.Multiline = true;
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.ReadOnly = true;
            this.txtOutput.Size = new System.Drawing.Size(458, 342);
            this.txtOutput.TabIndex = 3;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(458, 342);
            this.Controls.Add(this.labelDelta);
            this.Controls.Add(this.labelPosition);
            this.Controls.Add(this.txtOutput);
            this.Name = "MainForm";
            this.Text = "Mouse and Keyboard Hooks FormComponent Example";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MouseKeyboardActivityMonitor.Controls.MouseKeyEventProvider inputEventsProvider;
        private System.Windows.Forms.Label labelPosition;
        private System.Windows.Forms.Label labelDelta;
        private System.Windows.Forms.TextBox txtOutput;
    }
}

