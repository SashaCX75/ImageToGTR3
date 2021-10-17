using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageToGTR3
{
    class ColorMap
    {
        public int ColorMapCount;
        private byte ColorMapEntrySize;
        public byte[] _colorMap;
        List<Color> Colors = new List<Color>();

        /// <summary>Формируем карту цветов</summary>
        /// <param name="streamBuffer">Набор байт файла</param>
        /// <param name="colorMapCount">Количество цветов в карте</param>
        /// <param name="colorMapEntrySize">Битность цвета (24, 32)</param>
        /// <param name="offset">Смещение от начала файла до карты цветов</param>
        public ColorMap(byte[] streamBuffer, int colorMapCount, byte colorMapEntrySize, int offset)
        {
            ColorMapCount = colorMapCount;
            ColorMapEntrySize = colorMapEntrySize;
            int bit = ColorMapEntrySize / 8;
            int lenght = bit * ColorMapCount;
            _colorMap = new byte[lenght];
            Array.Copy(streamBuffer, offset, _colorMap, 0, lenght);

            ArrayToColors();
        }

        private void ArrayToColors()
        {
            Colors.Clear();
            if (ColorMapEntrySize == 24)
            {
                for (int i = 0; i < ColorMapCount; i++)
                {
                    if (i * 3 + 2 < _colorMap.Length)
                    {
                        Colors.Add(Color.FromArgb(_colorMap[i * 3], _colorMap[i * 3 + 1], _colorMap[i * 3 + 2]));
                    }
                }
            }
            if (ColorMapEntrySize == 32)
            {
                for (int i = 0; i < ColorMapCount; i++)
                {
                    if (i * 4 + 3 < _colorMap.Length)
                    {
                        Colors.Add(Color.FromArgb(_colorMap[i * 4], _colorMap[i * 4 + 1], _colorMap[i * 4 + 2], _colorMap[i * 4 + 3]));
                    }
                }
            }
        }

        private void ARGB_BGRA(bool blackAlpha, byte errorValue,bool r, bool g, bool b)
        {
            for (int i = 0; i < Colors.Count; i++)
            {
                byte A = Colors[i].A;
                byte R = Colors[i].R;
                byte G = Colors[i].G;
                byte B = Colors[i].B;
                float scale = A / 255f;
                R = (byte)(R * scale);
                G = (byte)(G * scale);
                B = (byte)(B * scale);

                if (blackAlpha)
                {
                    if (R < errorValue && G < errorValue && B < errorValue) A = 0;
                }
                if (r) A = R;
                if (g) A = G;
                if (b) A = B;

                Colors[i] = Color.FromArgb(R, G, B, A);
            }
        }
        private void SetTransparent(bool blackAlpha, byte errorValue, bool r, bool g, bool b)
        {
            for (int i = 0; i < Colors.Count; i++)
            {
                byte A = Colors[i].A;
                byte R = Colors[i].R;
                byte G = Colors[i].G;
                byte B = Colors[i].B;

                if (blackAlpha)
                {
                    if (R < errorValue && G < errorValue && B < errorValue) A = 0;
                }
                if (r) A = R;
                if (g) A = G;
                if (b) A = B;

                Colors[i] = Color.FromArgb(A, R, G, B);
            }
        }

        public void ColorsFix(bool argb_brga, bool blackAlpha, byte errorValue,
            bool r, bool g, bool b, bool firstСolor, bool lastСolor, int colorMapCount, byte colorMapEntrySize)
        {
            if (firstСolor) Colors[0] = Color.FromArgb(0, Colors[0].R, Colors[0].G, Colors[0].B);
            if (lastСolor)
            {
                int index = Colors.Count - 1;
                Colors[index] = Color.FromArgb(0, Colors[index].R, Colors[index].G, Colors[index].B);
            }
            if (argb_brga) ARGB_BGRA(blackAlpha, errorValue, r, g, b);
            else if (colorMapEntrySize!=31) SetTransparent(blackAlpha, errorValue, r, g, b);

            ColorMapCount = colorMapCount;
            ColorMapEntrySize = colorMapEntrySize;
            int bit = ColorMapEntrySize / 8;
            int lenght = bit * ColorMapCount;
            _colorMap = new byte[lenght];
            for (int i = 0; i < ColorMapCount; i++)
            {
                Color color = Color.FromArgb(0, 0, 0, 0);
                if (i < Colors.Count) color = Colors[i];
                _colorMap[i * bit] = color.A;
                _colorMap[i * bit + 1] = color.R;
                _colorMap[i * bit + 2] = color.G;
                _colorMap[i * bit + 3] = color.B;
            }
            ArrayToColors();
        }

        public void RestoreColor(List<Color> colorMapList)
        {
            ColorMapCount = colorMapList.Count;
            ColorMapEntrySize = 32;
            int bit = ColorMapEntrySize / 8;
            int lenght = bit * ColorMapCount;
            _colorMap = new byte[lenght];
            for (int i = 0; i < ColorMapCount; i++)
            {
                Color color = Color.FromArgb(0, 0, 0, 0);
                if (i < colorMapList.Count) color = colorMapList[i];
                _colorMap[i * bit] = color.A;
                _colorMap[i * bit + 1] = color.R;
                _colorMap[i * bit + 2] = color.G;
                _colorMap[i * bit + 3] = color.B;
            }
            ArrayToColors();
        }
        public void SetColorCount(int newCount)
        {
            ColorMapCount = newCount;
            int bit = ColorMapEntrySize / 8;
            int lenght = bit * ColorMapCount;
            _colorMap = new byte[lenght];
            if (ColorMapEntrySize == 24)
            {
                for (int i = 0; i < ColorMapCount; i++)
                {
                    Color color = Color.FromArgb(0, 0, 0, 0);
                    if (i < Colors.Count) color = Colors[i];
                    //_colorMap[i * bit] = color.A;
                    _colorMap[i * bit] = color.R;
                    _colorMap[i * bit + 1] = color.G;
                    _colorMap[i * bit + 2] = color.B;
                }
            }
            if (ColorMapEntrySize == 32)
            {
                for (int i = 0; i < ColorMapCount; i++)
                {
                    Color color = Color.FromArgb(0, 0, 0, 0);
                    if (i < Colors.Count) color = Colors[i];
                    _colorMap[i * bit] = color.A;
                    _colorMap[i * bit + 1] = color.R;
                    _colorMap[i * bit + 2] = color.G;
                    _colorMap[i * bit + 3] = color.B;
                }
            }
            ArrayToColors();
        }
    }
}
