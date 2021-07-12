/*==============================================================================
    Priject_産学連携
    [CMovieCIntroller.cs]
    ・動画の一時停止、再生

--------------------------------------------------------------------------------
    2021.07.05 @Author Hirano Tomoki
================================================================================
    History
        
/*============================================================================*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
 
public class CMovieController : MonoBehaviour {
	[SerializeField]private VideoClip vcVideoClip;
	[SerializeField]private GameObject gScreen;
	[SerializeField] VideoPlayer vpVideoPlayer;
    [SerializeField] AudioSource audioSource; 

	void Start() {
		vpVideoPlayer = gScreen.AddComponent<VideoPlayer>();  // videoPlayerコンポーネントの追加

		vpVideoPlayer.source = VideoSource.VideoClip;               // 動画ソースの設定
		vpVideoPlayer.clip = vcVideoClip;

        vpVideoPlayer.isLooping = false;                            // ループの設定

        vpVideoPlayer.loopPointReached += LoopPointReached;
        vpVideoPlayer.Play();

      
        audioSource.Play();
    }

	public void Play_Pause() {
		vpVideoPlayer = GetComponent<VideoPlayer>();

        // ボタンを押した時の処理
        if (!vpVideoPlayer.isPlaying){ 
            vpVideoPlayer.Play();       // 動画を再生する。
            audioSource.UnPause();
        }
        else
        {
            vpVideoPlayer.Pause();      // 動画を一時停止する。
            audioSource.Pause();
        }
    }

	public void LoopPointReached(VideoPlayer vp)
    {
		CSceneManager CSM = GameObject.Find("FadeCanvas").GetComponent<CSceneManager>();
		CSM.OnChangeScene_Title();
	}
}