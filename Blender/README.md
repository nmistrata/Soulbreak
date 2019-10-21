# Blender Models

Our `.blend` models. Edit with Blender >= 2.8.

See [this
article](https://gamedevacademy.org/how-to-import-blender-models-into-unity-your-one-stop-guide/)
for an explanation of the intricacies of converting `.blend` models
into something that we can import into
Unity. [This](https://lmhpoly.com/how-to-export-game-assets-from-blender-to-unity-5/#Export%20Blender%20Model%20as%20.FBX)
is also an interesting reference.

We will take the approach of **exporting the model in Blender as an
FBX** and importing the `.fbx`, rather than directly loading the
`.blend` into Unity.

Some things to keep in mind:

 - Do all of your Blender mesh transformations (scale, translation,
   rotation) **in Edit mode**, _not_ in Object mode!
   [(Why?)](https://blender.stackexchange.com/questions/28558/scaling-in-object-and-edit-mode)
 - Blender's coordinate system is **right handed**, but Unity's is
   **left handed**, which just means that when you export as `.fbx`
   you should make sure the Y-axis is up. When you edit in Blender,
   the Z-axis will be up.
 - Remember to **Apply Transforms**, **Set the origin**, and **Fix the
   Normals** before exporting (read the article).

Additional stuff:

 - Unity's model import documentation: https://docs.unity3d.com/Manual/HOWTO-exportFBX.html

 > "Unity's default unit scale is 1 unit = 1 meter, so the scale of
 > your imported mesh is maintained, and applied to physics
 > calculations. For example, a crumbling skyscraper is going to fall
 > apart very differently than a tower made of toy blocks, so objects
 > of different sizes should be modeled to accurate scale."
 >
 > "If you are modeling a human make sure he is around 2 meters tall in
 > Unity. To check if your object has the right size compare it to the
 > default cube. You can create a cube using GameObject->Create
 > Other->Cube. The cube's height will be exactly 1 meter, so your
 > human should be twice as tall."
