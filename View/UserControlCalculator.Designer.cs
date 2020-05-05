namespace MyProjects.Calculator.View
{
    partial class UserControlCalculator
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
            this.inputView1 = new MyProjects.Calculator.View.InputView();
            this.showResultView1 = new MyProjects.Calculator.View.ShowResultView();
            this.SuspendLayout();
            // 
            // inputView1
            // 
            this.inputView1.Location = new System.Drawing.Point(3, 117);
            this.inputView1.MaximumSize = new System.Drawing.Size(256, 260);
            this.inputView1.MinimumSize = new System.Drawing.Size(256, 260);
            this.inputView1.Name = "inputView1";
            this.inputView1.Size = new System.Drawing.Size(256, 260);
            this.inputView1.TabIndex = 1;
            // 
            // showResultView1
            // 
            this.showResultView1.Location = new System.Drawing.Point(3, 3);
            this.showResultView1.MaximumSize = new System.Drawing.Size(256, 110);
            this.showResultView1.MinimumSize = new System.Drawing.Size(256, 110);
            this.showResultView1.Name = "showResultView1";
            this.showResultView1.Size = new System.Drawing.Size(256, 110);
            this.showResultView1.TabIndex = 0;
            // 
            // UserControlCalculator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.showResultView1);
            this.Controls.Add(this.inputView1);
            this.MaximumSize = new System.Drawing.Size(261, 380);
            this.MinimumSize = new System.Drawing.Size(261, 380);
            this.Name = "UserControlCalculator";
            this.Size = new System.Drawing.Size(261, 380);
            this.ResumeLayout(false);

        }

        #endregion

        private InputView inputView1;
        private ShowResultView showResultView1;
    }
}
