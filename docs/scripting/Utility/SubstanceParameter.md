# SubstanceParameter
Allows quick and easy selection of Substance inputs via the inspector. If an input is part of a group, it will be nested under the appropriate group name. This class stores information on the targeted input for use when getting or setting values. Parameter labels display information on the target input's identifier and type to easily tell what the input is at a glance.

<picture>
  <img alt="SubstanceParameter search window" src="/docs/img/Inspectors/SubstanceParameter01.png" width="354" height="448">
</picture>

***Example Script***
```C#
using UnityEngine;
using Adobe.Substance;
using SOS.SubstanceExtensions;

public class SubstanceParameterExample : MonoBehaviour
{
    [Tooltip("Input to reference.")]
    public SubstanceParameter input = new SubstanceParameter();
}
```

## See Also

 - [ISubstanceInputParameter](/docs/scripting/Interfaces/ISubstanceInputParameter.md)
 - [SubstanceParameterValue](/docs/scripting/Utility/SubstanceParameterValue.md)

## Properties

| Property | Type | Description |
| -------- | ---- | ----------- |
| GUID | string | Unity project GUID for the `SubstanceGraphSO` targeted by this parameter. Primarily used for editor tooling, not used at runtime. |
| Name | string | Name for the input parameter. This is based on the input's identifier value. |
| Index | int | Index for the input parameter on the target graph. |
| ValueType | `SubstanceValueType` | Value type for the parameter. |
| WidgetType | `SubstanceWidgetType`  | Inspector widget used for the input parameter. This is primarily used for tooling purposes. |
| RangeMin | `Vector4` | Min slider values used for float parameters. X is used for Float parameters. X, Y for Float2, etc. |
| RangeMax | `Vector4` | Max slider values used for float parameters. X is used for Float parameters. X, Y for Float2, etc. |
| RangeIntMin | `Vector4Int` | Min slider values used for integer parameters. X is used for Int parameters. X, Y for Int2, etc. |
| RangeIntMax | `Vector4Int` | Max slider values used for integer parameters. X is used for Int parameters. X, Y for Int2, etc. |
| EditorAsset | `SubstanceGraphSO`  | [Editor Only] `SubstanceGraphSO` asset referenced for input parameters. |