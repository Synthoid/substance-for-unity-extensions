# Changelog
All notable changes to the Substance3D For Unity Extensions package will be documented here.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/)

## Unreleased

## [0.1.1] - 2022-06-09

### Added
- SubstanceParameter, SubstanceParameterValue, and SubstanceOutput structs now have an EditorAsset property for convenient access to targeted SubstanceFileSO asset in the editor.

### Changed
- SubstanceParameter and SubstanceOutput now use a popup search window when selecting values.
- SubstanceOutput now displays graph instance names instead of index values for its labels' root section (ie "graph_01/Test" vs "01/Test").

### Fixed
- Index out of range exception when changing targeted Substance asset on SubstanceParameterValue fields to an asset with less graphs or inputs than the previous value.
- Null reference exception when SubstanceParameter field is viewed after its target substance is destroyed.
- Null reference exception when SubstanceParameterValue field is viewed after its target substance is destroyed.
- Null reference exception when SusbtanceOutput field is viewed after its target substance is destroyed.

## [0.1.0] - 2022-06-6

### Added
- Support for Substance3D for Unity v0.0.100. For 0.0.90 and older, use [0.0.9].

### Changed
- SubstanceParameter selection now use graph names for input path labels instead of generic index numbers.
- SubstanceParameter selection now properly indexes for inputs with shared names across graph instances.
- Renamed some extension classes to better match their corresponding classes in v0.0.100 of the plugin.

### Fixed
- Tooltip error in OutputSizeLabel.

## [0.0.9] - 2022-06-4
### Added
- Initial package commit.
