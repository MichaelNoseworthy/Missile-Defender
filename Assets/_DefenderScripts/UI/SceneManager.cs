using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour {
    
    public void LoadGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
    }
    /*
    public void LoadSetup()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("SetupMenu");
    }
    public void LoadMainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
    public void LoadFightTest()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("FightTest");
    }

    public void LoadCatapult()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Catapulter");
    }
    public void LoadGetClosestTarget()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("GetClosestTarget");
    }
    */
    public void ClickExit()
    {
        Debug.Log("Game is exiting");
        Application.Quit();
    }
}
