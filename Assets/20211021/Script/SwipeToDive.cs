using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwipeToDive : MonoBehaviour
{
    private Vector3 startPos;
    private Vector3 endPos;
    private float timer;
    private void Start()
    {
        startPos = transform.position;
        endPos = new Vector3(transform.position.x, transform.position.y + 700f, transform.position.z);

    }
    private void Update()
    {
        timer += Time.deltaTime;

        if (timer < 1.5f)
        { 
            transform.position = Vector2.Lerp(transform.position, endPos, Time.deltaTime * 5f);
            var color = GetComponent<Image>().color;
            color.a -= Time.deltaTime;
            GetComponent<Image>().color = color;
        }
        else
        {
            transform.position = startPos;
            timer = 0f;
            GetComponent<Image>().color = Color.white;
        }
    }
}
