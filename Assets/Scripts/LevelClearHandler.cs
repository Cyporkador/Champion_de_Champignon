using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelClearHandler : MonoBehaviour
{
    public GameObject levelClearMenu;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            levelClearMenu.SetActive(true);
        }
    }
}
