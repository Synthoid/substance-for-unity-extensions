# ISubstanceInputParameter
Interface implemented by classes containing data for substance graph inputs.

## See Also

 - [SubstanceParameter](/docs/scripting/Utility/SubstanceParameter.md)

## Properties

| Property | Type | Description |
| -------- | ---- | ----------- |
| Name | string | Name for the input parameter. This is based on the input's identifier value. |
| Index | int | Index for the input parameter on the target graph. |
| ValueType | `SubstanceValueType` | Value type for the parameter. |
| WidgetType | `SubstanceWidgetType` | Inspector widget used for the input parameter. This is primarily used for tooling purposes. |
| RangeMin | `Vector4` | Min slider values used for float parameters. X is used for Float parameters. X, Y for Float2, etc. |
| RangeMax | `Vector4` | Max slider values used for float parameters. X is used for Float parameters. X, Y for Float2, etc. |
| RangeIntMin | `Vector4Int` | Min slider values used for integer parameters. X is used for Int parameters. X, Y for Int2, etc. |
| RangeIntMax | `Vector4Int` | Max slider values used for integer parameters. X is used for Int parameters. X, Y for Int2, etc. |