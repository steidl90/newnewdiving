using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public GameObject sound;
    private void OnTriggerEnter(Collider other) // 렉돌로 변하는 용도
    {
        var playerControl = GameManager.gameManager.player.GetComponent<PlayerController>();
        playerControl.OnTrigger(other, transform.position);

        //var speed = 29f;

        var speed = playerControl.IsReplay && Vars.stage > 5 ? -1f : 29f;

        if (other.CompareTag("Plane") && GetComponent<Rigidbody>().velocity.magnitude >= speed / 5f)
        {
            sound.GetComponent<DestroySound>().SoundPlay(transform.position);
            other.gameObject.transform.parent.GetComponentInChildren<FraggedChild>().Damage(speed);
            other.gameObject.transform.parent.GetComponent<PlaneMove>().speed = 0f;
            var childs = other.gameObject.transform.parent.GetComponentsInChildren<FraggedChild>();
            foreach (var child in childs)
            {
                child.GetComponent<Rigidbody>().AddExplosionForce(10f, transform.position, 20f, 5f, ForceMode.Impulse);
            }

            var house = GameManager.gameManager.destoryHouse.GetComponent<DestroyHouse>().houses;
            foreach (var elem in house)
            {
                if (elem.Value == other.transform.parent.gameObject)
                    playerControl.houseKey = elem.Key;
            }
            Debug.Log(playerControl.houseKey);
        }

        if (other.CompareTag("House") && GetComponent<Rigidbody>().velocity.magnitude >= speed)
        {
            sound.GetComponent<DestroySound>().SoundPlay(transform.position);
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
