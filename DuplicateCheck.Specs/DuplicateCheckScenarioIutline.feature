Feature: Duplicate Check Scenatio Outline
	In order to avoid duplcate entries in the system
	As a user
	I want to be told that I entered a duplicate if a simular person allready exists in the system 

Scenario Outline: Duplicate check
	Given I have the following persons in the system:
		| FirstName		| LastName		| DateOfBirth |
		| Hans			| Meier			| 27.5.1953   | 
	When I add the following person to the system:
		| Property		| Value			|
		| FirstName     | <FirstName>	|
		| LastName      | <LastName>	|
		| DateOfBirth   | <DateOfBirth>	|
	Then the duplicate check result is <DuplicateCheckResult>

Examples:
| FirstName | LastName	| DateOfBirth	| DuplicateCheckResult |
| Hans      | Meier		| 27.5.1953		| Duplicate            |
| Hans      | Meier		| 27.5.1954		| No duplicate         |
| Fritz     | Fischer   | 27.5.1953		| No duplicate         |