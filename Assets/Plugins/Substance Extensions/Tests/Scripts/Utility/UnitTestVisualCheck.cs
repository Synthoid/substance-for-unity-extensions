using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace SOS.SubstanceExtensions.Tests
{
    //TODO: This class will block UnityTest progression until a confirm or reject key is pressed, or a max time limit is reached...
    public class UnitTestVisualCheck : MonoBehaviour
    {
        public enum CheckStatus
        {
            None    = 0,
            Passed  = 1,
            Failed  = 2,
            Ignored = 3
        }

        [Tooltip("Time (in seconds) before a vidual check times out and auto progresses.")]
        public float timeout = 60f;
        [Tooltip("Key pressed to mark a visual check as 'passed'.")]
        public KeyCode passKey = KeyCode.RightArrow;
        [Tooltip("Key pressed to mark a visual check as 'failed'.")]
        public KeyCode failKey = KeyCode.LeftArrow;
        [Tooltip("Key pressed to mark a visual check as 'inored'.")]
        public KeyCode ignoreKey = KeyCode.DownArrow;
        [Header("UI")]
        public Text instructionLabel = null;

        private System.Action<CheckStatus> Callback { get; set; }

        public void StartVisualCheck(string instruction, System.Action<CheckStatus> callback)
        {
            instructionLabel.text = instruction;
            Callback = callback;

            StartCoroutine(WaitCoroutine());
        }

        private IEnumerator WaitCoroutine()
        {
            float t = timeout;

            while(!Input.anyKeyDown || !(Input.GetKeyDown(passKey) || Input.GetKeyDown(failKey) || Input.GetKeyDown(ignoreKey)) && t > 0f)
            {
                t -= Time.deltaTime;
                yield return null;
            }

            if(Input.GetKeyDown(passKey))
            {
                Callback.Invoke(CheckStatus.Passed);
            }
            else if(Input.GetKeyDown(failKey))
            {
                Callback.Invoke(CheckStatus.Failed);
            }
            else
            {
                Callback.Invoke(CheckStatus.Ignored);
            }

            Callback = null;
        }
    }
}