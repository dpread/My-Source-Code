using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary> script that moves the body according to the headset.</summary>   
public class ik_Body : MonoBehaviour {
	/// <summary>Varibales for the various parts needed for the 'ik'</summary>   
	GameObject headset,pelvis, origional, pelvOri;
	GameObject spine, head;
	Vector3 prev ;
	Vector3 offLeft,offRight,offsetSpine,offsetPelvis;
	public GameObject currentAva;
	float flo;
	bool go = false;
	/// <summary> initiates objects, and calls the delayed start co-routine</summary>  
	void Start() {
		headset = GameObject.Find ("Camera (eye)");
		pelvis = GameObject.Find ("pelvis");
		spine = GameObject.Find ("spine_01");
		head = GameObject.Find ("head");
		prev = new Vector3(0,0,0);
		StartCoroutine(delayedStart (3));

	}
		
	/// <summary> checkss if there is a change in the headset position and rotation. If so, reposition pevlis, and rotate head.</summary>  
	void Update() {
		if (go) {
			headset = GameObject.Find ("Camera (eye)");
			//
			if (prev != headset.transform.position) { 
				if (((headset.transform.position.y < origional.transform.position.y - 0.03f) || (headset.transform.position.y > origional.transform.position.y + 0.03f ) ) && (origional.transform.eulerAngles.x < -55f && origional.transform.eulerAngles.x > 70f)) {
				}else {
				Vector3 value = headset.transform.position - offsetPelvis;
				pelvis.transform.position = new Vector3(pelvis.transform.position.x, value.y,pelvis.transform.position.x);

			}
				if ((headset.transform.position.z > 0.025f) || (headset.transform.position.z < -0.02f)) {
					
					spine.transform.rotation = headset.transform.rotation;
					Vector3 change = head.transform.position - headset.transform.position;
					pelvis.transform.position = pelvOri.transform.position - change;
				}
			prev = headset.transform.position;
		}
		}
	}
	/// <summary> delayed to allow the user to get into position. Gets the orgional position, which are used for updates. 
	// Maps the head position to headset, and scales. </summary>  
	IEnumerator delayedStart(float delay)
	{
		yield return new WaitForSeconds(delay);
		origional = headset;
		Debug.Log (origional.transform.position.z);

		float yValue = GameObject.Find("Camera (eye)").transform.position.y / head.transform.position.y;
		float holder = yValue  * 0.03f;
		yValue = yValue - holder;
		currentAva.transform.localScale = new Vector3 (yValue, yValue, yValue);
		offsetPelvis = headset.transform.position - pelvis.transform.position;
		pelvOri = pelvis;
		go = true;

	}
	/// <summary> Used to set what avatar is currently being used. </summary>  
	public void setAva(GameObject ava) {
		currentAva = ava;
	}
}
