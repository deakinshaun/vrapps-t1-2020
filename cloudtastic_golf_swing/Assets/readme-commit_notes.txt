Notes:

01/05/2020
MDeed - committed changes to include on device body tracking on Android.
Changed platform to Android
Player settings changed:
Allow unsafe code checked - required to run external device native tensorflowlite libraries for pose tracking.
Changed architecture to target ARM64 - code doesn't appear to run on my older ARM7 phone.
Changed min API to level 8 - to enable ARM64 checkbox
Changed scripting backend to IL2CPP to work with native libraries.

Before Building/Deploy:
MainScene now has PoseManager & PoseSkeletonManager GameObjects to hold the FetchPose & PoseSkeleton scripts.
The PoseSkeletonManager/PoseSkeleton script has public variables for joint and bone prefabs that need to be assigned in the editor prior to building the project - these are the 3D meshes to display a skeleton.
The PoseManager/FetchPose script has public variables for the TrackingDisplayPlane, TrackingDisplayPlaneMaterial, PoseVisualizer - these need to be assigned in the Editor prior to building the project
See screenshots folder for detail.

Runtime considerations:
Can only track a single skeleton - so only one person in frame or wont work.
Currently only tracks in landscape mode either left or right (forget which). Should be able to track a person in a Youtube clip - so google golf swings and try!


7/05/2020
further changes to body tracking
changed display from 3D plane to rawImage with appropriate sizing
added singleton static gamemanager class with scene loader 
added posedata and poseclip classes to allow us to save/store/compare captured motion data