/*==============================================================================
    Priject_�Y�w�A�g
    [CMovieCIntroller.cs]
    �E����̈ꎞ��~�A�Đ�

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
	[SerializeField] VideoPlayer videoPlayer;

	void Start() {
		VideoPlayer vpVideoPlayer = gScreen.AddComponent<VideoPlayer>();  // videoPlayer�R���|�[�l���g�̒ǉ�

		vpVideoPlayer.source = VideoSource.VideoClip;               // ����\�[�X�̐ݒ�
		vpVideoPlayer.clip = vcVideoClip;

		vpVideoPlayer.isLooping = false;                            // ���[�v�̐ݒ�

		videoPlayer.loopPointReached += LoopPointReached;
		videoPlayer.Play();
	}

	public void Play_Pause() {
		VideoPlayer vpVideoPlayer = GetComponent<VideoPlayer>();

		if (!vpVideoPlayer.isPlaying)	// �{�^�������������̏���
			vpVideoPlayer.Play();		// ������Đ�����B
		else
			vpVideoPlayer.Pause();		// ������ꎞ��~����B
	}

	public void LoopPointReached(VideoPlayer vp)
    {
		//CSceneManager CSM = GameObject.Find("EventSystem").GetComponent<CSceneManager>();
		//CSM.OnChangeScene_Title();
	}
}