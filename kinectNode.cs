using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary> This class creates a node representing kinect.  </summary>  
public class kinectNode : MonoBehaviour {
	Renderer rend,rend2;
	public Material green;
	public Material transp;
	GameObject kinect, fov;
	/// <summary> Loads materials, creates the cube and attaches the loaded field of view volume.Then sets position  </summary>  
	public void createNode( )

	{
		transp = (Material)Instantiate(Resources.Load("Transparent"));
		green = (Material)Instantiate(Resources.Load("Green"));
		kinect = GameObject.CreatePrimitive(PrimitiveType.Cube);
		fov = (GameObject)Instantiate(Resources.Load("Mesh16"));
		kinect.name = "KinectNode";
		kinect.transform.position = new Vector3 (0, 1.06f, 3.2f);
		kinect.transform.localScale = new Vector3 (0.5f, 0.1f, 0.1f);
		fov.transform.localScale = new Vector3 (0.0095f, 0.019f, 0.0041f);
		fov.transform.parent = kinect.transform;

		fov.transform.localPosition = new Vector3 (0.1f, 1f, -20.8f);
		fov.transform.rotation = Quaternion.AngleAxis (180, Vector3.up);
		fov.transform.rotation = Quaternion.AngleAxis (90, Vector3.right);

		rend = kinect.GetComponent<Renderer> ();
		rend2 = fov.GetComponent<Renderer> ();
		if (rend != null && rend2 != null) {
			rend.material = green;
			rend2.material = transp;
		}
	}
	/// <summary> Deletes the node and volume  </summary>  
	public void deleteNode() 
	{
		Destroy (kinect);
		Destroy (fov);

	}	
	/// <summary> Returns the volume.  </summary>  
	public GameObject getNode() {
		return kinect;
	}
		
}
