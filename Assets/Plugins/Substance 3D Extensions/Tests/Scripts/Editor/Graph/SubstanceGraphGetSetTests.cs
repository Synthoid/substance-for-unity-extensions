using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using Adobe.Substance;
using SOS.SubstanceExtensions;

namespace SOS.SubstanceExtensions.Tests
{
    /// <summary>
    /// Unit tests for <see cref="SubstanceGraphSO"/> get/set value operations.
    /// </summary>
    public class SubstanceGraphGetSetTests
    {
        [SetUp, OneTimeTearDown]
        public void SetUpAndTearDown()
        {
            SubstanceGraphSO substance = SubstanceGraphTestAsset.Substance;

            SubstanceParameterValue defaultStringValue = SubstanceGraphTestAsset.DefaultStringValue;
            SubstanceParameterValue defaultBoolValue = SubstanceGraphTestAsset.DefaultBoolValue;
            SubstanceParameterValue defaultEnumValue = SubstanceGraphTestAsset.DefaultEnumValue;
            SubstanceParameterValue defaultIntValue = SubstanceGraphTestAsset.DefaultIntValue;
            SubstanceParameterValue defaultInt2Value = SubstanceGraphTestAsset.DefaultInt2Value;
            SubstanceParameterValue defaultInt3Value = SubstanceGraphTestAsset.DefaultInt3Value;
            SubstanceParameterValue defaultInt4Value = SubstanceGraphTestAsset.DefaultInt4Value;
            SubstanceParameterValue defaultFloatValue = SubstanceGraphTestAsset.DefaultFloatValue;
            SubstanceParameterValue defaultFloat2Value = SubstanceGraphTestAsset.DefaultFloat2Value;
            SubstanceParameterValue defaultFloat3Value = SubstanceGraphTestAsset.DefaultFloat3Value;
            SubstanceParameterValue defaultFloat4Value = SubstanceGraphTestAsset.DefaultFloat4Value;
            SubstanceParameterValue defaultTextureValue = SubstanceGraphTestAsset.DefaultTextureValue;
            SubstanceParameterValue defaultNullTextureValue = SubstanceGraphTestAsset.DefaultTextureNullValue;
            SubstanceParameterValue defaultOutputSizeValue = SubstanceGraphTestAsset.DefaultOutputSizeValue;
            SubstanceParameterValue defaultRandomSeedValue = SubstanceGraphTestAsset.DefaultRandomSeedValue;

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
            substance.SetOutputSize(defaultOutputSizeValue.Int2Value);
            substance.SetRandomSeed(defaultRandomSeedValue.IntValue);
        }

        #region String

        [Test, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if a string input value can properly be referenced on a target SubstanceGraphSO asset.")]
        public void GetString()
        {
            SubstanceGraphSO substance = SubstanceGraphTestAsset.Substance;
            SubstanceParameterValue parameterValue = SubstanceGraphTestAsset.DefaultStringValue;

            string inputValue = substance.GetString(parameterValue.Parameter);

            Assert.AreEqual(parameterValue.StringValue, inputValue);
        }

        [Test, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if a string input value can properly be referenced on a target SubstanceGraphSO asset using the try get method.")]
        public void TryGetString()
        {
            SubstanceGraphSO substance = SubstanceGraphTestAsset.Substance;
            SubstanceParameterValue parameterValue = SubstanceGraphTestAsset.DefaultStringValue;

            bool success = substance.TryGetString(out string inputValue, parameterValue.Parameter);

            Assert.IsTrue(success);
            Assert.AreEqual(parameterValue.StringValue, inputValue);
        }

        [Test, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if a default string value can properly be returned when referencing a non-existant input on a target SubstanceGraphSO asset using the try get method.")]
        public void TryGetStringFail()
        {
            SubstanceGraphSO substance = SubstanceGraphTestAsset.Substance;

            bool success = substance.TryGetString(out string inputValue, "I am a great magician.");

            Assert.IsFalse(success);
            Assert.AreEqual("", inputValue);
        }

        [Test, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if a string input value can properly be set on a target SubstanceGraphSO asset.")]
        public void SetString()
        {
            SubstanceGraphSO substance = SubstanceGraphTestAsset.Substance;
            SubstanceParameterValue defaultValue = SubstanceGraphTestAsset.DefaultStringValue;

            string inputValue = substance.GetString(defaultValue.Parameter);

            Assert.AreEqual(defaultValue.StringValue, inputValue);

            SubstanceParameterValue setValue = SubstanceGraphTestAsset.SetStringValue;

            substance.SetString(setValue.StringValue, setValue.Parameter);

            inputValue = substance.GetString(setValue.Parameter);

            Assert.AreEqual(setValue.StringValue, inputValue);
        }

        [Test, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if a string input value can properly be set on a target SubstanceGraphSO asset using the try set method.")]
        public void TrySetString()
        {
            SubstanceGraphSO substance = SubstanceGraphTestAsset.Substance;
            SubstanceParameterValue defaultValue = SubstanceGraphTestAsset.DefaultStringValue;

            string inputValue = substance.GetString(defaultValue.Parameter);

            Assert.AreEqual(defaultValue.StringValue, inputValue);

            SubstanceParameterValue setValue = SubstanceGraphTestAsset.SetStringValue;

            bool success = substance.TrySetString(setValue.StringValue, setValue.Parameter);

            inputValue = substance.GetString(setValue.Parameter);

            Assert.IsTrue(success);
            Assert.AreEqual(setValue.StringValue, inputValue);
        }

        [Test, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if attempting to set a non-existant string input value properly returns false when setting its value on a target SubstanceGraphSO asset using the try set method.")]
        public void TrySetStringFail()
        {
            SubstanceGraphSO substance = SubstanceGraphTestAsset.Substance;
            SubstanceParameterValue defaultValue = SubstanceGraphTestAsset.DefaultStringValue;

            string inputValue = substance.GetString(defaultValue.Parameter);

            Assert.AreEqual(defaultValue.StringValue, inputValue);

            bool success = substance.TrySetString("Master Betty, what do we do?", "I am a great magician.");

            substance.TryGetString(out inputValue, "I am a great magician.");

            Assert.IsFalse(success);
            Assert.AreEqual("", inputValue);
        }

        #endregion

        #region Int

        #region Bool

        [Test, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if a bool input value can properly be referenced on a target SubstanceGraphSO asset.")]
        public void GetBool()
        {
            SubstanceGraphSO substance = SubstanceGraphTestAsset.Substance;
            SubstanceParameterValue parameterValue = SubstanceGraphTestAsset.DefaultBoolValue;

            bool inputValue = substance.GetBool(parameterValue.Parameter);

            Assert.AreEqual(parameterValue.BoolValue, inputValue);
        }

        [Test, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if a bool input value can properly be referenced on a target SubstanceGraphSO asset using the try get method.")]
        public void TryGetBool()
        {
            SubstanceGraphSO substance = SubstanceGraphTestAsset.Substance;
            SubstanceParameterValue parameterValue = SubstanceGraphTestAsset.DefaultBoolValue;

            bool success = substance.TryGetBool(out bool inputValue, parameterValue.Parameter);

            Assert.IsTrue(success);
            Assert.AreEqual(parameterValue.BoolValue, inputValue);
        }

        [Test, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if a default bool value can properly be returned when referencing a non-existant input on a target SubstanceGraphSO asset using the try get method.")]
        public void TryGetBoolFail()
        {
            SubstanceGraphSO substance = SubstanceGraphTestAsset.Substance;

            bool success = substance.TryGetBool(out bool inputValue, "Your clothes are black.");

            Assert.IsFalse(success);
            Assert.AreEqual(false, inputValue);
        }

        [Test, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if a bool input value can properly be set on a target SubstanceGraphSO asset.")]
        public void SetBool()
        {
            SubstanceGraphSO substance = SubstanceGraphTestAsset.Substance;
            SubstanceParameterValue defaultValue = SubstanceGraphTestAsset.DefaultBoolValue;

            bool inputValue = substance.GetBool(defaultValue.Parameter);

            Assert.AreEqual(defaultValue.BoolValue, inputValue);

            SubstanceParameterValue setValue = SubstanceGraphTestAsset.SetBoolValue;

            substance.SetBool(setValue.BoolValue, setValue.Parameter);

            inputValue = substance.GetBool(setValue.Parameter);

            Assert.AreEqual(setValue.BoolValue, inputValue);
        }

        [Test, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if a bool input value can properly be set on a target SubstanceGraphSO asset using the try set method.")]
        public void TrySetBool()
        {
            SubstanceGraphSO substance = SubstanceGraphTestAsset.Substance;
            SubstanceParameterValue defaultValue = SubstanceGraphTestAsset.DefaultBoolValue;

            bool inputValue = substance.GetBool(defaultValue.Parameter);

            Assert.AreEqual(defaultValue.BoolValue, inputValue);

            SubstanceParameterValue setValue = SubstanceGraphTestAsset.SetBoolValue;

            bool success = substance.TrySetBool(setValue.BoolValue, setValue.Parameter);

            inputValue = substance.GetBool(setValue.Parameter);

            Assert.IsTrue(success);
            Assert.AreEqual(setValue.BoolValue, inputValue);
        }

        [Test, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if attempting to set a non-existant bool input value properly returns false when setting its value on a target SubstanceGraphSO asset using the try set method.")]
        public void TrySetBoolFail()
        {
            SubstanceGraphSO substance = SubstanceGraphTestAsset.Substance;
            SubstanceParameterValue defaultValue = SubstanceGraphTestAsset.DefaultBoolValue;

            bool inputValue = substance.GetBool(defaultValue.Parameter);

            Assert.AreEqual(defaultValue.BoolValue, inputValue);

            bool success = substance.TrySetBool(true, "I am a great magician.");

            substance.TryGetBool(out inputValue, "I am a great magician.");

            Assert.IsFalse(success);
            Assert.IsFalse(inputValue);
        }

        #endregion

        #region Enum

        [Test, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if an enum int input value can properly be referenced on a target SubstanceGraphSO asset.")]
        public void GetEnum()
        {
            SubstanceGraphSO substance = SubstanceGraphTestAsset.Substance;
            SubstanceParameterValue parameterValue = SubstanceGraphTestAsset.DefaultEnumValue;

            int inputValue = substance.GetInt(parameterValue.Index);

            Assert.AreEqual(parameterValue.IntValue, inputValue);
        }

        [Test, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if an enum int input value can properly be referenced on a target SubstanceGraphSO asset and cast to a specific enum type.")]
        public void GetEnumCast()
        {
            SubstanceGraphSO substance = SubstanceGraphTestAsset.Substance;
            SubstanceParameterValue parameterValue = SubstanceGraphTestAsset.DefaultEnumValue;
            TestInputEnum enumValue = SubstanceGraphTestAsset.DefaultEnumCastValue;

            TestInputEnum inputValue = substance.GetEnum<TestInputEnum>(parameterValue.Parameter);

            Assert.AreEqual(enumValue, inputValue);
        }

        [Test, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if an enum int input value can properly be referenced on a target SubstanceGraphSO asset using the try get method.")]
        public void TryGetEnumCast()
        {
            SubstanceGraphSO substance = SubstanceGraphTestAsset.Substance;
            SubstanceParameterValue parameterValue = SubstanceGraphTestAsset.DefaultEnumValue;
            TestInputEnum enumValue = SubstanceGraphTestAsset.DefaultEnumCastValue;

            bool success = substance.TryGetEnum<TestInputEnum>(out TestInputEnum inputValue, parameterValue.Parameter);

            Assert.IsTrue(success);
            Assert.AreEqual(enumValue, inputValue);
        }

        [Test, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if a default enum value can properly be returned when referencing a non-existant enum input on a target SubstanceGraphSO asset using the try get method.")]
        public void TryGetEnumCastFail()
        {
            SubstanceGraphSO substance = SubstanceGraphTestAsset.Substance;
            TestInputEnum defaultValue = TestInputEnum.Blue;

            bool success = substance.TryGetEnum<TestInputEnum>(out TestInputEnum inputValue, "Red clothes!", defaultValue);

            Assert.IsFalse(success);
            Assert.AreEqual(defaultValue, inputValue);
        }

        [Test, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if an enum input value can properly be set on a target SubstanceGraphSO asset.")]
        public void SetEnum()
        {
            SubstanceGraphSO substance = SubstanceGraphTestAsset.Substance;
            SubstanceParameterValue defaultValue = SubstanceGraphTestAsset.DefaultEnumValue;

            TestInputEnum inputValue = substance.GetEnum<TestInputEnum>(defaultValue.Parameter);

            Assert.AreEqual((TestInputEnum)defaultValue.IntValue, inputValue);

            SubstanceParameterValue setValue = SubstanceGraphTestAsset.SetEnumValue;
            TestInputEnum enumValue = SubstanceGraphTestAsset.SetEnumCastValue;

            substance.SetEnum(enumValue, setValue.Parameter);

            inputValue = substance.GetEnum<TestInputEnum>(setValue.Parameter);

            Assert.AreEqual((TestInputEnum)setValue.IntValue, inputValue);
        }

        [Test, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if an enum input value can properly be set on a target SubstanceGraphSO asset using the try set method.")]
        public void TrySetEnum()
        {
            SubstanceGraphSO substance = SubstanceGraphTestAsset.Substance;
            SubstanceParameterValue defaultValue = SubstanceGraphTestAsset.DefaultEnumValue;

            TestInputEnum inputValue = substance.GetEnum<TestInputEnum>(defaultValue.Parameter);

            Assert.AreEqual((TestInputEnum)defaultValue.IntValue, inputValue);

            SubstanceParameterValue setValue = SubstanceGraphTestAsset.SetEnumValue;

            bool success = substance.TrySetEnum<TestInputEnum>((TestInputEnum)setValue.IntValue, setValue.Parameter);

            inputValue = substance.GetEnum<TestInputEnum>(setValue.Parameter);

            Assert.IsTrue(success);
            Assert.AreEqual((TestInputEnum)setValue.IntValue, inputValue);
        }

        [Test, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if attempting to set a non-existant enum input value properly returns false when setting its value on a target SubstanceGraphSO asset using the try set method.")]
        public void TrySetEnumFail()
        {
            SubstanceGraphSO substance = SubstanceGraphTestAsset.Substance;
            SubstanceParameterValue defaultValue = SubstanceGraphTestAsset.DefaultEnumValue;

            TestInputEnum inputValue = substance.GetEnum<TestInputEnum>(defaultValue.Parameter);

            Assert.AreEqual((TestInputEnum)defaultValue.IntValue, inputValue);

            bool success = substance.TrySetEnum<TestInputEnum>(TestInputEnum.Black, "I am a great magician.");

            substance.TryGetEnum<TestInputEnum>(out inputValue, "I am a great magician.");

            Assert.IsFalse(success);
            Assert.AreEqual((TestInputEnum)defaultValue.IntValue, inputValue);
        }

        #endregion

        [Test, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if an int input value can properly be referenced on a target SubstanceGraphSO asset.")]
        public void GetInt()
        {
            SubstanceGraphSO substance = SubstanceGraphTestAsset.Substance;
            SubstanceParameterValue parameterValue = SubstanceGraphTestAsset.DefaultIntValue;

            int inputValue = substance.GetInt(parameterValue.Parameter);

            Assert.AreEqual(parameterValue.IntValue, inputValue);
        }

        [Test, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if an int input value can properly be referenced on a target SubstanceGraphSO asset using the try get method.")]
        public void TryGetInt()
        {
            SubstanceGraphSO substance = SubstanceGraphTestAsset.Substance;
            SubstanceParameterValue parameterValue = SubstanceGraphTestAsset.DefaultIntValue;

            bool success = substance.TryGetInt(out int inputValue, parameterValue.Parameter);

            Assert.IsTrue(success);
            Assert.AreEqual(parameterValue.IntValue, inputValue);
        }

        [Test, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if a default int value can properly be returned when referencing a non-existant input on a target SubstanceGraphSO asset using the try get method.")]
        public void TryGetIntFail()
        {
            SubstanceGraphSO substance = SubstanceGraphTestAsset.Substance;

            bool success = substance.TryGetInt(out int inputValue, "Red clothes!");

            Assert.IsFalse(success);
            Assert.AreEqual(0, inputValue);
        }

        [Test, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if an int input value can properly be set on a target SubstanceGraphSO asset.")]
        public void SetInt()
        {
            SubstanceGraphSO substance = SubstanceGraphTestAsset.Substance;
            SubstanceParameterValue defaultValue = SubstanceGraphTestAsset.DefaultIntValue;

            int inputValue = substance.GetInt(defaultValue.Parameter);

            Assert.AreEqual(defaultValue.IntValue, inputValue);

            SubstanceParameterValue setValue = SubstanceGraphTestAsset.SetIntValue;

            substance.SetInt(setValue.IntValue, setValue.Parameter);

            inputValue = substance.GetInt(setValue.Parameter);

            Assert.AreEqual(setValue.IntValue, inputValue);
        }

        [Test, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if an int input value can properly be set on a target SubstanceGraphSO asset using the try set method.")]
        public void TrySetInt()
        {
            SubstanceGraphSO substance = SubstanceGraphTestAsset.Substance;
            SubstanceParameterValue defaultValue = SubstanceGraphTestAsset.DefaultIntValue;

            int inputValue = substance.GetInt(defaultValue.Parameter);

            Assert.AreEqual(defaultValue.IntValue, inputValue);

            SubstanceParameterValue setValue = SubstanceGraphTestAsset.SetIntValue;

            bool success = substance.TrySetInt(setValue.IntValue, setValue.Parameter);

            inputValue = substance.GetInt(setValue.Parameter);

            Assert.IsTrue(success);
            Assert.AreEqual(setValue.IntValue, inputValue);
        }

        [Test, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if attempting to set a non-existant int input value properly returns false when setting its value on a target SubstanceGraphSO asset using the try set method.")]
        public void TrySetIntFail()
        {
            SubstanceGraphSO substance = SubstanceGraphTestAsset.Substance;
            SubstanceParameterValue defaultValue = SubstanceGraphTestAsset.DefaultIntValue;

            int inputValue = substance.GetInt(defaultValue.Parameter);

            Assert.AreEqual(defaultValue.IntValue, inputValue);

            bool success = substance.TrySetInt(1, "I am a great magician.");

            substance.TryGetInt(out inputValue, "I am a great magician.");

            Assert.IsFalse(success);
            Assert.AreEqual(0, inputValue);
        }

        #endregion

        #region Int2

        [Test, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if an int2 input value can properly be referenced on a target SubstanceGraphSO asset.")]
        public void GetInt2()
        {
            SubstanceGraphSO substance = SubstanceGraphTestAsset.Substance;
            SubstanceParameterValue parameterValue = SubstanceGraphTestAsset.DefaultInt2Value;

            Vector2Int inputValue = substance.GetInt2(parameterValue.Parameter);

            Assert.AreEqual(parameterValue.Int2Value, inputValue);
        }

        [Test, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if an int2 input value can properly be referenced on a target SubstanceGraphSO asset using the try get method.")]
        public void TryGetInt2()
        {
            SubstanceGraphSO substance = SubstanceGraphTestAsset.Substance;
            SubstanceParameterValue parameterValue = SubstanceGraphTestAsset.DefaultInt2Value;

            bool success = substance.TryGetInt2(out Vector2Int inputValue, parameterValue.Parameter);

            Assert.IsTrue(success);
            Assert.AreEqual(parameterValue.Int2Value, inputValue);
        }

        [Test, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if a default int2 value can properly be returned when referencing a non-existant input on a target SubstanceGraphSO asset using the try get method.")]
        public void TryGetInt2Fail()
        {
            SubstanceGraphSO substance = SubstanceGraphTestAsset.Substance;

            bool success = substance.TryGetInt2(out Vector2Int inputValue, "That's a lot of nuts!");

            Assert.IsFalse(success);
            Assert.AreEqual(Vector2Int.zero, inputValue);
        }

        [Test, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if an int2 input value can properly be set on a target SubstanceGraphSO asset.")]
        public void SetInt2()
        {
            SubstanceGraphSO substance = SubstanceGraphTestAsset.Substance;
            SubstanceParameterValue defaultValue = SubstanceGraphTestAsset.DefaultIntValue;

            int inputValue = substance.GetInt(defaultValue.Parameter);

            Assert.AreEqual(defaultValue.IntValue, inputValue);

            SubstanceParameterValue setValue = SubstanceGraphTestAsset.SetIntValue;

            substance.SetInt(setValue.IntValue, setValue.Parameter);

            inputValue = substance.GetInt(setValue.Parameter);

            Assert.AreEqual(setValue.IntValue, inputValue);
        }

        [Test, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if an int2 input value can properly be set on a target SubstanceGraphSO asset using the try set method.")]
        public void TrySetInt2()
        {
            SubstanceGraphSO substance = SubstanceGraphTestAsset.Substance;
            SubstanceParameterValue defaultValue = SubstanceGraphTestAsset.DefaultInt2Value;

            Vector2Int inputValue = substance.GetInt2(defaultValue.Parameter);

            Assert.AreEqual(defaultValue.Int2Value, inputValue);

            SubstanceParameterValue setValue = SubstanceGraphTestAsset.SetInt2Value;

            bool success = substance.TrySetInt2(setValue.Int2Value, setValue.Parameter);

            inputValue = substance.GetInt2(setValue.Parameter);

            Assert.IsTrue(success);
            Assert.AreEqual(setValue.Int2Value, inputValue);
        }

        [Test, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if attempting to set a non-existant int2 input value properly returns false when setting its value on a target SubstanceGraphSO asset using the try set method.")]
        public void TrySetInt2Fail()
        {
            SubstanceGraphSO substance = SubstanceGraphTestAsset.Substance;
            SubstanceParameterValue defaultValue = SubstanceGraphTestAsset.DefaultInt2Value;

            Vector2Int inputValue = substance.GetInt2(defaultValue.Parameter);

            Assert.AreEqual(defaultValue.Int2Value, inputValue);

            bool success = substance.TrySetInt2(Vector2Int.one, "I am a great magician.");

            substance.TryGetInt2(out inputValue, "I am a great magician.");

            Assert.IsFalse(success);
            Assert.AreEqual(Vector2Int.zero, inputValue);
        }

        #endregion

        #region Int3

        [Test, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if an int3 input value can properly be referenced on a target SubstanceGraphSO asset.")]
        public void GetInt3()
        {
            SubstanceGraphSO substance = SubstanceGraphTestAsset.Substance;
            SubstanceParameterValue parameterValue = SubstanceGraphTestAsset.DefaultInt3Value;

            Vector3Int inputValue = substance.GetInt3(parameterValue.Parameter);

            Assert.AreEqual(parameterValue.Int3Value, inputValue);
        }

        [Test, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if an int3 input value can properly be referenced on a target SubstanceGraphSO asset using the try get method.")]
        public void TryGetInt3()
        {
            SubstanceGraphSO substance = SubstanceGraphTestAsset.Substance;
            SubstanceParameterValue parameterValue = SubstanceGraphTestAsset.DefaultInt3Value;

            bool success = substance.TryGetInt3(out Vector3Int inputValue, parameterValue.Parameter);

            Assert.IsTrue(success);
            Assert.AreEqual(parameterValue.Int3Value, inputValue);
        }

        [Test, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if a default int3 value can properly be returned when referencing a non-existant input on a target SubstanceGraphSO asset using the try get method.")]
        public void TryGetInt3Fail()
        {
            SubstanceGraphSO substance = SubstanceGraphTestAsset.Substance;

            bool success = substance.TryGetInt3(out Vector3Int inputValue, "Ventriloquists, huh?");

            Assert.IsFalse(success);
            Assert.AreEqual(Vector3Int.zero, inputValue);
        }

        [Test, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if an int3 input value can properly be set on a target SubstanceGraphSO asset.")]
        public void SetInt3()
        {
            SubstanceGraphSO substance = SubstanceGraphTestAsset.Substance;
            SubstanceParameterValue defaultValue = SubstanceGraphTestAsset.DefaultInt3Value;

            Vector3Int inputValue = substance.GetInt3(defaultValue.Parameter);

            Assert.AreEqual(defaultValue.Int3Value, inputValue);

            SubstanceParameterValue setValue = SubstanceGraphTestAsset.SetInt3Value;

            substance.SetInt3(setValue.Int3Value, setValue.Parameter);

            inputValue = substance.GetInt3(setValue.Parameter);

            Assert.AreEqual(setValue.Int3Value, inputValue);
        }

        [Test, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if an int3 input value can properly be set on a target SubstanceGraphSO asset using the try set method.")]
        public void TrySetInt3()
        {
            SubstanceGraphSO substance = SubstanceGraphTestAsset.Substance;
            SubstanceParameterValue defaultValue = SubstanceGraphTestAsset.DefaultInt3Value;

            Vector3Int inputValue = substance.GetInt3(defaultValue.Parameter);

            Assert.AreEqual(defaultValue.Int3Value, inputValue);

            SubstanceParameterValue setValue = SubstanceGraphTestAsset.SetInt3Value;

            bool success = substance.TrySetInt3(setValue.Int3Value, setValue.Parameter);

            inputValue = substance.GetInt3(setValue.Parameter);

            Assert.IsTrue(success);
            Assert.AreEqual(setValue.Int3Value, inputValue);
        }

        [Test, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if attempting to set a non-existant int3 input value properly returns false when setting its value on a target SubstanceGraphSO asset using the try set method.")]
        public void TrySetInt3Fail()
        {
            SubstanceGraphSO substance = SubstanceGraphTestAsset.Substance;
            SubstanceParameterValue defaultValue = SubstanceGraphTestAsset.DefaultInt3Value;

            Vector3Int inputValue = substance.GetInt3(defaultValue.Parameter);

            Assert.AreEqual(defaultValue.Int3Value, inputValue);

            bool success = substance.TrySetInt3(Vector3Int.one, "I am a great magician.");

            substance.TryGetInt3(out inputValue, "I am a great magician.");

            Assert.IsFalse(success);
            Assert.AreEqual(Vector3Int.zero, inputValue);
        }

        #endregion

        #region Int4

        [Test, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if an int4 input value can properly be referenced on a target SubstanceGraphSO asset.")]
        public void GetInt4()
        {
            SubstanceGraphSO substance = SubstanceGraphTestAsset.Substance;
            SubstanceParameterValue parameterValue = SubstanceGraphTestAsset.DefaultInt4Value;

            int[] defaultValue = new int[4] { parameterValue.Int4Value.x, parameterValue.Int4Value.y, parameterValue.Int4Value.z, parameterValue.Int4Value.w };
            int[] inputValue = substance.GetInt4(parameterValue.Parameter);

            Assert.AreEqual(defaultValue, inputValue);
        }

        [Test, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if an int4 input value can properly be referenced on a target SubstanceGraphSO asset using the try get method.")]
        public void TryGetInt4()
        {
            SubstanceGraphSO substance = SubstanceGraphTestAsset.Substance;
            SubstanceParameterValue parameterValue = SubstanceGraphTestAsset.DefaultInt4Value;
            int[] defaultValue = new int[4] { parameterValue.Int4Value.x, parameterValue.Int4Value.y, parameterValue.Int4Value.z, parameterValue.Int4Value.w };

            bool success = substance.TryGetInt4(out int[] inputValue, parameterValue.Parameter);

            Assert.IsTrue(success);
            Assert.AreEqual(defaultValue, inputValue);
        }

        [Test, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if a default int4 value can properly be returned when referencing a non-existant input on a target SubstanceGraphSO asset using the try get method.")]
        public void TryGetInt4Fail()
        {
            SubstanceGraphSO substance = SubstanceGraphTestAsset.Substance;
            int[] defaultValue = new int[4] { 0, 0, 0, 0 };

            bool success = substance.TryGetInt4(out int[] inputValue, "Face to foot style!");

            Assert.IsFalse(success);
            Assert.AreEqual(defaultValue, inputValue);
        }

        [Test, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if an int4 input value can properly be referenced on a target SubstanceGraphSO asset.")]
        public void GetInt4Vector()
        {
            SubstanceGraphSO substance = SubstanceGraphTestAsset.Substance;
            SubstanceParameterValue parameterValue = SubstanceGraphTestAsset.DefaultInt4Value;

            Vector4Int inputValue = substance.GetInt4Vector(parameterValue.Parameter);

            Assert.AreEqual(parameterValue.Int4Value, inputValue);
        }

        [Test, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if an int4 input value can properly be referenced on a target SubstanceGraphSO asset using the try get method.")]
        public void TryGetInt4Vector()
        {
            SubstanceGraphSO substance = SubstanceGraphTestAsset.Substance;
            SubstanceParameterValue parameterValue = SubstanceGraphTestAsset.DefaultInt4Value;

            bool success = substance.TryGetInt4Vector(out Vector4Int inputValue, parameterValue.Parameter);

            Assert.IsTrue(success);
            Assert.AreEqual(parameterValue.Int4Value, inputValue);
        }

        [Test, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if a default int4 value can properly be returned when referencing a non-existant input on a target SubstanceGraphSO asset using the try get method.")]
        public void TryGetInt4VectorFail()
        {
            SubstanceGraphSO substance = SubstanceGraphTestAsset.Substance;

            bool success = substance.TryGetInt4Vector(out Vector4Int inputValue, "Face to foot style!");

            Assert.IsFalse(success);
            Assert.AreEqual(Vector4Int.zero, inputValue);
        }

        [Test, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if an int4 input value can properly be set on a target SubstanceGraphSO asset.")]
        public void SetInt4()
        {
            SubstanceGraphSO substance = SubstanceGraphTestAsset.Substance;
            SubstanceParameterValue defaultValue = SubstanceGraphTestAsset.DefaultInt4Value;

            Vector4Int inputValue = substance.GetInt4Vector(defaultValue.Parameter);

            Assert.AreEqual(defaultValue.Int4Value, inputValue);

            SubstanceParameterValue setValue = SubstanceGraphTestAsset.SetInt4Value;

            substance.SetInt4(setValue.Int4Value, setValue.Parameter);

            inputValue = substance.GetInt4Vector(setValue.Parameter);

            Assert.AreEqual(setValue.Int4Value, inputValue);
        }

        [Test, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if an int4 input value can properly be set on a target SubstanceGraphSO asset using the try set method.")]
        public void TrySetInt4()
        {
            SubstanceGraphSO substance = SubstanceGraphTestAsset.Substance;
            SubstanceParameterValue defaultValue = SubstanceGraphTestAsset.DefaultInt4Value;

            Vector4Int inputValue = substance.GetInt4Vector(defaultValue.Parameter);

            Assert.AreEqual(defaultValue.Int4Value, inputValue);

            SubstanceParameterValue setValue = SubstanceGraphTestAsset.SetInt4Value;

            bool success = substance.TrySetInt4(setValue.Int4Value, setValue.Parameter);

            inputValue = substance.GetInt4Vector(setValue.Parameter);

            Assert.IsTrue(success);
            Assert.AreEqual(setValue.Int4Value, inputValue);
        }

        [Test, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if attempting to set a non-existant int4 input value properly returns false when setting its value on a target SubstanceGraphSO asset using the try set method.")]
        public void TrySetInt4Fail()
        {
            SubstanceGraphSO substance = SubstanceGraphTestAsset.Substance;
            SubstanceParameterValue defaultValue = SubstanceGraphTestAsset.DefaultInt4Value;

            Vector4Int inputValue = substance.GetInt4Vector(defaultValue.Parameter);

            Assert.AreEqual(defaultValue.Int4Value, inputValue);

            bool success = substance.TrySetInt4(Vector4Int.one, "I am a great magician.");

            substance.TryGetInt4Vector(out inputValue, "I am a great magician.");

            Assert.IsFalse(success);
            Assert.AreEqual(Vector4Int.zero, inputValue);
        }

        #endregion

        #region Float

        [Test, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if a float input value can properly be referenced on a target SubstanceGraphSO asset.")]
        public void GetFloat()
        {
            SubstanceGraphSO substance = SubstanceGraphTestAsset.Substance;
            SubstanceParameterValue parameterValue = SubstanceGraphTestAsset.DefaultFloatValue;

            float inputValue = substance.GetFloat(parameterValue.Parameter);

            Assert.AreEqual(parameterValue.FloatValue, inputValue);
        }

        [Test, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if a float input value can properly be referenced on a target SubstanceGraphSO asset using the try get method.")]
        public void TryGetFloat()
        {
            SubstanceGraphSO substance = SubstanceGraphTestAsset.Substance;
            SubstanceParameterValue parameterValue = SubstanceGraphTestAsset.DefaultFloatValue;

            bool success = substance.TryGetFloat(out float inputValue, parameterValue.Parameter);

            Assert.IsTrue(success);
            Assert.AreEqual(parameterValue.FloatValue, inputValue);
        }

        [Test, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if a default float value can properly be returned when referencing a non-existant input on a target SubstanceGraphSO asset using the try get method.")]
        public void TryGetFloatFail()
        {
            SubstanceGraphSO substance = SubstanceGraphTestAsset.Substance;

            bool success = substance.TryGetFloat(out float inputValue, "And so, he walked.");

            Assert.IsFalse(success);
            Assert.AreEqual(0f, inputValue);
        }

        [Test, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if a float input value can properly be set on a target SubstanceGraphSO asset.")]
        public void SetFloat()
        {
            SubstanceGraphSO substance = SubstanceGraphTestAsset.Substance;
            SubstanceParameterValue defaultValue = SubstanceGraphTestAsset.DefaultFloatValue;

            float inputValue = substance.GetFloat(defaultValue.Parameter);

            Assert.AreEqual(defaultValue.FloatValue, inputValue);

            SubstanceParameterValue setValue = SubstanceGraphTestAsset.SetFloatValue;

            substance.SetFloat(setValue.FloatValue, setValue.Parameter);

            inputValue = substance.GetFloat(setValue.Parameter);

            Assert.AreEqual(setValue.FloatValue, inputValue);
        }

        [Test, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if a float input value can properly be set on a target SubstanceGraphSO asset using the try set method.")]
        public void TrySetFloat()
        {
            SubstanceGraphSO substance = SubstanceGraphTestAsset.Substance;
            SubstanceParameterValue defaultValue = SubstanceGraphTestAsset.DefaultFloatValue;

            float inputValue = substance.GetFloat(defaultValue.Parameter);

            Assert.AreEqual(defaultValue.FloatValue, inputValue);

            SubstanceParameterValue setValue = SubstanceGraphTestAsset.SetFloatValue;

            bool success = substance.TrySetFloat(setValue.FloatValue, setValue.Parameter);

            inputValue = substance.GetFloat(setValue.Parameter);

            Assert.IsTrue(success);
            Assert.AreEqual(setValue.FloatValue, inputValue);
        }

        [Test, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if attempting to set a non-existant float input value properly returns false when setting its value on a target SubstanceGraphSO asset using the try set method.")]
        public void TrySetFloatFail()
        {
            SubstanceGraphSO substance = SubstanceGraphTestAsset.Substance;
            SubstanceParameterValue defaultValue = SubstanceGraphTestAsset.DefaultFloatValue;

            float inputValue = substance.GetFloat(defaultValue.Parameter);

            Assert.AreEqual(defaultValue.FloatValue, inputValue);

            bool success = substance.TrySetFloat(1f, "I am a great magician.");

            substance.TryGetFloat(out inputValue, "I am a great magician.");

            Assert.IsFalse(success);
            Assert.AreEqual(0f, inputValue);
        }

        #endregion

        #region Float2

        [Test, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if a float2 input value can properly be referenced on a target SubstanceGraphSO asset.")]
        public void GetFloat2()
        {
            SubstanceGraphSO substance = SubstanceGraphTestAsset.Substance;
            SubstanceParameterValue parameterValue = SubstanceGraphTestAsset.DefaultFloat2Value;

            Vector2 inputValue = substance.GetFloat2(parameterValue.Parameter);

            Assert.AreEqual(parameterValue.Float2Value, inputValue);
        }

        [Test, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if a float2 input value can properly be referenced on a target SubstanceGraphSO asset using the try get method.")]
        public void TryGetFloat2()
        {
            SubstanceGraphSO substance = SubstanceGraphTestAsset.Substance;
            SubstanceParameterValue parameterValue = SubstanceGraphTestAsset.DefaultFloat2Value;

            bool success = substance.TryGetFloat2(out Vector2 inputValue, parameterValue.Parameter);

            Assert.IsTrue(success);
            Assert.AreEqual(parameterValue.Float2Value, inputValue);
        }

        [Test, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if a default float2 value can properly be returned when referencing a non-existant input on a target SubstanceGraphSO asset using the try get method.")]
        public void TryGetFloat2Fail()
        {
            SubstanceGraphSO substance = SubstanceGraphTestAsset.Substance;

            bool success = substance.TryGetFloat2(out Vector2 inputValue, "And sometimes, drove.");

            Assert.IsFalse(success);
            Assert.AreEqual(Vector2.zero, inputValue);
        }

        [Test, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if a float2 input value can properly be set on a target SubstanceGraphSO asset.")]
        public void SetFloat2()
        {
            SubstanceGraphSO substance = SubstanceGraphTestAsset.Substance;
            SubstanceParameterValue defaultValue = SubstanceGraphTestAsset.DefaultFloat2Value;

            Vector2 inputValue = substance.GetFloat2(defaultValue.Parameter);

            Assert.AreEqual(defaultValue.Float2Value, inputValue);

            SubstanceParameterValue setValue = SubstanceGraphTestAsset.SetFloat2Value;

            substance.SetFloat2(setValue.Float2Value, setValue.Parameter);

            inputValue = substance.GetFloat2(setValue.Parameter);

            Assert.AreEqual(setValue.Float2Value, inputValue);
        }

        [Test, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if a float2 input value can properly be set on a target SubstanceGraphSO asset using the try set method.")]
        public void TrySetFloat2()
        {
            SubstanceGraphSO substance = SubstanceGraphTestAsset.Substance;
            SubstanceParameterValue defaultValue = SubstanceGraphTestAsset.DefaultFloat2Value;

            Vector2 inputValue = substance.GetFloat2(defaultValue.Parameter);

            Assert.AreEqual(defaultValue.Float2Value, inputValue);

            SubstanceParameterValue setValue = SubstanceGraphTestAsset.SetFloat2Value;

            bool success = substance.TrySetFloat2(setValue.Float2Value, setValue.Parameter);

            inputValue = substance.GetFloat2(setValue.Parameter);

            Assert.IsTrue(success);
            Assert.AreEqual(setValue.Float2Value, inputValue);
        }

        [Test, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if attempting to set a non-existant float2 input value properly returns false when setting its value on a target SubstanceGraphSO asset using the try set method.")]
        public void TrySetFloat2Fail()
        {
            SubstanceGraphSO substance = SubstanceGraphTestAsset.Substance;
            SubstanceParameterValue defaultValue = SubstanceGraphTestAsset.DefaultFloat2Value;

            Vector2 inputValue = substance.GetFloat2(defaultValue.Parameter);

            Assert.AreEqual(defaultValue.Float2Value, inputValue);

            bool success = substance.TrySetFloat2(Vector2.one, "I am a great magician.");

            substance.TryGetFloat2(out inputValue, "I am a great magician.");

            Assert.IsFalse(success);
            Assert.AreEqual(Vector2.zero, inputValue);
        }

        #endregion

        #region Float3

        [Test, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if a float3 input value can properly be referenced on a target SubstanceGraphSO asset.")]
        public void GetFloat3()
        {
            SubstanceGraphSO substance = SubstanceGraphTestAsset.Substance;
            SubstanceParameterValue parameterValue = SubstanceGraphTestAsset.DefaultFloat3Value;

            Vector3 inputValue = substance.GetFloat3(parameterValue.Parameter);

            Assert.AreEqual(parameterValue.Float3Value, inputValue);
        }

        [Test, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if a float3 input value can properly be referenced on a target SubstanceGraphSO asset using the try get method.")]
        public void TryGetFloat3()
        {
            SubstanceGraphSO substance = SubstanceGraphTestAsset.Substance;
            SubstanceParameterValue parameterValue = SubstanceGraphTestAsset.DefaultFloat3Value;

            bool success = substance.TryGetFloat3(out Vector3 inputValue, parameterValue.Parameter);

            Assert.IsTrue(success);
            Assert.AreEqual(parameterValue.Float3Value, inputValue);
        }

        [Test, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if a default float3 value can properly be returned when referencing a non-existant input on a target SubstanceGraphSO asset using the try get method.")]
        public void TryGetFloat3Fail()
        {
            SubstanceGraphSO substance = SubstanceGraphTestAsset.Substance;

            bool success = substance.TryGetFloat3(out Vector3 inputValue, "I mean, I'm no doctor.");

            Assert.IsFalse(success);
            Assert.AreEqual(Vector3.zero, inputValue);
        }

        [Test, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if a float3 input value can properly be set on a target SubstanceGraphSO asset.")]
        public void SetFloat3()
        {
            SubstanceGraphSO substance = SubstanceGraphTestAsset.Substance;
            SubstanceParameterValue defaultValue = SubstanceGraphTestAsset.DefaultFloat3Value;

            Vector3 inputValue = substance.GetFloat3(defaultValue.Parameter);

            Assert.AreEqual(defaultValue.Float3Value, inputValue);

            SubstanceParameterValue setValue = SubstanceGraphTestAsset.SetFloat3Value;

            substance.SetFloat3(setValue.Float3Value, setValue.Parameter);

            inputValue = substance.GetFloat3(setValue.Parameter);

            Assert.AreEqual(setValue.Float3Value, inputValue);
        }

        [Test, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if a float3 input value can properly be set on a target SubstanceGraphSO asset using the try set method.")]
        public void TrySetFloat3()
        {
            SubstanceGraphSO substance = SubstanceGraphTestAsset.Substance;
            SubstanceParameterValue defaultValue = SubstanceGraphTestAsset.DefaultFloat3Value;

            Vector3 inputValue = substance.GetFloat3(defaultValue.Parameter);

            Assert.AreEqual(defaultValue.Float3Value, inputValue);

            SubstanceParameterValue setValue = SubstanceGraphTestAsset.SetFloat3Value;

            bool success = substance.TrySetFloat3(setValue.Float3Value, setValue.Parameter);

            inputValue = substance.GetFloat3(setValue.Parameter);

            Assert.IsTrue(success);
            Assert.AreEqual(setValue.Float3Value, inputValue);
        }

        [Test, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if attempting to set a non-existant float3 input value properly returns false when setting its value on a target SubstanceGraphSO asset using the try set method.")]
        public void TrySetFloat3Fail()
        {
            SubstanceGraphSO substance = SubstanceGraphTestAsset.Substance;
            SubstanceParameterValue defaultValue = SubstanceGraphTestAsset.DefaultFloat3Value;

            Vector3 inputValue = substance.GetFloat3(defaultValue.Parameter);

            Assert.AreEqual(defaultValue.Float3Value, inputValue);

            bool success = substance.TrySetFloat3(Vector3.one, "I am a great magician.");

            substance.TryGetFloat3(out inputValue, "I am a great magician.");

            Assert.IsFalse(success);
            Assert.AreEqual(Vector3.zero, inputValue);
        }

        #endregion

        #region Float4

        [Test, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if a float4 input value can properly be referenced on a target SubstanceGraphSO asset.")]
        public void GetFloat4()
        {
            SubstanceGraphSO substance = SubstanceGraphTestAsset.Substance;
            SubstanceParameterValue parameterValue = SubstanceGraphTestAsset.DefaultFloat4Value;

            Vector4 inputValue = substance.GetFloat4(parameterValue.Parameter);

            Assert.AreEqual(parameterValue.Float4Value, inputValue);
        }

        [Test, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if a float4 input value can properly be referenced on a target SubstanceGraphSO asset using the try get method.")]
        public void TryGetFloat4()
        {
            SubstanceGraphSO substance = SubstanceGraphTestAsset.Substance;
            SubstanceParameterValue parameterValue = SubstanceGraphTestAsset.DefaultFloat4Value;

            bool success = substance.TryGetFloat4(out Vector4 inputValue, parameterValue.Parameter);

            Assert.IsTrue(success);
            Assert.AreEqual(parameterValue.Float4Value, inputValue);
        }

        [Test, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if a default float4 value can properly be returned when referencing a non-existant input on a target SubstanceGraphSO asset using the try get method.")]
        public void TryGetFloat4Fail()
        {
            SubstanceGraphSO substance = SubstanceGraphTestAsset.Substance;

            bool success = substance.TryGetFloat4(out Vector4 inputValue, "It was like, one clean chunk!");

            Assert.IsFalse(success);
            Assert.AreEqual(Vector4.zero, inputValue);
        }

        [Test, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if a float4 input value can properly be set on a target SubstanceGraphSO asset.")]
        public void SetFloat4()
        {
            SubstanceGraphSO substance = SubstanceGraphTestAsset.Substance;
            SubstanceParameterValue defaultValue = SubstanceGraphTestAsset.DefaultFloat4Value;

            Vector4 inputValue = substance.GetFloat4(defaultValue.Parameter);

            Assert.AreEqual(defaultValue.Float4Value, inputValue);

            SubstanceParameterValue setValue = SubstanceGraphTestAsset.SetFloat4Value;

            substance.SetFloat4(setValue.Float4Value, setValue.Parameter);

            inputValue = substance.GetFloat4(setValue.Parameter);

            Assert.AreEqual(setValue.Float4Value, inputValue);
        }

        [Test, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if a float4 input value can properly be set on a target SubstanceGraphSO asset using the try set method.")]
        public void TrySetFloat4()
        {
            SubstanceGraphSO substance = SubstanceGraphTestAsset.Substance;
            SubstanceParameterValue defaultValue = SubstanceGraphTestAsset.DefaultFloat4Value;

            Vector4 inputValue = substance.GetFloat4(defaultValue.Parameter);

            Assert.AreEqual(defaultValue.Float4Value, inputValue);

            SubstanceParameterValue setValue = SubstanceGraphTestAsset.SetFloat4Value;

            bool success = substance.TrySetFloat4(setValue.Float4Value, setValue.Parameter);

            inputValue = substance.GetFloat4(setValue.Parameter);

            Assert.IsTrue(success);
            Assert.AreEqual(setValue.Float4Value, inputValue);
        }

        [Test, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if attempting to set a non-existant float4 input value properly returns false when setting its value on a target SubstanceGraphSO asset using the try set method.")]
        public void TrySetFloat4Fail()
        {
            SubstanceGraphSO substance = SubstanceGraphTestAsset.Substance;
            SubstanceParameterValue defaultValue = SubstanceGraphTestAsset.DefaultFloat4Value;

            Vector4 inputValue = substance.GetFloat4(defaultValue.Parameter);

            Assert.AreEqual(defaultValue.Float4Value, inputValue);

            bool success = substance.TrySetFloat4(Vector2.one, "I am a great magician.");

            substance.TryGetFloat4(out inputValue, "I am a great magician.");

            Assert.IsFalse(success);
            Assert.AreEqual(Vector4.zero, inputValue);
        }

        #endregion

        #region Texture

        [Test, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if a texture input value can properly be referenced on a target SubstanceGraphSO asset.")]
        public void GetTexture()
        {
            SubstanceGraphSO substance = SubstanceGraphTestAsset.Substance;
            SubstanceParameterValue parameterValue = SubstanceGraphTestAsset.DefaultTextureValue;

            Texture2D inputValue = substance.GetTexture(parameterValue.Parameter);

            Assert.AreEqual(parameterValue.TextureValue, inputValue);
        }

        [Test, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if a null texture input value can properly be referenced on a target SubstanceGraphSO asset.")]
        public void GetNullTexture()
        {
            SubstanceGraphSO substance = SubstanceGraphTestAsset.Substance;
            SubstanceParameterValue parameterValue = SubstanceGraphTestAsset.DefaultTextureNullValue;

            Texture2D inputValue = substance.GetTexture(parameterValue.Parameter);

            Assert.IsNull(inputValue);
        }

        [Test, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if a texture input value can properly be referenced on a target SubstanceGraphSO asset using the try get method.")]
        public void TryGetTexture()
        {
            SubstanceGraphSO substance = SubstanceGraphTestAsset.Substance;
            SubstanceParameterValue parameterValue = SubstanceGraphTestAsset.DefaultTextureValue;

            bool success = substance.TryGetTexture(out Texture2D inputValue, parameterValue.Parameter);

            Assert.IsTrue(success);
            Assert.AreEqual(parameterValue.TextureValue, inputValue);
        }

        [Test, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if a default texture value can properly be returned when referencing a non-existant input on a target SubstanceGraphSO asset using the try get method.")]
        public void TryGetTextureFail()
        {
            SubstanceGraphSO substance = SubstanceGraphTestAsset.Substance;

            bool success = substance.TryGetTexture(out Texture2D inputValue, "I mean, I'm no doctor.");

            Assert.IsFalse(success);
            Assert.IsNull(inputValue);
        }

        #endregion

        #region Output Size

        [Test, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if an $outputsize input value can properly be referenced on a target SubstanceGraphSO asset.")]
        public void GetOutputSize()
        {
            SubstanceGraphSO substance = SubstanceGraphTestAsset.Substance;
            SubstanceParameterValue parameterValue = SubstanceGraphTestAsset.DefaultOutputSizeValue;

            Vector2Int inputValue = substance.GetOutputSize();

            Assert.AreEqual(parameterValue.Int2Value, inputValue);
        }

        [Test, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if an $outputsize input value can properly be set on a target SubstanceGraphSO asset.")]
        public void SetOutputSize()
        {
            SubstanceGraphSO substance = SubstanceGraphTestAsset.Substance;
            SubstanceParameterValue parameterValue = SubstanceGraphTestAsset.DefaultOutputSizeValue;

            Vector2Int inputValue = substance.GetOutputSize();

            Assert.AreEqual(parameterValue.Int2Value, inputValue);

            SubstanceParameterValue setValue = SubstanceGraphTestAsset.SetOutputSizeValue;

            substance.SetOutputSize(setValue.Int2Value);

            inputValue = substance.GetOutputSize(setValue.Parameter);

            Assert.AreEqual(setValue.Int2Value, inputValue);
        }

        #endregion

        #region Random Seed

        [Test, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if a $randomseed input value can properly be referenced on a target SubstanceGraphSO asset.")]
        public void GetRandomSeed()
        {
            SubstanceGraphSO substance = SubstanceGraphTestAsset.Substance;
            SubstanceParameterValue parameterValue = SubstanceGraphTestAsset.DefaultRandomSeedValue;

            int inputValue = substance.GetRandomSeed();

            Assert.AreEqual(parameterValue.IntValue, inputValue);
        }

        [Test, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if a $randomseed input value can properly be set on a target SubstanceGraphSO asset.")]
        public void SetRandomSeed()
        {
            SubstanceGraphSO substance = SubstanceGraphTestAsset.Substance;
            SubstanceParameterValue parameterValue = SubstanceGraphTestAsset.DefaultRandomSeedValue;

            int inputValue = substance.GetRandomSeed();

            Assert.AreEqual(parameterValue.IntValue, inputValue);

            SubstanceParameterValue setValue = SubstanceGraphTestAsset.SetRandomSeedValue;

            substance.SetRandomSeed(setValue.IntValue);

            inputValue = substance.GetRandomSeed(setValue.Parameter);

            Assert.AreEqual(setValue.IntValue, inputValue);
        }

        #endregion

        #region Output

        [Test, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if an output map can properly be referenced on a target SubstanceGraphSO asset.")]
        public void GetOutputMap()
        {
            SubstanceGraphSO substance = SubstanceGraphTestAsset.Substance;
            SubstanceOutput outputMap = SubstanceGraphTestAsset.OutputMap;
            Texture2D outputValue = SubstanceGraphTestAsset.OutputValue;

            Texture2D outputTexture = substance.GetOutputTexture(outputMap.Name);

            Assert.AreEqual(outputValue, outputTexture);
        }

        #endregion
    }
}