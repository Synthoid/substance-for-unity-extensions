using UnityEngine;
using SOS.SubstanceExtensions;
using Adobe.Substance;

namespace SOS.SubstanceExtensions.Tests
{
    /// <summary>
    /// Asset containing values for <see cref="SubstanceGraphSO"/> unit tests.
    /// </summary>
    [CreateAssetMenu(fileName="Substance Graph Test Asset", menuName="SOS/Substance/Tests/Substance Graph")]
    public class SubstanceGraphTestAsset : UnitTestAsset
    {
        [SerializeField, Tooltip("SubstanceGraph used for testing get operations.")]
        private SubstanceGraphSO substance = null;
        //Defaults
        [SerializeField, Tooltip("Default set value for the target string input on the test substance.")]
        private SubstanceParameterValue defaultStringValue = new SubstanceParameterValue();
        [SerializeField, Tooltip("Default set value for the target bool input on the test substance.")]
        private SubstanceParameterValue defaultBoolValue = new SubstanceParameterValue();
        [SerializeField, Tooltip("Default set int value for the target enum input on the test substance.")]
        private SubstanceParameterValue defaultEnumValue = new SubstanceParameterValue();
        [SerializeField, Tooltip("Default set enum value for the target enum input on the test substance.")]
        private TestInputEnum defaultEnumCastValue = TestInputEnum.Black;
        [SerializeField, Tooltip("Default set value for the target int input on the test substance.")]
        private SubstanceParameterValue defaultIntValue = new SubstanceParameterValue();
        [SerializeField, Tooltip("Default set value for the target int2 input on the test substance.")]
        private SubstanceParameterValue defaultInt2Value = new SubstanceParameterValue();
        [SerializeField, Tooltip("Default set value for the target int3 input on the test substance.")]
        private SubstanceParameterValue defaultInt3Value = new SubstanceParameterValue();
        [SerializeField, Tooltip("Default set value for the target int4 input on the test substance.")]
        private SubstanceParameterValue defaultInt4Value = new SubstanceParameterValue();
        [SerializeField, Tooltip("Default set value for the target float input on the test substance.")]
        private SubstanceParameterValue defaultFloatValue = new SubstanceParameterValue();
        [SerializeField, Tooltip("Default set value for the target float2 input on the test substance.")]
        private SubstanceParameterValue defaultFloat2Value = new SubstanceParameterValue();
        [SerializeField, Tooltip("Default set value for the target float3 input on the test substance.")]
        private SubstanceParameterValue defaultFloat3Value = new SubstanceParameterValue();
        [SerializeField, Tooltip("Default set value for the target float4 input on the test substance.")]
        private SubstanceParameterValue defaultFloat4Value = new SubstanceParameterValue();
        [SerializeField, Tooltip("Default set value for the target texture input on the test substance.")]
        private SubstanceParameterValue defaultTextureValue = new SubstanceParameterValue();
        [SerializeField, Tooltip("Default set value for the target null texture input on the test substance.")]
        private SubstanceParameterValue defaultTextureNullValue = new SubstanceParameterValue();
        [SerializeField, Tooltip("Default set value for the target output size input on the test substance.")]
        private SubstanceParameterValue defaultOutputSizeValue = new SubstanceParameterValue();
        [SerializeField, Tooltip("Default set value for the target random seed input on the test substance.")]
        private SubstanceParameterValue defaultRandomSeedValue = new SubstanceParameterValue();
        //Set
        [SerializeField, Tooltip("Test set value for the target string input on the test substance.")]
        private SubstanceParameterValue setStringValue = new SubstanceParameterValue();
        [SerializeField, Tooltip("Test set value for the target bool input on the test substance.")]
        private SubstanceParameterValue setBoolValue = new SubstanceParameterValue();
        [SerializeField, Tooltip("Test set int value for the target enum input on the test substance.")]
        private SubstanceParameterValue setEnumValue = new SubstanceParameterValue();
        [SerializeField, Tooltip("Test set enum value for the target enum input on the test substance.")]
        private TestInputEnum setEnumCastValue = TestInputEnum.Black;
        [SerializeField, Tooltip("Test set value for the target int input on the test substance.")]
        private SubstanceParameterValue setIntValue = new SubstanceParameterValue();
        [SerializeField, Tooltip("Test set value for the target int2 input on the test substance.")]
        private SubstanceParameterValue setInt2Value = new SubstanceParameterValue();
        [SerializeField, Tooltip("Test set value for the target int3 input on the test substance.")]
        private SubstanceParameterValue setInt3Value = new SubstanceParameterValue();
        [SerializeField, Tooltip("Test set value for the target int4 input on the test substance.")]
        private SubstanceParameterValue setInt4Value = new SubstanceParameterValue();
        [SerializeField, Tooltip("Test set value for the target float input on the test substance.")]
        private SubstanceParameterValue setFloatValue = new SubstanceParameterValue();
        [SerializeField, Tooltip("Test set value for the target float2 input on the test substance.")]
        private SubstanceParameterValue setFloat2Value = new SubstanceParameterValue();
        [SerializeField, Tooltip("Test set value for the target float3 input on the test substance.")]
        private SubstanceParameterValue setFloat3Value = new SubstanceParameterValue();
        [SerializeField, Tooltip("Test set value for the target float4 input on the test substance.")]
        private SubstanceParameterValue setFloat4Value = new SubstanceParameterValue();
        [SerializeField, Tooltip("Test set value for the target texture input on the test substance.")]
        private SubstanceParameterValue setTextureValue = new SubstanceParameterValue();
        [SerializeField, Tooltip("Test set value for the target null texture input on the test substance.")]
        private SubstanceParameterValue setTextureNullValue = new SubstanceParameterValue();
        [SerializeField, Tooltip("Test set value for the target output size input on the test substance.")]
        private SubstanceParameterValue setOutputSizeValue = new SubstanceParameterValue();
        [SerializeField, Tooltip("Test set value for the target random seed input on the test substance.")]
        private SubstanceParameterValue setRandomSeedValue = new SubstanceParameterValue();
        //Outputs
        [SerializeField, Tooltip("Parameter for the test output map.")]
        private SubstanceOutput outputMap = new SubstanceOutput();
        [SerializeField, Tooltip("Expected value for output map.")]
        private Texture2D outputValue = null;

        private static SubstanceGraphTestAsset instance = null;

        private static SubstanceGraphTestAsset Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = GetTestAsset<SubstanceGraphTestAsset>();
                }

                return instance;
            }
        }


        public static SubstanceGraphSO Substance
        {
            get { return Instance != null ? Instance.substance : null; }
        }


        public static SubstanceParameterValue DefaultStringValue
        {
            get { return Instance != null ? Instance.defaultStringValue : default(SubstanceParameterValue); }
        }


        public static SubstanceParameterValue DefaultBoolValue
        {
            get { return Instance != null ? Instance.defaultBoolValue : default(SubstanceParameterValue); }
        }


        public static SubstanceParameterValue DefaultEnumValue
        {
            get { return Instance != null ? Instance.defaultEnumValue : default(SubstanceParameterValue); }
        }


        public static TestInputEnum DefaultEnumCastValue
        {
            get { return Instance != null ? Instance.defaultEnumCastValue : TestInputEnum.Black; }
        }


        public static SubstanceParameterValue DefaultIntValue
        {
            get { return Instance != null ? Instance.defaultIntValue : default(SubstanceParameterValue); }
        }


        public static SubstanceParameterValue DefaultInt2Value
        {
            get { return Instance != null ? Instance.defaultInt2Value : default(SubstanceParameterValue); }
        }


        public static SubstanceParameterValue DefaultInt3Value
        {
            get { return Instance != null ? Instance.defaultInt3Value : default(SubstanceParameterValue); }
        }


        public static SubstanceParameterValue DefaultInt4Value
        {
            get { return Instance != null ? Instance.defaultInt4Value : default(SubstanceParameterValue); }
        }


        public static SubstanceParameterValue DefaultFloatValue
        {
            get { return Instance != null ? Instance.defaultFloatValue : default(SubstanceParameterValue); }
        }


        public static SubstanceParameterValue DefaultFloat2Value
        {
            get { return Instance != null ? Instance.defaultFloat2Value : default(SubstanceParameterValue); }
        }


        public static SubstanceParameterValue DefaultFloat3Value
        {
            get { return Instance != null ? Instance.defaultFloat3Value : default(SubstanceParameterValue); }
        }


        public static SubstanceParameterValue DefaultFloat4Value
        {
            get { return Instance != null ? Instance.defaultFloat4Value : default(SubstanceParameterValue); }
        }


        public static SubstanceParameterValue DefaultTextureValue
        {
            get { return Instance != null ? Instance.defaultTextureValue : default(SubstanceParameterValue); }
        }


        public static SubstanceParameterValue DefaultTextureNullValue
        {
            get { return Instance != null ? Instance.defaultTextureNullValue : default(SubstanceParameterValue); }
        }


        public static SubstanceParameterValue DefaultOutputSizeValue
        {
            get { return Instance != null ? Instance.defaultOutputSizeValue : default(SubstanceParameterValue); }
        }


        public static SubstanceParameterValue DefaultRandomSeedValue
        {
            get { return Instance != null ? Instance.defaultRandomSeedValue : default(SubstanceParameterValue); }
        }


        public static SubstanceParameterValue SetStringValue
        {
            get { return Instance != null ? Instance.setStringValue : default(SubstanceParameterValue); }
        }


        public static SubstanceParameterValue SetBoolValue
        {
            get { return Instance != null ? Instance.setBoolValue : default(SubstanceParameterValue); }
        }


        public static SubstanceParameterValue SetEnumValue
        {
            get { return Instance != null ? Instance.setEnumValue : default(SubstanceParameterValue); }
        }


        public static TestInputEnum SetEnumCastValue
        {
            get { return Instance != null ? Instance.setEnumCastValue : TestInputEnum.Black; }
        }


        public static SubstanceParameterValue SetIntValue
        {
            get { return Instance != null ? Instance.setIntValue : default(SubstanceParameterValue); }
        }


        public static SubstanceParameterValue SetInt2Value
        {
            get { return Instance != null ? Instance.setInt2Value : default(SubstanceParameterValue); }
        }


        public static SubstanceParameterValue SetInt3Value
        {
            get { return Instance != null ? Instance.setInt3Value : default(SubstanceParameterValue); }
        }


        public static SubstanceParameterValue SetInt4Value
        {
            get { return Instance != null ? Instance.setInt4Value : default(SubstanceParameterValue); }
        }


        public static SubstanceParameterValue SetFloatValue
        {
            get { return Instance != null ? Instance.setFloatValue : default(SubstanceParameterValue); }
        }


        public static SubstanceParameterValue SetFloat2Value
        {
            get { return Instance != null ? Instance.setFloat2Value : default(SubstanceParameterValue); }
        }


        public static SubstanceParameterValue SetFloat3Value
        {
            get { return Instance != null ? Instance.setFloat3Value : default(SubstanceParameterValue); }
        }


        public static SubstanceParameterValue SetFloat4Value
        {
            get { return Instance != null ? Instance.setFloat4Value : default(SubstanceParameterValue); }
        }


        public static SubstanceParameterValue SetTextureValue
        {
            get { return Instance != null ? Instance.setTextureValue : default(SubstanceParameterValue); }
        }


        public static SubstanceParameterValue SetTextureNullValue
        {
            get { return Instance != null ? Instance.setTextureNullValue : default(SubstanceParameterValue); }
        }


        public static SubstanceParameterValue SetOutputSizeValue
        {
            get { return Instance != null ? Instance.setOutputSizeValue : default(SubstanceParameterValue); }
        }


        public static SubstanceParameterValue SetRandomSeedValue
        {
            get { return Instance != null ? Instance.setRandomSeedValue : default(SubstanceParameterValue); }
        }

        public static SubstanceOutput OutputMap
        {
            get { return Instance != null ? Instance.outputMap : default(SubstanceOutput); }
        }

        public static Texture2D OutputValue
        {
            get { return Instance != null ? Instance.outputValue : null; }
        }
    }
}