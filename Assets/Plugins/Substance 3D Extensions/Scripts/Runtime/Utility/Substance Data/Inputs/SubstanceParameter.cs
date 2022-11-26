using UnityEngine;
using Adobe.Substance;
using Adobe.Substance.Input;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace SOS.SubstanceExtensions
{
    /// <summary>
    /// Convenience struct that allows easy selection of <see cref="SubstanceGraphSO"/> input parameters in the inspector.
    /// </summary>
    [System.Serializable]
    public struct SubstanceParameter : ISubstanceInputParameter
    {
        #region Fields

        /// <summary>
        /// GUID for the parent <see cref="SubstanceGraphSO"/> asset containing the target parameter.
        /// </summary>
        [SerializeField]
        private string guid;
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
        [SerializeField, UnityEngine.Serialization.FormerlySerializedAs("type")]
        private SubstanceValueType valueType;
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
        /// [Editor Only] GUID for the <see cref="SubstanceGraphSO"/> targeted by this parameter. Primarily used for editor tooling, not used at runtime.
        /// </summary>
        public string GUID
        {
            get { return guid; }
        }

        public string Name
        {
            get { return name; }
        }
        
        public int Index
        {
            get { return index; }
        }


#if UNITY_EDITOR
        /// <summary>
        /// [Editor Only] <see cref="SubstanceGraphSO"/> asset referenced for input parameters.
        /// </summary>
        public SubstanceGraphSO EditorAsset
        {
            get { return AssetDatabase.LoadAssetAtPath<SubstanceGraphSO>(AssetDatabase.GUIDToAssetPath(guid)); }
        }
#endif

#if UNITY_EDITOR
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
#endif
        [System.Obsolete("Use ValueType instead.")]
        public SubstanceValueType Type
        {
            get { return valueType; }
        }

        public SubstanceValueType ValueType
        {
            get { return valueType; }
        }

        public SubstanceWidgetType WidgetType
        {
            get { return widgetType; }
        }

        public Vector4 RangeMin
        {
            get { return rangeMin; }
        }

        public Vector4 RangeMax
        {
            get { return rangeMax; }
        }

        public Vector4Int RangeIntMin
        {
            get { return rangeIntMin; }
        }

        public Vector4Int RangeIntMax
        {
            get { return rangeIntMax; }
        }

        #endregion

        #region Constructors

        #region Floats

        public SubstanceParameter(int index, float min, float max, string name="", string graphGuid="") : this(index, SubstanceValueType.Float, name, graphGuid, "", SubstanceWidgetType.Slider, new Vector4(min, 0f, 0f, 0f), new Vector4(max, 0f, 0f, 0f), default, default)
        {

        }

        public SubstanceParameter(int index, Vector2 min, Vector2 max, string name="", string graphGuid="") : this(index, SubstanceValueType.Float2, name, graphGuid, "", SubstanceWidgetType.Slider, min, max, default, default)
        {

        }

        public SubstanceParameter(int index, Vector3 min, Vector3 max, string name="", string graphGuid="") : this(index, SubstanceValueType.Float3, name, graphGuid, "", SubstanceWidgetType.Slider, min, max, default, default)
        {

        }

        public SubstanceParameter(int index, Vector4 min, Vector4 max, string name="", string graphGuid="") : this(index, SubstanceValueType.Float4, name, graphGuid, "", SubstanceWidgetType.Slider, min, max, default, default)
        {

        }

        #endregion

        #region Ints

        public SubstanceParameter(int index, int min, int max, string name="", string graphGuid="") : this(index, SubstanceValueType.Int, name, graphGuid, "", SubstanceWidgetType.Slider, default, default, new Vector4Int(min), new Vector4Int(max))
        {

        }

        public SubstanceParameter(int index, Vector2Int min, Vector2Int max, string name="", string graphGuid="") : this(index, SubstanceValueType.Int2, name, graphGuid, "", SubstanceWidgetType.Slider, default, default, new Vector4Int(min.x, min.y), new Vector4Int(max.x, max.y))
        {

        }

        public SubstanceParameter(int index, Vector3Int min, Vector3Int max, string name="", string graphGuid="") : this(index, SubstanceValueType.Int3, name, graphGuid, "", SubstanceWidgetType.Slider, default, default, new Vector4Int(min.x, min.y, min.z), new Vector4Int(max.x, max.y, max.z))
        {

        }

        public SubstanceParameter(int index, Vector4Int min, Vector4Int max, string name="", string graphGuid="") : this(index, SubstanceValueType.Int4, name, graphGuid, "", SubstanceWidgetType.Slider, default, default, min, max)
        {

        }

        #endregion

        public SubstanceParameter(int index, SubstanceValueType valueType, string name, string graphGuid="") : this(index, valueType, name, graphGuid, "", SubstanceWidgetType.NoWidget, default, default, default, default)
        {

        }

        public SubstanceParameter(int index, SubstanceValueType valueType, string name="", string graphGuid="", string guid="", SubstanceWidgetType widgetType=SubstanceWidgetType.NoWidget, Vector4 sliderMin=default, Vector4 sliderMax=default, Vector4Int sliderIntMin=default, Vector4Int sliderIntMax=default)
        {
            this.guid = guid;
            this.name = name;
            this.index = index;
            this.valueType = valueType;
            this.widgetType = widgetType;
            this.rangeMin = sliderMin;
            this.rangeMax = sliderMax;
            this.rangeIntMin = sliderIntMin;
            this.rangeIntMax = sliderIntMax;
        }

        #endregion
    }
}