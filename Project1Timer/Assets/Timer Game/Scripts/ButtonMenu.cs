using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class ButtonMenu : MonoBehaviour
{
    public void LoadSceneByName(string MainGame)
    {
        SceneManager.LoadScene("MainGame");
    }
}
