using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;
public class RenderingQualityManager : MonoBehaviour
{
    public static  RenderingQualityManager instance;
    public Volume globalVolume;
    public ScreenSpaceReflection ssrComponent;
    public GlobalIllumination giComponent;
    public ScreenSpaceAmbientOcclusion ssaoComponent;
    private void Start()
    {
        instance = this;
        globalVolume=GetComponent<Volume>();

        globalVolume.profile.TryGet<ScreenSpaceReflection>(out ssrComponent);
        globalVolume.profile.TryGet<GlobalIllumination>(out giComponent);
        globalVolume.profile.TryGet<ScreenSpaceAmbientOcclusion>(out ssaoComponent);
    }
    public void SetRenderingQuality(int qualityID)
    {
        switch ((qualityID))
        {
            case 0:
                giComponent.fullResolution = false;
                ssrComponent.fullResolution = false;
                ssaoComponent.fullResolution =false;
                break;
            case 1:
                giComponent.fullResolution = true;
                ssrComponent.fullResolution = true;
                ssaoComponent.fullResolution = true;
                break;
            case 2:
                giComponent.fullResolution = true;
                ssrComponent.fullResolution = true;
                ssaoComponent.fullResolution = true;
                break;
            default:

                break;
        }
    }
}
