using UnityEngine;
using Adobe.Substance;
using Adobe.Substance.Input;

namespace SOS.SubstanceExtensions
{
    /// <summary>
    /// Convenience struct that allows easy selection of <see cref="SubstanceMaterialInstanceSO"/> input parameters in the inspector.
    /// </summary>
    [System.Serializable]
    public struct SubstanceParameter
    {
        #region Fields

        /// <summary>
        /// GUID for the <see cref="SubstanceMaterialInstanceSO"/> asset containing the target parameter.
        /// </summary>
        [SerializeField]
        private string guid;
        /// <summary>
        /// Name for the target parameter.
        /// </summary>
        [SerializeField]
        private string name;
        /// <summary>
        /// Index for the graph associated with this parameter.
        /// </summary>
        [SerializeField]
        private int graphId;
        /// <summary>
        /// Index for the target parameter.
        /// </summary>
        [SerializeField]
        private int index;
        /// <summary>
        /// Value type for the parameter.
        /// </summary>
        [SerializeField]
        private SubstanceValueType type;
        /// <summary>
        /// Widget type for the parameter.
        /// </summary>
        [SerializeField]
        private SubstanceWidgetType widgetType;
        /// <summary>
        /// Min value for float sliders. Only applies when the widget type is set to <see cref="SubstanceWidgetType.Slider"/>.
        /// </summary>
        [SerializeField]
        private Vector4 rangeMin;
        /// <summary>
        /// Max value for float sliders. Only applies when the widget type is set to <see cref="SubstanceWidgetType.Slider"/>.
        /// </summary>
        [SerializeField]
        private Vector4 rangeMax;
        /// <summary>
        /// Min value for int sliders. Only applies when the widget type is set to <see cref="SubstanceWidgetType.Slider"/>.
        /// </summary>
        [SerializeField]
        private Vector4Int rangeIntMin;
        /// <summary>
        /// Max value for int sliders. Only applies when the widget type is set to <see cref="SubstanceWidgetType.Slider"/>.
        /// </summary>
        [SerializeField]
        private Vector4Int rangeIntMax;

        #endregion

        #region Properties

        /// <summary>
        /// [Editor Only] GUID for the <see cref="SubstanceMaterialInstanceSO"/> targeted by this parameter. Primarily used for editor tooling, not used at runtime.
        /// </summary>
        public string GUID
        {
            get { return guid; }
        }

        /// <summary>
        /// Name for the input parameter. This is based on the input's identifier value.
        /// </summary>
        public string Name
        {
            get { return name; }
        }

        /// <summary>
        /// Index for the graph containing the input parameter.
        /// </summary>
        public int GraphId
        {
            get { return graphId; }
        }

        /// <summary>
        /// Index for the input parameter on the target graph.
        /// </summary>
        public int Index
        {
            get { return index; }
        }

        /// <summary>
        /// Value type for the parameter.
        /// </summary>
        public SubstanceValueType Type
        {
            get { return type; }
        }

        /// <summary>
        /// Inspector widget used for the input parameter. This is primarily used for tooling purposes.
        /// </summary>
        public SubstanceWidgetType WidgetType
        {
            get { return widgetType; }
        }

        /// <summary>
        /// Min slider values used for float parameters. X is used for Float parameters. X, Y for Float2, etc.
        /// </summary>
        public Vector4 RangeMin
        {
            get { return rangeMin; }
        }

        /// <summary>
        /// Max slider values used for float parameters. X is used for Float parameters. X, Y for Float2, etc.
        /// </summary>
        public Vector4 RangeMax
        {
            get { return rangeMax; }
        }

        /// <summary>
        /// Min slider values used for integer parameters. X is used for Int parameters. X, Y for Int2, etc.
        /// </summary>
        public Vector4Int RangeIntMin
        {
            get { return rangeIntMin; }
        }

        /// <summary>
        /// Max slider values used for integer parameters. X is used for Int parameters. X, Y for Int2, etc.
        /// </summary>
        public Vector4Int RangeIntMax
        {
            get { return rangeIntMax; }
        }

        #endregion

        #region Constructors

        #region Floats

        public SubstanceParameter(int index, float min, float max, string name="", int graphId=0) : this(index, SubstanceValueType.Float, name, graphId, "", SubstanceWidgetType.Slider, new Vector4(min, 0f, 0f, 0f), new Vector4(max, 0f, 0f, 0f), default, default)
        {

        }

        public SubstanceParameter(int index, Vector2 min, Vector2 max, string name="", int graphId=0) : this(index, SubstanceValueType.Float2, name, graphId, "", SubstanceWidgetType.Slider, min, max, default, default)
        {

        }

        public SubstanceParameter(int index, Vector3 min, Vector3 max, string name="", int graphId=0) : this(index, SubstanceValueType.Float3, name, graphId, "", SubstanceWidgetType.Slider, min, max, default, default)
        {

        }

        public SubstanceParameter(int index, Vector4 min, Vector4 max, string name="", int graphId=0) : this(index, SubstanceValueType.Float4, name, graphId, "", SubstanceWidgetType.Slider, min, max, default, default)
        {

        }

        #endregion

        #region Ints

        public SubstanceParameter(int index, int min, int max, string name="", int graphId=0) : this(index, SubstanceValueType.Int, name, graphId, "", SubstanceWidgetType.Slider, default, default, new Vector4Int(min), new Vector4Int(max))
        {

        }

        public SubstanceParameter(int index, Vector2Int min, Vector2Int max, string name="", int graphId=0) : this(index, SubstanceValueType.Int2, name, graphId, "", SubstanceWidgetType.Slider, default, default, new Vector4Int(min.x, min.y), new Vector4Int(max.x, max.y))
        {

        }

        public SubstanceParameter(int index, Vector3Int min, Vector3Int max, string name="", int graphId=0) : this(index, SubstanceValueType.Int3, name, graphId, "", SubstanceWidgetType.Slider, default, default, new Vector4Int(min.x, min.y, min.z), new Vector4Int(max.x, max.y, max.z))
        {

        }

        public SubstanceParameter(int index, Vector4Int min, Vector4Int max, string name="", int graphId=0) : this(index, SubstanceValueType.Int4, name, graphId, "", SubstanceWidgetType.Slider, default, default, min, max)
        {

        }

        #endregion

        public SubstanceParameter(int index, SubstanceValueType type, string name, int graphId = 0) : this(index, type, name, graphId, "", SubstanceWidgetType.NoWidget, default, default, default, default)
        {

        }

        public SubstanceParameter(int index, SubstanceValueType type, string name="", int graphId=0, string guid="", SubstanceWidgetType widgetType=SubstanceWidgetType.NoWidget, Vector4 sliderMin=default, Vector4 sliderMax=default, Vector4Int sliderIntMin=default, Vector4Int sliderIntMax=default)
        {
            this.guid = guid;
            this.name = name;
            this.graphId = graphId;
            this.index = index;
            this.type = type;
            this.widgetType = widgetType;
            this.rangeMin = sliderMin;
            this.rangeMax = sliderMax;
            this.rangeIntMin = sliderIntMin;
            this.rangeIntMax = sliderIntMax;
        }

        #endregion

        /// <summary>
        /// Returns the input on the given <see cref="SubstanceMaterialInstanceSO"/> targeted by this parameter.
        /// </summary>
        /// <param name="substance"><see cref="SubstanceMaterialInstanceSO"/> to obtain the target input from.</param>
        public ISubstanceInput GetInput(SubstanceFileSO substance)
        {
            return substance.Instances[GraphId].Input[Index];
        }

        /// <summary>
        /// Returns the input on the given <see cref="SubstanceMaterialInstanceSO"/> targeted by this parameter.
        /// </summary>
        /// <typeparam name="T">Expected type for the input data.</typeparam>
        /// <param name="substance"><see cref="SubstanceMaterialInstanceSO"/> to obtain the target input from.</param>
        public T GetInput<T>(SubstanceFileSO substance) where T : ISubstanceInput
        {
            return (T)GetInput(substance);
        }
    }
}