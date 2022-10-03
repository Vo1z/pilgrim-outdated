using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

public class EnemyRendererManager : MonoBehaviour
{
    [SerializeField] private int pixelDetectionThreshold = 10;
   
    
    [SerializeField] private Camera camera;
    [SerializeField] private Material material;

    [SerializeField] private LayerMask maskForEnvironment;
    [SerializeField] private LayerMask maskForEnvironmentWithPlayer;
    [SerializeField] private LayerMask maskForPlayer;

#if UNITY_EDITOR
    [SerializeField] private bool shouldTexBeVisible = false;
    //todo Preview
    [SerializeField] 
    [ShowIf("shouldTexBeVisible")]
    private RenderTexture renderTexture;
#endif
    
    private Texture2D _texture;
    private int _width=256, _height=256;
    
    private void Start()
    {
        camera.backgroundColor = new Color(0,0,0,0);
       RenderView();
    }
    
    private void RenderView()
    {
        var enviro = GetRenderTexture(maskForEnvironment);
        var all = GetRenderTexture(maskForEnvironmentWithPlayer);

        var displayedPlayersPixels = 0;
        var enviroPixels = enviro.GetPixels();
        var allPixels = all.GetPixels();
        for (int i = 0; i < enviroPixels.Length ; i++)
        {
            if (enviroPixels[i] != allPixels[i])
            {
                displayedPlayersPixels++;
            }
        }

        if (displayedPlayersPixels>=pixelDetectionThreshold)
        {
            Debug.Log($"I See You... Player's pixels:{displayedPlayersPixels}, threshold to detect:{pixelDetectionThreshold}");
            return;
        }
        Debug.Log($"I Cant See You... Player's pixels:{displayedPlayersPixels}, threshold to detect:{pixelDetectionThreshold}");
        var player = GetRenderTexture(maskForPlayer);
        var playerPixels = player.GetPixels();
        var totalNumberOfPlayersPixels = 0;
        var bgc = camera.backgroundColor;
        foreach (var c in playerPixels)
        {
            if (c!=bgc)
            {
                totalNumberOfPlayersPixels ++;
            }
        }
    }

    private Texture2D GetRenderTexture( LayerMask mask)
    {
        camera.cullingMask = mask;
        camera.gameObject.SetActive(true);
        camera.Render();
        RenderTexture.active = camera.targetTexture;
        var texture = new Texture2D(_width, _height, TextureFormat.RGBA32, false);
        texture.ReadPixels(new(0,0,_width,_height),0,0);
        texture.Apply();
        //camera.cullingMask = ;
        camera.gameObject.SetActive(false);     
        return texture;
    }
}
