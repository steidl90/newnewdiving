using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var playerControl = GameManager.gameManager.player.GetComponent<PlayerController>();
        playerControl.OnTrigger(other, transform.position);
        var speed = playerControl.IsReplay ? -1f : 29f;
        //Debug.Log($"{GetComponent<Rigidbody>().velocity.magnitude}, {speed}");
        if (other.CompareTag("House") && GetComponent<Rigidbody>().velocity.magnitude >= speed)
        {
            var house = GameManager.gameManager.destoryHouse.GetComponent<DestroyHouse>().houses;
            foreach (var elem in house)
            {
                if (elem.Value == other.gameObject)
                    playerControl.houseKey = elem.Key;
            }
            Debug.Log(playerControl.houseKey);
            var frags = Physics.OverlapSphere(transform.position, 3f);
            foreach (var obj in frags)
            {
                obj.gameObject.SendMessage("Damage", 3.5, SendMessageOptions.DontRequireReceiver);
            }
        }
    }
}
