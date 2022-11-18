using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SOS.SubstanceExtensions.Tests
{
    public class RuntimeTestRotation : MonoBehaviour
    {
        [SerializeField]
        private Vector3 speed = new Vector3(0f, 180f, 0f);
        
        private void Update()
        {
            transform.Rotate(speed * Time.deltaTime);
        }
    }
}