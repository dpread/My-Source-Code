# My-Source-Code
Here is the stored code for my final year project, written in C#

The project was a Unity-based toolkit that was designed to help Virtual Reality designers make their games more user friendly, by implementing several constraints; spatial, user, ergonomic, physical and the format. 

This toolkit operates in two areas, in editor and play modes. The editor mode is more for the design and positioning of items and the world, whereas the play mode was where the VR mechanics were tested, so if the user could reach something, and how ergonomically stressful it was on their body.  

ReadMe taken from Unity folder:

Custom Inspector

The Inspectors layouts reflects a possible use case of the system. 
Select Avatar - There is a drop down where the user can select from 3 default avatar positions; stadning, sitting and walking. These represent the user and the VR interaction format. 
Add Node - The user can add a node that is a visual representation of the VR tracking technology, this is mainly for designing.  
Spatial constraint - A model, usually a table, that is a 'real life' object is is not included in-game.
					It is also to aid design, by allowing the designer to not put elements underneath.
					Rather it is what the user will be dealing with, such as playing on a desk. 

--Add Interaction Box---
This Function makes use of the InteractionObject variable. The function adds a box to the selected object. 

--InteractionObject - This is an object that the user adds themselves. This is will be a model of an in-game element. This will then be used via the interaction box button
					which adds a box to the object. This box is then used via the interaction check. 

--Interaction Check---
The user uses this function to check whether the Interactee can reach the Target. 
	-Interactee - This is added by the user, and is one of the three avatars that is currently being used. 
	-Target -  This is InteractionObject that the avatar is trying to interact with.  	 	 

--Run RULA Assessment---
This is a function that only takes affect once the scene has been changed. 
It measures the rotation of the various points on the avatar, and assigns a value that represents ergonpmic stress


The following scripts were not made by me, but were correctly referenced: 

ikLimbBruno..... the similar C# versions of my source code where adpated in the editor, for making the legs, and body move correctly. 
SteamVR and VR interaction-like scripts, as in reality, this was not part of my job as toolkit designer, as game developer would decide what happens during interaction, these scripts were mereley tests to see if interactivity was sucessful. 
