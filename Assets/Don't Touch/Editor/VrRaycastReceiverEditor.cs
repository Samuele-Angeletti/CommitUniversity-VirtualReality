using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(VrRaycastReceiver))]
public class VrRaycastReceiverEditor : Editor
{
    public override void OnInspectorGUI()
    {
        VrRaycastReceiver receiver = (VrRaycastReceiver)target;

        if (GUILayout.Button("Test Button"))
        {
            receiver.Action();
        }

        base.OnInspectorGUI();
    }
}
