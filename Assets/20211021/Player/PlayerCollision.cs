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
        Debug.Log(other.name);

        if (other.CompareTag("Plane") && GetComponent<Rigidbody>().velocity.magnitude >= speed / 5f)
        {
            other.gameObject.transform.parent.GetComponentInChildren<FraggedChild>().Damage(speed);
            other.gameObject.transform.parent.GetComponent<PlaneMove>().speed = 0f;
            var childs = other.gameObject.transform.parent.GetComponentsInChildren<FraggedChild>();
            foreach (var child in childs)
            {
                //child.GetComponent<Rigidbody>().AddForce(new Vector3(0f, 5f, 5f), ForceMode.Impulse);
                child.GetComponent<Rigidbody>().AddExplosionForce(25f, transform.position, 30f, 10f, ForceMode.Impulse);
            }
        }

        if (other.CompareTag("House") && GetComponent<Rigidbody>().velocity.magnitude >= speed)
        {
            var house = GameManager.gameManager.destoryHouse.GetComponent<DestroyHouse>().houses;
            foreach (var elem in house)
            {
                if (elem.Value == other.gameObject)
                    playerControl.houseKey = elem.Key;
            }
            Debug.Log(playerControl.houseKey);
            other.GetComponentInChildren<FraggedChild>().Damage(speed);

        }

    }
}
