Feature: Duplicate Check
	In order to avoid duplcate entries in the system
	As a user
	I want to be told that I entered a duplicate if a simular person allready exists in the system 


Scenario: Duplicate check with exact match
	Given I have a person with firstname "Hans" and lastname "Meier" in the system
	When I add a person with fistname "Hans" and lastname "Meier" to the system
	Then the system tells me that I try to add a duplicate
	

Scenario: Duplicate check with no match
	Given I have a person with firstname "Hans" and lastname "Meier" in the system
	When I add a person with fistname "Fritz" and lastname "Fischer" to the system
	Then the system accepts my entry without dublicate message
