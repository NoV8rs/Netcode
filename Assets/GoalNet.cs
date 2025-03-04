using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class GoalNet : NetworkBehaviour
{
    private PointSystem pointSystem;
    private NetworkObject currentBall;
    
    public override void OnNetworkSpawn()
    {
        pointSystem = GameObject.Find("PointSystem").GetComponent<PointSystem>();
    }
    
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Ball"))
        {
            Debug.Log("Goal!");
            HandleGoalServerRpc(gameObject.CompareTag("HomeGoal"));
            Destroy(collider.gameObject);
        }
    }

    [ServerRpc]
    private void HandleGoalServerRpc(bool isHomeGoal)
    {
        if (isHomeGoal)
        {
            pointSystem.AddHomePointServerRpc();
        }
        else
        {
            pointSystem.AddAwayPointServerRpc();
        }
    }
}

