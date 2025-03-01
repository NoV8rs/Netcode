using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Unity.Netcode;

public class GoalNet : MonoBehaviour
{
    public GameObject ballPrefab;
    public Transform ballSpawnPoint;
    
    [ServerRpc]
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Ball"))
        {
            Debug.Log("Goal!");
            collider.gameObject.IsDestroyed();
            SpawnBallServerRpc();
        }
    }
    
    [ServerRpc]
    private void SpawnBallServerRpc()
    {
        // Spawn a new ball in the center of the field
        GameObject newBall = Instantiate(ballPrefab, ballSpawnPoint.position, Quaternion.identity);
        newBall.GetComponent<NetworkObject>().Spawn();
        newBall.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }
}
