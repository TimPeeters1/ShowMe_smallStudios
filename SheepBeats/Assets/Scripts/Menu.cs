using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    int Main;

    public void LoadMainLevel()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(Main);
    }

        
}
