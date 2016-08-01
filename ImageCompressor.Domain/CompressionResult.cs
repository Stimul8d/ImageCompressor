using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageCompressor.Domain
{
    public class CompressionResult
    {
        public string NewPath { get; }
        public long NewSize { get; }
        public string OrginalPath { get; }
        public long OriginalSize { get; }

        public CompressionResult(string originalPath, long originalSize, string newPath, long newSize)
        {
            OrginalPath = originalPath;
            OriginalSize = originalSize;
            NewPath = newPath;
            NewSize = newSize;
        }

    }
}
