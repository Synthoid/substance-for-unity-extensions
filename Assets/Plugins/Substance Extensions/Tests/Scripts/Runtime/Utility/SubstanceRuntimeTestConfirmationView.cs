using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace SOS.SubstanceExtensions.Tests
{
    public class SubstanceRuntimeTestConfirmationView : MonoBehaviour
    {
        public const int kStatusValid   = 1;
        public const int kStatusInvalid = 2;
        public const int kStatusIgnore  = 3;
        public const int kStatusBreak   = 4;

#if ENABLE_INPUT_SYSTEM
        private const string kInputSystemValidBinding = "<Keyboard>/Return";
        private const string kInputSystemInvalidBinding = "<Keyboard>/Backspace";
        private const string kInputSystemIgnoreBinding = "<Keyboard>/Space";
        private const string kInputSystemBreakBinding = "<Keyboard>/Escape";
#else
        /// <summary>
        /// Key pressed to mark a viewed substance as valid.
        /// </summary>
        private const KeyCode kValidKey = KeyCode.Return;
        /// <summary>
        /// Key pressed to mark a viewed substance as invalid.
        /// </summary>
        private const KeyCode kInvalidKey = KeyCode.Backspace;
        /// <summary>
        /// Key pressed to ignore a viewed substance.
        /// </summary>
        private const KeyCode kIgnoreKey = KeyCode.Space;
        /// <summary>
        /// Key pressed to mark a viewed substance as invalid stop further tests.
        /// </summary>
        private const KeyCode kBreakKey = KeyCode.Escape;
#endif

        [SerializeField, Tooltip("Label displaying instructions for the current test.")]
        private TMP_Text instructionsLabel = null;
        [SerializeField, Tooltip("Label displaying notes for the current test.")]
        private TMP_Text notesLabel = null;
        [Header("Expected")]
        [SerializeField, Tooltip("RawImage displaying the expected substance texture's visuals in 2D space.")]
        private RawImage expectedPreview2D = null;
        [SerializeField, Tooltip("Renderer displaying the expected substance texture's visuals in 3D space.")]
        private Renderer expectedPreview3D = null;
        [Header("Results")]
        [SerializeField, Tooltip("RawImage displaying the rendered substance texture in 2D space.")]
        private RawImage resultPreview2D = null;
        [SerializeField, Tooltip("Renderer displaying the rendered substance texture in 3D space.")]
        private Renderer resultPreview3D = null;

        private static SubstanceRuntimeTestConfirmationView instance = null;

        public static SubstanceRuntimeTestConfirmationView Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = FindObjectOfType<SubstanceRuntimeTestConfirmationView>();
                }

                return instance;
            }
        }


        public void ShowResults(Texture2D expected, Texture2D render, string note, System.Action<int> callback)
        {
            expectedPreview2D.texture = expected;
            expectedPreview3D.material.mainTexture = expected;

            resultPreview2D.texture = render;
            resultPreview3D.material.mainTexture = render;

            notesLabel.text = note;

            if(callback != null) StartCoroutine(WaitForValidationInput(callback));
        }


        private IEnumerator WaitForValidationInput(System.Action<int> callback)
        {
            int status = 0;

#if ENABLE_INPUT_SYSTEM
            
#else
            while(status == 0)
            {
                yield return null;

                if(Input.anyKeyDown)
                {
                    //Valid
                    if(Input.GetKeyDown(kValidKey))
                    {
                        status = kStatusValid;
                    }
                    //Invalid
                    else if(Input.GetKeyDown(kInvalidKey))
                    {
                        status = kStatusInvalid;
                    }
                    //Ignore
                    else if(Input.GetKeyDown(kIgnoreKey))
                    {
                        status = kStatusIgnore;
                    }
                    //Break
                    else if(Input.GetKeyDown(kBreakKey))
                    {
                        status = kStatusBreak;
                    }
                }
            }
#endif

            callback.Invoke(status);
        }
    }
}