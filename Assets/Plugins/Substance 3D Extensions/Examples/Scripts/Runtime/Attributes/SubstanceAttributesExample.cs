using UnityEngine;
using Adobe.Substance;

namespace SOS.SubstanceExtensions.Examples
{
    /// <summary>
    /// Example class showcasing various attributes for working with substances.
    /// </summary>
    public class SubstanceAttributesExample : MonoBehaviour
    {
        [Header("Runtime Graph Only")]
        [RuntimeGraphOnly, Tooltip("SubstanceGraphSO marked as runtime only. This will display no warnings.")]
        public SubstanceGraphSO runtimeGraph = null;
        [RuntimeGraphOnly, Tooltip("SubstanceGraphSO not marked as runtime only. This displays a default warning.")]
        public SubstanceGraphSO staticGraph = null;
        [RuntimeGraphOnly("You can customize warnings too!"), Tooltip("SubstanceGraphSO not marked as runtime only. This displays a custom warning.")]
        public SubstanceGraphSO staticGraphCustom = null;
        [Header("Transform Matrix")]
        [TransformMatrix, Tooltip("Matrix control for a Vector4 field.")]
        public Vector4 matrix = new Vector4(1f, 0f, 0f, 1f);
    }
}