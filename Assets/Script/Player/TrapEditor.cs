using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(Trap))]public class TrapEditor : Editor
{
    // public override void OnInspectorGUI()
    // {
        
    //     Trap trap = (Trap)target;
    //     trap.TrapTypes = (Trap.TrapType)EditorGUILayout.EnumPopup("Trap Types",trap.TrapTypes);



    //     trap.target = EditorGUILayout.Vector3Field("Target", trap.target);
    //     trap.duration = EditorGUILayout.FloatField("Duration", trap.duration);

    //     if(GUI.changed){
    //         EditorUtility.SetDirty(trap);
    //     }
    // }

}
