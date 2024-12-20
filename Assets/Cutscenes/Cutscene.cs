using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscene : MonoBehaviour, IPlayerTriggerable
{
    [SerializeReference]
    [SerializeField] List<CutsceneAction> actions;

    public bool TriggerRepeatedly => false;

    public IEnumerator Play()
    {
        GameController.Instance.StartCutsceneState();

        CameraShaker cameraShaker = FindObjectOfType<CameraShaker>();
        if (cameraShaker != null)
        {
            cameraShaker.StartShaking();
        }

        foreach (var action in actions)
        {
            yield return action.Play();
        }

        // GameController.Instance.StartFreeRoamState();
    } 

    public void AddAction(CutsceneAction action)
    {
        action.Name = action.GetType().ToString();
        actions.Add(action);
    }


    public void OnPlayerTriggered(PlayerController player)
    {
        // Debug.Log("here");
        player.Character.Animator.IsMoving = false;
        StartCoroutine(Play());

    }
}