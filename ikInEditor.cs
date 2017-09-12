using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]

public class ikInEditor : MonoBehaviour {

	public Transform upperArm, forearm, hand;
	public Transform target, elbowTarget;
	GameObject forearmL;
	GameObject upperL;
	GameObject handL,handR;
	GameObject forearmR;
	GameObject upperR;
	public bool IsEnabled, debug;

	public float transition = 1.0f;

	public bool idleOptimization = false;

	public enum HandRotations {
		KeepLocalRotation,
		KeepGlobalRotation,
		UseTargetRotation
	};
	public HandRotations handRotationPolicy = HandRotations.KeepLocalRotation;

	private Quaternion upperArmStartRotation, forearmStartRotation, handStartRotation;
	private Vector3 targetRelativeStartPosition, elbowTargetRelativeStartPosition;

	//helper GOs that are reused every frame
	private GameObject upperArmAxisCorrection, forearmAxisCorrection, handAxisCorrection;

	//hold last positions so recalculation is only done if needed
	private Vector3 lastUpperArmPosition, lastTargetPosition, lastElbowTargetPosition;

	void Start(){
		upperArmStartRotation = upperArm.rotation;
		forearmStartRotation = forearm.rotation;
		handStartRotation = hand.rotation;
		//targetRelativeStartPosition = target.position - upperArm.position;
		elbowTargetRelativeStartPosition = elbowTarget.position - upperArm.position;

//		//create helper GOs
//		//if (GameObject.Find("upperArmAxisCorrection") != null) {
//			//DestroyImmediate (GameObject.Find ("upperArmAxisCorrection"));
//			DestroyImmediate (GameObject.Find ("forearmAxisCorrection"));
//			DestroyImmediate (GameObject.Find ("handAxisCorrection"));
//
//		}
		upperArmAxisCorrection = new GameObject("upperArmAxisCorrection");
		forearmAxisCorrection = new GameObject("forearmAxisCorrection");
		handAxisCorrection = new GameObject("handAxisCorrection");

		//set helper hierarchy
		upperArmAxisCorrection.transform.parent = transform;
		forearmAxisCorrection.transform.parent = upperArmAxisCorrection.transform;
		handAxisCorrection.transform.parent = forearmAxisCorrection.transform;

		//guarantee first-frame update
		lastUpperArmPosition = upperArm.position + 5*Vector3.up;
	}

	void LateUpdate () {
		if (!IsEnabled){
			return;
		}
		CalculateIK();
	}

	void CalculateIK(){
		if(target == null) {
			targetRelativeStartPosition = Vector3.zero;
			return;
		}

		if(targetRelativeStartPosition == Vector3.zero && target != null) {
			targetRelativeStartPosition = target.position - upperArm.position;
		}

		if(
			idleOptimization
			&&
			lastUpperArmPosition == upperArm.position
			&&
			lastTargetPosition == target.position
			&&
			lastElbowTargetPosition == elbowTarget.position
		) {
			if(debug) {
				Debug.DrawLine(forearm.position, elbowTarget.position, Color.yellow);
				Debug.DrawLine(upperArm.position, target.position, Color.red);
			}

			return;
		}

		lastUpperArmPosition = upperArm.position;
		lastTargetPosition = target.position;
		lastElbowTargetPosition = elbowTarget.position;

		//Calculate ikAngle variable.
		float upperArmLength = Vector3.Distance(upperArm.position, forearm.position);
		float forearmLength = Vector3.Distance(forearm.position, hand.position);
		float armLength = upperArmLength + forearmLength;
		float hypotenuse = upperArmLength;

		float targetDistance = Vector3.Distance(upperArm.position, target.position);	
		targetDistance = Mathf.Min(targetDistance, armLength - 0.0001f); //Do not allow target distance be further away than the arm's length.

		//var adjacent : float = (targetDistance * hypotenuse) / armLength;
		//var adjacent : float = (Mathf.Pow(hypotenuse,2) - Mathf.Pow(forearmLength,2) + Mathf.Pow(targetDistance,2))/(2*targetDistance);
		float adjacent = (hypotenuse*hypotenuse - forearmLength*forearmLength + targetDistance*targetDistance) /(2*targetDistance);

		float ikAngle  = Mathf.Acos(adjacent/hypotenuse) * Mathf.Rad2Deg;

		//Store pre-ik info.
		Vector3 targetPosition = target.position;
		Vector3 elbowTargetPosition = elbowTarget.position;

		Transform upperArmParent = upperArm.parent;
		Transform forearmParent = forearm.parent;
		Transform handParent = hand.parent; 

		Vector3 upperArmScale = upperArm.localScale;
		Vector3 forearmScale = forearm.localScale;
		Vector3 handScale = hand.localScale;
		Vector3 upperArmLocalPosition = upperArm.localPosition;
		Vector3 forearmLocalPosition = forearm.localPosition;
		Vector3 handLocalPosition = hand.localPosition;

		Quaternion upperArmRotation = upperArm.rotation;
		Quaternion forearmRotation = forearm.rotation;
		Quaternion handRotation = hand.rotation;
		Quaternion handLocalRotation = hand.localRotation;

		//Reset arm.
		target.position = targetRelativeStartPosition + upperArm.position;
		elbowTarget.position = elbowTargetRelativeStartPosition + upperArm.position;
		upperArm.rotation = upperArmStartRotation;
		forearm.rotation = forearmStartRotation;
		hand.rotation = handStartRotation;

		//Work with temporaty game objects and align & parent them to the arm.
		transform.position = upperArm.position;
		transform.LookAt(targetPosition, elbowTargetPosition - transform.position);

		upperArmAxisCorrection.transform.position = upperArm.position;
		//upperArmAxisCorrection.transform.LookAt(forearm.position, transform.root.up);
		upperArmAxisCorrection.transform.LookAt(forearm.position, upperArm.up);
		upperArm.parent = upperArmAxisCorrection.transform;

		forearmAxisCorrection.transform.position = forearm.position;
		//forearmAxisCorrection.transform.LookAt(hand.position, transform.root.up);
		forearmAxisCorrection.transform.LookAt(hand.position, forearm.up);
		forearm.parent = forearmAxisCorrection.transform;

		handAxisCorrection.transform.position = hand.position;
		hand.parent = handAxisCorrection.transform;

		//Reset targets.
		target.position = targetPosition;
		elbowTarget.position = elbowTargetPosition;	

		//Apply rotation for temporary game objects.
		upperArmAxisCorrection.transform.LookAt(target,elbowTarget.position - upperArmAxisCorrection.transform.position);

		upperArmAxisCorrection.transform.localRotation = //.x -= ikAngle;
			Quaternion.Euler(upperArmAxisCorrection.transform.localRotation.eulerAngles - new Vector3(ikAngle, 0, 0));

		forearmAxisCorrection.transform.LookAt(target,elbowTarget.position - upperArmAxisCorrection.transform.position);
		handAxisCorrection.transform.rotation = target.rotation;

		//Restore limbs.
		upperArm.parent = upperArmParent;
		forearm.parent = forearmParent;
		hand.parent = handParent;
		upperArm.localScale = upperArmScale;
		forearm.localScale = forearmScale;
		hand.localScale = handScale;
		upperArm.localPosition = upperArmLocalPosition;
		forearm.localPosition = forearmLocalPosition;
		hand.localPosition = handLocalPosition;

		//Transition.
		transition = Mathf.Clamp01(transition);
		upperArm.rotation = Quaternion.Slerp(upperArmRotation, upperArm.rotation, transition);
		forearm.rotation = Quaternion.Slerp(forearmRotation, forearm.rotation, transition);
		hand.rotation = Quaternion.Slerp(handRotation, hand.rotation, transition);

		switch(handRotationPolicy) {
		case HandRotations.KeepLocalRotation:
			hand.localRotation = handLocalRotation;

			break;
		case HandRotations.KeepGlobalRotation:
			hand.rotation = handRotation;

			break;
		case HandRotations.UseTargetRotation:
			hand.rotation = target.rotation;

			break;
		}

		//Debug.
		if (debug){
			Debug.DrawLine(forearm.position, elbowTarget.position, Color.yellow);
			Debug.DrawLine(upperArm.position, target.position, Color.red);

			Debug.Log("[IK Limb] adjacent: " + adjacent);
		}
	}
	public void setIKsL() {
		setLRots ();
		GameObject.Find("ikLA").GetComponent<ikInEditor>().upperArm = GameObject.Find("upperarm_l").transform;
		GameObject.Find("ikLA").GetComponent<ikInEditor>().forearm = GameObject.Find("lowerarm_l").transform;
		GameObject.Find("ikLA").GetComponent<ikInEditor>().hand = GameObject.Find("hand_l").transform;
		GameObject.Find("ikLA").GetComponent<ikInEditor>().target = GameObject.Find("WristLeft").transform;
		GameObject.Find("ikLA").GetComponent<ikInEditor>().elbowTarget = GameObject.Find("ElbowLeft").transform;
		GameObject.Find ("ikLA").GetComponent<ikInEditor> ().debug = false;
		GameObject.Find ("ikLA").GetComponent<ikInEditor> ().IsEnabled = true;
	}
	public void setIKsR() {
		setRRots ();
		GameObject.Find("ikRA").GetComponent<ikInEditor>().upperArm = GameObject.Find("upperarm_r").transform;
		GameObject.Find("ikRA").GetComponent<ikInEditor>().forearm = GameObject.Find("lowerarm_r").transform;
		GameObject.Find("ikRA").GetComponent<ikInEditor>().hand = GameObject.Find("hand_r").transform;
		GameObject.Find("ikRA").GetComponent<ikInEditor>().target = GameObject.Find("WristRight").transform;
		GameObject.Find("ikRA").GetComponent<ikInEditor>().elbowTarget = GameObject.Find("ElbowRight").transform;
		GameObject.Find ("ikRA").GetComponent<ikInEditor> ().debug = false;
		GameObject.Find ("ikRA").GetComponent<ikInEditor> ().IsEnabled = true;
	}
	public void setLRots() {
		forearmL = GameObject.Find("lowerarm_l");
		upperL = GameObject.Find("upperarm_l");
		handL= GameObject.Find("hand_l");
				Quaternion rot = upperL.transform.localRotation;
				rot.eulerAngles = new Vector3 (-167f, 72f, 92f);
				upperL.transform.localRotation = rot;
		
				rot = forearmL.transform.localRotation;
				rot.eulerAngles = new Vector3 (-15f, -7.9f, 5.8f);
				forearmL.transform.localRotation = rot;
		
				rot = handL.transform.localRotation;
				rot.eulerAngles = new Vector3 (-0.39f, -34.3f,-11.8f);
				handL.transform.localRotation = rot;
	}
	public void setRRots() {
	
		forearmR = GameObject.Find("lowerarm_l");
		upperR = GameObject.Find("upperarm_r");


		Vector3 pos = upperR.transform.localPosition;
		pos = new Vector3 (-1.6f, 0.15f, 2.97f);

		Quaternion rot = upperR.transform.localRotation;
		rot.eulerAngles = new Vector3 (182f, -89.7f, -90.9f);
		upperR.transform.localRotation = rot;

		rot = forearmR.transform.localRotation;
		rot.eulerAngles = new Vector3 (-1.13f, 14.6f,-0.76f);
		forearmR.transform.localRotation = rot;
	}
}
