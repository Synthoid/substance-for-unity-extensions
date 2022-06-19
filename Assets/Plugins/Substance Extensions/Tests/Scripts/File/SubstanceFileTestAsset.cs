using UnityEngine;
using SOS.SubstanceExtensions;
using Adobe.Substance;

namespace SOS.SubstanceExtensions.Tests
{
    /// <summary>
    /// Asset containing values for <see cref="SubstanceFileSO"/> unit tests.
    /// </summary>
    [CreateAssetMenu(fileName="Substance File Test Asset", menuName="SOS/Substance/Tests/Substance File")]
    public class SubstanceFileTestAsset : UnitTestAsset
    {
        [SerializeField, Tooltip("SubstanceFile used for testing.")]
        private SubstanceFileSO substance = null;
        [Header("Get")]
        [SerializeField, Tooltip("Expected value for the target string input on the test substance.")]
        private SubstanceParameterValue getStringValue = new SubstanceParameterValue();
        [SerializeField, Tooltip("Expected value for the target bool input on the test substance.")]
        private SubstanceParameterValue getBoolValue = new SubstanceParameterValue();
        [SerializeField, Tooltip("Expected value for the target enum input on the test substance.")]
        private SubstanceParameterValue getEnumValue = new SubstanceParameterValue();
        [SerializeField, Tooltip("Expected value for the target int input on the test substance.")]
        private SubstanceParameterValue getIntValue = new SubstanceParameterValue();
        [SerializeField, Tooltip("Expected value for the target int2 input on the test substance.")]
        private SubstanceParameterValue getInt2Value = new SubstanceParameterValue();
        [SerializeField, Tooltip("Expected value for the target int3 input on the test substance.")]
        private SubstanceParameterValue getInt3Value = new SubstanceParameterValue();
        [SerializeField, Tooltip("Expected value for the target int4 input on the test substance.")]
        private SubstanceParameterValue getInt4Value = new SubstanceParameterValue();
        [SerializeField, Tooltip("Expected value for the target float input on the test substance.")]
        private SubstanceParameterValue getFloatValue = new SubstanceParameterValue();
        [SerializeField, Tooltip("Expected value for the target float2 input on the test substance.")]
        private SubstanceParameterValue getFloat2Value = new SubstanceParameterValue();
        [SerializeField, Tooltip("Expected value for the target float3 input on the test substance.")]
        private SubstanceParameterValue getFloat3Value = new SubstanceParameterValue();
        [SerializeField, Tooltip("Expected value for the target float4 input on the test substance.")]
        private SubstanceParameterValue getFloat4Value = new SubstanceParameterValue();
        [SerializeField, Tooltip("Expected value for the target texture input on the test substance.")]
        private SubstanceParameterValue getTextureValue = new SubstanceParameterValue();
        [SerializeField, Tooltip("Expected value for the target null texture input on the test substance.")]
        private SubstanceParameterValue getTextureNullValue = new SubstanceParameterValue();
        [SerializeField, Tooltip("Expected value for the target output size input on the test substance.")]
        private SubstanceParameterValue getOutputSizeValue = new SubstanceParameterValue();
        [SerializeField, Tooltip("Expected value for the target random seed input on the test substance.")]
        private SubstanceParameterValue getRandomSeedValue = new SubstanceParameterValue();
        [Header("Set")]
        [SerializeField, Tooltip("Test set value for the target string input on the test substance.")]
        private SubstanceParameterValue setStringValue = new SubstanceParameterValue();
        [SerializeField, Tooltip("Test set value for the target bool input on the test substance.")]
        private SubstanceParameterValue setBoolValue = new SubstanceParameterValue();
        [SerializeField, Tooltip("Test set value for the target enum input on the test substance.")]
        private SubstanceParameterValue setEnumValue = new SubstanceParameterValue();
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
        [Header("Default")]
        [SerializeField, Tooltip("Default set value for the target string input on the test substance.")]
        private SubstanceParameterValue defaultStringValue = new SubstanceParameterValue();
        [SerializeField, Tooltip("Default set value for the target bool input on the test substance.")]
        private SubstanceParameterValue defaultBoolValue = new SubstanceParameterValue();
        [SerializeField, Tooltip("Default set value for the target enum input on the test substance.")]
        private SubstanceParameterValue defaultEnumValue = new SubstanceParameterValue();
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

        private static SubstanceFileTestAsset instance = null;

        private static SubstanceFileTestAsset Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = GetTestAsset<SubstanceFileTestAsset>();
                }

                return instance;
            }
        }


        public static SubstanceFileSO Substance
        {
            get { return Instance != null ? Instance.substance : null; }
        }


        public static SubstanceParameterValue GetStringValue
        {
            get { return Instance != null ? Instance.getStringValue : default(SubstanceParameterValue); }
        }


        public static SubstanceParameterValue GetBoolValue
        {
            get { return Instance != null ? Instance.getBoolValue : default(SubstanceParameterValue); }
        }


        public static SubstanceParameterValue GetEnumValue
        {
            get { return Instance != null ? Instance.getEnumValue : default(SubstanceParameterValue); }
        }


        public static SubstanceParameterValue GetIntValue
        {
            get { return Instance != null ? Instance.getIntValue : default(SubstanceParameterValue); }
        }


        public static SubstanceParameterValue GetInt2Value
        {
            get { return Instance != null ? Instance.getInt2Value : default(SubstanceParameterValue); }
        }


        public static SubstanceParameterValue GetInt3Value
        {
            get { return Instance != null ? Instance.getInt3Value : default(SubstanceParameterValue); }
        }


        public static SubstanceParameterValue GetInt4Value
        {
            get { return Instance != null ? Instance.getInt4Value : default(SubstanceParameterValue); }
        }


        public static SubstanceParameterValue GetFloatValue
        {
            get { return Instance != null ? Instance.getFloatValue : default(SubstanceParameterValue); }
        }


        public static SubstanceParameterValue GetFloat2Value
        {
            get { return Instance != null ? Instance.getFloat2Value : default(SubstanceParameterValue); }
        }


        public static SubstanceParameterValue GetFloat3Value
        {
            get { return Instance != null ? Instance.getFloat3Value : default(SubstanceParameterValue); }
        }


        public static SubstanceParameterValue GetFloat4Value
        {
            get { return Instance != null ? Instance.getFloat4Value : default(SubstanceParameterValue); }
        }


        public static SubstanceParameterValue GetTextureValue
        {
            get { return Instance != null ? Instance.getTextureValue : default(SubstanceParameterValue); }
        }


        public static SubstanceParameterValue GetTextureNullValue
        {
            get { return Instance != null ? Instance.getTextureNullValue : default(SubstanceParameterValue); }
        }


        public static SubstanceParameterValue GetOutputSizeValue
        {
            get { return Instance != null ? Instance.getOutputSizeValue : default(SubstanceParameterValue); }
        }


        public static SubstanceParameterValue GetRandomSeedValue
        {
            get { return Instance != null ? Instance.getRandomSeedValue : default(SubstanceParameterValue); }
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
    }
}