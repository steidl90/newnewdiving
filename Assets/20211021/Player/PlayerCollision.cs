using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        GameManager.gameManager.player.GetComponent<PlayerController>().OnTrigger(other, transform.position);
        Debug.Log(GetComponent<Rigidbody>().velocity.magnitude);
        if (other.tag.Equals("House") && GetComponent<Rigidbody>().velocity.magnitude >= 29f)
        {
            var frags = Physics.OverlapSphere(transform.position, 1f);
            foreach (var obj in frags)
            {
                obj.gameObject.SendMessage("Damage", 3.5, SendMessageOptions.DontRequireReceiver);
            }
        }
    }
}
