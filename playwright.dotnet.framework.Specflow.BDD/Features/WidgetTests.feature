Feature: WidgetTests
    In order to ensure Widgets work correctly
    As a user
    I want to run widget tests
Background: 
    Given I print "test has started" before test execution

Rule: All tests should pass
Scenario Outline: Get widget by ID
    Given I have widget test data for ID <id>
    When I retrieve the widget by ID
    Then the widget response should match the expected result for GET Widget By Id

    Examples:
    | id  |
    | 139396  |
    | 139397  |
    | 139398  |
    | 139399  |
    | 139400  |

Scenario Outline: Create a widget
    Given I have widget creation test data for widget named <widgetName>
    When I create a new widget
    But something happens
    Then the widget response should contain an Id number

    Examples:
    | widgetName  |
    | "TEST WIDGET ONE"  |
    | "TEST WIDGET TWO"  |
    | "TEST WIDGET THREE"  |
    | "TEST WIDGET FOUR"  |
    | "TEST WIDGET FIVE"  |
