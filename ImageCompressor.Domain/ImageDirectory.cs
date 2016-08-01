using System.IO;
using System.Drawing;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Threading;

namespace ImageCompressor.Domain
{
    public class ImageDirectory
    {
        private string directoryPath;
        public IEnumerable<ImageFile> Images { get; }
        private const string CompressedPrefix = "_compressed_";

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

        //HACK: Move to ImageFile
        private bool IsSupportedImage(ImageFormat format)
        {
            return ImageFormat.Jpeg.Equals(format)
                || ImageFormat.Png.Equals(format)
                || ImageFormat.Gif.Equals(format);
        }

        //HACK: Move to ImageFile
        private bool IsntTransparent(ImageFile file)
        {
            return (Image.FromFile(file.FilePath).Flags & 0x02) == 0;
        }

        //HACK: Move to ImageFile
        private bool IsntCompressed(ImageFile file)
        {
            return !file.FilePath.Contains(CompressedPrefix);
        }

        //HACK: Move to ImageFile
        private string GetNewPath(string path)
        {
            return Path.Combine(Path.GetDirectoryName(path),
                   CompressedPrefix + Path.GetFileName(path));
        }


        public IEnumerable<CompressionResult> Compress(int quality)
        {
            var compressor = new FileCompressor();

            return Images
                .Where(IsntTransparent)
                .Where(IsntCompressed)
                .Select(i => compressor.CompressFile(i.FilePath, GetNewPath(i.FilePath), quality));
        }
    }
}
