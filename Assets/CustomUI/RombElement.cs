using UnityEngine;
using UnityEngine.UIElements;

namespace CustomUI
{
    public class RombElement : VisualElement
    {
        public new class UxmlFactory : UxmlFactory<RombElement> { }
        
        public RombElement()
        {
            generateVisualContent += GenerateVisualContent;
        }

        Vertex[] vertices = new Vertex[4];
        ushort[] indices = { 0, 1, 2, 2, 3, 0};

        void GenerateVisualContent(MeshGenerationContext mgc)
        {
            vertices[0].tint = new Color32(255, 0, 0, 255);
            vertices[1].tint = new Color32(0, 255, 0, 255);
            vertices[2].tint = new Color32(0, 0, 255, 255);
            vertices[3].tint = new Color32(17, 55, 55, 255);

            var top = 0;
            var left = 0f;
            var middleX = contentRect.width / 2;
            var middleY = contentRect.height / 2;
            var right = contentRect.width;
            var bottom = contentRect.height;

            vertices[0].position = new Vector3(left, middleY, Vertex.nearZ);
            vertices[1].position = new Vector3(middleX, top, Vertex.nearZ);
            vertices[2].position = new Vector3(right, middleY, Vertex.nearZ);
            vertices[3].position = new Vector3(middleX, bottom, Vertex.nearZ);

            MeshWriteData mwd = mgc.Allocate(vertices.Length, indices.Length);
            mwd.SetAllVertices(vertices);
            mwd.SetAllIndices(indices);
        }
    }
}
