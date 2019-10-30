using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    int Main = 1;

    public void LoadMainLevel()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(Main);
    }

        
}
