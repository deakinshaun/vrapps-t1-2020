using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using UnityEditor;
using UnityEngine;

/*
 * Loads external native C++ libraries (from wk7 notes example 7.1.3) libposeinterface.so for body tracking
 * These libraries are for Linux/Android and will not run under Unity Editor.
 * Also suspect that they will not run under Linux desktop as all attempts failed import in Linux desktop environment.
 * ie We can only run these on Android device with ARM64 architecture
 * */

public static class LoadPlugins
{

    [DllImport("poseinterface")]
    unsafe public static extern void initPose(string modelfile);
    [DllImport("poseinterface")]
    unsafe public static extern int computePose(IntPtr texture, int w, int h, float* results);
    [DllImport("poseinterface")]
    unsafe public static extern int computePoseData(byte[] imageData, int w, int h, float* results);



    static LoadPlugins()
    {
        var currentPath = Environment.GetEnvironmentVariable("PATH", EnvironmentVariableTarget.Process);
#if UNITY_EDITOR_32
        var dllPath = Application.dataPath
            + Path.DirectorySeparatorChar + "Plugins"
            + Path.DirectorySeparatorChar + "x86";
#elif UNITY_EDITOR_64
        var dllPath = Application.dataPath
            + Path.DirectorySeparatorChar + "Plugins"
            + Path.DirectorySeparatorChar + "x86_64";
#else // Player
            var dllPath = Application.dataPath
            + Path.DirectorySeparatorChar + "Plugins";

#endif
        if (currentPath != null && currentPath.Contains(dllPath) == false)
        {
            Environment.SetEnvironmentVariable("PATH", currentPath + Path.PathSeparator + dllPath, EnvironmentVariableTarget.Process);
            Debug.Log("SetEnvironmentVariable to: " + currentPath + Path.PathSeparator + dllPath);
        }
    }
}
