using UnityEngine;
using System.Threading.Tasks;
using Adobe.Substance;
using Adobe.Substance.Input;

namespace SOS.SubstanceExtensions
{
    /// <summary>
    /// Interface implemented by data classes allowing substance input parameter values to be set up in the inspector.
    /// </summary>
    public interface ISubstanceInputParameterValue
    {
        #region Properties

        /// <summary>
        /// Parameter being referenced.
        /// </summary>
        ISubstanceInputParameter Parameter { get; }
        /// <summary>
        /// Name for the input parameter associated with this value.
        /// </summary>
        string Name { get; }
        /// <summary>
        /// Index for the input parameter associated with this value.
        /// </summary>
        int Index { get; }
        /// <summary>
        /// Value type for the input parameter associated with this value.
        /// </summary>
        SubstanceValueType ValueType { get; }
        /// <summary>
        /// Inspector widget used for the input parameter associated with this value.
        /// </summary>
        SubstanceWidgetType WidgetType { get; }

        /// <summary>
        /// Bool value for the target input parameter.
        /// </summary>
        bool BoolValue { get; set; }
        /// <summary>
        /// Int value for the target input parameter.
        /// </summary>
        int IntValue { get; set; }
        /// <summary>
        /// Int2 value for the target input parameter.
        /// </summary>
        Vector2Int Int2Value { get; set; }
        /// <summary>
        /// Int3 value for the target input parameter.
        /// </summary>
        Vector3Int Int3Value { get; set; }
        /// <summary>
        /// Int4 value for the target input parameter.
        /// </summary>
        Vector4Int Int4Value { get; set; }
        /// <summary>
        /// Float value for the target input parameter.
        /// </summary>
        float FloatValue { get; set; }
        /// <summary>
        /// Float2 value for the target input parameter.
        /// </summary>
        Vector2 Float2Value { get; set; }
        /// <summary>
        /// Float3 value for the target input parameter.
        /// </summary>
        Vector3 Float3Value { get; set; }
        /// <summary>
        /// Float4 value for the target input parameter.
        /// </summary>
        Vector4 Float4Value { get; set; }
        /// <summary>
        /// Color value for the target input parameter.
        /// </summary>
        Color ColorValue { get; set; }
        /// <summary>
        /// String value for the target input parameter.
        /// </summary>
        string StringValue { get; set; }
        /// <summary>
        /// Texture value for the target input parameter.
        /// </summary>
        Texture2D TextureValue { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Returns the input on the given <see cref="SubstanceGraphSO"/> targeted by this parameter.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to obtain the target input from.</param>
        ISubstanceInput GetInput(SubstanceGraphSO substance);

        /// <summary>
        /// Returns the input on the given <see cref="SubstanceGraphSO"/> targeted by this parameter.
        /// </summary>
        /// <typeparam name="T">Expected type for the input data.</typeparam>
        /// <param name="substance"><see cref="SubstanceGraphSO"/> to obtain the target input from.</param>
        T GetInput<T>(SubstanceGraphSO substance) where T : ISubstanceInput;

        /// <summary>
        /// Update the given handler with this parameter's values.
        /// Note: When setting texture values, it is recommended to use <see cref="SetValueAsync"/>, otherwise your texture must be read/writable.
        /// </summary>
        /// <param name="nativeGraph">Runtime graph to update values on.</param>
        void SetValue(SubstanceNativeGraph nativeGraph);

        /// <summary>
        /// Update the given substance with this parameter's values.
        /// </summary>
        /// <param name="substance">Substance to update values on.</param>
        /// <returns>True if the substance has the target parameter set.</returns>
        bool SetValue(SubstanceGraphSO substance);

        /// <summary>
        /// Asynchronously update the given handler with this parameter's values.
        /// </summary>
        /// <param name="nativeGraph">Runtime graph to update values on.</param>
        /// <returns>Task representing the set operation. Only texture assignments should require asynchronous execution, all other value types are set instantly.</returns>
        Task SetValueAsync(SubstanceNativeGraph nativeGraph);

        #endregion
    }
}