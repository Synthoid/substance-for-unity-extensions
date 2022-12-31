# Documentation
Welcome to the Substance 3D for Unity extensions repo documentation page! This page contains information on various classes that extend the core functionality of the Substance 3D for Unity plugin. All extension and utility classes are contained in the `SOS.SubstanceExtensions.Runtime` assembly definition and the `SOS.SubstanceExtensions` namespace.

## Utility
These scripts make referencing and working with substance values easier.

### SubstanceParameter
Allows quick and easy selection of Substance inputs via the inspector. If an input is part of a group, it will be nested under the appropriate group name. This class stores information on the targeted input for use when getting or setting values. Parameter labels display information on the target input's identifier and type to easily tell what the input is at a glance.

<picture>
  <img alt="SubstanceParameter seaerch window" src="img/Inspectors/SubstanceParameter01.png" width="354" height="448">
</picture>

### SubstanceParameterValue
Handles referencing a Substance input and displaying an appropriate value field in the inspector. The value field shown will automatically adjust to match the target input's desired widget. This includes dropdowns when targeting enum int inputs, sliders when targeting clamped float inputs, color fields when targeting Float3 or Float4 color inputs, etc. $randomseed and $outputsize inputs also have special value fields.

<picture>
  <img alt="SubstanceParameterValues displaying various input controls." src="img/Inspectors/SubstanceParameterValue02.png" width="354" height="540">
</picture>

### SubstanceOutput
Allows easy selection of Substance output textures via the inspector. The selected output texture's identifier as specified in Substance Designer (ie "basecolor") is stored.

<picture>
  <img alt="SubstanceOutput search window" src="img/Inspectors/SubstanceOutput01.png" width="354" height="352">
</picture>

## Runtime
Several extension methods have been created to help make runtime Substance rendering easier to work with. Runtime rendering involves obtaining a `SubstanceNativeGraph` for a `SubstanceGraphSO` asset, setting input values on it, rendering the native graph and updating the subtance graph with the result, then disposing of that native graph when you are done rendering. To streamline this process, there are two main extension methods to use:

  - `SubstanceGraphExtensions.BeginRuntimeEditing();`
    - Handles initializing the Substance graph asset for runtime and returns the generated `SubstanceNativeGraph` reference.
    - This method caches native graphs so they can be reused easily.
  - `SubstanceGraphExtensions.EndRuntimeEditing();`
    - Removes the Substance graph's native graph from the cache and disposes of it. This frees up resources that were allocated for rendering.

After calling `substanceGraph.BeginRuntimeEditing();` you can set input values on the native graph before calling either `substanceGraph.Render(nativerGraph)` or `substanceGraph.RenderAsync(nativeGraph)` to render the Substance using the new input values. Once your render is complete, you can then call `substanceGraph.EndRuntimeEditing()` to dispose of the native graph, or you can keep a reference to it if you are going to rerender the Substance graph again.

**See the various runtime example scenes for more details on how these methods can be used.**

## Attributes
These attributes can streamline working with substance asset in your inspectors.

### RuntimeGraphOnlyAttribute
Displays a warning if a referenced `SubstanceGraphSO` asset is not marked as runtime only. You can customize the shown warning if desired.

<picture>
  <img alt="RuntimeGraphOnly fields" src="img/Inspectors/Attributes/AttributeRuntimeGraphOnly.png" width="354" height="144">
</picture>

### TransformMatrixAttribute
Draws a `Vector4` field with similar controls to Substance Designer's transform matrix fields.

<picture>
  <img alt="RuntimeGraphOnly fields" src="img/Inspectors/Attributes/AttributeTransformMatrix01.png" width="354" height="228">
</picture>

***Standard Matrix View***

<picture>
  <img alt="RuntimeGraphOnly fields" src="img/Inspectors/Attributes/AttributeTransformMatrix02.png" width="354" height="120">
</picture>

***Raw Matrix View***

## Package Extensions

Extended functionality to support various Unity packages.

 - [Timeline](extensions/timeline/index.md)
