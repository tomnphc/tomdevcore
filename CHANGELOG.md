# Changelog
All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [1.0.0] - 2024-06-28

### Added
- Initial release of TomDev Core package
- TomDev.DataSO namespace with ScriptableObject data management
  - ScriptableDataBase: Base class with Saved and PrefKey functionality
  - ScriptableInt: Integer value storage with operator overloads
  - ScriptableFloat: Float value storage with operator overloads
  - ScriptableBool: Boolean value storage with operator overloads
  - ScriptableString: String value storage with helper methods
  - ScriptableCustom<T>: Generic class for custom data types
- Context menu "Check Existed Key" for PlayerPrefs validation
- Auto-save functionality when Saved is enabled
- Comprehensive documentation and examples
- PlayerData and GameSettings example implementations
- **Optional Odin Inspector Integration**
  - Enhanced inspector with buttons and validation
  - Conditional field display with ShowIf/HideIf
  - Property ordering and organization
  - Required field validation
  - Min/Max value constraints
  - Toggle buttons and read-only fields
  - Clear PlayerPrefs key functionality
  - **Action Buttons for all Scriptable Values**
    - Set Value, Reset to Default buttons
    - Increment/Decrement for numbers
    - Toggle, Set True/False for booleans
    - Clear, Append for strings
    - Check if Default for custom types 