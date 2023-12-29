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
    But I want to know what is the shortest string between <values>
    Then the widget response should match the expected result for GET Widget By Id

    Examples:
    | id     | values                  |
    | 139396 | one,three,courts,blorts |
    | 139397 | b,ab,abc,abcd           |
    | 139398 | one,three               |
    | 139399 | which,is,the,shortes    |
    | 139400 | this,one,is             |

Scenario Outline: Create a widget
    Given I have widget creation test data for widget named <widgetName>
    When I create a new widget
    But something happens
    | First thing | Second thing | Third Thing |
    | This happened first | This happened afterwards | This happened last |
    Then the widget response should contain an Id number

    Examples:
    | widgetName  |
    | "TEST WIDGET ONE"  |
    | "TEST WIDGET TWO"  |
    | "TEST WIDGET THREE"  |
    | "TEST WIDGET FOUR"  |
    | "TEST WIDGET FIVE"  |
