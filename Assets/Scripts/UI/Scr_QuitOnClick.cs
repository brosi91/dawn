using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_QuitOnClick : MonoBehaviour {

	public void Quit(){


	#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
		//inEditor
	#else
			Application.Quit();
			//inBuild
	#endif
	}

}
