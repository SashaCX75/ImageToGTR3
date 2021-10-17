using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace ImageToGTR3
{
    public partial class Form1 : Form
    {
        private byte[] _streamBuffer;
        public Form1()
        {
            InitializeComponent();
        }

        // меняем цвет текста и рамки для groupBox
        private void groupBox_Paint(object sender, PaintEventArgs e)
        {
            GroupBox box = sender as GroupBox;
            if (box.Enabled) DrawGroupBox(box, e.Graphics, Color.Black, Color.DarkGray);
            else DrawGroupBox(box, e.Graphics, Color.DarkGray, Color.DarkGray);
        }
        private void DrawGroupBox(GroupBox box, Graphics g, Color textColor, Color borderColor)
        {
            if (box != null)
            {
                Brush textBrush = new SolidBrush(textColor);
                Brush borderBrush = new SolidBrush(borderColor);
                Pen borderPen = new Pen(borderBrush);
                SizeF strSize = g.MeasureString(box.Text, box.Font);
                Rectangle rect = new Rectangle(box.ClientRectangle.X,
                                               box.ClientRectangle.Y + (int)(strSize.Height / 2),
                                               box.ClientRectangle.Width - 1,
                                               box.ClientRectangle.Height - (int)(strSize.Height / 2) - 5);

                // Clear text and border
                g.Clear(this.BackColor);

                // Draw text
                g.DrawString(box.Text, box.Font, textBrush, box.Padding.Left, 0);

                // Drawing Border
                //Left
                g.DrawLine(borderPen, rect.Location, new Point(rect.X, rect.Y + rect.Height));
                //Right
                g.DrawLine(borderPen, new Point(rect.X + rect.Width, rect.Y), new Point(rect.X + rect.Width, rect.Y + rect.Height));
                //Bottom
                g.DrawLine(borderPen, new Point(rect.X, rect.Y + rect.Height), new Point(rect.X + rect.Width, rect.Y + rect.Height));
                //Top1
                g.DrawLine(borderPen, new Point(rect.X, rect.Y), new Point(rect.X + box.Padding.Left, rect.Y));
                //Top2
                g.DrawLine(borderPen, new Point(rect.X + box.Padding.Left + (int)(strSize.Width), rect.Y), new Point(rect.X + rect.Width, rect.Y));
            }
        }
        private void button_TgaToPng_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png, *.tga) | *.png; *.tga";
            openFileDialog.RestoreDirectory = true;
            openFileDialog.Multiselect = false;
            //openFileDialog.Title = Properties.FormStrings.Dialog_Title_Pack;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string fileNameFull = openFileDialog.FileName;
                    string fileName = Path.GetFileNameWithoutExtension(fileNameFull);
                    string path = Path.GetDirectoryName(fileNameFull);
                    fileName = Path.Combine(path, fileName);
                    ImageMagick.MagickImage image = new ImageMagick.MagickImage(fileNameFull, ImageMagick.MagickFormat.Tga);
                    if (checkBox_RGB_BRG.Checked)
                    {
                        ImageMagick.IMagickImage Blue = image.Separate(ImageMagick.Channels.Blue).First();
                        ImageMagick.IMagickImage Red = image.Separate(ImageMagick.Channels.Red).First();
                        image.Composite(Red, ImageMagick.CompositeOperator.Replace, ImageMagick.Channels.Blue);
                        image.Composite(Blue, ImageMagick.CompositeOperator.Replace, ImageMagick.Channels.Red);
                    }
                    //image.ColorType = ImageMagick.ColorType.Palette;
                    image.Write(fileName + "_.png");
                    //Bitmap bitmap = image.ToBitmap();
                    //panel1.BackgroundImage = bitmap;
                }
                catch (Exception)
                {
                    MessageBox.Show("Не верный формат исходного файла");
                }
            }
        }

        private void button_PngToTga_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png) | *.png";
            openFileDialog.RestoreDirectory = true;
            openFileDialog.Multiselect = false;
            //openFileDialog.Title = Properties.FormStrings.Dialog_Title_Pack;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string fileNameFull = openFileDialog.FileName;
                    string fileName = Path.GetFileNameWithoutExtension(fileNameFull);
                    string path = Path.GetDirectoryName(fileNameFull);
                    fileName = Path.Combine(path, fileName);
                    ImageMagick.MagickImage image = new ImageMagick.MagickImage(fileNameFull);
                    ImageMagick.MagickImage image_temp = new ImageMagick.MagickImage(fileNameFull);
                    //ImageMagick.MagickImage image = new ImageMagick.MagickImage(fileNameFull, ImageMagick.MagickFormat.Tga);

                    //ushort[] p = { 0, 0, 300 };
                    //image.GetPixels().SetPixel(0, 0, p);
                    image.ColorType = ImageMagick.ColorType.Palette;
                    if (image.ColorSpace == ImageMagick.ColorSpace.Gray) 
                    {
                        image = image_temp;
                        //image.ColorSpace = ImageMagick.ColorSpace.sRGB;
                        ImageMagick.Pixel pixel = image.GetPixels().GetPixel(0, 0);
                        ushort[] p ;
                        if (pixel[2] > 256)
                        {
                            if (pixel.Channels == 4) p = new ushort[] { pixel[0], pixel[1], (ushort)(pixel[2]-256), pixel[3] };
                            else p = new ushort[] { pixel[0], pixel[1], (ushort)(pixel[2] - 256) };
                        }
                        else
                        {
                            if (pixel.Channels == 4) p = new ushort[] { pixel[0], pixel[1], (ushort)(pixel[2] + 256), pixel[3] };
                            else p = new ushort[] { pixel[0], pixel[1], (ushort)(pixel[2] + 256) };
                        }
                        image.GetPixels().SetPixel(0,0, p);
                        pixel = image.GetPixels().GetPixel(0, 0);
                        image.ColorType = ImageMagick.ColorType.Palette;
                        pixel = image.GetPixels().GetPixel(0, 0);
                        if (image.ColorSpace == ImageMagick.ColorSpace.Gray)
                        {
                            MessageBox.Show("Изображение не должно быть монохромным");
                            return; 
                        }
                    }


                    //image.Format = ImageMagick.MagickFormat.Tga;
                    List<string> colorMapList= new List<string>();
                    for (int i = 0; i < image.ColormapSize; i++)
                    {
                        Color tempColor = image.GetColormap(i);
                        colorMapList.Add(tempColor.ToArgb().ToString());
                        //Color tempColor2 = Color.FromArgb(Int32.Parse(colorMapList[i]));
                    }
                    File.WriteAllLines(fileName + ".txt", colorMapList);

                    //ImageMagick.MagickColor color1 = image.GetColormap(1);
                    //image.SetColormap(10, ImageMagick.MagickColor.FromRgba(1, 1, 1, 1));
                    //image.SetColormap(1, color1);
                    //color1 = ImageMagick.MagickColor.FromRgba(0, 0, 0, 0);
                    //image.SetColormap(1, color1);
                    //image.CycleColormap(11);
                    //image.Write(fileName + ".tga", ImageMagick.MagickFormat.Tga);
                    image.Write(fileName + ".tga", ImageMagick.MagickFormat.Tga);
                    //image.Write(fileName + "t.png");
                    //image.Write(fileName + "_.png");
                    //Bitmap bitmap = image.ToBitmap();
                    //panel1.BackgroundImage = bitmap;
                }
                catch (Exception)
                {
                    MessageBox.Show("Не верный формат исходного файла");
                }
            }
        }

        private void checkBox_32bit_CheckedChanged(object sender, EventArgs e)
        {
            checkBox_ARGB_BGRA.Enabled = checkBox_32bit.Checked;
            groupBox_alphaChenel.Enabled = checkBox_32bit.Checked;
            checkBox_RestoreFromTxt.Enabled = checkBox_32bit.Checked;
            if (!checkBox_32bit.Checked) checkBox_ARGB_BGRA.Checked = false;
        }

        private void radioButton_BlackAlpha_CheckedChanged(object sender, EventArgs e)
        {
            label1.Enabled = radioButton_BlackAlpha.Checked;
            numericUpDown1.Enabled = radioButton_BlackAlpha.Checked;
        }

        private void button_ImageFix_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "TGA files (*.tga) | *.tga";
            openFileDialog.RestoreDirectory = true;
            openFileDialog.Multiselect = false;
            //openFileDialog.Title = Properties.FormStrings.Dialog_Title_Pack;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string fileNameFull = openFileDialog.FileName;
                    string fileName = Path.GetFileNameWithoutExtension(fileNameFull);
                    string path = Path.GetDirectoryName(fileNameFull);
                    fileName = Path.Combine(path, fileName);

                    ImageMagick.MagickImage image = new ImageMagick.MagickImage(fileNameFull, ImageMagick.MagickFormat.Tga);

                    // читаем картинку в массив
                    using (var fileStream = File.OpenRead(fileNameFull))
                    {
                        _streamBuffer = new byte[fileStream.Length];
                        fileStream.Read(_streamBuffer, 0, (int)fileStream.Length);

                        Header header = new Header(_streamBuffer);
                        ImageDescription imageDescription = new ImageDescription(_streamBuffer, header.GetImageIDLength());

                        int ColorMapCount = header.GetColorMapCount(); // количество цветов в карте
                        byte ColorMapEntrySize = header.GetColorMapEntrySize(); // битность цвета
                        byte ImageIDLength = header.GetImageIDLength(); // длина описания
                        ColorMap ColorMap = new ColorMap(_streamBuffer, ColorMapCount, ColorMapEntrySize, 18 + ImageIDLength);

                        int ColorMapLength = ColorMap._colorMap.Length;
                        Image_data imageData = new Image_data(_streamBuffer, 18 + ImageIDLength + ColorMapLength);

                        Footer footer = new Footer();

                        #region fix
                        if (checkBox_ImageIDLength46.Checked)
                        {
                            header.SetImageIDLength(46);
                            imageDescription.SetSize(46);
                        }
                        int colorMapCount = ColorMap.ColorMapCount;
                        if (checkBox_Color256.Checked) 
                        {
                            colorMapCount = 256;
                            header.SetColorMapCount(colorMapCount);
                            if (!checkBox_32bit.Checked) ColorMap.SetColorCount(colorMapCount);
                        }
                        if (checkBox_32bit.Checked)
                        {
                            bool argb_brga = checkBox_ARGB_BGRA.Checked;
                            bool blackAlpha = radioButton_BlackAlpha.Checked;
                            byte errorValue = (byte)numericUpDown1.Value;
                            bool r = radioButton_RAlpha.Checked;
                            bool g = radioButton_GAlpha.Checked;
                            bool b = radioButton_BAlpha.Checked;
                            bool firstСolor = radioButton_FirstСolor.Checked;
                            bool lastСolor = radioButton_LastСolor.Checked;
                            byte colorMapEntrySize = 32;
                            if (checkBox_RestoreFromTxt.Checked)
                            {
                                List<string> colorMapListS = new List<string>();
                                colorMapListS = File.ReadLines(fileName + ".txt").ToList();

                                List<Color> colorMapList = new List<Color>();
                                foreach (string item in colorMapListS)
                                {
                                    int value = Int32.Parse(item);
                                    colorMapList.Add(Color.FromArgb(value));
                                }

                                ColorMap.RestoreColor(colorMapList);
                            }
                            ColorMap.ColorsFix(argb_brga, blackAlpha, errorValue, r, g, b, firstСolor, lastСolor,
                                colorMapCount, colorMapEntrySize);
                            header.SetColorMapEntrySize(32);
                        }
                        #endregion

                            int newLength = 18 + header.GetImageIDLength() + ColorMap._colorMap.Length + imageData._imageData.Length;
                        if (checkBox_Footer.Checked) newLength = newLength + footer._footer.Length;
                        byte[] newTGA = new byte[newLength];

                        header._header.CopyTo(newTGA, 0);
                        int offset = header._header.Length;

                        imageDescription._imageDescription.CopyTo(newTGA, offset);
                        offset = offset + imageDescription._imageDescription.Length;

                        ColorMap._colorMap.CopyTo(newTGA, offset);
                        offset = offset + ColorMap._colorMap.Length;

                        imageData._imageData.CopyTo(newTGA, offset);
                        offset = offset + imageData._imageData.Length;

                        if (checkBox_Footer.Checked) footer._footer.CopyTo(newTGA, offset);

                        if (newTGA != null && newTGA.Length > 0)
                        {
                            using (var fileStreamTGA = File.OpenWrite(fileName + "Fix.png"))
                            {
                                fileStreamTGA.Write(newTGA, 0, newTGA.Length);
                                fileStreamTGA.Flush();
                            }
                        }
                    }

                }
                catch (Exception)
                {
                    MessageBox.Show("Ошибка открытия файла");
                }
            }
        }

    }
}
