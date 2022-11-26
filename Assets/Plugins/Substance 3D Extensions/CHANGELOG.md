# Changelog
https://github.com/Synthoid/substance-for-unity-extensions

All notable changes to the Substance3D For Unity Extensions package will be documented here.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/)

## [Unreleased]

## [0.2.2]

### Added
- README file to project folder.
- `ISubstanceInputParameter` and `ISubstanceInputParameterValue` interfaces.
- Timeline support for setting substance native graph values and rendering substances.
- Example scene comprised entirely of runtime substance materials.
- Example scene showcasing Timeline functionality.
- Custom importer to force reimport substances when an `.sbsar` file is updated.

### Changed
- Extension methods previously using `SubstanceParameter` and `SubstanceParameterValue` arguments now use `ISubstanceInputParameter` and `ISubstanceInputParameterValue` arguments.
- `SubstanceNativeGraphExtensions.SetInputTextureGPUAsync()` can now accept any `Texture` value, not just `Texture2D` values.
- `SubstanceInputTypeFilterAttribute` can now be applied to `SubstanceParameterValue` fields to limit selectable inputs by value type.

## Deprecated
- `SubstanceParameter` and `SubstanceParameterValue` `Type` properties are now deprecated. Use `ValueType` instead.
- `SubstanceGraphSO` `SetInputs()` extension method. Should use `SetValues()` extension methods instead.

## [0.2.1] - 2022-11-21

### Added
- `SubstanceGraphRuntimeExtensions.UpdateOutputTextureSizes()` method to handle resizing output textures when $outputsize has been changed.
- Advanced runtime example now uses a custom control prefab for output size inputs.
- Texture control for advanced runtime example.
- Set $outputsize example scene showcasing several ways to set a substance's output size input.

### Changed
- `SubstanceGraphRuntimeExtension`s' `Render()` and `RenderAsync()` methods now account for $outputsize changes and properly resize output textures as needed.
- `SubstanceParameterValue` `SetValue(SubstanceNativeGraph)` and `SetValueAsync(SubstanceNativeGraph)` methods no longer return bool values.
- Moved texture input SetTexture and GetTexture extension methods to SubstanceInputExtensions.

### Fixed
- Advanced runtime example multi-input controls now properly populate values.

## [0.2.0] - 2022-11-17

### Added
- Extension methods for `SubstanceGraphSO` and `SubstanceNativeGraph` to streamline runtime rendering.
- Extension methods for `SubstanceNativeGraph` to set Texture input values synchronously with CPU textures (read/write enabled) and asynchronously with GPU textures.
- Convenience extension methods for working with `Vector4Int` values in native graph and input classes.
- `TransformMatrixAttribute` for drawing Vector4 fields in a similar manner to SubstanceDesigner's matrix attribute fields.
- `FloatExtensions` and `VectorExtensions` classes.
- `Vector4Int` constructor that accepts an array of ints.
- `RuntimeGraphOnlyAttribute` to show a warning when referencing SubstanceGraphSO assets that are not marked as runtime only.
- Example scenes showcasing extension code functionality.
- Unit tests for interacting with substance assets and runtime graphs.

### Changed
- Updated supported plugin version to 3.4.0 release.
- Renamed root "Substance Extensions" folder to "Substance 3D Extensions"
- `SubstanceParameter` and `SubstanceOutput` now target `SubstanceGraphSO` instead of `SubstanceFileSO` assets.
- `SubstanceParameter` and `SubstanceOutput` popup search windows now display the name of the graph being interacted with.
- `SubstanceParameter` and `SubstanceParameterValue` now display input types and identifiers as part of their labels.
- `SubstanceOutput` now displays output identifier and channel information as part of its labels.
- Moved existing test substances to Tests folder.
- None options in controls are now stylized as "<None>" to separate them from possible actual values called "None".
- Renamed `SubstanceGraphExtensions.GetOutputMap()` to `GetOutputTexture()`.

### Fixed
- `SubstanceParameterValue` inspector for Int3 values now uses XYZ labels instead of XYW.
- Inspector indenting no longer breaks `SubstanceParameter` or `SubstanceOutput` inspector visuals.

### Removed
- SubstanceFileExtensions.cs as its functionality should be instead accessed through SubstanceGraphExtensions.cs now.

## [0.1.1] - 2022-06-09

### Added
- SubstanceParameter, SubstanceParameterValue, and SubstanceOutput structs now have an EditorAsset property for convenient access to targeted SubstanceFileSO asset in the editor.
- SubstanceExtensionsRuntimeUtility for containing runtime utility and convenience methods.

### Changed
- SubstanceParameter and SubstanceOutput now use a popup search window when selecting values.
- SubstanceOutput now displays graph instance names instead of index values for its labels' root section (ie "graph_01/Test" vs "01/Test").
- Moved engine and plugin name, guid, and path retrieval methods from SubstanceExtensionsEditorUtility to new SubstanceExtensionsRuntimeUtilty class.

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
