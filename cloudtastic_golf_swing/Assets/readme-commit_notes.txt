Notes:

01/05/2020
MDeed - committed changes to include on device body tracking on Android.
Changed platform to Android
Player settings changed:
Allow unsafe code checked - required to run external device native tensorflowlite libraries for pose tracking.
Changed architecture to target ARM64 - code doesn't appear to run on my older ARM7 phone.
Changed min API to level 8 - to enable ARM64 checkbox
Changed scripting backend to IL2CPP to work with native libraries.

7/05/2020
further changes to body tracking:
changed display from 3D plane to rawImage with appropriate sizing
added singleton static gamemanager class with scene loader - may want to place instructions/title here or provide navigation to other functions
added posedata and poseclip classes to allow us to save/store/compare captured motion data.
GameManager object/class in the menuscene is carried over to the mainscene. It (Gamemanager) is required in the Mainscene for running the file save coroutine - so start the menuscene 1st.

12/05/2020
Added some code comments
Added some null checking to clean up play/pause/record logic.
Added some buttons for the expert golf swing - these are currently playing/loading the player golf swing data - still to seperate into player vs expert motion files.
Including a Test.json file with some motion captured with my phone - useful for testing on PC. The app reads this from Application.persistentDataPath + "/Test.json" which on my PC is:
C:\Users\simulation\AppData\LocalLow\CloudTastic\Golf Swing\Test.json

Before Building/Deploy:
MainScene has RawImage & PoseVisualiser GameObjects to hold the FetchPose & PoseSkeleton scripts.
The Posevisualiser/PoseSkeleton script has public variables for joint and bone prefabs that need to be assigned in the editor prior to building the project - these are the 3D meshes to display a skeleton.
The Rawimage/FetchPose script has public variable for the PoseVisualizer - needs to be assigned in the Editor prior to building the project


Runtime considerations:
Can only track a single skeleton - so only one person in frame or wont work.
Currently only tracks in landscape mode either left or right (forget which). Should be able to track a person in a Youtube clip - so google golf swings and try!

