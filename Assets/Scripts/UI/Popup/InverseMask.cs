using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InverseMask : Mask
{
    public override bool IsRaycastLocationValid(Vector2 sp, Camera eventCamera)
    {
        return !base.IsRaycastLocationValid(sp, eventCamera);
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        Graphic graphic = GetComponent<Graphic>();
        if (graphic != null)
        {
            graphic.material = Instantiate(graphic.material);
            graphic.material.SetInt("_StencilComp", (int)UnityEngine.Rendering.CompareFunction.NotEqual);
        }
    }

    protected override void OnDisable()
    {
        Graphic graphic = GetComponent<Graphic>();
        if (graphic != null)
        {
            graphic.material.SetInt("_StencilComp", (int)UnityEngine.Rendering.CompareFunction.Always);
        }
        base.OnDisable();
    }
}
