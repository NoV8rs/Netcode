using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerController : NetworkBehaviour
{
    float speed = 5.0f;
    private Rigidbody playerRb;
    
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerRb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!IsOwner)
        {
            return;
        }
        
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        
        Vector3 moveDirection = new Vector3(moveHorizontal, 0.0f, moveVertical);
        playerRb.MovePosition(playerRb.position + moveDirection * speed * Time.fixedDeltaTime);
        
        MovePlayerServerRpc(moveDirection);
    }
    
    [ServerRpc]
    private void MovePlayerServerRpc(Vector3 moveDirection)
    {
        playerRb.MovePosition(playerRb.position + moveDirection * speed * Time.fixedDeltaTime);
    }
}
