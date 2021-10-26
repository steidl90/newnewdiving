using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        GameManager.gameManager.player.GetComponent<PlayerController>().OnTrigger(other, transform.position);
    }
}
