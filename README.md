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

Meet our valiant elephants, gentlemen — 'Vertices' and 'Indices'; henceforth, we'll refer to them as vertices and indices.

A Vertex is an internal structure of Unity UI Toolkit, employed in rendering UI elements. The key aspect for us is:

```csharp
public struct Vertex
{
    ...
    public Vector3 position; 
    public Color32 tint; 
    ...
}
```

Under the hood, it utilizes Vector3 for the vertex position and Color32 (32-bit RGBA representation) for the color shade.

In essence, vertices define their spatial coordinates and color.

Next up are the indices, responsible for connecting the mesh vertices.

The majority of game engines and 3D modeling tools employ a triangular mesh approach, where objects are comprised of triangles.

Similarly, in our case, when we state that the indices array is filled – {0, 1, 2}, we imply that the output will be a triangle formed by these vertices.

Hence, the minimum description of a shape involves three numbers to outline a single triangle.

![alt](https://github.com/linzer0/divide-and-conquer-interfaces-in-Unity/blob/main/ReadmeResouces/3.png)

You can read more about the anatomy of the mesh and its structure [here](https://docs.unity3d.com/2022.2/Documentation/Manual/AnatomyofaMesh.html).

Moving along in the code, we've reached the body of our method, which will be invoked upon regenerating our UI element:

```csharp
void GenerateVisualContent(MeshGenerationContext mgc)
{
    vertices[0].tint = Color.red;
    vertices[1].tint = Color.red;
    vertices[2].tint = Color.red;
}
```

Keep in mind that our vertices have a 'tint' field (vertex color), and right here is where we've defined them, setting all to red

Next, we'll determine their positions (who is where):

```csharp
var leftCorner = 0f;
var rightCorner = contentRect.width;
var top = 0;
var bottom = contentRect.height;
var middleX = contentRect.width / 2;

vertices[0].position = new Vector3(leftCorner, bottom, Vertex.nearZ);
vertices[1].position = new Vector3(middleX, top, Vertex.nearZ);
vertices[2].position = new Vector3(rightCorner, bottom, Vertex.nearZ);
```

Let's begin with the fact that the vertex position is described using the Vector3 type (where 3 represents the number of dimensions, i.e., in our case, X, Y, Z).

To specify positions, we need some theoretical support:

Any VisualElement has a rendering area called the [contentRect](https://docs.unity3d.com/ScriptReference/Rect.html).
It has properties for height and width.

I want to draw my triangle always in the center of this area, so that, regardless of the size of the area, my triangle is positioned in the middle.

That's why I'm binding it to the size of the rendering area. If it changes, for example, due to a screen resize, it will trigger the regeneration of the entire UI element. Consequently, this method will be called again, but this time with updated contentRect dimensions.

![alt](https://github.com/linzer0/divide-and-conquer-interfaces-in-Unity/blob/main/ReadmeResouces/4.png)

Here, the origin, as you may have noticed, is located in the top-left corner – position { x = 0, y = 0 }.

The region of our contentRect ends at the values { x = width, y = height }, which is logical overall. The farthest point on the X-axis is equal to the width of the element, and on the Y-axis, it's equal to the height.

So, what did we end up with in terms of vertices?

- Vertex with index 0 is located at coordinates { x = 0, y = height } (bottom-left corner).
- Vertex with index 1 is located at coordinates { x = width / 2, y = 0 } (top center).
- Vertex with index 2 is located at coordinates { x = width, y = height } (bottom-right corner).

After preparing the position and color values, it's finally time to render all of this!

```csharp
MeshWriteData mwd = mgc.Allocate(vertices.Length, indices.Length);
mwd.SetAllVertices(vertices);
mwd.SetAllIndices(indices);
```

Here, in short, we are saying that we want to create a mesh with a certain number of vertices and a specific number of indices.

Then, through the SetAllVertices() method, we define our vertices, and through SetAllIndices(), we assign our indices.

And voilà! We've got ourselves a triangle:


https://github.com/linzer0/divide-and-conquer-interfaces-in-Unity/assets/17702351/78da6db0-00d8-4e0c-b09f-85f6778732b2



