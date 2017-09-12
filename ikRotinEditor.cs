using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]

public class ikRotinEditor : MonoBehaviour {

	GameObject forearmL;
	GameObject upperL;
	GameObject handL,handR;
	GameObject forearmR;
	GameObject upperR;

	// Use this for initialization</summary>
	void Start () {
		if(GameObject.Find("hand_l")) {
			setPos ();
		}
	}


	// Update is called once per frame
	void Update () {
	}
	private void setPos() {
		
		forearmL = GameObject.Find("lowerarm_l");
		upperL = GameObject.Find("upperarm_l");
		handL= GameObject.Find("hand_l");
		handR= GameObject.Find("hand_r");
		forearmR = GameObject.Find("lowerarm_l");
		upperR = GameObject.Find("upperarm_r");



	}
}
