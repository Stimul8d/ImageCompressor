using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace ImageCompressor.Domain
{
    public class FileCompressor
    {
        public CompressionResult CompressFile(string path, string newPath, int quality)
        {
            File.Delete(newPath);

            var jpgEncoder = ImageCodecInfo.GetImageEncoders()
                .First(c => c.FormatID == ImageFormat.Jpeg.Guid);

            var image = Image.FromFile(path);
            Encoder encoder = Encoder.Quality;
            EncoderParameters encoderParameters = new EncoderParameters(1);

            EncoderParameter encoderParameter = new EncoderParameter(encoder, quality);
            encoderParameters.Param[0] = encoderParameter;

            FileStream ms = new FileStream(newPath, FileMode.Create, FileAccess.Write);
            image.Save(ms, jpgEncoder, encoderParameters);
            ms.Flush();
            ms.Close();

            return new CompressionResult(path, 1, path, 0);


        }

    }
}
