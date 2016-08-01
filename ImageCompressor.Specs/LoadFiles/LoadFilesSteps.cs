using ImageCompressor.Domain;
using System;
using System.IO;
using TechTalk.SpecFlow;
using System.Linq;
using FluentAssertions;

namespace ImageCompressor.Specs.LoadFiles
{
    [Binding]
    public class LoadFilesSteps
    {
        private string testDirPath;
        private ImageDirectory testDir;
        private int expectedImageCount;

        [Given(@"A directory containing various files")]
        public void GivenADirectoryContainingVariousFiles()
        {
            testDirPath =  $"{Path.GetDirectoryName(typeof(LoadFilesSteps).Assembly.Location)}/TestDirectory/";
            var files = Directory.GetFiles(testDirPath);

            expectedImageCount = files.Count(f => f.Contains("jpg") || f.Contains("jpeg")
                || f.Contains("png") || f.Contains("gif"));

            expectedImageCount.Should().NotBe(files.Length);
        }

        [When(@"I select a directory")]
        public void WhenISelectADirectory()
        {
            testDir = new ImageDirectory(testDirPath);
        }

        [Then(@"I should see a list of supported files ready to process\.")]
        public void ThenIShouldSeeAListOfSupportedFilesReadyToProcess_()
        {
            testDir.Images.Count().Should().Be(expectedImageCount);
        }
    }
}
