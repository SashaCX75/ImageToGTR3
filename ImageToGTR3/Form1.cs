using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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
        List<Color> colorMapList = new List<Color>();
        int ImageWidth;
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
            openFileDialog.Multiselect = true;
            //openFileDialog.Title = Properties.FormStrings.Dialog_Title_Pack;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                List<string> FileNames = openFileDialog.FileNames.ToList();
                progressBar1.Value = 0;
                progressBar1.Maximum = FileNames.Count;
                string path = "";
                progressBar1.Visible = true;
                foreach (String file in FileNames)
                {
                    progressBar1.Value++;
                    try
                    {
                        //string fileNameFull = openFileDialog.FileName;
                        string fileNameFull = file;
                        string fileName = Path.GetFileNameWithoutExtension(fileNameFull);
                        path = Path.GetDirectoryName(fileNameFull);
                        //fileName = Path.Combine(path, fileName);
                        int RealWidth = -1;
                        using (var fileStream = File.OpenRead(fileNameFull)) 
                        {
                            _streamBuffer = new byte[fileStream.Length];
                            fileStream.Read(_streamBuffer, 0, (int)fileStream.Length);

                            Header header = new Header(_streamBuffer);
                            ImageDescription imageDescription = new ImageDescription(_streamBuffer, header.GetImageIDLength());
                            RealWidth = imageDescription.GetRealWidth();
                        }

                        ImageMagick.MagickImage image = new ImageMagick.MagickImage(fileNameFull, ImageMagick.MagickFormat.Tga);
                        image.Format = ImageMagick.MagickFormat.Png32;
                        if(RealWidth>0 && RealWidth != image.Width)
                        {
                            int height = image.Height;
                            image = (ImageMagick.MagickImage)image.Clone(RealWidth, height);
                        }

                        ImageMagick.IMagickImage Blue = image.Separate(ImageMagick.Channels.Blue).First();
                        ImageMagick.IMagickImage Red = image.Separate(ImageMagick.Channels.Red).First();
                        image.Composite(Red, ImageMagick.CompositeOperator.Replace, ImageMagick.Channels.Blue);
                        image.Composite(Blue, ImageMagick.CompositeOperator.Replace, ImageMagick.Channels.Red);

                        //image.ColorType = ImageMagick.ColorType.Palette;
                        path = Path.Combine(path, "Png");
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }
                        string newFileName = Path.Combine(path, fileName + ".png");
                        image.Write(newFileName);
                        //Bitmap bitmap = image.ToBitmap();
                        //panel1.BackgroundImage = bitmap;
                    }
                    catch (Exception exp)
                    {
                        MessageBox.Show("Не верный формат исходного файла" + Environment.NewLine +
                            exp);
                    }
                }
                progressBar1.Visible = false;
                if (Directory.Exists(path))
                {
                    Process.Start(new ProcessStartInfo("explorer.exe", path));
                }
            }
        }

        private void button_PngToTga_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png) | *.png";
            openFileDialog.RestoreDirectory = true;
            openFileDialog.Multiselect = true;
            //openFileDialog.Title = Properties.FormStrings.Dialog_Title_Pack;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                List<string> FileNames = openFileDialog.FileNames.ToList();
                progressBar1.Value = 0;
                progressBar1.Maximum = FileNames.Count;
                progressBar1.Visible = true;
                string fileNameFull = "";
                foreach (String file in FileNames)
                {
                    progressBar1.Value++;
                    fileNameFull = PngToTga(file);
                    if (fileNameFull != null) ImageFix(fileNameFull);
                }
                progressBar1.Visible = false; 
                if (Directory.Exists(Path.GetDirectoryName(fileNameFull)))
                {
                    Process.Start(new ProcessStartInfo("explorer.exe", Path.GetDirectoryName(fileNameFull)));
                }
            }
        }

        private string PngToTga(string fileNameFull)
        {
            if (File.Exists(fileNameFull))
            {
                colorMapList.Clear();
                try
                {
                    //string fileNameFull = openFileDialog.FileName;
                    string fileName = Path.GetFileNameWithoutExtension(fileNameFull);
                    string path = Path.GetDirectoryName(fileNameFull);
                    //fileName = Path.Combine(path, fileName);
                    ImageMagick.MagickImage image = new ImageMagick.MagickImage(fileNameFull);
                    ImageMagick.MagickImage image_temp = new ImageMagick.MagickImage(fileNameFull);
                    ImageWidth = image.Width;
                    int newWidth = ImageWidth;
                    int newHeight = image.Height;
                    while (newWidth % 16 != 0)
                    {
                        newWidth++;
                    }


                    if (ImageWidth != newWidth)
                    {
                        //ImageMagick.MagickImage imageNew =
                        //    new ImageMagick.MagickImage(fileNameFull);
                        ////imageNew.ColorSpace = ImageMagick.ColorSpace.sRGB;
                        ////imageNew.Format = ImageMagick.MagickFormat.Png;
                        ////imageNew.Depth = 8;
                        ////imageNew.ColorSpace = ImageMagick.ColorSpace.sRGB;
                        ////image = (ImageMagick.MagickImage)image.Clone(newWidth, newHeight);
                        ////image_temp = (ImageMagick.MagickImage)image_temp.Clone(newWidth, newHeight);
                        ////imageNew.Composite(image, ImageMagick.CompositeOperator.Src);
                        //ImageMagick.Drawables dr = new ImageMagick.Drawables();
                        //dr.Rectangle(image.Width + 1, 0, newWidth, newHeight);
                        //dr.FillOpacity(new ImageMagick.Percentage(100));
                        //image = (ImageMagick.MagickImage)image.Clone(newWidth, newHeight);
                        //image_temp = (ImageMagick.MagickImage)image_temp.Clone(newWidth, newHeight);
                        //image.Draw();
                        //image_temp.Draw();
                        ////image = (ImageMagick.MagickImage)imageNew.Clone(newWidth, newHeight);
                        //image_temp = (ImageMagick.MagickImage)imageNew.Clone(newWidth, newHeight);
                        Bitmap bitmap = image.ToBitmap();
                        Bitmap bitmapNew = new Bitmap(newWidth, newHeight);
                        Graphics gfx = Graphics.FromImage(bitmapNew);
                        gfx.DrawImage(bitmap, 0, 0, bitmap.Width, bitmap.Height);
                        image = new ImageMagick.MagickImage(bitmapNew);
                        image_temp = new ImageMagick.MagickImage(bitmapNew);
                    }
                    image.ColorType = ImageMagick.ColorType.Palette;
                    if (image.ColorSpace != ImageMagick.ColorSpace.sRGB)
                    {
                        image = image_temp;
                        //image.ColorSpace = ImageMagick.ColorSpace.sRGB;
                        ImageMagick.Pixel pixel = image.GetPixels().GetPixel(0, 0);
                        ushort[] p;
                        if (pixel[2] > 256)
                        {
                            if (pixel.Channels == 4) p = new ushort[] { pixel[0], pixel[1], (ushort)(pixel[2] - 256), pixel[3] };
                            else p = new ushort[] { pixel[0], pixel[1], (ushort)(pixel[2] - 256) };
                        }
                        else
                        {
                            if (pixel.Channels == 4) p = new ushort[] { pixel[0], pixel[1], (ushort)(pixel[2] + 256), pixel[3] };
                            else p = new ushort[] { pixel[0], pixel[1], (ushort)(pixel[2] + 256) };
                        }
                        image.GetPixels().SetPixel(0, 0, p);
                        pixel = image.GetPixels().GetPixel(0, 0);
                        image.ColorType = ImageMagick.ColorType.Palette;
                        pixel = image.GetPixels().GetPixel(0, 0);
                        if (image.ColorSpace != ImageMagick.ColorSpace.sRGB)
                        {
                            MessageBox.Show("Изображение не должно быть монохромным и должно быть в формате 32bit" +
                                Environment.NewLine + fileNameFull);
                            return null;
                        }
                    }


                    //image.Format = ImageMagick.MagickFormat.Tga;
                    //List<string> colorMapList = new List<string>();
                    for (int i = 0; i < image.ColormapSize; i++)
                    {

                        colorMapList.Add(image.GetColormap(i));
                        //Color tempColor = image.GetColormap(i);
                        //colorMapList.Add(tempColor.ToArgb().ToString());
                        //Color tempColor2 = Color.FromArgb(Int32.Parse(colorMapList[i]));
                    }
                    //File.WriteAllLines(fileName + ".txt", colorMapList);
                    path = Path.Combine(path, "Fix");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    string newFileName = Path.Combine(path, fileName + ".tga");
                    image.Write(newFileName, ImageMagick.MagickFormat.Tga);
                    return newFileName;

                }
                catch (Exception exp)
                {
                    MessageBox.Show("Не верный формат исходного файла" + Environment.NewLine +
                            exp);
                }
            }
            return null;
        }

        private void ImageFix(string fileNameFull)
        {
            if (File.Exists(fileNameFull))
            {
                try
                {
                    //string fileNameFull = openFileDialog.FileName;
                    string fileName = Path.GetFileNameWithoutExtension(fileNameFull);
                    string path = Path.GetDirectoryName(fileNameFull);
                    //fileName = Path.Combine(path, fileName);

                    //ImageMagick.MagickImage image = new ImageMagick.MagickImage(fileNameFull, ImageMagick.MagickFormat.Tga);

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
                        header.SetImageIDLength(46);
                        imageDescription.SetSize(46, ImageWidth);
                        //imageDescription.SetSize(46, header.Width);

                        int colorMapCount = ColorMap.ColorMapCount;
                        //if (checkBox_Color256.Checked && !checkBox_32bit.Checked)
                        //{
                        //    colorMapCount = 256;
                        //    header.SetColorMapCount(colorMapCount);
                        //    if (!checkBox_32bit.Checked) ColorMap.SetColorCount(colorMapCount);
                        //}
                        bool argb_brga = true;
                        colorMapCount = 256;
                        header.SetColorMapCount(colorMapCount);
                        byte colorMapEntrySize = 32;

                        ColorMap.RestoreColor(colorMapList);
                        ColorMap.ColorsFix(argb_brga, colorMapCount, colorMapEntrySize);
                        header.SetColorMapEntrySize(32);
                        #endregion

                        int newLength = 18 + header.GetImageIDLength() + ColorMap._colorMap.Length + imageData._imageData.Length;
                        //if (checkBox_Footer.Checked) newLength = newLength + footer._footer.Length;
                        byte[] newTGA = new byte[newLength];

                        header._header.CopyTo(newTGA, 0);
                        int offset = header._header.Length;

                        imageDescription._imageDescription.CopyTo(newTGA, offset);
                        offset = offset + imageDescription._imageDescription.Length;

                        ColorMap._colorMap.CopyTo(newTGA, offset);
                        offset = offset + ColorMap._colorMap.Length;

                        imageData._imageData.CopyTo(newTGA, offset);
                        offset = offset + imageData._imageData.Length;

                        //if (checkBox_Footer.Checked) footer._footer.CopyTo(newTGA, offset);

                        if (newTGA != null && newTGA.Length > 0)
                        {
                            string newFileName = Path.Combine(path, fileName + ".png");

                            using (var fileStreamTGA = File.OpenWrite(newFileName))
                            {
                                fileStreamTGA.Write(newTGA, 0, newTGA.Length);
                                fileStreamTGA.Flush();
                            }
                        }
                    }

                    try
                    {
                        File.Delete(fileNameFull);
                    }
                    catch (Exception)
                    {
                    }

                }
                catch (Exception exp)
                {
                    MessageBox.Show("Ошибка открытия файла" + Environment.NewLine +
                            exp);
                }
            }
        }


    }
}
