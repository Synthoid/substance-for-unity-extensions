# SubstanceParameterValue
Handles referencing a Substance input and displaying an appropriate value field in the inspector. The value field shown will automatically adjust to match the target input's desired widget. This includes dropdowns when targeting enum int inputs, sliders when targeting clamped float inputs, color fields when targeting Float3 or Float4 color inputs, etc. `$randomseed` and `$outputsize` inputs also have special value fields.

<picture>
  <img alt="SubstanceParameterValues displaying various input controls." src="/docs/img/Inspectors/SubstanceParameterValue02.png" width="354" height="540">
</picture>

***Example Script***
```C#
using UnityEngine;
using Adobe.Substance;
using SOS.SubstanceExtensions;

public class SubstanceParameterValueExample : MonoBehaviour
{
    [Tooltip("Input to reference.")]
    public SubstanceParameterValue input = new SubstanceParameterValue();
}
```