namespace SpaceSharp
{
    partial class MainWindow
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tmrTick = new System.Windows.Forms.Timer(this.components);
            this.tmrComet = new System.Windows.Forms.Timer(this.components);
            this.tmrSupernova = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // tmrTick
            // 
            this.tmrTick.Enabled = true;
            this.tmrTick.Interval = 20;
            this.tmrTick.Tick += new System.EventHandler(this.tmrTick_Tick);
            // 
            // tmrComet
            // 
            this.tmrComet.Enabled = true;
            this.tmrComet.Interval = 3000;
            this.tmrComet.Tick += new System.EventHandler(this.tmrComet_Tick);
            // 
            // tmrSupernova
            // 
            this.tmrSupernova.Enabled = true;
            this.tmrSupernova.Interval = 1500;
            this.tmrSupernova.Tick += new System.EventHandler(this.tmrSupernova_Tick);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "MainWindow";
            this.Text = "mmm";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.MainWindow_Paint);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer tmrTick;
        private System.Windows.Forms.Timer tmrComet;
        private System.Windows.Forms.Timer tmrSupernova;
    }
}

