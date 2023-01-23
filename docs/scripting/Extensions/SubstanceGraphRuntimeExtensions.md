# SubstanceGraphRuntimeExtensions
Contains extension methods for `SubstanceGraphSO` runtime functionality.

## Methods

| Method | Returns | Description |
| ------ | ------- | ----------- |
| SetInputValues(this `SubstanceGraphSO`, `SubstanceNativeGraph`) | void | Set the input values on the graph asset to match the inputs on the given native graph. |
| ClearCachedGraphs() | void | Displose all cached runtime native graphs and clear the dictionary cache. |
| GetCachedNativeGraph(this `SubstanceGraphSO`, bool) | `SubstanceNativeGraph` | Get the cahced native graph associated with the given graph asset. |
| DisposeNativeGraph(this `SubstanceGraphSO`) | void | Dispose the cached native graph associated with the given graph. |
| DisposeNativeGraph(this `SubstanceGraphSO`, `SubstanceNativeGraph`) | void | Dispose the native graph associated with the given graph. |
| BeginRuntimeEditing(this `SubstanceGraphSO`) | `SubstanceNativeGraph` | Initialize the graph for runtime generation and create a cached native graph for use in render operations. Note: You should call `EndRuntimeEditing(SubstanceGraphSO, SubstanceNativeGraph)` when you are done rendering with the returned native graph. |
| EndRuntimeEditing(this `SubstanceGraphSO`) | void | Dispose the cached native graph associated with the given graph. |
| EndRuntimeEditing(this `SubstanceGraphSO`, `SubstanceNativeGraph`) | void | Dispose the native graph associated with the given graph. |
| Render(this `SubstanceGraphSO`) | void | Render the graph. If the graph has not been initialized for runtime, it will be initialized as part of this operation. |
| Render(this `SubstanceGraphSO`, `SubstanceNativeGraph`) | void | Render the graph with the given native graph handle. |
| RenderAsync(this `SubstanceGraphSO`) | `Task` | Asynchronously render the graph. |
| RenderAsync(this `SubstanceGraphSO`, `SubstanceNativeGraph`) | `Task` | Asynchronously render the graph using the given native graph. |
| UpdateOutputTextureSizes(this `SubstanceGraphSO`, `IntPtr`) | void | Wrapper for `SubstanceGraphSO.UpdateOutputTextures(IntPtr)` that will resize output textures if needed based on the given render result. |
| RenderAndForget(this `SubstanceGraphSO`) | void | Render the graph and immediately dispose of its native graph handle. This can be used when a graph only needs to render once and its handle doesn't need to be kept in memory. |
| RenderAndForgetAsync(this `SubstanceGraphSO`, `System.Action`) | void | Asynchronously render the graph and dispose of its native graph handle after. This can be used when a graph only needs to render once and its handle doesn't need to be kept in memory. |
| RenderAndForgetAsync(this `SubstanceGraphSO`) | `Task` | Asynchronously render the graph and dispose of its native graph handle after. This can be used when a graph only needs to render once and its handle doesn't need to be kept in memory. |