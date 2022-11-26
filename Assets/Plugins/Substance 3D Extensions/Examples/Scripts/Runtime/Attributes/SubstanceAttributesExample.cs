using UnityEngine;
using Adobe.Substance;

namespace SOS.SubstanceExtensions.Examples
{
    /// <summary>
    /// Example class showcasing various attributes for working with substances in the inspector.
    /// </summary>
    public class SubstanceAttributesExample : MonoBehaviour
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