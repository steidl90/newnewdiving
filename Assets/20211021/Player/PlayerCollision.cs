using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public GameObject sound;
    private readonly int HouseDestructionStage = 4;
    private void OnTriggerEnter(Collider other)
    {
        var playerControl = GameManager.gameManager.player.GetComponent<PlayerController>();
        playerControl.OnTrigger(other, transform.position);

        var speed = playerControl.IsReplay && StaticVariable.stage > 5 ? -1f : 29f;
        // 렉돌로 변하는 용도
        if (other.CompareTag("Plane") && /*GetComponent<Rigidbody>().velocity.magnitude >= speed / 5f*/
            StaticVariable.stage > 8)
        {
            for (int i = 0; i < other.transform.childCount; i++)
            {
                Destroy(other.transform.GetChild(i).gameObject);
            }
            var planeMove = other.gameObject.transform.parent.GetComponent<PlaneMove>();
            planeMove.isCollision = true;
            planeMove.speed = 0f;
            sound.GetComponent<DestroySound>().SoundPlay(transform.position);
            other.gameObject.transform.parent.GetComponentInChildren<FraggedChild>().Damage(speed);
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
        }
        if (other.CompareTag("House") && StaticVariable.stage > HouseDestructionStage)
        {
            sound.GetComponent<DestroySound>().SoundPlay(transform.position);
            var house = GameManager.gameManager.destoryHouse.GetComponent<DestroyHouse>().houses;
            foreach (var elem in house)
            {
                if (elem.Value == other.gameObject)
                    playerControl.houseKey = elem.Key;
            }
            other.GetComponentInChildren<FraggedChild>().Damage(speed);
        }
    }
}
