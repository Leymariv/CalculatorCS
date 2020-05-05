namespace MyProjects.Calculator.View
{
    partial class ShowResultView
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBoxOp = new System.Windows.Forms.TextBox();
            this.textBoxResult = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // textBoxOp
            // 
            this.textBoxOp.Location = new System.Drawing.Point(3, 89);
            this.textBoxOp.Name = "textBoxOp";
            this.textBoxOp.Size = new System.Drawing.Size(250, 20);
            this.textBoxOp.TabIndex = 0;
            this.textBoxOp.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBoxOp.Click += new System.EventHandler(this.TextBoxOpClick);
            this.textBoxOp.TextChanged += new System.EventHandler(this.TextBoxOpTextChanged);
            this.textBoxOp.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBoxOpKeyDown);
            // 
            // textBoxResult
            // 
            this.textBoxResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxResult.Location = new System.Drawing.Point(3, 3);
            this.textBoxResult.Name = "textBoxResult";
            this.textBoxResult.ReadOnly = true;
            this.textBoxResult.Size = new System.Drawing.Size(250, 62);
            this.textBoxResult.TabIndex = 1;
            // 
            // ShowResultView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.textBoxResult);
            this.Controls.Add(this.textBoxOp);
            this.MaximumSize = new System.Drawing.Size(256, 110);
            this.MinimumSize = new System.Drawing.Size(256, 110);
            this.Name = "ShowResultView";
            this.Size = new System.Drawing.Size(256, 110);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxOp;
        private System.Windows.Forms.TextBox textBoxResult;
    }
}
