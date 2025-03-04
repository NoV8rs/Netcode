using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class SoccerBallSpawning : NetworkBehaviour
{
    public GameObject ballPrefab;
    public Transform ballSpawnPoint;
    
    private NetworkVariable<NetworkObjectReference> currentBall = new NetworkVariable<NetworkObjectReference>();

    
    private void Start()
    {
        if (IsServer)
        {
            SpawnBall();
        }
    }
    
    private void SpawnBall()
    {
        if (!currentBall.Value.TryGet(out NetworkObject _))
        {
            GameObject ball = Instantiate(ballPrefab, ballSpawnPoint.position, Quaternion.identity);
            NetworkObject networkObject = ball.GetComponent<NetworkObject>();
            networkObject.Spawn();
            currentBall.Value = networkObject;
        }
    }
    
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Ball"))
        {
            Destroy(collider.gameObject);
            currentBall.Value = default;
            SpawnBall();
            
        }
    }
}
