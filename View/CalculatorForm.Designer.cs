namespace MyProjects.Calculator.View
{
    partial class CalculatorForm
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
            this.userControlCalculator1 = new MyProjects.Calculator.View.UserControlCalculator();
            this.SuspendLayout();
            // 
            // userControlCalculator1
            // 
            this.userControlCalculator1.Location = new System.Drawing.Point(4, 12);
            this.userControlCalculator1.MaximumSize = new System.Drawing.Size(261, 380);
            this.userControlCalculator1.MinimumSize = new System.Drawing.Size(261, 380);
            this.userControlCalculator1.Name = "userControlCalculator1";
            this.userControlCalculator1.Size = new System.Drawing.Size(261, 380);
            this.userControlCalculator1.TabIndex = 0;
            // 
            // CalculatorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(265, 391);
            this.Controls.Add(this.userControlCalculator1);
            this.KeyPreview = true;
            this.Name = "CalculatorForm";
            this.Text = "CalculatorForm";
            this.ResumeLayout(false);

        }

        #endregion

        private UserControlCalculator userControlCalculator1;

    }
}