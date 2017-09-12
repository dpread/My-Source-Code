using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI; 
/// <summary> This class is a type of custom editor, using button scripts for the functionality.  </summary>  
[CustomEditor(typeof(ButtonScripts))] 
public class MyInspector : Editor {
	GameObject currentAva, baseGo, holder, kinectNode, table;
	List<GameObject> editorElements = new List<GameObject>();
	bool rulaRun = false;
	//bool editor = true; 
	GameObject interactionObj = null;
	GameObject targetIO = null;
	GameObject interactee = null;
	GameObject ikTarget = null;
	GameObject vive = null;
	/// <summary> Builds the inspector. Creates the inspector item, then attaches the buttonscript functionality to it.   </summary>  
	public override void OnInspectorGUI() 
	{
		//Constantly searches for current avatar 
		baseGo = GameObject.Find ("MAIN MENU");
		holder = GameObject.Find ("Male_Standing_Pose(Clone)");
		if (holder != null) {
			currentAva = holder; 
			if (!editorElements.Contains(currentAva)) {
				editorElements.Add (currentAva);
			}
		}
		holder = GameObject.Find ("Male_Sitting_Pose(Clone)");

		if (holder != null) {
			currentAva = holder; 
			if (!editorElements.Contains(currentAva)) {
				editorElements.Add (currentAva);
			}
		}
		holder = GameObject.Find ("walking(Clone)");

		if (holder != null) {
			currentAva = holder; 
			if (!editorElements.Contains(currentAva)) {
				editorElements.Add (currentAva);
			}			
		}
		ButtonScripts script = (ButtonScripts)target;
		script.setbGO (baseGo);

		int selected = 0;
		string[] options = new string[]
		{
			"None", "Avater Sit", "Avatar Stand", "Avatar Walk", "Remove Avatar"
		};
		selected = EditorGUILayout.Popup ("Select Avatar :", selected, options);

		if (selected == 1) 
		{
			if (currentAva != null) {
				deleteCAva ();
			}
			currentAva = script.CreateSit ();
			currentAva.AddComponent<RULA> ();
			currentAva.AddComponent<RULA_Play> ();
			currentAva.AddComponent<ikArmRotCheck> ();
			currentAva.AddComponent<ikRotinEditor> ();
			GameObject.Find ("ikBody").GetComponent<ik_Body> ().setAva (currentAva);
		}
		if (selected == 2 )
		{
			if (currentAva != null) {
				deleteCAva ();
			}			
			currentAva = script.CreateStand ();
			currentAva.AddComponent<RULA> ();
			currentAva.AddComponent<RULA_Play> ();
			currentAva.AddComponent<ikArmRotCheck> ();
			currentAva.AddComponent<ikRotinEditor> ();
			GameObject.Find ("ikBody").GetComponent<ik_Body> ().setAva (currentAva);

		}
		if (selected == 3 )
		{
			if (currentAva != null) {
				deleteCAva ();
			}			
			currentAva = script.CreateWalk ();
			currentAva.AddComponent<RULA> ();
			currentAva.AddComponent<RULA_Play> ();
			currentAva.AddComponent<ikArmRotCheck> ();
			currentAva.AddComponent<ikRotinEditor> ();
			GameObject.Find ("ikBody").GetComponent<ik_Body> ().setAva (currentAva);

		}

		if (selected == 4 )
		{
			deleteCAva ();
			editorElements.Remove(currentAva);			

		}
			

		if (GUILayout.Button ("Add Kinect Node"))
		{
			if (GameObject.Find ("KinectNode") == null) {
				kinectNode = script.createKinectNode ();
				editorElements.Add (kinectNode);
			}
		}
		if (GUILayout.Button ("Add Vive Area"))
		{
			if (GameObject.Find ("[CameraRig]") == null) {
				//kinectNode = script.createKinectNode ();
				//editorElements.Add (kinectNode);
				vive = (GameObject)Instantiate(Resources.Load("[CameraRig]"));
				vive.name = "[CameraRig]";
			}
		}
		int selectedSensor = 0; 
		string[] optionsSensor = new string[]
		{
			"None", "Table"
		};
		selectedSensor = EditorGUILayout.Popup ("Spatial Constraint:", selectedSensor, optionsSensor);

		if (selectedSensor == 1) 
		{
			table = script.createTable ();
			editorElements.Add(table);

		}
		interactionObj = (GameObject)EditorGUILayout.ObjectField ("InteractionObject",interactionObj, typeof(GameObject), true);

		if (GUILayout.Button ("Add Interaction Box to interactObj"))
		{
			script.interactObj = interactionObj;
			GameObject spatial = script.createInteractionBox ();
			editorElements.Add (spatial);
		}
		interactee = (GameObject)EditorGUILayout.ObjectField ("Interactee",interactee, typeof(GameObject), true);
		targetIO = (GameObject)EditorGUILayout.ObjectField ("Target",targetIO, typeof(GameObject), true);

		if (GUILayout.Button ("Check Interaction"))
			{
				script.interactee = interactee;
				script.target = targetIO;
				script.interactionCheck();
			}
		ikTarget = (GameObject)EditorGUILayout.ObjectField ("RULA Target",ikTarget, typeof(GameObject), true);
		if (GUILayout.Button ("Move Left Arm to RULA Target"))
		{
			if (GameObject.Find("ikLA").GetComponent<ikInEditor>() == null) {
				GameObject.Find ("ikLA").AddComponent<ikInEditor> ();
				GameObject.Find ("ikLA").GetComponent<ikInEditor> ().setIKsL ();
			}
			if (ikTarget == null) {
				Debug.LogError("You must add a RULA Target First");
					} else {
			script.moveLeftToTarget (ikTarget);
					}
		}
		if (GUILayout.Button ("Move Right Arm to RULA Target")) {
			if (GameObject.Find ("ikRA").GetComponent<ikInEditor> () == null) {
				GameObject.Find ("ikRA").AddComponent<ikInEditor> ();
				GameObject.Find ("ikRA").GetComponent<ikInEditor> ().setIKsR ();
			}
			if (ikTarget == null) {
				Debug.LogError("You must add a RULA Target First");
			} else {
				script.moveRightToTarget (ikTarget);

			}
		}


		if (GUILayout.Button ("Run RULA assessment")) {
			if (Application.isPlaying) {
				if (!rulaRun) {
					rulaRun = true;
					currentAva.GetComponent<RULA_Play> ().setRun (rulaRun);
				} else {
					rulaRun = false;
					currentAva.GetComponent<RULA_Play> ().setRun (rulaRun);
				}
			} else {
				if (!rulaRun) {
					rulaRun = true;
					script.runRULA (rulaRun);
				} else {
					rulaRun = false;
					script.runRULA (rulaRun);
				}
			}

		}
		string[] sceneSelect = new string[]
		{
			"None", "editor Scene", "previewVR Scene"
		};
		int selectedScene = 0; 

		selectedScene = EditorGUILayout.Popup ("Change Editor View:", selectedScene, sceneSelect);

		if (selectedScene == 1) 
		{
			script.setEditorScene ();

		}
		if (selectedScene == 2 )
		{
			script.setPreviewScene ();
		}

	}	
	private void deleteCAva() {
		
		if (currentAva != null) {
			editorElements.Remove(currentAva);			
			DestroyImmediate (currentAva);
		}

	}
}


