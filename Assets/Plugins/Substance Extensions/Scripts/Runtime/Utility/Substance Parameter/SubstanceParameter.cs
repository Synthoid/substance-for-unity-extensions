using UnityEngine;
using Adobe.Substance;

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

        public SubstanceParameter(int index, SubstanceValueType type, string name="", string guid="", SubstanceWidgetType widgetType=SubstanceWidgetType.NoWidget)
        {
            this.guid = guid;
            this.name = name;
            this.index = index;
            this.type = type;
            this.widgetType = widgetType;
        }

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

        public SubstanceValueType Type
        {
            get { return type; }
        }

        public SubstanceWidgetType WidgetType
        {
            get { return widgetType; }
        }
    }
}