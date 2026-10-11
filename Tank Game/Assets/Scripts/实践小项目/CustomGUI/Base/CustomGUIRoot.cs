using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[ExecuteAlways]
public class CustomGUIRoot : MonoBehaviour
{
    private CustomGUIControl[] allControls;
    public void Start()
    {
        allControls = this.GetComponentsInChildren<CustomGUIControl>();
    }
    private void OnGUI()
    {
        allControls = this.GetComponentsInChildren<CustomGUIControl>();
        for(int i = 0; i < allControls.Length; i++)
        {
            allControls[i].DrawGUI();
        }
    }
}
