@sql
Feature: SQL Queries
	In order manage SQL Database
	As a admin
	I want to Create, Read, Update, Delete tables from DB

@sql @insert
Scenario Outline: I want to be able to insert new records to table.
	Given Connection with database is opened
	When I insert this row with data to table
	| Id   | Name   | Count   |
	| <id> | <name> | <count> |
	Then New row was added with data
	| Id   | Name   | Count   |
	| <id> | <name> | <count> |
Examples:
	| id | name     | count |
	| 23 | 'Test23' | 23    |

@sql @delete
Scenario Outline: I want to be able to delete any record in table.
	Given Connection with database is opened
	When I insert this row with data to table
	| Id   | Name   | Count   |
	| <id> | <name> | <count> |
	And i delete row with data
	| Id   |
	| <id> |
	Then the row with this data doest't exist
	| Id   | Name   | Count   |
	| <id> | <name> | <count> |
Examples:
	| id | name     | count |
	| 23 | 'Test23' | 23    |

@sql @update
Scenario Outline: I want to be able to update any record in table.
	Given Connection with database is opened
	When I insert this row with data to table
	| Id   | Name   | Count   |
	| <id> | <name> | <count> |
	And i update row with data where id
	| Updated Name   | Id   |
	| <updated_name> | <id> |
	Then the row updated and have this data
	| Id   | updated_name   | Count   |
	| <id> | <updated_name> | <count> |
Examples:
	| id | name     | count | updated_name |
	| 23 | 'Test23' | 23    | 'Denchik'    |
