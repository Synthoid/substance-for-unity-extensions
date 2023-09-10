# Documentation
Welcome to the Substance 3D for Unity Extensions documentation page! This page contains information on various classes that extend the core functionality of the Substance 3D for Unity plugin. All extension and utility classes are contained in the `SOS.SubstanceExtensions.Runtime` assembly definition and the `SOS.SubstanceExtensions` namespace.

Summaries for extensions and added functionality are listed below, as are links to more in depth documentation.

## Utility
These classes make referencing and working with substance values easier.

| Class | Description |
| ----- | ----------- |
| [SubstanceParameter](scripting/Utility/SubstanceParameter.md) | Allows quick and easy selection of Substance inputs via the inspector. |
| [SubstanceParameterValue](scripting/Utility/SubstanceParameterValue.md) | Handles referencing a Substance input and displaying an appropriate value field in the inspector. |
| [SubstanceOutput](scripting/Utility/SubstanceOutput.md) | Allows easy selection of Substance output textures via the inspector. |
| [SceneSubstanceGraphData](scripting/Utility/SceneSubstanceGraphData.md) | Handles referencing substance graphs that are used in open scenes. |

## Extensions
These classes streamline substance runtime and editor functionality.

| Class | Description |
| ----- | ----------- |
| [SubstanceGraphRuntimeExtensions](scripting/Extensions/SubstanceGraphRuntimeExtensions.md) | Contains extension methods for `SubstanceGraphSO` runtime functionality. |

## Attributes
These attributes streamline working with substance assets in your inspectors.

| Class | Description |
| ----- | ----------- |
| [RuntimeGraphOnlyAttribute](scripting/Attributes/RuntimeGraphOnlyAttribute.md) | Displays a warning if a referenced `SubstanceGraphSO` asset is not marked as runtime only. |
| [TransformMatrixAttribute](scripting/Attributes/TransformMatrixAttribute.md) | Draws a `Vector4` field with similar controls to Substance Designer's transform matrix fields. |
| [SubstanceInputTypeFilterAttribute](scripting/Attributes/SubstanceInputTypeFilterAttribute.md) | Filters selectable inputs for `SubstanceParameter` fields. |

## Engine
These classes streamline interactions with the core Substance `Engine` class.

| Class | Description |
| ----- | ----------- |
| [SubstanceEngineManager](scripting/Engine/SubstanceEngineManager.md) | Wraps the Substance plugin's `Engine` class and streamlines interactions with it. |

## Package Extensions

Extended functionality to support various Unity packages.

 - [Timeline](extensions/timeline/index.md)

## Runtime
Several extension methods have been created to help make runtime Substance rendering easier to work with. 

In depth documentation on working with runtime materials can be viewed [here](scripting/Runtime.md).

**See the various runtime example scenes for more details on how these methods can be used.**