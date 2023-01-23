# ISubstanceProvider
Interface implemented by components and assets referencing `SubstanceGraphSO` assets. This is primarily useful when referencing substances that do not have their materials or textures directly referenced by objects in a scene. For example, when rendering a substance and assigning its outputs to custom materials at runtime.

## Public Methods

| Method | Description |
| ------ | ----------- |
| GetSubstances | Get an array of all referenced `SubstanceGraphSO` assets. |