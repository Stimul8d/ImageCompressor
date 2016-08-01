using System.Drawing.Imaging;

namespace ImageCompressor.Domain
{
    public class ImageFile
    {
        public string FilePath { get; }
        public ImageFormat Format { get; }

        public ImageFile(string path, ImageFormat format)
        {
            FilePath = path;
            Format = format;
        }

    }
}
