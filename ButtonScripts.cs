using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ButtonScripts : MonoBehaviour {
	Material green,transp;
	public  GameObject sitFirst,staFirst, walFirst ;
	GameObject currentAva, baseGO, node, camVR, eR, eL;
	public GameObject interactObj;
	public GameObject interactee;
	public GameObject target;
	GameObject interField, table;
	List<GameObject> list;


	public void setbGO(GameObject baseSet) {
		baseGO = baseSet;

	}
	 
	/// <summary>Changes the scene to prview mode, where only game elements are visible.  </summary> 

	public void setPreviewScene() {
		list = new List<GameObject> ();
		GameObject[] objects = GameObject.FindGameObjectsWithTag("editor");
		foreach (GameObject ele in objects) {
			list.Add (ele);
			ele.SetActive (false);
		}

	}
	/// <summary>Changes the scene to editor mode, where everything is visible.  </summary> 

	public void setEditorScene() {

		foreach (GameObject ele in list) {
			if (ele == null) {
				list.Remove (ele);
			} else {
				ele.SetActive (true);
			}
		}
		list.Clear ();

	}
	/// <summary>
	///checks the interaction between target and interactee. Does so via the targets mesh renderer, 
	/// which is added if there is none present.</summary> 


	public void interactionCheck()
	{
		if (target == null || interactee == null) {			
			Debug.LogError ("You must add objects to the target and interactee fields");
		} else {
			if (interactee.GetComponent<MeshRenderer> () == null) {
				interactee.AddComponent<MeshRenderer> ();
			}
			if (interactee.GetComponent<InteractionCheck> () == null) {
					interactee.AddComponent<InteractionCheck> ();
			}
			interactee.GetComponent<InteractionCheck> ().setTarget (target);
			interactee.GetComponent<InteractionCheck> ().run = true;
			interactee.GetComponent<InteractionCheck> ().check ();
		}
	}
	/// <summary>runs rula assessment </summary> 

	public void runRULA(bool run)
	{
		if (run) {
			currentAva.GetComponent<RULA> ().setRun (run);
			print ("RULA only runs during a change in the scene");
		} else {
			currentAva.GetComponent<RULA> ().setRun (run);
		}
	}
	/// <summary>moves the left arm to the iktarget </summary>
	public void moveLeftToTarget(GameObject ikTarget)
	{
		GameObject.Find ("ikLA").GetComponent<ikInEditor> ().target = ikTarget.transform;
	}
	/// <summarymoves the right arm to the iktarget </summary>

	public void moveRightToTarget(GameObject ikTarget)
	{
		GameObject.Find ("ikRA").GetComponent<ikInEditor> ().target = ikTarget.transform;

	}
	/// <summaryRuns RULA during playing in game view.  </summary>
	public void runRULAPreview(bool run)
	{
		if (run) {
			currentAva.GetComponent<RULA_Play> ().setRun (run);
			print ("RULA only runs during a change in the scene");
		} else {
			currentAva.GetComponent<RULA_Play> ().setRun (run);
		}
	}
	/// <summarycreate Kinect Node </summary>
	public GameObject createKinectNode()
	{
		baseGO.AddComponent<kinectNode>();
		baseGO.GetComponent<kinectNode> ().createNode ();
		node = baseGO.GetComponent<kinectNode> ().getNode ();
		node.tag = "editor";
		DestroyImmediate(baseGO.GetComponent<kinectNode> ());
		return node;
	}	
	/// <summaryloads thet table model from the resources folder.  </summary>
	public GameObject createTable() 
	{
		table = (GameObject)Instantiate(Resources.Load("old_wooden_table"));
		table.transform.position = new Vector3 (0, 0, 0.6f);
		table.transform.localScale = new Vector3 (1, 1f, 1f);
		table.tag = "editor";
		return table;
	}
	/// <summaryAdds a box as a child to the interaction object variable. Used to show the result of the interaction check.  </summary>
	public GameObject createInteractionBox()
	{
		if (interactObj != null) {
			InteractionObj targ = new InteractionObj (interactObj);
			targ.printSuccess ();
			targ.getBox().tag = "editor";
			return targ.getBox ();
		} else 
		{
			Debug.LogError("You must put the target Interactable Object into the interactObj  inspector");
			return null;
		}
	}	
	/// <summaryloads the sitting avatar, and attaches the reach volume.  </summary>
	public GameObject CreateSit() 
	{
		delIK ();
		currentAva = (GameObject)Instantiate(Resources.Load("Male_Sitting_Pose"));
		currentAva.tag = "editor";
		interField = (GameObject)Instantiate(Resources.Load("ReachSphere"));
		interField.transform.localScale  = new Vector3 (0.32f, 0.4f, 0.337f);
		interField.transform.parent = currentAva.transform;
		interField.transform.localPosition = new Vector3 (-0.07f,0.944f, -0.023f);
		Renderer rend2 = interField.GetComponent<Renderer> ();
		transp = (Material)Instantiate(Resources.Load("Transparent"));
		if (rend2 != null) {
			rend2.material = transp;
		}

		createIks();
		return currentAva;
	}
	/// <summaryloads the standing avatar, and attaches the reach volume.  </summary>

	public GameObject CreateStand()
	{
		delIK ();
		currentAva = (GameObject)Instantiate(Resources.Load("Male_Standing_Pose"));
		currentAva.tag = "editor";
		interField = (GameObject)Instantiate(Resources.Load("ReachSphere"));
		interField.transform.localScale  = new Vector3 (0.338f, 0.4275f, 0.3453f); 

		interField.transform.parent = currentAva.transform;
		interField.transform.localPosition = new Vector3 (-0.014f,1.2f, -0.153f);
		Renderer rend2 = interField.GetComponent<Renderer> ();
		transp = (Material)Instantiate(Resources.Load("Transparent"));
		if (rend2 != null) {
			rend2.material = transp;
		}
		createIks();

		return currentAva;
	}
	/// <summaryloads the walking avatar, and attaches the reach volume.  </summary>
	public GameObject CreateWalk()
	{	
		delIK ();
		currentAva = (GameObject)Instantiate(Resources.Load("walking"));
		currentAva.tag = "editor";
		interField = (GameObject)Instantiate(Resources.Load("ReachSphere"));
		interField.transform.localScale  = new Vector3 (0.045f,1.15f, -0.148f);
		interField.transform.parent = currentAva.transform;
		interField.transform.localPosition = new Vector3 (-0.045f, 1.15f, -0.148f);
		Renderer rend2 = interField.GetComponent<Renderer> ();
		transp = (Material)Instantiate(Resources.Load("Transparent"));
		if (rend2 != null) {
			rend2.material = transp;
		}
		createIks();

		return currentAva;
	}
	/// <summarysets the ik variables for the editor and game scripts. Called after creating the avatar.  </summary>
	private void createIks() {
		GameObject.Find("ikLA").GetComponent<IKLimb_BrunoFerreira>().upperArm = GameObject.Find("upperarm_l").transform;
		GameObject.Find("ikLA").GetComponent<IKLimb_BrunoFerreira>().forearm = GameObject.Find("lowerarm_l").transform;
		GameObject.Find("ikLA").GetComponent<IKLimb_BrunoFerreira>().hand = GameObject.Find("hand_l").transform;
		GameObject.Find("ikLA").GetComponent<IKLimb_BrunoFerreira>().target = GameObject.Find("WristLeft").transform;
		GameObject.Find("ikLA").GetComponent<IKLimb_BrunoFerreira>().elbowTarget = GameObject.Find("ElbowLeft").transform;
		GameObject.Find ("ikLA").GetComponent<IKLimb_BrunoFerreira> ().debug = false;
		GameObject.Find ("ikLA").GetComponent<IKLimb_BrunoFerreira> ().IsEnabled = true;

		GameObject.Find("ikRA").GetComponent<IKLimb_BrunoFerreira>().upperArm = GameObject.Find("upperarm_r").transform;
		GameObject.Find("ikRA").GetComponent<IKLimb_BrunoFerreira>().forearm = GameObject.Find("lowerarm_r").transform;
		GameObject.Find("ikRA").GetComponent<IKLimb_BrunoFerreira>().hand = GameObject.Find("hand_r").transform;
		GameObject.Find("ikRA").GetComponent<IKLimb_BrunoFerreira>().target = GameObject.Find("WristRight").transform;
		GameObject.Find("ikRA").GetComponent<IKLimb_BrunoFerreira>().elbowTarget = GameObject.Find("ElbowRight").transform;
		GameObject.Find ("ikRA").GetComponent<IKLimb_BrunoFerreira> ().debug = false;
		GameObject.Find ("ikRA").GetComponent<IKLimb_BrunoFerreira> ().IsEnabled = true;

		GameObject.Find("ikLeftLeg").GetComponent<ikLegs>().upperArm = GameObject.Find("thigh_l").transform;
		GameObject.Find("ikLeftLeg").GetComponent<ikLegs>().forearm = GameObject.Find("calf_l").transform;
		GameObject.Find("ikLeftLeg").GetComponent<ikLegs>().hand = GameObject.Find("foot_l").transform;
		GameObject.Find("ikLeftLeg").GetComponent<ikLegs>().target = GameObject.Find("FootPivotLeft").transform;
		GameObject.Find("ikLeftLeg").GetComponent<ikLegs>().elbowTarget = GameObject.Find("LeftKnee").transform;

		GameObject.Find("ikRightLeg").GetComponent<ikLegs>().upperArm = GameObject.Find("thigh_r").transform;
		GameObject.Find("ikRightLeg").GetComponent<ikLegs>().forearm = GameObject.Find("calf_r").transform;
		GameObject.Find("ikRightLeg").GetComponent<ikLegs>().hand = GameObject.Find("foot_r").transform;
		GameObject.Find("ikRightLeg").GetComponent<ikLegs>().target = GameObject.Find("FootPivotRight").transform;
		GameObject.Find("ikRightLeg").GetComponent<ikLegs>().elbowTarget = GameObject.Find("RightKnee").transform;

	}
	/// <summaryDeletse iks. </summary>
	private void delIK() {
		if (GameObject.Find("ikLA").GetComponent<ikInEditor>() != null) {
			DestroyImmediate(GameObject.Find("ikLA").GetComponent<ikInEditor>());

		}
		if (GameObject.Find("ikRA").GetComponent<ikInEditor>() != null) {
			DestroyImmediate(GameObject.Find("ikRA").GetComponent<ikInEditor>());

		}

	}

}


