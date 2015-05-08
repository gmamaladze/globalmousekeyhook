namespace Gma.System.MouseKeyHook.Demo
{
    partial class TestFormHookListeners {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private global::System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.labelMousePosition = new global::System.Windows.Forms.Label();
            this.textBoxLog = new global::System.Windows.Forms.TextBox();
            this.labelWheel = new global::System.Windows.Forms.Label();
            this.groupBox2 = new global::System.Windows.Forms.GroupBox();
            this.checkBoxSuppressMouse = new global::System.Windows.Forms.CheckBox();
            this.checkBoxEnabled = new global::System.Windows.Forms.CheckBox();
            this.panelSeparator = new global::System.Windows.Forms.Panel();
            this.radioGlobal = new global::System.Windows.Forms.RadioButton();
            this.radioApplication = new global::System.Windows.Forms.RadioButton();
            this.checkBoxKeyUp = new global::System.Windows.Forms.CheckBox();
            this.checkBoxKeyPress = new global::System.Windows.Forms.CheckBox();
            this.checkBoxKeyDown = new global::System.Windows.Forms.CheckBox();
            this.checkBoxMouseWheel = new global::System.Windows.Forms.CheckBox();
            this.checkBoxMouseDoubleClick = new global::System.Windows.Forms.CheckBox();
            this.checkBoxOnMouseUp = new global::System.Windows.Forms.CheckBox();
            this.checkBoxOnMouseDown = new global::System.Windows.Forms.CheckBox();
            this.checkBoxOnMouseClick = new global::System.Windows.Forms.CheckBox();
            this.checkBoxOnMouseMove = new global::System.Windows.Forms.CheckBox();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelMousePosition
            // 
            this.labelMousePosition.Anchor = ((global::System.Windows.Forms.AnchorStyles)((global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Right)));
            this.labelMousePosition.AutoSize = true;
            this.labelMousePosition.BackColor = global::System.Drawing.Color.White;
            this.labelMousePosition.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 8.25F, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelMousePosition.Location = new global::System.Drawing.Point(392, 59);
            this.labelMousePosition.Name = "labelMousePosition";
            this.labelMousePosition.Size = new global::System.Drawing.Size(125, 13);
            this.labelMousePosition.TabIndex = 2;
            this.labelMousePosition.Text = "x={0:####}; y={1:####}";
            // 
            // textBoxLog
            // 
            this.textBoxLog.BackColor = global::System.Drawing.Color.White;
            this.textBoxLog.BorderStyle = global::System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxLog.Dock = global::System.Windows.Forms.DockStyle.Fill;
            this.textBoxLog.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 8.25F, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxLog.Location = new global::System.Drawing.Point(0, 212);
            this.textBoxLog.Multiline = true;
            this.textBoxLog.Name = "textBoxLog";
            this.textBoxLog.ReadOnly = true;
            this.textBoxLog.ScrollBars = global::System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxLog.Size = new global::System.Drawing.Size(560, 241);
            this.textBoxLog.TabIndex = 5;
            this.textBoxLog.WordWrap = false;
            // 
            // labelWheel
            // 
            this.labelWheel.Anchor = ((global::System.Windows.Forms.AnchorStyles)((global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Right)));
            this.labelWheel.AutoSize = true;
            this.labelWheel.BackColor = global::System.Drawing.Color.White;
            this.labelWheel.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 8.25F, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelWheel.Location = new global::System.Drawing.Point(392, 79);
            this.labelWheel.Name = "labelWheel";
            this.labelWheel.Size = new global::System.Drawing.Size(89, 13);
            this.labelWheel.TabIndex = 6;
            this.labelWheel.Text = "Wheel={0:####}";
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = global::System.Drawing.Color.White;
            this.groupBox2.Controls.Add(this.checkBoxSuppressMouse);
            this.groupBox2.Controls.Add(this.checkBoxEnabled);
            this.groupBox2.Controls.Add(this.panelSeparator);
            this.groupBox2.Controls.Add(this.radioGlobal);
            this.groupBox2.Controls.Add(this.radioApplication);
            this.groupBox2.Controls.Add(this.checkBoxKeyUp);
            this.groupBox2.Controls.Add(this.labelWheel);
            this.groupBox2.Controls.Add(this.checkBoxKeyPress);
            this.groupBox2.Controls.Add(this.labelMousePosition);
            this.groupBox2.Controls.Add(this.checkBoxKeyDown);
            this.groupBox2.Controls.Add(this.checkBoxMouseWheel);
            this.groupBox2.Controls.Add(this.checkBoxMouseDoubleClick);
            this.groupBox2.Controls.Add(this.checkBoxOnMouseUp);
            this.groupBox2.Controls.Add(this.checkBoxOnMouseDown);
            this.groupBox2.Controls.Add(this.checkBoxOnMouseClick);
            this.groupBox2.Controls.Add(this.checkBoxOnMouseMove);
            this.groupBox2.Dock = global::System.Windows.Forms.DockStyle.Top;
            this.groupBox2.FlatStyle = global::System.Windows.Forms.FlatStyle.Flat;
            this.groupBox2.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 8.25F, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new global::System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new global::System.Drawing.Size(560, 212);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            // 
            // checkBoxSuppressMouse
            // 
            this.checkBoxSuppressMouse.AutoSize = true;
            this.checkBoxSuppressMouse.Location = new global::System.Drawing.Point(395, 105);
            this.checkBoxSuppressMouse.Name = "checkBoxSuppressMouse";
            this.checkBoxSuppressMouse.Size = new global::System.Drawing.Size(159, 17);
            this.checkBoxSuppressMouse.TabIndex = 13;
            this.checkBoxSuppressMouse.Text = "Suppress Right Mouse Click";
            this.checkBoxSuppressMouse.UseVisualStyleBackColor = true;
            this.checkBoxSuppressMouse.CheckedChanged += new global::System.EventHandler(this.checkBoxSuppressMouse_CheckedChanged);
            // 
            // checkBoxEnabled
            // 
            this.checkBoxEnabled.AutoSize = true;
            this.checkBoxEnabled.Checked = true;
            this.checkBoxEnabled.CheckState = global::System.Windows.Forms.CheckState.Checked;
            this.checkBoxEnabled.Location = new global::System.Drawing.Point(233, 14);
            this.checkBoxEnabled.Name = "checkBoxEnabled";
            this.checkBoxEnabled.Size = new global::System.Drawing.Size(65, 17);
            this.checkBoxEnabled.TabIndex = 12;
            this.checkBoxEnabled.Text = "Enabled";
            this.checkBoxEnabled.UseVisualStyleBackColor = true;
            // 
            // panelSeparator
            // 
            this.panelSeparator.Anchor = ((global::System.Windows.Forms.AnchorStyles)(((global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Left) 
            | global::System.Windows.Forms.AnchorStyles.Right)));
            this.panelSeparator.BackColor = global::System.Drawing.Color.White;
            this.panelSeparator.BorderStyle = global::System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelSeparator.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 8.25F, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panelSeparator.Location = new global::System.Drawing.Point(6, 37);
            this.panelSeparator.Name = "panelSeparator";
            this.panelSeparator.Size = new global::System.Drawing.Size(542, 1);
            this.panelSeparator.TabIndex = 11;
            // 
            // radioGlobal
            // 
            this.radioGlobal.AutoSize = true;
            this.radioGlobal.BackColor = global::System.Drawing.Color.White;
            this.radioGlobal.Checked = true;
            this.radioGlobal.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 8.25F, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioGlobal.Location = new global::System.Drawing.Point(128, 13);
            this.radioGlobal.Name = "radioGlobal";
            this.radioGlobal.Size = new global::System.Drawing.Size(87, 17);
            this.radioGlobal.TabIndex = 10;
            this.radioGlobal.TabStop = true;
            this.radioGlobal.Text = "Global hooks";
            this.radioGlobal.UseVisualStyleBackColor = false;
            // 
            // radioApplication
            // 
            this.radioApplication.AutoSize = true;
            this.radioApplication.BackColor = global::System.Drawing.Color.White;
            this.radioApplication.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 8.25F, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioApplication.Location = new global::System.Drawing.Point(12, 13);
            this.radioApplication.Name = "radioApplication";
            this.radioApplication.Size = new global::System.Drawing.Size(109, 17);
            this.radioApplication.TabIndex = 9;
            this.radioApplication.Text = "Application hooks";
            this.radioApplication.UseVisualStyleBackColor = false;
            // 
            // checkBoxKeyUp
            // 
            this.checkBoxKeyUp.AutoSize = true;
            this.checkBoxKeyUp.BackColor = global::System.Drawing.Color.White;
            this.checkBoxKeyUp.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 8.25F, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxKeyUp.Location = new global::System.Drawing.Point(189, 105);
            this.checkBoxKeyUp.Name = "checkBoxKeyUp";
            this.checkBoxKeyUp.Size = new global::System.Drawing.Size(58, 17);
            this.checkBoxKeyUp.TabIndex = 8;
            this.checkBoxKeyUp.Text = "KeyUp";
            this.checkBoxKeyUp.UseVisualStyleBackColor = false;
            this.checkBoxKeyUp.CheckedChanged += new global::System.EventHandler(this.checkBoxKeyUp_CheckedChanged);
            // 
            // checkBoxKeyPress
            // 
            this.checkBoxKeyPress.AutoSize = true;
            this.checkBoxKeyPress.BackColor = global::System.Drawing.Color.White;
            this.checkBoxKeyPress.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 8.25F, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxKeyPress.Location = new global::System.Drawing.Point(189, 81);
            this.checkBoxKeyPress.Name = "checkBoxKeyPress";
            this.checkBoxKeyPress.Size = new global::System.Drawing.Size(70, 17);
            this.checkBoxKeyPress.TabIndex = 7;
            this.checkBoxKeyPress.Text = "KeyPress";
            this.checkBoxKeyPress.UseVisualStyleBackColor = false;
            this.checkBoxKeyPress.CheckedChanged += new global::System.EventHandler(this.checkBoxKeyPress_CheckedChanged);
            // 
            // checkBoxKeyDown
            // 
            this.checkBoxKeyDown.AutoSize = true;
            this.checkBoxKeyDown.BackColor = global::System.Drawing.Color.White;
            this.checkBoxKeyDown.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 8.25F, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxKeyDown.Location = new global::System.Drawing.Point(189, 58);
            this.checkBoxKeyDown.Name = "checkBoxKeyDown";
            this.checkBoxKeyDown.Size = new global::System.Drawing.Size(72, 17);
            this.checkBoxKeyDown.TabIndex = 6;
            this.checkBoxKeyDown.Text = "KeyDown";
            this.checkBoxKeyDown.UseVisualStyleBackColor = false;
            this.checkBoxKeyDown.CheckedChanged += new global::System.EventHandler(this.checkBoxKeyDown_CheckedChanged);
            // 
            // checkBoxMouseWheel
            // 
            this.checkBoxMouseWheel.AutoSize = true;
            this.checkBoxMouseWheel.BackColor = global::System.Drawing.Color.White;
            this.checkBoxMouseWheel.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 8.25F, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxMouseWheel.Location = new global::System.Drawing.Point(12, 174);
            this.checkBoxMouseWheel.Name = "checkBoxMouseWheel";
            this.checkBoxMouseWheel.Size = new global::System.Drawing.Size(89, 17);
            this.checkBoxMouseWheel.TabIndex = 5;
            this.checkBoxMouseWheel.Text = "MouseWheel";
            this.checkBoxMouseWheel.UseVisualStyleBackColor = false;
            this.checkBoxMouseWheel.CheckedChanged += new global::System.EventHandler(this.checkBoxMouseWheel_CheckedChanged);
            // 
            // checkBoxMouseDoubleClick
            // 
            this.checkBoxMouseDoubleClick.AutoSize = true;
            this.checkBoxMouseDoubleClick.BackColor = global::System.Drawing.Color.White;
            this.checkBoxMouseDoubleClick.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 8.25F, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxMouseDoubleClick.Location = new global::System.Drawing.Point(12, 150);
            this.checkBoxMouseDoubleClick.Name = "checkBoxMouseDoubleClick";
            this.checkBoxMouseDoubleClick.Size = new global::System.Drawing.Size(115, 17);
            this.checkBoxMouseDoubleClick.TabIndex = 4;
            this.checkBoxMouseDoubleClick.Text = "MouseDoubleClick";
            this.checkBoxMouseDoubleClick.UseVisualStyleBackColor = false;
            this.checkBoxMouseDoubleClick.CheckedChanged += new global::System.EventHandler(this.checkBoxMouseDoubleClick_CheckedChanged);
            // 
            // checkBoxOnMouseUp
            // 
            this.checkBoxOnMouseUp.AutoSize = true;
            this.checkBoxOnMouseUp.BackColor = global::System.Drawing.Color.White;
            this.checkBoxOnMouseUp.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 8.25F, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxOnMouseUp.Location = new global::System.Drawing.Point(12, 129);
            this.checkBoxOnMouseUp.Name = "checkBoxOnMouseUp";
            this.checkBoxOnMouseUp.Size = new global::System.Drawing.Size(72, 17);
            this.checkBoxOnMouseUp.TabIndex = 3;
            this.checkBoxOnMouseUp.Text = "MouseUp";
            this.checkBoxOnMouseUp.UseVisualStyleBackColor = false;
            this.checkBoxOnMouseUp.CheckedChanged += new global::System.EventHandler(this.checkBoxOnMouseUp_CheckedChanged);
            // 
            // checkBoxOnMouseDown
            // 
            this.checkBoxOnMouseDown.AutoSize = true;
            this.checkBoxOnMouseDown.BackColor = global::System.Drawing.Color.White;
            this.checkBoxOnMouseDown.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 8.25F, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxOnMouseDown.Location = new global::System.Drawing.Point(12, 105);
            this.checkBoxOnMouseDown.Name = "checkBoxOnMouseDown";
            this.checkBoxOnMouseDown.Size = new global::System.Drawing.Size(86, 17);
            this.checkBoxOnMouseDown.TabIndex = 2;
            this.checkBoxOnMouseDown.Text = "MouseDown";
            this.checkBoxOnMouseDown.UseVisualStyleBackColor = false;
            this.checkBoxOnMouseDown.CheckedChanged += new global::System.EventHandler(this.checkBoxOnMouseDown_CheckedChanged);
            // 
            // checkBoxOnMouseClick
            // 
            this.checkBoxOnMouseClick.AutoSize = true;
            this.checkBoxOnMouseClick.BackColor = global::System.Drawing.Color.White;
            this.checkBoxOnMouseClick.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 8.25F, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxOnMouseClick.Location = new global::System.Drawing.Point(12, 81);
            this.checkBoxOnMouseClick.Name = "checkBoxOnMouseClick";
            this.checkBoxOnMouseClick.Size = new global::System.Drawing.Size(81, 17);
            this.checkBoxOnMouseClick.TabIndex = 1;
            this.checkBoxOnMouseClick.Text = "MouseClick";
            this.checkBoxOnMouseClick.UseVisualStyleBackColor = false;
            // 
            // checkBoxOnMouseMove
            // 
            this.checkBoxOnMouseMove.AutoSize = true;
            this.checkBoxOnMouseMove.BackColor = global::System.Drawing.Color.White;
            this.checkBoxOnMouseMove.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 8.25F, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxOnMouseMove.Location = new global::System.Drawing.Point(12, 58);
            this.checkBoxOnMouseMove.Name = "checkBoxOnMouseMove";
            this.checkBoxOnMouseMove.Size = new global::System.Drawing.Size(85, 17);
            this.checkBoxOnMouseMove.TabIndex = 0;
            this.checkBoxOnMouseMove.Text = "MouseMove";
            this.checkBoxOnMouseMove.UseVisualStyleBackColor = false;
            this.checkBoxOnMouseMove.CheckedChanged += new global::System.EventHandler(this.checkBoxOnMouseMove_CheckedChanged);
            // 
            // TestFormHookListeners
            // 
            this.AutoScaleDimensions = new global::System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new global::System.Drawing.Size(560, 453);
            this.Controls.Add(this.textBoxLog);
            this.Controls.Add(this.groupBox2);
            this.Name = "TestFormHookListeners";
            this.Text = "Test for the class HookManager";
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private global::System.Windows.Forms.Label labelMousePosition;
        private global::System.Windows.Forms.TextBox textBoxLog;
        private global::System.Windows.Forms.Label labelWheel;
        private global::System.Windows.Forms.GroupBox groupBox2;
        private global::System.Windows.Forms.CheckBox checkBoxKeyUp;
        private global::System.Windows.Forms.CheckBox checkBoxKeyPress;
        private global::System.Windows.Forms.CheckBox checkBoxKeyDown;
        private global::System.Windows.Forms.CheckBox checkBoxMouseWheel;
        private global::System.Windows.Forms.CheckBox checkBoxMouseDoubleClick;
        private global::System.Windows.Forms.CheckBox checkBoxOnMouseUp;
        private global::System.Windows.Forms.CheckBox checkBoxOnMouseDown;
        private global::System.Windows.Forms.CheckBox checkBoxOnMouseClick;
        private global::System.Windows.Forms.CheckBox checkBoxOnMouseMove;
        private global::System.Windows.Forms.RadioButton radioApplication;
        private global::System.Windows.Forms.Panel panelSeparator;
        private global::System.Windows.Forms.RadioButton radioGlobal;
        private global::System.Windows.Forms.CheckBox checkBoxEnabled;
        private global::System.Windows.Forms.CheckBox checkBoxSuppressMouse;
    }
}