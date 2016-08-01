using System.IO;
using System.Drawing;
using System.Collections.Generic;
using System.Drawing.Imaging;

namespace ImageCompressor.Domain
{
    public class ImageDirectory
    {
        private string directoryPath;
        public IEnumerable<ImageFile> Images { get; }

        public ImageDirectory(string directoryPath)
        {
            this.directoryPath = directoryPath;
            var files = Directory.GetFiles(directoryPath);

            Images = new List<ImageFile>(GetImages(files));

        }

        private IEnumerable<ImageFile> GetImages(string[] files)
        {
            foreach (var file in files)
            {
                if (!file.Contains("jpg") && !file.Contains("jpeg")
                    && !file.Contains("png") && !file.Contains("gif")) continue;

                using (var fs = new FileStream(file, FileMode.Open, FileAccess.Read))
                using (var img = Image.FromStream(fs))
                {
                    if (!IsSupportedImage(img.RawFormat)) continue;

                    yield return new ImageFile(file, img.RawFormat);
                }
            }
        }

        private bool IsSupportedImage(ImageFormat format)
        {
            return ImageFormat.Jpeg.Equals(format)
                || ImageFormat.Png.Equals(format)
                || ImageFormat.Gif.Equals(format);
        }

    }
}
