Feature: CompressFiles
	In order improve page load performane
	I want to lower the quality of images
	So that the file size of images is smaller

Scenario: Compress
	Given I have a list of image files
	When I compress them
	Then the compressed files smaller than the originals
