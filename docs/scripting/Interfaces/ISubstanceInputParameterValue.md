# ISubstanceInputParameterValue
Interface implemented by classes containing data for substance graph inputs.

## See Also

 - [ISubstanceInputParameter](/docs/scripting/Interfaces/ISubstanceInputParameter.md)

## Properties

| Property | Type | Description |
| -------- | ---- | ----------- |
| Parameter | `ISubstanceInputParameter` | Parameter being referenced. |
| Name | string | Name for the input parameter associated with this value. |
| Index | int | Index for the input parameter associated with this value. |
| ValueType | `SubstanceValueType` | Value type for the input parameter associated with this value. |
| WidgetType | `SubstanceWidgetType` | Inspector widget used for the input parameter associated with this value. |
| BoolValue | bool | Bool value for the target input parameter. |
| IntValue | int | Int value for the target input parameter. |
| Int2Value | `Vector2Int` | Int2 value for the target input parameter. |
| Int3Value | `Vector3Int` | Int3 value for the target input parameter. |
| Int4Value | `Vector4Int` | Int4 value for the target input parameter. |
| FloatValue | float | Float value for the target input parameter. |
| Float2Value | `Vector2` | Float2 value for the target input parameter. |
| Float3Value | `Vector3` | Float3 value for the target input parameter. |
| Float4Value | `Vector4` | Float4 value for the target input parameter. |
| ColorValue | `Color` | Color value for the target input parameter. |
| StringValue | string | String value for the target input parameter. |
| TextureValue | `Texture2D` | Texture value for the target input parameter. |

## Methods

| Method | Returns | Description |
| ------ | ------- | ----------- |
| GetInput(SubstanceGraphSO) | `ISubstanceInput` | Returns the input on the given `SubstanceGraphSO` targeted by this parameter. |
| SetValue(SubstanceGraphSO) | bool | Update the given substance with this parameter's values. |
| SetValue(SubstanceNativeGraph) | void | Update the given handler with this parameter's values. |
| SetValueAsync(SubstanceNativeGraph) | `Task` | Asynchronously update the given handler with this parameter's values. Only texture assignments should require asynchronous execution, all other value types are set instantly. |
