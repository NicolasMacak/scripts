using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Constants;

public class RespawnController : MonoBehaviour
{
    private Vector3 checkpoint;

    public BlackscreenController BlackScreenPanel;
    public GameObject startLocation;
    public CharacterController characterController;

    // Start is called before the first frame update
    void Start()
    {
        checkpoint = startLocation.transform.position;
    }

    private IEnumerator ach()
    {
        yield return new WaitForSeconds(2);
        KillPlayer();
        print(transform.position);
    }

    public void KillPlayer()
    {
        BlackScreenPanel.TriggerBlackScreen();
        characterController.enabled = false;
        characterController.transform.position = checkpoint;
        characterController.enabled = true;
    }

    public void setCheckPoint(string retriewedItemName)
    {
        GameObject newCheckpoint = GameObject.Find(retriewedItemName + nameSpacesStrings.CheckPoint);

        if(newCheckpoint == null)
        {
            print("no such checkpoint");
            return;
        }
        checkpoint = newCheckpoint.transform.position;
    }
}
