Feature: Duplicate Check
	In order to avoid duplcate entries in the system
	As a user
	I want to be told that I entered a duplicate if a simular person allready exists in the system 


Scenario: Duplicate check with exact match
	Given I have the following persons in the system:
		| FirstName		| LastName		| DateOfBirth |
		| Hans			| Meier			| 27.5.1953   |
	When I add the following person to the system:
		| Property		| Value			|
		| FirstName     | Hans			|
		| LastName      | Meier			|
		| DateOfBirth   | 27.5.1953		|
	Then the system tells me that I try to add a duplicate
	

Scenario: Duplicate check with no match
	Given I have the following persons in the system:
		| FirstName		| LastName		| DateOfBirth |
		| Hans			| Meier			| 27.5.1953   |
	When I add the following person to the system:
		| Property		| Value			|
		| FirstName     | Fritz			|
		| LastName      | Fischer		|
		| DateOfBirth   | 27.5.1953		|
	Then the system accepts my entry without dublicate message
