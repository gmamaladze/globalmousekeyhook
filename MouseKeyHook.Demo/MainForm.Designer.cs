namespace Gma.System.MouseKeyHook.Demo
{
    partial class MainForm
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
            this.button1 = new global::System.Windows.Forms.Button();
            this.button2 = new global::System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new global::System.Drawing.Point(13, 13);
            this.button1.Name = "button1";
            this.button1.Size = new global::System.Drawing.Size(116, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new global::System.Drawing.Point(13, 43);
            this.button2.Name = "button2";
            this.button2.Size = new global::System.Drawing.Size(116, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new global::System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new global::System.Drawing.Size(155, 96);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.ResumeLayout(false);

        }

        #endregion

        private global::System.Windows.Forms.Button button1;
        private global::System.Windows.Forms.Button button2;
    }
}