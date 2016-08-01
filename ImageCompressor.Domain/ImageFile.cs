using System.Drawing.Imaging;

namespace ImageCompressor.Domain
{
    public class ImageFile
    {
        private string file;
        private ImageFormat format;

        public ImageFile(string file, ImageFormat format)
        {
            this.file = file;
            this.format = format;
        }
    }
}
