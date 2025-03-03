using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class GoalNet : NetworkBehaviour
{
    public GameObject ballPrefab;
    public Transform ballSpawnPoint;
    
    private PointSystem pointSystem;
    
    void Start()
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

        SpawnBallServerRpc();
    }
    
    [ServerRpc]
    private void SpawnBallServerRpc()
    {
        GameObject newBall = Instantiate(ballPrefab, ballSpawnPoint.position, Quaternion.identity);
        NetworkObject networkObject = newBall.GetComponent<NetworkObject>();

        if (networkObject != null)
        {
            networkObject.Spawn(true); // Ensure it's properly spawned on all clients
            NotifyClientsOfNewBallClientRpc(networkObject.NetworkObjectId);
        }
        else
        {
            Debug.LogError("Spawned ball does not have a NetworkObject component!");
        }
    }


    [ClientRpc]
    private void NotifyClientsOfNewBallClientRpc(ulong networkObjectId)
    {
        if (NetworkManager.Singleton.SpawnManager.SpawnedObjects.TryGetValue(networkObjectId, out NetworkObject newBall))
        {
            newBall.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
        else
        {
            Debug.LogError("Failed to find the spawned ball on the client.");
        }
    }
}

