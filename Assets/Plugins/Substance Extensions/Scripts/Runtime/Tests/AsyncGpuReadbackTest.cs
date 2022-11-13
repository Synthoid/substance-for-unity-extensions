using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;

namespace SOS.SubstanceExtensions
{
    public class AsyncGpuReadbackTest : MonoBehaviour
    {
        public RawImage originalImage = null;
        public RawImage generatedImage = null;
        public Texture2D texture = null;

        private void Test()
        {
            AsyncGPUReadback.Request(texture, 0, OnAsyncReadbackComplete);
        }


        private void OnAsyncReadbackComplete(AsyncGPUReadbackRequest request)
        {
            byte[] bytes = request.GetData<byte>().ToArray();
            Color32[] colors = new Color32[bytes.Length / 4];

            Debug.Log($"{bytes.Length} => {colors.Length} | {(bytes.Length / 4 == colors.Length)}");

            for(int i=0; i < colors.Length; i++)
            {
                int index = i * 4;
                colors[i] = new Color32(bytes[index], bytes[index + 1], bytes[index + 2], bytes[index + 3]);
            }

            Debug.Log(colors[0]);

            Texture2D newTexture = new Texture2D(texture.width, texture.height);

            //newTexture.LoadImage(bytes);
            newTexture.SetPixels32(colors);

            newTexture.Apply();

            generatedImage.texture = newTexture;
        }


        private void Start()
        {
            originalImage.texture = texture;

            Test();
        }
    }
}