using System.Collections;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PixelPerfectMainCamera))]
public class PixelPerfectMainCameraEditor : Editor
{

	public override void OnInspectorGUI()
    {

        PixelPerfectMainCamera cam = (PixelPerfectMainCamera)target;

        EditorGUI.BeginChangeCheck();

        cam.PPU = EditorGUILayout.IntField("Pixels Per Unit", cam.PPU);
        cam.screenHeight = EditorGUILayout.IntField("Screen height", cam.screenHeight);

        if(EditorGUI.EndChangeCheck()) {
            cam.UpdateOrthoSize();
        }

        EditorGUILayout.LabelField("Calculated Orthographic Size", cam.orthoSize + "");

        if(GUILayout.Button("Apply to Main Camera"))
        {

            cam.ApplyOrthoSize();

        }




    }

}
