using UnityEngine;
using Adobe.Substance;
using Adobe.Substance.Input;

namespace SOS.SubstanceExtensions
{
    [System.Serializable]
    public struct SubstanceParameter
    {
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

        public string GUID
        {
            get { return guid; }
        }


        public string Name
        {
            get { return name; }
        }


        public int GraphId
        {
            get { return graphId; }
        }


        public int Index
        {
            get { return index; }
        }

        public SubstanceValueType Type
        {
            get { return type; }
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

        /// <summary>
        /// Returns the input on the given <see cref="SubstanceMaterialInstanceSO"/> targeted by this parameter.
        /// </summary>
        /// <param name="graph"><see cref="SubstanceMaterialInstanceSO"/> to obtain the target input from.</param>
        public ISubstanceInput GetInput(SubstanceMaterialInstanceSO graph)
        {
            return graph.Graphs[GraphId].Input[Index];
        }

        /// <summary>
        /// Returns the input on the given <see cref="SubstanceMaterialInstanceSO"/> targeted by this parameter.
        /// </summary>
        /// <typeparam name="T">Expected type for the input data.</typeparam>
        /// <param name="graph"><see cref="SubstanceMaterialInstanceSO"/> to obtain the target input from.</param>
        public T GetInput<T>(SubstanceMaterialInstanceSO graph) where T : ISubstanceInput
        {
            return (T)GetInput(graph);
        }
    }
}