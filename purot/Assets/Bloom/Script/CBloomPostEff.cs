using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CBloomPostEff : MonoBehaviour
{
    [SerializeField, Range(1, 30)]
    private int iIteration = 1;
    [SerializeField, Range(0.0f, 1.0f)]
    private float fThreshold = 0.0f;
    [SerializeField, Range(0.0f, 1.0f)]
    private float fSoftThreshold = 0.0f;
    [SerializeField, Range(0.0f, 10.0f)]
    private float fIntensity = 1.0f;
    [SerializeField]
    private bool bDebug;

    // 4点をサンプリングして色を作るマテリアル
    [SerializeField]
    private Material Material;

    private RenderTexture[] RenderTextures = new RenderTexture[30];

    private void OnRenderImage(RenderTexture source, RenderTexture dest) {
        var filterParams = Vector4.zero;
        var knee = fThreshold * fSoftThreshold;
        filterParams.x = fThreshold;
        filterParams.y = fThreshold - knee;
        filterParams.z = knee * 2.0f;
        filterParams.w = 0.25f / (knee + 0.00001f);
        Material.SetVector("_FilterParams", filterParams);
        Material.SetFloat("_Intensity", fIntensity);
        Material.SetTexture("_SourceTex", source);

        var width = source.width;
        var height = source.height;
        var currentSource = source;

        var pathIndex = 0;
        var i = 0;
        RenderTexture currentDest = null;

        // ダウンサンプリング
        for (; i < iIteration; i++) {
            width /= 2;
            height /= 2;
            if (width < 2 || height < 2) {
                break;
            }
            currentDest = RenderTextures[i] = RenderTexture.GetTemporary(width, height, 0, source.format);

            // 最初の一回は明度抽出用のパスを使ってダウンサンプリングする
            pathIndex = i == 0 ? 0 : 1;
            Graphics.Blit(currentSource, currentDest, Material, pathIndex);

            currentSource = currentDest;
        }

        // アップサンプリング
        for (i -= 2; i >= 0; i--) {
            currentDest = RenderTextures[i];

            // Blit時にマテリアルとパスを指定する
            Graphics.Blit(currentSource, currentDest, Material, 2);

            RenderTextures[i] = null;
            RenderTexture.ReleaseTemporary(currentSource);
            currentSource = currentDest;
        }

        // 最後にdestにBlit
        pathIndex = bDebug ? 4 : 3;
        Graphics.Blit(currentSource, dest, Material, pathIndex);
        RenderTexture.ReleaseTemporary(currentSource);
    }
}
