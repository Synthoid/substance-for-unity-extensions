using UnityEngine;
using Adobe.Substance;

namespace SOS.SubstanceExtensions.Timeline
{
    /// <summary>
    /// Version of <see cref="SubstanceParameter"/> that uses a Timeline track binding as the target substance.
    /// </summary>
    [System.Serializable]
    public struct SubstanceBindingParameter
    {
        /// <summary>
        /// Name for the target parameter.
        /// </summary>
        [SerializeField]
        private string name;
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

        /// <summary>
        /// Name for the input parameter. This is based on the input's identifier value.
        /// </summary>
        public string Name
        {
            get { return name; }
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



        #region Constructors

        #region Floats

        public SubstanceBindingParameter(int index, float min, float max, string name = "") : this(index, SubstanceValueType.Float, name, SubstanceWidgetType.Slider, new Vector4(min, 0f, 0f, 0f), new Vector4(max, 0f, 0f, 0f), default, default)
        {

        }

        public SubstanceBindingParameter(int index, Vector2 min, Vector2 max, string name = "") : this(index, SubstanceValueType.Float2, name, SubstanceWidgetType.Slider, min, max, default, default)
        {

        }

        public SubstanceBindingParameter(int index, Vector3 min, Vector3 max, string name = "") : this(index, SubstanceValueType.Float3, name, SubstanceWidgetType.Slider, min, max, default, default)
        {

        }

        public SubstanceBindingParameter(int index, Vector4 min, Vector4 max, string name = "") : this(index, SubstanceValueType.Float4, name, SubstanceWidgetType.Slider, min, max, default, default)
        {

        }

        #endregion

        #region Ints

        public SubstanceBindingParameter(int index, int min, int max, string name = "") : this(index, SubstanceValueType.Int, name, SubstanceWidgetType.Slider, default, default, new Vector4Int(min), new Vector4Int(max))
        {

        }

        public SubstanceBindingParameter(int index, Vector2Int min, Vector2Int max, string name = "") : this(index, SubstanceValueType.Int2, name, SubstanceWidgetType.Slider, default, default, new Vector4Int(min.x, min.y), new Vector4Int(max.x, max.y))
        {

        }

        public SubstanceBindingParameter(int index, Vector3Int min, Vector3Int max, string name = "") : this(index, SubstanceValueType.Int3, name, SubstanceWidgetType.Slider, default, default, new Vector4Int(min.x, min.y, min.z), new Vector4Int(max.x, max.y, max.z))
        {

        }

        public SubstanceBindingParameter(int index, Vector4Int min, Vector4Int max, string name = "") : this(index, SubstanceValueType.Int4, name, SubstanceWidgetType.Slider, default, default, min, max)
        {

        }

        #endregion

        public SubstanceBindingParameter(int index, SubstanceValueType type, string name) : this(index, type, name, SubstanceWidgetType.NoWidget, default, default, default, default)
        {

        }

        public SubstanceBindingParameter(int index, SubstanceValueType type, string name = "", SubstanceWidgetType widgetType = SubstanceWidgetType.NoWidget, Vector4 sliderMin = default, Vector4 sliderMax = default, Vector4Int sliderIntMin = default, Vector4Int sliderIntMax = default)
        {
            this.name = name;
            this.index = index;
            this.type = type;
            this.widgetType = widgetType;
            this.rangeMin = sliderMin;
            this.rangeMax = sliderMax;
            this.rangeIntMin = sliderIntMin;
            this.rangeIntMax = sliderIntMax;
        }

        #endregion
    }
}