Feature: Duplicate Check with probability
	In order to avoid duplicate entries in the system
	As a user
	I want to be told that I entered a duplicate if a similar person already exists in the system 

Scenario Outline: Duplicate check
	Given I have the following persons in the system:
		| FirstName		| LastName		| DateOfBirth	|
		| Hans			| Meier			| 27.05.1953	| 
		| Hansli		| Meier			| 27.05.1953	| 
		| Sepp			| Huber			| 03.05.1977	|
		| Rita			| Müller		| 15.12.1980	|  
	When I add the following person to the system:
		| Property		| Value			|
		| FirstName     | <FirstName>	|
		| LastName      | <LastName>	|
		| DateOfBirth   | <DateOfBirth>	|
	Then the duplicate check result is <DuplicateCheckResult>
	And the duplicate check details are:
	| Name             | Probability            |
	| <DuplicateName1> | <DuplicateProbabilty1> |
	| <DuplicateName2> | <DuplicateProbabilty2> |

Examples:	
| FirstName | LastName  | DateOfBirth | DuplicateCheckResult | DuplicateName1 | DuplicateProbabilty1	| DuplicateName2 | DuplicateProbabilty2		|
| Hans      | Meier     | 27.05.1953  | Duplicate            | Hans Meier     | 1						| Hansli Meier   | 0.45						|
| Hansi     | Meier     | 27.05.1953  | Duplicate            | Hans Meier     | 0.59					| Hansli Meier   | 0.54						|
| Hansli    | Meier     | 27.05.1953  | Duplicate            | Hans Meier     | 0.45					| Hansli Meier   | 1						|
| Hans      | Maier     | 27.05.1953  | Duplicate            | Hans Meier     | 0.48					| Hansli Meier   | 0.45						|
| Hans      | Meyer     | 27.05.1953  | Duplicate            | Hans Meier     | 0.45					| Hansli Meier   | 0.45						|
| Hans      | Meier     | 27.05.1954  | No duplicate         |                |							|                |							|
| Rita      | Müller    | 15.12.1980  | Duplicate            | Rita Müller    | 1						|				 |							|
| Rita      | Mueller   | 15.12.1980  | Duplicate            | Rita Müller    | 0.41					|				 |							|
| Meier     | Hans      | 27.05.1953  | No duplicate         |                |							|                |							|
| Meier     | Hansjakob | 27.05.1953  | No duplicate         |                |							|                |							|
| Fritz     | Fischer   | 27.05.1953  | No duplicate         |                |							|                |							|                                                                  