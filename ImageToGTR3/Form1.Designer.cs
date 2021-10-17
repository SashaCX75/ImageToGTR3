
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
            this.button_TgaToPng = new System.Windows.Forms.Button();
            this.checkBox_RGB_BRG = new System.Windows.Forms.CheckBox();
            this.button_PngToTga = new System.Windows.Forms.Button();
            this.button_ImageFix = new System.Windows.Forms.Button();
            this.checkBox_Color256 = new System.Windows.Forms.CheckBox();
            this.checkBox_32bit = new System.Windows.Forms.CheckBox();
            this.checkBox_ARGB_BGRA = new System.Windows.Forms.CheckBox();
            this.checkBox_ImageIDLength46 = new System.Windows.Forms.CheckBox();
            this.groupBox_alphaChenel = new System.Windows.Forms.GroupBox();
            this.radioButton_LastСolor = new System.Windows.Forms.RadioButton();
            this.radioButton_FirstСolor = new System.Windows.Forms.RadioButton();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.radioButton_BAlpha = new System.Windows.Forms.RadioButton();
            this.radioButton_GAlpha = new System.Windows.Forms.RadioButton();
            this.radioButton_RAlpha = new System.Windows.Forms.RadioButton();
            this.radioButton_BlackAlpha = new System.Windows.Forms.RadioButton();
            this.radioButton_NoAlpha = new System.Windows.Forms.RadioButton();
            this.checkBox_Footer = new System.Windows.Forms.CheckBox();
            this.checkBox_RestoreFromTxt = new System.Windows.Forms.CheckBox();
            this.groupBox_alphaChenel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // button_TgaToPng
            // 
            this.button_TgaToPng.Location = new System.Drawing.Point(12, 12);
            this.button_TgaToPng.Name = "button_TgaToPng";
            this.button_TgaToPng.Size = new System.Drawing.Size(275, 23);
            this.button_TgaToPng.TabIndex = 0;
            this.button_TgaToPng.Text = "Распознать изображение из циферблата";
            this.button_TgaToPng.UseVisualStyleBackColor = true;
            this.button_TgaToPng.Click += new System.EventHandler(this.button_TgaToPng_Click);
            // 
            // checkBox_RGB_BRG
            // 
            this.checkBox_RGB_BRG.AutoSize = true;
            this.checkBox_RGB_BRG.Checked = true;
            this.checkBox_RGB_BRG.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_RGB_BRG.Location = new System.Drawing.Point(12, 41);
            this.checkBox_RGB_BRG.Name = "checkBox_RGB_BRG";
            this.checkBox_RGB_BRG.Size = new System.Drawing.Size(84, 17);
            this.checkBox_RGB_BRG.TabIndex = 1;
            this.checkBox_RGB_BRG.Text = "RGB > BRG";
            this.checkBox_RGB_BRG.UseVisualStyleBackColor = true;
            // 
            // button_PngToTga
            // 
            this.button_PngToTga.Location = new System.Drawing.Point(12, 80);
            this.button_PngToTga.Name = "button_PngToTga";
            this.button_PngToTga.Size = new System.Drawing.Size(275, 23);
            this.button_PngToTga.TabIndex = 2;
            this.button_PngToTga.Text = "Конвертировать изображение в формат TGA";
            this.button_PngToTga.UseVisualStyleBackColor = true;
            this.button_PngToTga.Click += new System.EventHandler(this.button_PngToTga_Click);
            // 
            // button_ImageFix
            // 
            this.button_ImageFix.Location = new System.Drawing.Point(12, 134);
            this.button_ImageFix.Name = "button_ImageFix";
            this.button_ImageFix.Size = new System.Drawing.Size(275, 23);
            this.button_ImageFix.TabIndex = 3;
            this.button_ImageFix.Text = "Исправить изображение";
            this.button_ImageFix.UseVisualStyleBackColor = true;
            this.button_ImageFix.Click += new System.EventHandler(this.button_ImageFix_Click);
            // 
            // checkBox_Color256
            // 
            this.checkBox_Color256.AutoSize = true;
            this.checkBox_Color256.Checked = true;
            this.checkBox_Color256.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_Color256.Location = new System.Drawing.Point(12, 163);
            this.checkBox_Color256.Name = "checkBox_Color256";
            this.checkBox_Color256.Size = new System.Drawing.Size(135, 17);
            this.checkBox_Color256.TabIndex = 4;
            this.checkBox_Color256.Text = "256 цветов в палитре";
            this.checkBox_Color256.UseVisualStyleBackColor = true;
            // 
            // checkBox_32bit
            // 
            this.checkBox_32bit.AutoSize = true;
            this.checkBox_32bit.Checked = true;
            this.checkBox_32bit.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_32bit.Location = new System.Drawing.Point(12, 232);
            this.checkBox_32bit.Name = "checkBox_32bit";
            this.checkBox_32bit.Size = new System.Drawing.Size(84, 17);
            this.checkBox_32bit.TabIndex = 5;
            this.checkBox_32bit.Text = "24bit > 32bit";
            this.checkBox_32bit.UseVisualStyleBackColor = true;
            this.checkBox_32bit.CheckedChanged += new System.EventHandler(this.checkBox_32bit_CheckedChanged);
            // 
            // checkBox_ARGB_BGRA
            // 
            this.checkBox_ARGB_BGRA.AutoSize = true;
            this.checkBox_ARGB_BGRA.Checked = true;
            this.checkBox_ARGB_BGRA.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_ARGB_BGRA.Location = new System.Drawing.Point(12, 278);
            this.checkBox_ARGB_BGRA.Name = "checkBox_ARGB_BGRA";
            this.checkBox_ARGB_BGRA.Size = new System.Drawing.Size(98, 17);
            this.checkBox_ARGB_BGRA.TabIndex = 6;
            this.checkBox_ARGB_BGRA.Text = "ARGB > BGRA";
            this.checkBox_ARGB_BGRA.UseVisualStyleBackColor = true;
            // 
            // checkBox_ImageIDLength46
            // 
            this.checkBox_ImageIDLength46.AutoSize = true;
            this.checkBox_ImageIDLength46.Location = new System.Drawing.Point(12, 186);
            this.checkBox_ImageIDLength46.Name = "checkBox_ImageIDLength46";
            this.checkBox_ImageIDLength46.Size = new System.Drawing.Size(130, 17);
            this.checkBox_ImageIDLength46.TabIndex = 7;
            this.checkBox_ImageIDLength46.Text = "Длина заголовка 46";
            this.checkBox_ImageIDLength46.UseVisualStyleBackColor = true;
            // 
            // groupBox_alphaChenel
            // 
            this.groupBox_alphaChenel.Controls.Add(this.radioButton_LastСolor);
            this.groupBox_alphaChenel.Controls.Add(this.radioButton_FirstСolor);
            this.groupBox_alphaChenel.Controls.Add(this.numericUpDown1);
            this.groupBox_alphaChenel.Controls.Add(this.label1);
            this.groupBox_alphaChenel.Controls.Add(this.radioButton_BAlpha);
            this.groupBox_alphaChenel.Controls.Add(this.radioButton_GAlpha);
            this.groupBox_alphaChenel.Controls.Add(this.radioButton_RAlpha);
            this.groupBox_alphaChenel.Controls.Add(this.radioButton_BlackAlpha);
            this.groupBox_alphaChenel.Controls.Add(this.radioButton_NoAlpha);
            this.groupBox_alphaChenel.Location = new System.Drawing.Point(12, 301);
            this.groupBox_alphaChenel.Name = "groupBox_alphaChenel";
            this.groupBox_alphaChenel.Size = new System.Drawing.Size(275, 138);
            this.groupBox_alphaChenel.TabIndex = 8;
            this.groupBox_alphaChenel.TabStop = false;
            this.groupBox_alphaChenel.Text = "Способ создания алфа-канала";
            this.groupBox_alphaChenel.Paint += new System.Windows.Forms.PaintEventHandler(this.groupBox_Paint);
            // 
            // radioButton_LastСolor
            // 
            this.radioButton_LastСolor.AutoSize = true;
            this.radioButton_LastСolor.Location = new System.Drawing.Point(6, 111);
            this.radioButton_LastСolor.Name = "radioButton_LastСolor";
            this.radioButton_LastСolor.Size = new System.Drawing.Size(168, 17);
            this.radioButton_LastСolor.TabIndex = 8;
            this.radioButton_LastСolor.Text = "Последний цвет из палитры";
            this.radioButton_LastСolor.UseVisualStyleBackColor = true;
            // 
            // radioButton_FirstСolor
            // 
            this.radioButton_FirstСolor.AutoSize = true;
            this.radioButton_FirstСolor.Location = new System.Drawing.Point(6, 88);
            this.radioButton_FirstСolor.Name = "radioButton_FirstСolor";
            this.radioButton_FirstСolor.Size = new System.Drawing.Size(152, 17);
            this.radioButton_FirstСolor.TabIndex = 7;
            this.radioButton_FirstСolor.Text = "Первый цвет из палитры";
            this.radioButton_FirstСolor.UseVisualStyleBackColor = true;
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(215, 42);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(53, 20);
            this.numericUpDown1.TabIndex = 6;
            this.numericUpDown1.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(134, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Погрешность";
            // 
            // radioButton_BAlpha
            // 
            this.radioButton_BAlpha.AutoSize = true;
            this.radioButton_BAlpha.Location = new System.Drawing.Point(84, 65);
            this.radioButton_BAlpha.Name = "radioButton_BAlpha";
            this.radioButton_BAlpha.Size = new System.Drawing.Size(32, 17);
            this.radioButton_BAlpha.TabIndex = 4;
            this.radioButton_BAlpha.Text = "B";
            this.radioButton_BAlpha.UseVisualStyleBackColor = true;
            // 
            // radioButton_GAlpha
            // 
            this.radioButton_GAlpha.AutoSize = true;
            this.radioButton_GAlpha.Location = new System.Drawing.Point(45, 65);
            this.radioButton_GAlpha.Name = "radioButton_GAlpha";
            this.radioButton_GAlpha.Size = new System.Drawing.Size(33, 17);
            this.radioButton_GAlpha.TabIndex = 3;
            this.radioButton_GAlpha.Text = "G";
            this.radioButton_GAlpha.UseVisualStyleBackColor = true;
            // 
            // radioButton_RAlpha
            // 
            this.radioButton_RAlpha.AutoSize = true;
            this.radioButton_RAlpha.Location = new System.Drawing.Point(6, 65);
            this.radioButton_RAlpha.Name = "radioButton_RAlpha";
            this.radioButton_RAlpha.Size = new System.Drawing.Size(33, 17);
            this.radioButton_RAlpha.TabIndex = 2;
            this.radioButton_RAlpha.Text = "R";
            this.radioButton_RAlpha.UseVisualStyleBackColor = true;
            // 
            // radioButton_BlackAlpha
            // 
            this.radioButton_BlackAlpha.AutoSize = true;
            this.radioButton_BlackAlpha.Location = new System.Drawing.Point(6, 42);
            this.radioButton_BlackAlpha.Name = "radioButton_BlackAlpha";
            this.radioButton_BlackAlpha.Size = new System.Drawing.Size(91, 17);
            this.radioButton_BlackAlpha.TabIndex = 1;
            this.radioButton_BlackAlpha.Text = "Черный цвет";
            this.radioButton_BlackAlpha.UseVisualStyleBackColor = true;
            this.radioButton_BlackAlpha.CheckedChanged += new System.EventHandler(this.radioButton_BlackAlpha_CheckedChanged);
            // 
            // radioButton_NoAlpha
            // 
            this.radioButton_NoAlpha.AutoSize = true;
            this.radioButton_NoAlpha.Checked = true;
            this.radioButton_NoAlpha.Location = new System.Drawing.Point(6, 19);
            this.radioButton_NoAlpha.Name = "radioButton_NoAlpha";
            this.radioButton_NoAlpha.Size = new System.Drawing.Size(95, 17);
            this.radioButton_NoAlpha.TabIndex = 0;
            this.radioButton_NoAlpha.TabStop = true;
            this.radioButton_NoAlpha.Text = "Не создавать";
            this.radioButton_NoAlpha.UseVisualStyleBackColor = true;
            // 
            // checkBox_Footer
            // 
            this.checkBox_Footer.AutoSize = true;
            this.checkBox_Footer.Location = new System.Drawing.Point(12, 209);
            this.checkBox_Footer.Name = "checkBox_Footer";
            this.checkBox_Footer.Size = new System.Drawing.Size(129, 17);
            this.checkBox_Footer.TabIndex = 9;
            this.checkBox_Footer.Text = "Добавлять FOOTER";
            this.checkBox_Footer.UseVisualStyleBackColor = true;
            // 
            // checkBox_RestoreFromTxt
            // 
            this.checkBox_RestoreFromTxt.AutoSize = true;
            this.checkBox_RestoreFromTxt.Checked = true;
            this.checkBox_RestoreFromTxt.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_RestoreFromTxt.Location = new System.Drawing.Point(12, 255);
            this.checkBox_RestoreFromTxt.Name = "checkBox_RestoreFromTxt";
            this.checkBox_RestoreFromTxt.Size = new System.Drawing.Size(233, 17);
            this.checkBox_RestoreFromTxt.TabIndex = 10;
            this.checkBox_RestoreFromTxt.Text = "Востановить цвета из текстового файла";
            this.checkBox_RestoreFromTxt.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 451);
            this.Controls.Add(this.checkBox_RestoreFromTxt);
            this.Controls.Add(this.checkBox_Footer);
            this.Controls.Add(this.groupBox_alphaChenel);
            this.Controls.Add(this.checkBox_ImageIDLength46);
            this.Controls.Add(this.checkBox_ARGB_BGRA);
            this.Controls.Add(this.checkBox_32bit);
            this.Controls.Add(this.checkBox_Color256);
            this.Controls.Add(this.button_ImageFix);
            this.Controls.Add(this.button_PngToTga);
            this.Controls.Add(this.checkBox_RGB_BRG);
            this.Controls.Add(this.button_TgaToPng);
            this.Name = "Form1";
            this.Text = "ImageToGTR3";
            this.groupBox_alphaChenel.ResumeLayout(false);
            this.groupBox_alphaChenel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_TgaToPng;
        private System.Windows.Forms.CheckBox checkBox_RGB_BRG;
        private System.Windows.Forms.Button button_PngToTga;
        private System.Windows.Forms.Button button_ImageFix;
        private System.Windows.Forms.CheckBox checkBox_Color256;
        private System.Windows.Forms.CheckBox checkBox_32bit;
        private System.Windows.Forms.CheckBox checkBox_ARGB_BGRA;
        private System.Windows.Forms.CheckBox checkBox_ImageIDLength46;
        private System.Windows.Forms.GroupBox groupBox_alphaChenel;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton radioButton_BAlpha;
        private System.Windows.Forms.RadioButton radioButton_GAlpha;
        private System.Windows.Forms.RadioButton radioButton_RAlpha;
        private System.Windows.Forms.RadioButton radioButton_BlackAlpha;
        private System.Windows.Forms.RadioButton radioButton_NoAlpha;
        private System.Windows.Forms.CheckBox checkBox_Footer;
        private System.Windows.Forms.RadioButton radioButton_LastСolor;
        private System.Windows.Forms.RadioButton radioButton_FirstСolor;
        private System.Windows.Forms.CheckBox checkBox_RestoreFromTxt;
    }
}

