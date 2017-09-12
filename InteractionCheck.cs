using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>Checks whether avatar would be able to interact with the target game object, runs in editor mode.</summary>
	[ExecuteInEditMode]
public class InteractionCheck : MonoBehaviour {
	GameObject target;
	public bool run = false;
	/// <summary>Adds a mesh renderer if there is none. 
	//Checks bounds of the target and interaction reach, if they intersect, then check is successful. 
	//Sets interaction box to green to show the positive result.</summary> 
	void Start() {

		target.SetActive (true);

		if (target.GetComponent<Renderer> () == null) {
			target.AddComponent<MeshRenderer> ();
		}
		Bounds tBound = target.GetComponent<Renderer>().bounds;
		Bounds iBound = GameObject.Find ("ReachSphere(Clone)").GetComponent<Renderer> ().bounds;
		GameObject targetBox = GameObject.Find ("Interaction Box " + target.ToString ().Replace ("(UnityEngine.GameObject)", " "));
		Material tempMaterialL = new Material(targetBox.GetComponent<Renderer> ().sharedMaterial); 
		if (iBound.Intersects (tBound)) {
			print (targetBox.ToString ());
			if (targetBox != null) {
				tempMaterialL.color = Color.green;
				targetBox.GetComponent<Renderer> ().sharedMaterial = tempMaterialL;	

				Debug.Log ("Interaction Check is sucessful");
			}
		} else {
			tempMaterialL.color = Color.red;
			targetBox.GetComponent<Renderer> ().sharedMaterial = tempMaterialL; 
			Debug.Log ("Interaction Check failed");

		}
	}

	void Update() {

	}
	/// <summary>sets the target from elsewhere in the system.</summary> 
	public void setTarget(GameObject obj)
	{
		target = obj;
	}
	/// <summary>checks during updates.</summary> 
	public void check() {
		if (run) {
			target.SetActive (true);

			if (target.GetComponent<Renderer> () == null) {
				target.AddComponent<MeshRenderer> ();
			}
			Bounds tBound = target.GetComponent<Renderer> ().bounds;
			Bounds iBound = GameObject.Find ("ReachSphere(Clone)").GetComponent<Renderer> ().bounds;
			GameObject targetBox = GameObject.Find ("Interaction Box " + target.ToString ().Replace ("(UnityEngine.GameObject)", " "));
			Material tempMaterialL = new Material (targetBox.GetComponent<Renderer> ().sharedMaterial); 
			if (iBound.Intersects (tBound)) {
				print (targetBox.ToString ());
				if (targetBox != null) {
					tempMaterialL.color = Color.green;
					targetBox.GetComponent<Renderer> ().sharedMaterial = tempMaterialL;	

					Debug.Log ("Interaction Check is sucessful");
				}
			} else {
				tempMaterialL.color = Color.red;
				targetBox.GetComponent<Renderer> ().sharedMaterial = tempMaterialL; 
				Debug.Log ("Interaction Check failed");

			}
			run = false;
		}
	}

}

