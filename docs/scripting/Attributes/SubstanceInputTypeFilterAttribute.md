# SubstanceInputTypeFilterAttribute
Optional attribute for `SubstanceParameter` and `SubstanceParameterValue` fields that can filter selectable inputs. This makes it possible to limit what kinds of inputs are selectable (ie only allow selection of `Int` and `Int2` inputs).

***SubstanceParameter fields with different filter attributes***

<picture>
  <img alt="SubstanceInputTypeFilter fields" src="/docs/img/Inspectors/Attributes/AttributeInputFilter01.png" width="354" height="92">
</picture>

***SubstanceParameter search window when no filter is applied***

<picture>
  <img alt="SubstanceInputTypeFilter fields" src="/docs/img/Inspectors/Attributes/AttributeInputFilter02.png" width="354" height="396">
</picture>

***SubstanceParameter search window when only Int, Int2, Int3, and Int4 inputs are allowed***

<picture>
  <img alt="SubstanceInputTypeFilter fields" src="/docs/img/Inspectors/Attributes/AttributeInputFilter03.png" width="354" height="396">
</picture>

***SubstanceParameter search window when only Image inputs are allowed***

<picture>
  <img alt="SubstanceInputTypeFilter fields" src="/docs/img/Inspectors/Attributes/AttributeInputFilter04.png" width="354" height="396">
</picture>

***Example Script***
```C#
using UnityEngine;
using Adobe.Substance;
using SOS.SubstanceExtensions;

public class SubstanceInputTypeFilterAttributeExample : MonoBehaviour
{
    [Header("SubstanceParameter Filtering")]
    [Tooltip("Standard SubstanceParameter that can select all substance inputs.")]
    public SubstanceParameter allInputs = new SubstanceParameter();
    [SubstanceInputTypeFilter(SbsInputTypeFilter.Int | SbsInputTypeFilter.Int2 | SbsInputTypeFilter.Int3 | SbsInputTypeFilter.Int4), Tooltip("SubstanceParameter that can select only int, int2, int3, and int4 substance inputs.")]
    public SubstanceParameter intInputs = new SubstanceParameter();
    [SubstanceInputTypeFilter(SbsInputTypeFilter.Image), Tooltip("SubstanceParameter that can select only image substance inputs.")]
    public SubstanceParameter imageInputs = new SubstanceParameter();
    [Header("SubstanceParameterValue Filtering")]
    [Tooltip("Standard SubstanceParameterValue that can select all substance inputs.")]
    public SubstanceParameterValue allInputValues = new SubstanceParameterValue();
    [SubstanceInputTypeFilter(SbsInputTypeFilter.Int | SbsInputTypeFilter.Int2 | SbsInputTypeFilter.Int3 | SbsInputTypeFilter.Int4), Tooltip("SubstanceParameterValue that can select only int, int2, int3, and int4 substance inputs.")]
    public SubstanceParameterValue intInputValues = new SubstanceParameterValue();
    [SubstanceInputTypeFilter(SbsInputTypeFilter.Image), Tooltip("SubstanceParameterValue that can select only image substance inputs.")]
    public SubstanceParameterValue imageInputValues = new SubstanceParameterValue();
}
```