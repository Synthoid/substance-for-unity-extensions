# Runtime
Several extension methods have been created to help make runtime Substance rendering easier to work with. Runtime rendering involves obtaining a `SubstanceNativeGraph` for a `SubstanceGraphSO` asset, setting input values on it, rendering the native graph and updating the subtance graph with the result, then disposing of that native graph when you are done rendering. To streamline this process, there are two main extension methods to use:

  - `SubstanceGraphExtensions.BeginRuntimeEditing();`
    - Handles initializing the Substance graph asset for runtime and returns the generated `SubstanceNativeGraph` reference.
    - This method caches native graphs so they can be reused easily.
  - `SubstanceGraphExtensions.EndRuntimeEditing();`
    - Removes the Substance graph's native graph from the cache and disposes of it. This frees up resources that were allocated for rendering.

After calling `substanceGraph.BeginRuntimeEditing();` you can set input values on the native graph before calling either `substanceGraph.Render(nativerGraph)` or `substanceGraph.RenderAsync(nativeGraph)` to render the Substance using the new input values. Once your render is complete, you can then call `substanceGraph.EndRuntimeEditing()` to dispose of the native graph, or you can keep a reference to it if you are going to rerender the Substance graph again.

**See the various runtime example scenes for more details on how these methods can be used.**