using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Unity.Netcode;

public class GoalNet : MonoBehaviour
{
    public GameObject ballPrefab;
    public Transform ballSpawnPoint;
    
    private PointSystem pointSystem;
    
    void Start()
    {
        pointSystem = GameObject.Find("PointSystem").GetComponent<PointSystem>();
    }
    
    [ServerRpc]
    private void OnTriggerEnter(Collider collider)
    {
        if (gameObject.CompareTag("HomeGoal"))
        {
            if (collider.gameObject.CompareTag("Ball"))
            {
                Debug.Log("Goal!");
                Destroy(collider.gameObject);
                pointSystem.AddHomePoint();
                SpawnBallServerRpc();
            }
        }
        
        else if (gameObject.CompareTag("AwayGoal"))
        {
            if (!collider.gameObject.CompareTag("Ball"))
            {
                return;
            }
            
            Debug.Log("Goal!");
            Destroy(collider.gameObject);
            pointSystem.AddAwayPoint();
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
