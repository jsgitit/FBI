Feature: Access different types of files
	As an Agent 
	I want to access public and classified files indicated by my clearance level

Scenario: Access public files
	Given Clearance Level 1 or 2
	When an Agent tries to access public files
	Then the public file access message is returned and access is granted

Scenario: Access classified files
	Given Clearance Level 2
	When an Agent tries to access classified files
	Then the classified file access message is returned and access is granted


