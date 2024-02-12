using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalScene : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        SetLoadScene();
    }

    public void SetLoadScene()
    {
        SceneManager.LoadScene("GameOver");
    }
}
