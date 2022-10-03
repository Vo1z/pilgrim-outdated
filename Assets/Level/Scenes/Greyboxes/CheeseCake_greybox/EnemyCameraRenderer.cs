using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCameraRenderer : MonoBehaviour
{
    [SerializeField] private Material _material;
    private void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        //Graphics.Blit(src,dest,_material);
       // Debug.Log(123123);
    }
}
