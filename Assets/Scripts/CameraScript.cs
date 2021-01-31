using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{

    public Transform playerPos;

    // Update is called once per frame
    void Update()
    {
        float x = gameObject.transform.position.x + (playerPos.position.x - gameObject.transform.position.x) * 0.04f;
        float y = gameObject.transform.position.y + (playerPos.position.y - gameObject.transform.position.y) * 0.04f;

        gameObject.transform.position = new Vector3(x, y, gameObject.transform.position.z);
    }
}
