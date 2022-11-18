using UnityEngine;

namespace SOS.SubstanceExtensions.Examples
{
    public class ExampleRotate : MonoBehaviour
    {
        [SerializeField, Tooltip("Speed (degrees/second) for the object's rotation.")]
        private Vector3 speed = new Vector3(0f, 180f, 0f);

        private void Update()
        {
            transform.Rotate(speed * Time.deltaTime);
        }
    }
}