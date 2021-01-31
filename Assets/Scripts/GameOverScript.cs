using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{
    public EnemyInteractions ei;

    public void restart()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void menu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
