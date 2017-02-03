Feature: Search
	Search Website



Scenario: Search in the website
	Given I am at the Home Page
	And I have entered search content
	| searchcontent |
	| Automation    |
	| Automation    |
	| Automation    |
	| Automation    |
	When I press search button
	Then The result should be displayed on the screen

Scenario Outline: Search multiple in the website
	Given I am at the Home Page
	And I have entered <search>
	When I press search button
	Then The result should be displayed on the screen
Examples: 
	| search |
	| Automation    |
	| Performance Testing|
