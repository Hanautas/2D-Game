using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscene : MonoBehaviour
{
    public bool onStart;

    public CutsceneEvent[] scenes;

    void Start()
    {
        if (onStart)
        {
            StartCutscene();
        }
    }

    [ContextMenu("Start Cutscene")]
    public void StartCutscene()
    {
        foreach (CutsceneEvent scene in scenes)
        {
            StartCoroutine(StartScene(scene));
        }
    }

    private IEnumerator StartScene(CutsceneEvent scene)
    {
        yield return new WaitForSeconds(scene.delay);

        scene.cutsceneEvent.Invoke();
    }
}