using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary> This class represents an in game element.  </summary>  
public class InteractionObj {
	GameObject target;
	GameObject box;
	/// <summary> Adds an interaction box, to signify target object is interactable. .  </summary>  
	/// <param name="target">Target of the check. s.</param> 
	public InteractionObj(GameObject target) 
	{
		this.target = target;
		box = GameObject.CreatePrimitive(PrimitiveType.Cube);
		box.name = "Interaction Box " + target.ToString ().Replace ("(UnityEngine.GameObject)", " ");
		box.transform.SetParent(target.transform);
		box.transform.localScale = new Vector3 (1f, 1f, 1f);
		box.transform.localPosition = new Vector3 (0, 0, 0);
		Material tempMaterial = new Material(box.GetComponent<Renderer> ().sharedMaterial);
		tempMaterial.color = Color.red;
		box.GetComponent<Renderer> ().sharedMaterial = tempMaterial;

	}
	/// <summary> Prints a message stating the success of the adding. </summary>  
	public void printSuccess() 
	{
		Debug.Log ("Interaction box has been added to the " + target.ToString ().Replace ("(UnityEngine.GameObject)", " "));

	}
	/// <summary> Returns the bo object, to be able to tag in button scripts.  </summary>  
	public GameObject getBox() {
		return box;
	}


}
	
