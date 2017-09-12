using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>Script that combats the issue found of the ik script rotating arms at run time.</summary> 
public class ikArmRotCheck : MonoBehaviour {
	GameObject forearmL;
	GameObject upperL;
	GameObject handL,handR;
	GameObject forearmR;
	GameObject upperR;

	/// <summary>Use this for initialization. Searches if an avatar is in use.</summary> 
	void Start () {
		if(GameObject.Find("hand_l")) {
			setRotates ();
			}
	}
	/// <summary>sets the default rotations for arm parts.</summary> 
	private void setRotates() {
		forearmL = GameObject.Find("lowerarm_l");
		upperL = GameObject.Find("upperarm_l");
		handL= GameObject.Find("hand_l");
		forearmR = GameObject.Find("lowerarm_l");
		upperR = GameObject.Find("upperarm_r");
		handR= GameObject.Find("hand_r");

		Quaternion rot = upperL.transform.localRotation;
		rot.eulerAngles = new Vector3 (-167f, 72f, 92f);
		upperL.transform.localRotation = rot;

		rot = forearmL.transform.localRotation;
		rot.eulerAngles = new Vector3 (-15f, -7.9f, 5.8f);
		forearmL.transform.localRotation = rot;

		rot = handL.transform.localRotation;
		rot.eulerAngles = new Vector3 (-0.39f, -34.3f,-11.8f);
		handL.transform.localRotation = rot;

		Vector3 pos = upperR.transform.localPosition;
		pos = new Vector3 (-1.6f, 0.15f, 2.97f);

		rot = upperR.transform.localRotation;
		rot.eulerAngles = new Vector3 (182f, -89.7f, -90.9f);
		upperR.transform.localRotation = rot;

		rot = forearmR.transform.localRotation;
		rot.eulerAngles = new Vector3 (-1.13f, 14.6f,-0.76f);
		forearmR.transform.localRotation = rot;


	}

}
