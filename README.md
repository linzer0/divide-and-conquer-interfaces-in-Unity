# Divide and conquer User Interface in Unity

I tell you how to create custom UI elements through Mesh creation using **Unity** and **UI Toolkit**.

![alt](https://github.com/linzer0/divide-and-conquer-interfaces-in-Unity/blob/main/ReadmeResouces/0.png)

## What do we do?
Within the framework of this article, we will analyze the possibilities of generating custom UI elements using the **UI Toolkit**.

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


This is what the code looks like, now let's see what's what!

Let's start with the base, the declaration of our class - **TriangleElement**

```csharp
public class TriangleElement : VisualElement
{
    ....
}
```

To create a custom UI element, inheritance of the **VisualElement** class is used. (There are other ways, but I won't go into details right now).

We will use some methods and data from the parent class, such as **contentRect** and **generateVisualElement**;

Let's move on to **UxmlFactory** and displaying the element in the hierarchy:
```csharp
public new class UxmlFactory : UxmlFactory<TriangleElement> { };
```

This is a feature of the **UI Toolkit**, it is necessary for our **TriangleElement** to be added to the general list of UI elements.

After that, we can easily drag it to the layout window.

![alt](https://github.com/linzer0/divide-and-conquer-interfaces-in-Unity/blob/main/ReadmeResouces/2.png)

More details about creating custom elements can be found in the [documentation](https://docs.unity3d.com/Packages/com.unity.ui.builder@1.0/manual/uib-structuring-ui-custom-elements.html)


That's taken care of, let's move on:
```csharp
public TriangleElement()
{
    generateVisualContent += GenerateVisualContent;
}
```

As many of you know, the method we call the constructor is called when our object is created.

Specifically for us, it subscribes to the **Action** of the parent class (the one mentioned above)

What Action of the parent class?

```csharp
/// <summary>
/// <para>
/// Called when the VisualElement visual contents need to be (re)generated.
/// </para>
/// </summary>
public Action<MeshGenerationContext> generateVisualContent { get; set; }
```

Briefly, it is activated when our **VisualElement** needs to regenerate itself, this usually happens if there were changes in the UI element (change of properties, dimensions) or if the **MarkDirtyRepaint()** method was called

I will also leave a link to the [documentation](https://docs.unity3d.com/ScriptReference/UIElements.VisualElement-generateVisualContent.html)

When we catch this **Aciton**, we also get **MeshGenerationContext** and thanks to it, we can additionally render geometry on the element.

The method returns us a value of type - **MeshGenerationContext**, we will understand it later.

Let's make a small digression and make an introduction to game engines - let's talk about Mesh.

Mesh is a set of vertices, edges and polygons that define the shape and structure of an object.

To create a custom element, we create this very mash, but only at the interface level and in 2D space.

Next we will analyze the minimum data needed to create such an object:

```csharp
Vertex[] vertices = new Vertex[3];
ushort[] indices = { 0, 1, 2 };
```
