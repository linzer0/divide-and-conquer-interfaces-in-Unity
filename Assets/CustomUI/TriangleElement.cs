using UnityEngine;
using UnityEngine.UIElements;

namespace CustomUI
{
    public class TriangleElement : VisualElement
    {
        public new class UxmlFactory : UxmlFactory<TriangleElement>
        {
        };

        public TriangleElement()
        {
            generateVisualContent += GenerateVisualContent;
        }

        Vertex[] vertices = new Vertex[3];
        ushort[] indices = { 0, 1, 2 };

        void GenerateVisualContent(MeshGenerationContext mgc)
        {
            vertices[0].tint = Color.red;
            vertices[1].tint = Color.red;
            vertices[2].tint = Color.red;

            var leftCorner = 0f;
            var rightCorner = contentRect.width;
            var top = 0;
            var bottom = contentRect.height;
            var middleX = contentRect.width / 2;

            vertices[0].position = new Vector3(leftCorner, bottom, Vertex.nearZ);
            vertices[1].position = new Vector3(middleX, top, Vertex.nearZ);
            vertices[2].position = new Vector3(rightCorner, bottom, Vertex.nearZ);

            MeshWriteData mwd = mgc.Allocate(vertices.Length, indices.Length);
            mwd.SetAllVertices(vertices);
            mwd.SetAllIndices(indices);
        }
    }
}