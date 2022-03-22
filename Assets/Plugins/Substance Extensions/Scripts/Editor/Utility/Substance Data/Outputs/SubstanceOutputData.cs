using UnityEngine;
using Adobe.Substance;

namespace SOS.SubstanceExtensionsEditor
{
    public struct SubstanceOutputData
    {
        public int graphIndex;
        public int index;
        public string channel;

        public SubstanceOutputData(SubstanceOutputTexture outputData)
        {
            this.graphIndex = outputData.GraphIndex;
            this.index = outputData.Index;
            this.channel = outputData.Description.Channel;
        }
    }
}