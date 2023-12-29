Feature: UpdateWidgetTests
In order to ensure Widgets work correctly
    As a user
    I want to run widget tests

Scenario Outline: Update a widget
    Given I have widget update test data for widget id <id>
    When I retrieve the widget by ID
    And I update that widget by adding a unique description
    Then the widget response should match the expected result for PUT Widget By Id

    Examples:
    | id  |
    | 139401  |
    | 140946  |
    | 140939  |
    | 140906  |
    | 140937  |
