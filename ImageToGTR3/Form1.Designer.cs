
namespace ImageToGTR3
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.button_TgaToPng = new System.Windows.Forms.Button();
            this.button_PngToTga = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // button_TgaToPng
            // 
            resources.ApplyResources(this.button_TgaToPng, "button_TgaToPng");
            this.button_TgaToPng.Name = "button_TgaToPng";
            this.button_TgaToPng.UseVisualStyleBackColor = true;
            this.button_TgaToPng.Click += new System.EventHandler(this.button_TgaToPng_Click);
            // 
            // button_PngToTga
            // 
            resources.ApplyResources(this.button_PngToTga, "button_PngToTga");
            this.button_PngToTga.Name = "button_PngToTga";
            this.button_PngToTga.UseVisualStyleBackColor = true;
            this.button_PngToTga.Click += new System.EventHandler(this.button_PngToTga_Click);
            // 
            // progressBar1
            // 
            resources.ApplyResources(this.progressBar1, "progressBar1");
            this.progressBar1.Name = "progressBar1";
            // 
            // Form1
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.button_PngToTga);
            this.Controls.Add(this.button_TgaToPng);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button_TgaToPng;
        private System.Windows.Forms.Button button_PngToTga;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}

