Feature: LoadFiles
	In order to compress images
	I want to load them from a directory
	So that they can be processed

@mytag
Scenario: Load Files From Directory
	Given A directory containing various files
	When I select a directory
	Then I should see a list of supported files ready to process.
