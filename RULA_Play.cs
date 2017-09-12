using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary> Runs RULA when in the VR mode.</summary>  

public class RULA_Play : MonoBehaviour {
	bool run = false;
	int indexL = 0;
	int indexR = 0;
	int indexNTL = 0;

	int uArmScoreL = 0;
	int lArmScoreL = 0;
	int wristScoreL = 0;
	int wristTwistL = 0;

	int uArmScoreR = 0;
	int lArmScoreR = 0;
	int wristScoreR = 0;
	int wristTwistR = 0;

	int neck = 0;
	int trunk = 0;
	int legs = 0;

	int leftSideAW = 0;
	int rightSideAW = 0;
	int nTL = 0;

	int finalLSScore = 0;
	int finalRSScore = 0;

	int[][] uA1 = new int[3][];
	int[][] uA2 = new int[3][];
	int[][] uA3 = new int[3][];
	int[][] uA4 = new int[3][];
	int[][] uA5 = new int[3][];
	int[][] uA6 = new int[3][];

	int[][] postureScore = new int[6][];
	int[][] wristArm = new int[8][];

	GameObject leftCube;
	GameObject rightCube;
	GameObject gaO ;

	void Update() {
		if (run) {
			checkRULA ();

		}}

	public void setRun(bool value) {
		run = value;
	}
	/// <summary> Centre for the check, calls the two sets of checks, then creates tables, and looks for the results in the values.
	/// Then creates cubes to visually show the result. 
	/// </summary>  
	public void checkRULA() {
		gaO = GameObject.Find ("Male_Sitting_Pose(Clone)");
		createTableA ();
		createTableB ();
		createTableC ();

		calcArmWrist ();
		calcNTL ();
		indexL = wristScoreL + (wristScoreL - 1) - 1;
		if (wristTwistL == 2) {
			indexL = indexL + 1;
		}
		lArmScoreL--;
		if (uArmScoreL == 1) {

			leftSideAW = uA1 [lArmScoreL][indexL];
		}
		if (uArmScoreL == 2) {
			leftSideAW = uA2 [lArmScoreL][indexL];
		}
		if (uArmScoreL == 3) {
			leftSideAW = uA3 [lArmScoreL][indexL];
		}

		if (uArmScoreL == 4) {
			leftSideAW = uA4 [lArmScoreL][indexL];
		}
		if (uArmScoreL == 5) {
			leftSideAW = uA5 [lArmScoreL][indexL];
		}
		if (uArmScoreL == 6) {
			leftSideAW = uA6 [lArmScoreL][indexL];
		}

		//calculate right side
		indexR = wristScoreR + (wristScoreR - 1) - 1;
		if (wristTwistR == 2) {
			indexR = indexR + 1;
		}
		uArmScoreR--;
		if (uArmScoreR == 1) {
			rightSideAW = uA1 [lArmScoreR][indexR];
		}
		if (uArmScoreR == 2) {
			rightSideAW = uA2 [lArmScoreR][indexR];
		}
		if (uArmScoreR == 3) {
			rightSideAW = uA3 [lArmScoreR][indexR];
		}

		if (uArmScoreR == 4) {
			rightSideAW = uA4 [lArmScoreR][indexR];
		}
		if (uArmScoreR == 5) {
			rightSideAW = uA5 [lArmScoreR][indexR];
		}
		if (uArmScoreR == 6) {
			rightSideAW = uA6 [lArmScoreR][indexR];
		}
		indexNTL = trunk + (trunk - 1) - 1;
		if (legs == 2) {
			indexNTL = indexNTL + 1;
		}
		neck--;
		nTL = postureScore [neck] [indexNTL];
		if (leftSideAW > 8) {
			leftSideAW = 8;
		}
		if (rightSideAW > 8) {
			rightSideAW = 8;
		}
		if (nTL > 7) {
			nTL = 7;
		} 
		//Debug.Log (uArmScoreR + "   upper Right");
		//Debug.Log (leftSideAW + "    " + rightSideAW);
		//Debug.Log ( " upper  " + uArmScoreL + "  lower  " + lArmScoreL + "  wrsitSc " + wristScoreL + "  wristTwist " + wristTwistL);

		finalLSScore = wristArm [leftSideAW - 1] [nTL -1];
		finalRSScore = wristArm [rightSideAW - 1] [nTL -1];

		if ( leftCube == null) {
			leftCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
			leftCube.name = "clavicle_l_RULA";
			leftCube.transform.localPosition = new Vector3 (-1.59f, 1f, -0.08f);
			leftCube.transform.localScale = new Vector3 (3f, 2f, 0.13f); 

			Quaternion rot = leftCube.transform.localRotation;
			rot.eulerAngles = new Vector3 (0, 90f, 0f);
			leftCube.transform.localRotation = rot;

			rightCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
			rightCube.name = "clavicle_r_RULA";
			rightCube.transform.localPosition = new Vector3 (1.59f, 1f, -0.08f);
			rightCube.transform.localScale = new Vector3 (3f, 2f, 0.13f); 
			rightCube.transform.localRotation = rot;
		}
		Material tempMaterialL = new Material(leftCube.GetComponent<Renderer> ().sharedMaterial);
		switch (finalLSScore) {
		case 1:
		case 2:
			tempMaterialL.color = Color.green;
			leftCube.GetComponent<Renderer> ().sharedMaterial = tempMaterialL;	
			break;
		case 3:
		case 4:
			tempMaterialL.color = Color.yellow;
			leftCube.GetComponent<Renderer> ().sharedMaterial = tempMaterialL;	
			break;

		case 5:
		case 6:
			tempMaterialL.color = Color.HSVToRGB (255, 165, 0);
			leftCube.GetComponent<Renderer> ().sharedMaterial = tempMaterialL;	
			break;
		}
		switch (finalRSScore) {
		case 1:
		case 2:
			tempMaterialL.color = Color.green;
			rightCube.GetComponent<Renderer> ().sharedMaterial = tempMaterialL;	
			break;
		case 3:
		case 4:
			tempMaterialL.color = Color.yellow;
			rightCube.GetComponent<Renderer> ().sharedMaterial = tempMaterialL;	
			break;

		case 5:
		case 6:
			tempMaterialL.color = Color.HSVToRGB (255, 165, 0);
			rightCube.GetComponent<Renderer> ().sharedMaterial = tempMaterialL;	
			break;
		}
		if (finalLSScore >= 6) 
		{
			tempMaterialL.color = Color.red;
			rightCube.GetComponent<Renderer> ().sharedMaterial = tempMaterialL;	

		}
		if (finalRSScore >= 6) 
		{
			tempMaterialL.color = Color.red;
			rightCube.GetComponent<Renderer> ().sharedMaterial = tempMaterialL;	

		}

		Debug.Log ("The final RULA for the left side is " + finalLSScore); 
		Debug.Log ("The final RULA for the right side is " + finalRSScore);

	}
	/// <summary> Arm Check methods all called.  </summary>  

	void calcArmWrist() {
		calcUpperArm ();
		calcLowerArm ();
		calcWristPos ();
		calcWristTwist ();

	}
	/// <summary> Calls the body parts RULA check.  </summary>  

	void calcNTL() {
		calcNeckPos ();
		calcTrunkPos();
		calcLegs ();
	}
	/// <summary> Assigns a RULA score to both left and right upper arms by checking their rotations </summary>  

	void calcUpperArm() {
		//left side
		Quaternion qu = GameObject.Find ("upperarm_l").transform.rotation;
		if (qu.y > -0.218f ) { 
			uArmScoreL = 1;
		}
		if (qu.y < -0.218f && qu.y > - 0.328f) {
			uArmScoreL = 2;
		}
		if (qu.y < 0.276f && qu.y > 0.107f) {
			uArmScoreL = 2;
		}

		if (qu.y > 0.276f && qu.y < 0.64f) {
			uArmScoreL = 3;
		}
		if ( qu.y > 0.64f) {
			uArmScoreL = 4;
		}

		//right side
		Quaternion quR = GameObject.Find ("upperarm_r").transform.rotation;

		if (quR.y < 0.218f ) { 
			uArmScoreR = 1;
		}
		if (quR.y > 0.218f && quR.y < 0.328f) {
			uArmScoreR = 2;
		}
		if (quR.y > -0.276f && quR.y < -0.107f) {
			uArmScoreR = 2;
		}

		if (quR.y < -0.276f && quR.y > -0.64f) {
			uArmScoreR = 3;
		}
		if ( quR.y < -0.64f) {
			uArmScoreR = 4;
		}
		//Debug.Log (quR.y + "         " + uArmScoreR + "  upper");


	}
	/// <summary> Assigns a RULA score to both left and right lower arms by checking their rotations </summary>  

	void calcLowerArm() {
		//left side
		Quaternion qu = GameObject.Find ("lowerarm_l").transform.rotation;
		if (qu.y > 0.58f && qu.y < 0.73f) {
			lArmScoreL = 1;
		}
		if (qu.y >= 0.73f || qu.y <= 0.58f) {
			lArmScoreL = 2;

		}

		if (qu.z > 0.714f && qu.z < 0.64f) {
			lArmScoreL = lArmScoreL + 1;
		}
		//Debug.Log (qu.y + "           " + lArmScoreL + "  lower ");

		//right side
		Quaternion quR = GameObject.Find ("lowerarm_r").transform.rotation;
		if (quR.y > 0.58f && quR.y < 0.73f) {
			lArmScoreR = 1;
		}
		if (quR.y >= 0.73f || quR.y <= 0.58f) {
			lArmScoreR = 2;

		}

		if (quR.z > 0.714f && quR.z < 0.64f) {
			lArmScoreR = lArmScoreR + 1;
		}

	}
	/// <summary> Assigns a RULA score to both left and right wrists by checking their rotations </summary>  

	void calcWristPos() {
		//left side
		Quaternion qu = GameObject.Find ("hand_l").transform.rotation;
		if (qu.y > 0.643f && qu.y < 0.68f) {
			wristScoreL = 1;
		}
		if ((qu.y > 0.626f && qu.y < 0.694f)) {
			if (wristScoreL != 1) {
				wristScoreL = 2;
			}
		}
		if (qu.y < 0.626f ||  qu.y > 0.694f) {
			wristScoreL = 3;
		}
		if (qu.z > 0.2f || qu.z < 0.22f) {
			wristScoreL = wristScoreL + 1;
		}
		//Debug.Log (wristScoreL + "  left   " + qu.y);

		//right side
		Quaternion quR = GameObject.Find ("hand_r").transform.rotation;
		if (quR.y < -0.643f && quR.y > -0.68f) {
			wristScoreR = 1;
		}
		if ((quR.y < -0.626f && quR.y > -0.694f)) {
			if (wristScoreR != 1) {
				wristScoreR = 2;
			}
		}
		if (quR.y > -0.626f ||  quR.y < -0.694f) {
			wristScoreR = 3;
		}
		if (quR.z < -0.2f || quR.z > -0.22f) {
			wristScoreR = wristScoreR + 1;
		}
		//Debug.Log (wristScoreR + " right    " + quR.y);


	}
	/// <summary> Assigns a RULA score to both left and right wrist twist by checking their rotations </summary>  

	void calcWristTwist() {
		//left side
		Quaternion qu = GameObject.Find ("hand_l").transform.rotation;
		if (qu.z < -0.595f && qu.z > -0.64f) {
			wristTwistL = 1;
		} else {
			wristTwistL = 2;
		}

		//right side
		Quaternion quR = GameObject.Find ("hand_r").transform.rotation;
		if (quR.z > 0.595f && quR.z < 0.64f) {
			wristTwistR = 1;
		} else {
			wristTwistR = 2;
		}

	}
	/// <summary> Assigns a RULA score to the neck by checking  rotation </summary>  

	void calcNeckPos() {
		Quaternion qu = GameObject.Find ("neck_01").transform.rotation;
		neck = 0;
		if (gaO == null) {
			if (qu.y < 0.073f && qu.y > 0.0706f) {
				neck = 1;
			}
			if (qu.y < 0.0706f && qu.y > 0.0685f) {
				neck = 2;
			}
			if (qu.y < 0.0685f) {
				neck = 3;
			}
			if (qu.y > 0.073f) {
				neck = 4;
			}
		}
		if (gaO != null) {

			if (qu.x < -0.24f && qu.x > -0.29f) {
				neck = 1;
			}
			if (qu.x < -0.29f && qu.x > -0.38f) {
				neck = 2;
			}
			if (qu.x <  -0.38f) {
				neck = 3;
			}
			if (qu.x >  -0.24f) {
				neck = 4;
			}

		}
		if (qu.y >  0.25f || qu.y < -0.25f) {
			neck = neck + 1;
		}

		if (qu.z > 0.021f || qu.z < -0.019f) {
			neck = neck + 1;
		}

	}
	/// <summary> Assigns a RULA score to trunk checking their rotation </summary>  

	void calcTrunkPos() {
		Quaternion qu = GameObject.Find ("spine_03").transform.rotation;
		trunk = 0;
		if (gaO == null) {
			if (qu.x < 0.0165f && qu.x > -0.05235f) {
				trunk = 1;
			}
			if (qu.x < -0.05235f && qu.x > -0.150f) {
				trunk = 2;
			}
			if (qu.x < -0.150f && qu.x > -0.21f) {
				trunk = 3;
			}
			if (qu.x < -0.21f) {
				trunk = 4;
			}
			if (qu.y > -0.0289f || qu.y < 0.0336f) {
				trunk = trunk + 1;
			}
			if (qu.z < -0.0108f || qu.z > -0.0075) {
				trunk = trunk + 1;
			}
		}else {
			if (qu.x < -0.15f && qu.x > -0.25f) {
				trunk = 1;
			}
			if (qu.x < -0.25f && qu.x > -0.3f) {
				trunk = 2;
			}
			if (qu.x < -0.3f && qu.x > -0.35f) {
				trunk = 3;
			}
			if (qu.x < -0.35f) {
				trunk = 4;
			}
			if (qu.y > 0.007f || qu.y < 0.037f) {
				trunk = trunk + 1;
			}
			if (qu.z < -0.05f || qu.z > -0.015) {
				trunk = trunk + 1;
			}

		}
	}
	/// <summary> Assigns a RULA score to legs as a collective. </summary>  

	void calcLegs() {
		if (gaO != null) {
			legs = 1;
		} else {
			Quaternion qu = GameObject.Find ("thigh_l").transform.rotation;
			Quaternion quO = GameObject.Find ("thigh_r").transform.rotation;
			if ((qu.z < -0.310f || qu.z > -0.3f) || (quO.z > 0.320f || quO.z < 0.310f)) {
				legs = 2;
			} else {
				legs = 1;
			}
		}
	}
	/// <summary> Creates TableA </summary>  

	void createTableA() {
		uA1[0] = new int[] { 1,2,2,2,2,3,3,3};
		uA1[1] = new int[] { 2,2,2,2,3,3,3,3};
		uA1[2] = new int[] { 2,3,3,3,3,3,4,4};

		uA2[0] = new int[] { 2,3,3,3,3,4,4,4};
		uA2[1] = new int[] { 3,3,3,3,3,4,4,4};
		uA2[2] = new int[] { 3,4,4,4,4,4,5,5};

		uA3[0] = new int[] { 3,3,4,4,4,4,5,5};
		uA3[1] = new int[] { 3,4,4,4,4,4,5,5};
		uA3[2] = new int[] { 4,4,4,4,4,5,5,5};

		uA4[0] = new int[] { 4,4,4,4,4,5,5,5};
		uA4[1] = new int[] { 4,4,4,4,4,5,5,5};
		uA4[2] = new int[] { 4,4,4,5,5,5,6,6};

		uA5[0] = new int[] { 5,5,5,5,5,6,6,7};
		uA5[1] = new int[] { 5,6,6,6,6,7,7,7};
		uA5[2] = new int[] { 6,6,6,7,7,7,7,8};

		uA6[0] = new int[] { 7,7,7,7,7,8,8,9};
		uA6[1] = new int[] { 8,8,8,8,8,9,9,9};
		uA6[2] = new int[] { 9,9,9,9,9,9,9,9};

	}
	/// <summary> Creates TableB </summary>  

	void createTableB () {
		postureScore[0] = new int[] { 1,3,2,3,3,4,5,5,6,6,7,7};
		postureScore[1] = new int[] { 2,3,2,3,4,5,5,5,6,7,7,7};
		postureScore[2] = new int[] { 3,3,3,4,4,5,5,6,6,7,7,7};
		postureScore[3] = new int[] { 5,5,5,6,6,7,7,7,7,7,8,8};
		postureScore[4] = new int[] { 7,7,7,7,7,8,8,8,8,8,8,8};
		postureScore[5] = new int[] { 8,8,8,8,8,8,8,9,9,9,9,9};

	}	
	/// <summary> Creates TableC </summary>  

	void createTableC() {
		wristArm[0] = new int[] { 1,2,3,3,4,5,5};
		wristArm[1] = new int[] { 2,2,3,4,4,5,5};
		wristArm[2] = new int[] { 3,3,3,4,4,5,6};
		wristArm[3] = new int[] { 3,3,3,4,5,6,6};
		wristArm[4] = new int[] { 4,4,4,5,6,7,7};
		wristArm[5] = new int[] { 4,4,5,6,6,7,7};
		wristArm[6] = new int[] { 5,5,6,6,7,7,7};
		wristArm[7] = new int[] { 5,5,6,7,7,7,7};
	}

}