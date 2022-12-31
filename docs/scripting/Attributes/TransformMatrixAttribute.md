# TransformMatrixAttribute
Draws a `Vector4` field with similar controls to Substance Designer's transform matrix fields.

***Standard Matrix View***

<picture>
  <img alt="TransformMatrix standard fields" src="/docs/img/Inspectors/Attributes/AttributeTransformMatrix01.png" width="354" height="228">
</picture>

***Raw Matrix View***

<picture>
  <img alt="TransformMatrix raw fields" src="/docs/img/Inspectors/Attributes/AttributeTransformMatrix02.png" width="354" height="120">
</picture>

***Example Script***
```C#
using UnityEngine;
using SOS.SubstanceExtensions;

public class TransformMatrixAttributeExample : MonoBehaviour
{
    [TransformMatrix, Tooltip("Matrix control for a Vector4 field.")]
    public Vector4 matrix = new Vector4(1f, 0f, 0f, 1f);
}
```