Feature: TestExample
	The Google Search page Example

Scenario: FullStackDevelopmentTest
	Given I have no cookies set
	When I navigate to the url http://www.fullstackdevelopment.co.uk/
	Then the metadata page title should contain the text Full Stack Development

Scenario: GoogleTest
	Given I have no cookies set
	When I navigate to the url http://www.google.com/
	And I search for specflow
	And I wait 5 seconds
	Then the metadata page title should contain the text specflow
