using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using Adobe.Substance;
using SOS.SubstanceExtensions;

namespace SOS.SubstanceExtensions.Tests
{
    public class SubstanceFileTests
    {
        [SetUp]
        public void Setup()
        {
            SubstanceFileSO substance = SubstanceFileTestAsset.Substance;

            SubstanceParameterValue defaultStringValue = SubstanceFileTestAsset.DefaultStringValue;
            SubstanceParameterValue defaultBoolValue = SubstanceFileTestAsset.DefaultBoolValue;
            SubstanceParameterValue defaultEnumValue = SubstanceFileTestAsset.DefaultEnumValue;
            SubstanceParameterValue defaultIntValue = SubstanceFileTestAsset.DefaultIntValue;
            SubstanceParameterValue defaultInt2Value = SubstanceFileTestAsset.DefaultInt2Value;
            SubstanceParameterValue defaultInt3Value = SubstanceFileTestAsset.DefaultInt3Value;
            SubstanceParameterValue defaultInt4Value = SubstanceFileTestAsset.DefaultInt4Value;
            SubstanceParameterValue defaultFloatValue = SubstanceFileTestAsset.DefaultFloatValue;
            SubstanceParameterValue defaultFloat2Value = SubstanceFileTestAsset.DefaultFloat2Value;
            SubstanceParameterValue defaultFloat3Value = SubstanceFileTestAsset.DefaultFloat3Value;
            SubstanceParameterValue defaultFloat4Value = SubstanceFileTestAsset.DefaultFloat4Value;
            SubstanceParameterValue defaultTextureValue = SubstanceFileTestAsset.DefaultTextureValue;
            SubstanceParameterValue defaultNullTextureValue = SubstanceFileTestAsset.DefaultTextureNullValue;

            substance.SetString(defaultStringValue.StringValue, defaultStringValue.Parameter);
            substance.SetBool(defaultBoolValue.BoolValue, defaultBoolValue.Parameter);
            substance.SetInt(defaultEnumValue.IntValue, defaultEnumValue.Parameter);
            substance.SetInt(defaultIntValue.IntValue, defaultIntValue.Parameter);
            substance.SetInt2(defaultInt2Value.Int2Value, defaultInt2Value.Parameter);
            substance.SetInt3(defaultInt3Value.Int3Value, defaultInt3Value.Parameter);
            substance.SetInt4(defaultInt4Value.Int4Value, defaultInt4Value.Parameter);
            substance.SetFloat(defaultFloatValue.IntValue, defaultFloatValue.Parameter);
            substance.SetFloat2(defaultFloat2Value.Float2Value, defaultFloat2Value.Parameter);
            substance.SetFloat3(defaultFloat3Value.Float3Value, defaultFloat3Value.Parameter);
            substance.SetFloat4(defaultFloat4Value.Float4Value, defaultFloat4Value.Parameter);
            substance.SetTexture(defaultTextureValue.TextureValue, defaultTextureValue.Parameter);
            substance.SetTexture(defaultNullTextureValue.TextureValue, defaultNullTextureValue.Parameter);
        }

        #region Utility

        [Test, TestOf(typeof(SubstanceFileExtensions)), Author("Chris Ingerson"), Description("Tests if a graph index on a target SubstanceFileSO asset can properly be returned by referencing a graph guid.")]
        public void GetGraphIndex()
        {
            SubstanceFileSO substance = SubstanceFileTestAsset.Substance;
            SubstanceParameterValue parameterValue = SubstanceFileTestAsset.SetStringValue;

            int index = substance.GetGraphIndex(parameterValue.Parameter.GraphGuid);

            Assert.AreEqual(1, index);
        }

        #endregion

        #region String

        [Test, TestOf(typeof(SubstanceFileExtensions)), Author("Chris Ingerson"), Description("Tests if a string input value can properly be referenced on a target SubstanceFileSO asset.")]
        public void GetString()
        {
            SubstanceFileSO substance = SubstanceFileTestAsset.Substance;
            SubstanceParameterValue parameterValue = SubstanceFileTestAsset.GetStringValue;

            string inputValue = substance.GetString(parameterValue.Parameter);

            Assert.AreEqual(parameterValue.StringValue, inputValue);
        }

        [Test, TestOf(typeof(SubstanceFileExtensions)), Author("Chris Ingerson"), Description("Tests if a string input value can properly be referenced on a target SubstanceFileSO asset using the try get method.")]
        public void TryGetString()
        {
            SubstanceFileSO substance = SubstanceFileTestAsset.Substance;
            SubstanceParameterValue parameterValue = SubstanceFileTestAsset.GetStringValue;

            bool success = substance.TryGetString(out string inputValue, parameterValue.Parameter);

            Assert.IsTrue(success);
            Assert.AreEqual(parameterValue.StringValue, inputValue);
        }

        [Test, TestOf(typeof(SubstanceFileExtensions)), Author("Chris Ingerson"), Description("Tests if a default string value can properly be returned when referencing a non-existant input on a target SubstanceFileSO asset using the try get method.")]
        public void TryGetStringFail()
        {
            SubstanceFileSO substance = SubstanceFileTestAsset.Substance;

            bool success = substance.TryGetString(out string inputValue, "I am a great magician.", 0);

            Assert.IsFalse(success);
            Assert.AreEqual("", inputValue);
        }

        [Test, TestOf(typeof(SubstanceFileExtensions)), Author("Chris Ingerson"), Description("Tests if a string input value can properly be set on a target SubstanceFileSO asset.")]
        public void SetString()
        {
            SubstanceFileSO substance = SubstanceFileTestAsset.Substance;
            SubstanceParameterValue defaultValue = SubstanceFileTestAsset.DefaultStringValue;

            string inputValue = substance.GetString(defaultValue.Parameter);

            Assert.AreEqual(defaultValue.StringValue, inputValue);

            SubstanceParameterValue setValue = SubstanceFileTestAsset.SetStringValue;

            substance.SetString(setValue.StringValue, setValue.Parameter);

            inputValue = substance.GetString(setValue.Parameter);

            Assert.AreEqual(setValue.StringValue, inputValue);
        }

        [Test, TestOf(typeof(SubstanceFileExtensions)), Author("Chris Ingerson"), Description("Tests if a string input value can properly be set on a target SubstanceFileSO asset using the try set method.")]
        public void TrySetString()
        {
            SubstanceFileSO substance = SubstanceFileTestAsset.Substance;
            SubstanceParameterValue defaultValue = SubstanceFileTestAsset.DefaultStringValue;

            string inputValue = substance.GetString(defaultValue.Parameter);

            Assert.AreEqual(defaultValue.StringValue, inputValue);

            SubstanceParameterValue setValue = SubstanceFileTestAsset.SetStringValue;

            bool success = substance.TrySetString(setValue.StringValue, setValue.Parameter);

            inputValue = substance.GetString(setValue.Parameter);

            Assert.IsTrue(success);
            Assert.AreEqual(setValue.StringValue, inputValue);
        }

        [Test, TestOf(typeof(SubstanceFileExtensions)), Author("Chris Ingerson"), Description("Tests if a string input value can properly be set on a target SubstanceFileSO asset using the try set method.")]
        public void TrySetStringFail()
        {
            SubstanceFileSO substance = SubstanceFileTestAsset.Substance;
            SubstanceParameterValue defaultValue = SubstanceFileTestAsset.DefaultStringValue;

            string inputValue = substance.GetString(defaultValue.Parameter);

            Assert.AreEqual(defaultValue.StringValue, inputValue);

            SubstanceParameterValue setValue = SubstanceFileTestAsset.SetStringValue;

            bool success = substance.TrySetString(setValue.StringValue, "I am a great magician.", 0);

            substance.TryGetString(out inputValue, "I am a great magician.", 0);

            Assert.IsFalse(success);
            Assert.AreEqual("", inputValue);
        }

        #endregion

        #region Int

        #region Bool

        [Test, TestOf(typeof(SubstanceFileExtensions)), Author("Chris Ingerson"), Description("Tests if a bool input value can properly be referenced on a target SubstanceFileSO asset.")]
        public void GetBool()
        {
            SubstanceFileSO substance = SubstanceFileTestAsset.Substance;
            SubstanceParameterValue parameterValue = SubstanceFileTestAsset.GetBoolValue;

            bool inputValue = substance.GetBool(parameterValue.Index, parameterValue.GraphId);

            Assert.AreEqual(parameterValue.BoolValue, inputValue);
        }

        [Test, TestOf(typeof(SubstanceFileExtensions)), Author("Chris Ingerson"), Description("Tests if a bool input value can properly be referenced on a target SubstanceFileSO asset using the try get method.")]
        public void TryGetBool()
        {
            SubstanceFileSO substance = SubstanceFileTestAsset.Substance;
            SubstanceParameterValue parameterValue = SubstanceFileTestAsset.GetBoolValue;

            bool success = substance.TryGetBool(out bool inputValue, parameterValue.Index, parameterValue.GraphId);

            Assert.IsTrue(success);
            Assert.AreEqual(parameterValue.BoolValue, inputValue);
        }

        [Test, TestOf(typeof(SubstanceFileExtensions)), Author("Chris Ingerson"), Description("Tests if a default bool value can properly be returned when referencing a non-existant input on a target SubstanceFileSO asset using the try get method.")]
        public void TryGetBoolFail()
        {
            SubstanceFileSO substance = SubstanceFileTestAsset.Substance;

            bool success = substance.TryGetBool(out bool inputValue, "Your clothes are black.", 0);

            Assert.IsFalse(success);
            Assert.AreEqual(false, inputValue);
        }

        #endregion

        #region Enum

        [Test, TestOf(typeof(SubstanceFileExtensions)), Author("Chris Ingerson"), Description("Tests if an enum int input value can properly be referenced on a target SubstanceFileSO asset.")]
        public void GetEnum()
        {
            SubstanceFileSO substance = SubstanceFileTestAsset.Substance;
            SubstanceParameterValue parameterValue = SubstanceFileTestAsset.GetEnumValue;

            int inputValue = substance.GetInt(parameterValue.Index, parameterValue.GraphId);

            Assert.AreEqual(parameterValue.IntValue, inputValue);
        }

        #endregion

        [Test, TestOf(typeof(SubstanceFileExtensions)), Author("Chris Ingerson"), Description("Tests if an int input value can properly be referenced on a target SubstanceFileSO asset.")]
        public void GetInt()
        {
            SubstanceFileSO substance = SubstanceFileTestAsset.Substance;
            SubstanceParameterValue parameterValue = SubstanceFileTestAsset.GetIntValue;

            int inputValue = substance.GetInt(parameterValue.Index, parameterValue.GraphId);

            Assert.AreEqual(parameterValue.IntValue, inputValue);
        }

        [Test, TestOf(typeof(SubstanceFileExtensions)), Author("Chris Ingerson"), Description("Tests if an int input value can properly be referenced on a target SubstanceFileSO asset using the try get method.")]
        public void TryGetInt()
        {
            SubstanceFileSO substance = SubstanceFileTestAsset.Substance;
            SubstanceParameterValue parameterValue = SubstanceFileTestAsset.GetIntValue;

            bool success = substance.TryGetInt(out int inputValue, parameterValue.Index, parameterValue.GraphId);

            Assert.IsTrue(success);
            Assert.AreEqual(parameterValue.IntValue, inputValue);
        }

        [Test, TestOf(typeof(SubstanceFileExtensions)), Author("Chris Ingerson"), Description("Tests if a default int value can properly be returned when referencing a non-existant input on a target SubstanceFileSO asset using the try get method.")]
        public void TryGetIntFail()
        {
            SubstanceFileSO substance = SubstanceFileTestAsset.Substance;

            bool success = substance.TryGetInt(out int inputValue, "Red clothes!", 0);

            Assert.IsFalse(success);
            Assert.AreEqual(0, inputValue);
        }

        #endregion

        #region Int2

        [Test, TestOf(typeof(SubstanceFileExtensions)), Author("Chris Ingerson"), Description("Tests if an int2 input value can properly be referenced on a target SubstanceFileSO asset.")]
        public void GetInt2()
        {
            SubstanceFileSO substance = SubstanceFileTestAsset.Substance;
            SubstanceParameterValue parameterValue = SubstanceFileTestAsset.GetInt2Value;

            Vector2Int inputValue = substance.GetInt2(parameterValue.Index, parameterValue.GraphId);

            Assert.AreEqual(parameterValue.Int2Value, inputValue);
        }

        [Test, TestOf(typeof(SubstanceFileExtensions)), Author("Chris Ingerson"), Description("Tests if an int2 input value can properly be referenced on a target SubstanceFileSO asset using the try get method.")]
        public void TryGetInt2()
        {
            SubstanceFileSO substance = SubstanceFileTestAsset.Substance;
            SubstanceParameterValue parameterValue = SubstanceFileTestAsset.GetInt2Value;

            bool success = substance.TryGetInt2(out Vector2Int inputValue, parameterValue.Index, parameterValue.GraphId);

            Assert.IsTrue(success);
            Assert.AreEqual(parameterValue.Int2Value, inputValue);
        }

        [Test, TestOf(typeof(SubstanceFileExtensions)), Author("Chris Ingerson"), Description("Tests if a default int2 value can properly be returned when referencing a non-existant input on a target SubstanceFileSO asset using the try get method.")]
        public void TryGetInt2Fail()
        {
            SubstanceFileSO substance = SubstanceFileTestAsset.Substance;

            bool success = substance.TryGetInt2(out Vector2Int inputValue, "That's a lot of nuts!", 0);

            Assert.IsFalse(success);
            Assert.AreEqual(Vector2Int.zero, inputValue);
        }

        #endregion

        #region Int3

        [Test, TestOf(typeof(SubstanceFileExtensions)), Author("Chris Ingerson"), Description("Tests if an int3 input value can properly be referenced on a target SubstanceFileSO asset.")]
        public void GetInt3()
        {
            SubstanceFileSO substance = SubstanceFileTestAsset.Substance;
            SubstanceParameterValue parameterValue = SubstanceFileTestAsset.GetInt3Value;

            Vector3Int inputValue = substance.GetInt3(parameterValue.Index, parameterValue.GraphId);

            Assert.AreEqual(parameterValue.Int3Value, inputValue);
        }

        [Test, TestOf(typeof(SubstanceFileExtensions)), Author("Chris Ingerson"), Description("Tests if an int3 input value can properly be referenced on a target SubstanceFileSO asset using the try get method.")]
        public void TryGetInt3()
        {
            SubstanceFileSO substance = SubstanceFileTestAsset.Substance;
            SubstanceParameterValue parameterValue = SubstanceFileTestAsset.GetInt3Value;

            bool success = substance.TryGetInt3(out Vector3Int inputValue, parameterValue.Index, parameterValue.GraphId);

            Assert.IsTrue(success);
            Assert.AreEqual(parameterValue.Int3Value, inputValue);
        }

        [Test, TestOf(typeof(SubstanceFileExtensions)), Author("Chris Ingerson"), Description("Tests if a default int3 value can properly be returned when referencing a non-existant input on a target SubstanceFileSO asset using the try get method.")]
        public void TryGetInt3Fail()
        {
            SubstanceFileSO substance = SubstanceFileTestAsset.Substance;

            bool success = substance.TryGetInt3(out Vector3Int inputValue, "Ventriloquists, huh?", 0);

            Assert.IsFalse(success);
            Assert.AreEqual(Vector3Int.zero, inputValue);
        }

        #endregion

        #region Int4

        [Test, TestOf(typeof(SubstanceFileExtensions)), Author("Chris Ingerson"), Description("Tests if an int4 input value can properly be referenced on a target SubstanceFileSO asset.")]
        public void GetInt4()
        {
            SubstanceFileSO substance = SubstanceFileTestAsset.Substance;
            SubstanceParameterValue parameterValue = SubstanceFileTestAsset.GetInt4Value;

            Vector4Int inputValue = substance.GetInt4(parameterValue.Index, parameterValue.GraphId);

            Assert.AreEqual(parameterValue.Int4Value, inputValue);
        }

        [Test, TestOf(typeof(SubstanceFileExtensions)), Author("Chris Ingerson"), Description("Tests if an int4 input value can properly be referenced on a target SubstanceFileSO asset using the try get method.")]
        public void TryGetInt4()
        {
            SubstanceFileSO substance = SubstanceFileTestAsset.Substance;
            SubstanceParameterValue parameterValue = SubstanceFileTestAsset.GetInt4Value;

            bool success = substance.TryGetInt4(out Vector4Int inputValue, parameterValue.Index, parameterValue.GraphId);

            Assert.IsTrue(success);
            Assert.AreEqual(parameterValue.Int4Value, inputValue);
        }

        [Test, TestOf(typeof(SubstanceFileExtensions)), Author("Chris Ingerson"), Description("Tests if a default int4 value can properly be returned when referencing a non-existant input on a target SubstanceFileSO asset using the try get method.")]
        public void TryGetInt4Fail()
        {
            SubstanceFileSO substance = SubstanceFileTestAsset.Substance;

            bool success = substance.TryGetInt4(out Vector4Int inputValue, "Face to foot style!", 0);

            Assert.IsFalse(success);
            Assert.AreEqual(Vector4Int.zero, inputValue);
        }

        #endregion

        #region Float

        [Test, TestOf(typeof(SubstanceFileExtensions)), Author("Chris Ingerson"), Description("Tests if a float input value can properly be referenced on a target SubstanceFileSO asset.")]
        public void GetFloat()
        {
            SubstanceFileSO substance = SubstanceFileTestAsset.Substance;
            SubstanceParameterValue parameterValue = SubstanceFileTestAsset.GetFloatValue;

            float inputValue = substance.GetFloat(parameterValue.Index, parameterValue.GraphId);

            Assert.AreEqual(parameterValue.FloatValue, inputValue);
        }

        [Test, TestOf(typeof(SubstanceFileExtensions)), Author("Chris Ingerson"), Description("Tests if a float input value can properly be referenced on a target SubstanceFileSO asset using the try get method.")]
        public void TryGetFloat()
        {
            SubstanceFileSO substance = SubstanceFileTestAsset.Substance;
            SubstanceParameterValue parameterValue = SubstanceFileTestAsset.GetFloatValue;

            bool success = substance.TryGetFloat(out float inputValue, parameterValue.Index, parameterValue.GraphId);

            Assert.IsTrue(success);
            Assert.AreEqual(parameterValue.FloatValue, inputValue);
        }

        [Test, TestOf(typeof(SubstanceFileExtensions)), Author("Chris Ingerson"), Description("Tests if a default float value can properly be returned when referencing a non-existant input on a target SubstanceFileSO asset using the try get method.")]
        public void TryGetFloatFail()
        {
            SubstanceFileSO substance = SubstanceFileTestAsset.Substance;

            bool success = substance.TryGetFloat(out float inputValue, "And so, he walked.", 0);

            Assert.IsFalse(success);
            Assert.AreEqual(0f, inputValue);
        }

        #endregion

        #region Float2

        [Test, TestOf(typeof(SubstanceFileExtensions)), Author("Chris Ingerson"), Description("Tests if a float2 input value can properly be referenced on a target SubstanceFileSO asset.")]
        public void GetFloat2()
        {
            SubstanceFileSO substance = SubstanceFileTestAsset.Substance;
            SubstanceParameterValue parameterValue = SubstanceFileTestAsset.GetFloat2Value;

            Vector2 inputValue = substance.GetFloat2(parameterValue.Index, parameterValue.GraphId);

            Assert.AreEqual(parameterValue.Float2Value, inputValue);
        }

        [Test, TestOf(typeof(SubstanceFileExtensions)), Author("Chris Ingerson"), Description("Tests if a float2 input value can properly be referenced on a target SubstanceFileSO asset using the try get method.")]
        public void TryGetFloat2()
        {
            SubstanceFileSO substance = SubstanceFileTestAsset.Substance;
            SubstanceParameterValue parameterValue = SubstanceFileTestAsset.GetFloat2Value;

            bool success = substance.TryGetFloat2(out Vector2 inputValue, parameterValue.Index, parameterValue.GraphId);

            Assert.IsTrue(success);
            Assert.AreEqual(parameterValue.Float2Value, inputValue);
        }

        [Test, TestOf(typeof(SubstanceFileExtensions)), Author("Chris Ingerson"), Description("Tests if a default float2 value can properly be returned when referencing a non-existant input on a target SubstanceFileSO asset using the try get method.")]
        public void TryGetFloat2Fail()
        {
            SubstanceFileSO substance = SubstanceFileTestAsset.Substance;

            bool success = substance.TryGetFloat2(out Vector2 inputValue, "And sometimes, drove.", 0);

            Assert.IsFalse(success);
            Assert.AreEqual(Vector2.zero, inputValue);
        }

        #endregion

        #region Float3

        [Test, TestOf(typeof(SubstanceFileExtensions)), Author("Chris Ingerson"), Description("Tests if a float3 input value can properly be referenced on a target SubstanceFileSO asset.")]
        public void GetFloat3()
        {
            SubstanceFileSO substance = SubstanceFileTestAsset.Substance;
            SubstanceParameterValue parameterValue = SubstanceFileTestAsset.GetFloat3Value;

            Vector3 inputValue = substance.GetFloat3(parameterValue.Index, parameterValue.GraphId);

            Assert.AreEqual(parameterValue.Float3Value, inputValue);
        }

        [Test, TestOf(typeof(SubstanceFileExtensions)), Author("Chris Ingerson"), Description("Tests if a float3 input value can properly be referenced on a target SubstanceFileSO asset using the try get method.")]
        public void TryGetFloat3()
        {
            SubstanceFileSO substance = SubstanceFileTestAsset.Substance;
            SubstanceParameterValue parameterValue = SubstanceFileTestAsset.GetFloat3Value;

            bool success = substance.TryGetFloat3(out Vector3 inputValue, parameterValue.Index, parameterValue.GraphId);

            Assert.IsTrue(success);
            Assert.AreEqual(parameterValue.Float3Value, inputValue);
        }

        [Test, TestOf(typeof(SubstanceFileExtensions)), Author("Chris Ingerson"), Description("Tests if a default float3 value can properly be returned when referencing a non-existant input on a target SubstanceFileSO asset using the try get method.")]
        public void TryGetFloat3Fail()
        {
            SubstanceFileSO substance = SubstanceFileTestAsset.Substance;

            bool success = substance.TryGetFloat3(out Vector3 inputValue, "I mean, I'm no doctor.", 0);

            Assert.IsFalse(success);
            Assert.AreEqual(Vector3.zero, inputValue);
        }

        #endregion

        #region Float4

        [Test, TestOf(typeof(SubstanceFileExtensions)), Author("Chris Ingerson"), Description("Tests if a float4 input value can properly be referenced on a target SubstanceFileSO asset.")]
        public void GetFloat4()
        {
            SubstanceFileSO substance = SubstanceFileTestAsset.Substance;
            SubstanceParameterValue parameterValue = SubstanceFileTestAsset.GetFloat4Value;

            Vector4 inputValue = substance.GetFloat4(parameterValue.Index, parameterValue.GraphId);

            Assert.AreEqual(parameterValue.Float4Value, inputValue);
        }

        [Test, TestOf(typeof(SubstanceFileExtensions)), Author("Chris Ingerson"), Description("Tests if a float4 input value can properly be referenced on a target SubstanceFileSO asset using the try get method.")]
        public void TryGetFloat4()
        {
            SubstanceFileSO substance = SubstanceFileTestAsset.Substance;
            SubstanceParameterValue parameterValue = SubstanceFileTestAsset.GetFloat4Value;

            bool success = substance.TryGetFloat4(out Vector4 inputValue, parameterValue.Index, parameterValue.GraphId);

            Assert.IsTrue(success);
            Assert.AreEqual(parameterValue.Float4Value, inputValue);
        }

        [Test, TestOf(typeof(SubstanceFileExtensions)), Author("Chris Ingerson"), Description("Tests if a default float4 value can properly be returned when referencing a non-existant input on a target SubstanceFileSO asset using the try get method.")]
        public void TryGetFloat4Fail()
        {
            SubstanceFileSO substance = SubstanceFileTestAsset.Substance;

            bool success = substance.TryGetFloat4(out Vector4 inputValue, "It was like, one clean chunk!", 0);

            Assert.IsFalse(success);
            Assert.AreEqual(Vector4.zero, inputValue);
        }

        #endregion

        #region Texture

        [Test, TestOf(typeof(SubstanceFileExtensions)), Author("Chris Ingerson"), Description("Tests if a texture input value can properly be referenced on a target SubstanceFileSO asset.")]
        public void GetTexture()
        {
            SubstanceFileSO substance = SubstanceFileTestAsset.Substance;
            SubstanceParameterValue parameterValue = SubstanceFileTestAsset.GetTextureValue;

            Texture2D inputValue = substance.GetTexture(parameterValue.Index, parameterValue.GraphId);

            Assert.AreEqual(parameterValue.TextureValue, inputValue);
        }

        [Test, TestOf(typeof(SubstanceFileExtensions)), Author("Chris Ingerson"), Description("Tests if a null texture input value can properly be referenced on a target SubstanceFileSO asset.")]
        public void GetNullTexture()
        {
            SubstanceFileSO substance = SubstanceFileTestAsset.Substance;
            SubstanceParameterValue parameterValue = SubstanceFileTestAsset.GetTextureNullValue;

            Texture2D inputValue = substance.GetTexture(parameterValue.Index, parameterValue.GraphId);

            Assert.IsNull(inputValue);
        }

        [Test, TestOf(typeof(SubstanceFileExtensions)), Author("Chris Ingerson"), Description("Tests if a texture input value can properly be referenced on a target SubstanceFileSO asset using the try get method.")]
        public void TryGetTexture()
        {
            SubstanceFileSO substance = SubstanceFileTestAsset.Substance;
            SubstanceParameterValue parameterValue = SubstanceFileTestAsset.GetTextureValue;

            bool success = substance.TryGetTexture(out Texture2D inputValue, parameterValue.Index, parameterValue.GraphId);

            Assert.IsTrue(success);
            Assert.AreEqual(parameterValue.TextureValue, inputValue);
        }

        [Test, TestOf(typeof(SubstanceFileExtensions)), Author("Chris Ingerson"), Description("Tests if a default texture value can properly be returned when referencing a non-existant input on a target SubstanceFileSO asset using the try get method.")]
        public void TryGetTextureFail()
        {
            SubstanceFileSO substance = SubstanceFileTestAsset.Substance;

            bool success = substance.TryGetTexture(out Texture2D inputValue, "I mean, I'm no doctor.", 0);

            Assert.IsFalse(success);
            Assert.IsNull(inputValue);
        }

        #endregion

        #region Output Size

        [Test, TestOf(typeof(SubstanceFileExtensions)), Author("Chris Ingerson"), Description("Tests if an $outputsize input value can properly be referenced on a target SubstanceFileSO asset.")]
        public void GetOutputSize()
        {
            SubstanceFileSO substance = SubstanceFileTestAsset.Substance;
            SubstanceParameterValue parameterValue = SubstanceFileTestAsset.GetOutputSizeValue;

            Vector2Int inputValue = substance.GetOutputSize(parameterValue.GraphId);

            Assert.AreEqual(parameterValue.Int2Value, inputValue);
        }

        #endregion

        #region Random Seed

        [Test, TestOf(typeof(SubstanceFileExtensions)), Author("Chris Ingerson"), Description("Tests if a $randomseed input value can properly be referenced on a target SubstanceFileSO asset.")]
        public void GetRandomSeed()
        {
            SubstanceFileSO substance = SubstanceFileTestAsset.Substance;
            SubstanceParameterValue parameterValue = SubstanceFileTestAsset.GetRandomSeedValue;

            int inputValue = substance.GetRandomSeed(parameterValue.GraphId);

            Assert.AreEqual(parameterValue.IntValue, inputValue);
        }

        #endregion

        /*[UnityTest]
        public IEnumerator SubstanceFileTestsWithEnumeratorPasses()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return null;
        }*/
    }
}