# RuntimeGraphOnlyAttribute
Displays a warning if a referenced `SubstanceGraphSO` asset is not marked as runtime only. You can customize the shown warning if desired.

<picture>
  <img alt="RuntimeGraphOnly fields" src="/docs/img/Inspectors/Attributes/AttributeRuntimeGraphOnly.png" width="354" height="144">
</picture>

***Example Script***
```C#
using UnityEngine;
using Adobe.Substance;
using SOS.SubstanceExtensions;

public class RuntimeGraphOnlyAttributeExample : MonoBehaviour
{
    [RuntimeGraphOnly, Tooltip("SubstanceGraphSO marked as runtime only. This will display no warnings.")]
    public SubstanceGraphSO runtimeGraph = null;
    [RuntimeGraphOnly, Tooltip("SubstanceGraphSO not marked as runtime only. This displays a default warning.")]
    public SubstanceGraphSO staticGraph = null;
    [RuntimeGraphOnly("You can customize warnings too!"), Tooltip("SubstanceGraphSO not marked as runtime only. This displays a custom warning.")]
    public SubstanceGraphSO staticGraphCustom = null;
}
```