# Documentation
Welcome to the Substance 3D for Unity Extensions documentation page! This page contains information on various classes that extend the core functionality of the Substance 3D for Unity plugin. All extension and utility classes are contained in the `SOS.SubstanceExtensions.Runtime` assembly definition and the `SOS.SubstanceExtensions` namespace.

Summaries for extensions and added functionality are listed below, as are links to more in depth documentation.

## Utility
These classes make referencing and working with substance values easier.

| Class | Description |
| ----- | ----------- |
| (SubstanceParameter)[scripting/Utility/SubstanceParameter.md] | Allows quick and easy selection of Substance inputs via the inspector. |
| (SubstanceParameterValue)[scripting/Utility/SubstanceParameterValue.md] | Handles referencing a Substance input and displaying an appropriate value field in the inspector. |
| (SubstanceOutput)[scripting/Utility/SubstanceOutput.md] | Allows easy selection of Substance output textures via the inspector. |

## Attributes
These attributes can streamline working with substance asset in your inspectors.

| Class | Description |
| ----- | ----------- |
| (RuntimeGraphOnlyAttribute)[scripting/Attributes/RuntimeGraphOnlyAttribute.md] | Displays a warning if a referenced `SubstanceGraphSO` asset is not marked as runtime only. |
| (TransformMatrixAttribute)[scripting/Attributes/TransformMatrixAttribute.md] | Draws a `Vector4` field with similar controls to Substance Designer's transform matrix fields. |
| (SubstanceInputTypeFilterAttribute)[scripting/Attributes/SubstanceInputTypeFilterAttribute.md] | Filters selectable inputs for `SubstanceParameter` fields. |

## Package Extensions

Extended functionality to support various Unity packages.

 - [Timeline](extensions/timeline/index.md)

## Runtime
Several extension methods have been created to help make runtime Substance rendering easier to work with. Runtime rendering involves obtaining a `SubstanceNativeGraph` for a `SubstanceGraphSO` asset, setting input values on it, rendering the native graph and updating the subtance graph with the result, then disposing of that native graph when you are done rendering. To streamline this process, there are two main extension methods to use:

  - `SubstanceGraphExtensions.BeginRuntimeEditing();`
    - Handles initializing the Substance graph asset for runtime and returns the generated `SubstanceNativeGraph` reference.
    - This method caches native graphs so they can be reused easily.
  - `SubstanceGraphExtensions.EndRuntimeEditing();`
    - Removes the Substance graph's native graph from the cache and disposes of it. This frees up resources that were allocated for rendering.

After calling `substanceGraph.BeginRuntimeEditing();` you can set input values on the native graph before calling either `substanceGraph.Render(nativerGraph)` or `substanceGraph.RenderAsync(nativeGraph)` to render the Substance using the new input values. Once your render is complete, you can then call `substanceGraph.EndRuntimeEditing()` to dispose of the native graph, or you can keep a reference to it if you are going to rerender the Substance graph again.

**See the various runtime example scenes for more details on how these methods can be used.**