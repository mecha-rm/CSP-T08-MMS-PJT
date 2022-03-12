using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;

// modeled after the following tutorial: https://www.youtube.com/watch?v=XJJl19N2KFM

// inverts a mask so that the children cut out of the parent.
public class ImageInvertedMask : Image
{
    // changes the material for rendering.
    public override Material materialForRendering
    {
        get
        {
            // not changing the original material, so make a copy instead. 
            Material material = new Material(base.materialForRendering);

            // changes the comparison so that the objects cut out of the parent instead of the other way around.
            material.SetInt("_StencilComp", (int)CompareFunction.NotEqual);

            // returns the new material.
            return material;
        }
    }
}
