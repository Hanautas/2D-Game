using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyManager : MonoBehaviour
{
    public bool isShowing;

    public GameObject partyManagement;

    public void SetPartyManagementActive()
    {
        isShowing = !isShowing;

        partyManagement.SetActive(isShowing);
    }
}