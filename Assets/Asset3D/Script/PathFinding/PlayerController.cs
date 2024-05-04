using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10f;

    // Update is called once per frame
    void Update()
    {
        float xAsis = Input.GetAxis("Horizontal");
        float yAsis = Input.GetAxis("Vertical");
        transform.Translate(new Vector3(xAsis, 0, yAsis) * Time.deltaTime * speed);
    }
}
