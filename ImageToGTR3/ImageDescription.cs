using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageToGTR3
{
    class ImageDescription
    {
        public byte[] _imageDescription;
        public ImageDescription(byte[] streamBuffer, byte lenght)
        {
            _imageDescription = new byte[lenght];
            Array.Copy(streamBuffer, 18, _imageDescription, 0, lenght);
        }

        public void SetSize(int newSize)
        {
            Array.Resize(ref _imageDescription, newSize);
        }
    }
}
