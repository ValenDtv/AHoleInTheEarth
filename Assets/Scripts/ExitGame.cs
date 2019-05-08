using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitGame : MonoBehaviour
{
    public void Quit()
    {
        Application.Quit();
        // убивает процесс Юнити
        //System.Diagnostics.Process.GetCurrentProcess().Kill();

    }
}
