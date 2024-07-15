using System;
using UnityEngine;
using System.Threading.Tasks;
using AmarilisLib;

public class StateTrackerExample : MonoBehaviour
{
    // StateTracker usage example 1 : Save, Undo, Redo, GetHistory
    public void Example1()
    {
        var stateTracker = new StateTracker<int>(1);

        Debug.Log("Save: " + stateTracker.Save(2));
        Debug.Log("Save: " + stateTracker.Save(3));
        Debug.Log("Save: " + stateTracker.Save(4));

        Debug.Log("Current State: " + stateTracker.CurrentState);

        Debug.Log("Undo: " + stateTracker.Undo());
        Debug.Log("After Undo: " + stateTracker.CurrentState);

        Debug.Log("Redo: " + stateTracker.Redo());
        Debug.Log("After Redo: " + stateTracker.CurrentState);

        Debug.Log("Save: " + stateTracker.Save(5));
        Debug.Log("Undo: " + stateTracker.Undo());
        Debug.Log("Current State: " + stateTracker.CurrentState);
        Debug.Log("Save: " + stateTracker.Save(6));

        Debug.Log("Current State: " + stateTracker.CurrentState);

        Debug.Log("Save History:");
        foreach(var state in stateTracker.GetHistory())
        {
            Debug.Log(state);
        }
    }
    private void OnGUI()
    {
        if(GUILayout.Button("IOMonad usage example 1"))
        {
            Example1();
        }
    }
}