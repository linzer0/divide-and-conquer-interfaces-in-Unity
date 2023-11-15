# Divide and conquer User Interface in Unity

I tell you how to create custom UI elements through Mesh creation using Unity and UI Toolkit.

![alt](https://github.com/linzer0/divide-and-conquer-interfaces-in-Unity/blob/main/ReadmeResouces/0.png)

## What do we do?
Within the framework of this article, we will analyze the possibilities of generating custom UI elements using the UI Toolkit.

First, we'll take a closer look at creating a triangle based on mash generation, and second - a gradient rhombus!

## Chapter 1: Mesh and Triangle!
Within this chapter, let's look at how to make a custom triangle element, as well as tell you in general how this system works.

So, read the code and comments to it, so you can repeat it at home: ).

![alt](https://github.com/linzer0/divide-and-conquer-interfaces-in-Unity/blob/main/ReadmeResouces/1.png)

First, the code will be attached (for the smartest and most intricate) and then there will be a detailed breakdown of most of the points, here we go!

```csharp
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
```
