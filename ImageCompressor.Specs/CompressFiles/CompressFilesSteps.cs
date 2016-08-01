using FluentAssertions;
using ImageCompressor.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using TechTalk.SpecFlow;

namespace ImageCompressor.Specs.CompressFiles
{
    [Binding]
    public class CompressFilesSteps
    {
        private string testDirPath;
        private ImageDirectory testDir;
        private IEnumerable<CompressionResult> results;

        [Given(@"I have a list of image files")]
        public void GivenIHaveAListOfImageFiles()
        {
            testDirPath = $"{Path.GetDirectoryName(typeof(CompressFilesSteps).Assembly.Location)}/TestDirectory";
            var files = Directory.GetFiles(testDirPath);
            testDir = new ImageDirectory(testDirPath);
        }

        [When(@"I compress them")]
        public void WhenICompressThem()
        {
            results = testDir.Compress(50);
        }

        [Then(@"the compressed files smaller than the originals")]
        public void ThenTheCompressedFilesSmallerThanTheOriginals()
        {
            foreach (var result in results)
            {
                result.NewSize.Should().BeLessOrEqualTo(result.OriginalSize);
            }
        }
    }
}
