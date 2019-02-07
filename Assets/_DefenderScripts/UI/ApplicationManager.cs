using UnityEngine;
using System.Collections;

public class ApplicationManager : MonoBehaviour {




    public void LoadFightTest()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("FightTest");
    }

    public void LoadCharacterSelect()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("DragAndDrop");
    }

    public void LoadFight()
    {

        /*
        GameObject.FindWithTag("Image1").GetComponent<UnityEngine.UI.Text>().text;

        GameObject.FindWithTag("Image2").GetComponent<UnityEngine.UI.Text>().text;

        GameObject.FindWithTag("Image3").GetComponent<UnityEngine.UI.Text>().text;

        GameObject.FindWithTag("Image4").GetComponent<UnityEngine.UI.Text>().text;

        GameObject.FindWithTag("Image5").GetComponent<UnityEngine.UI.Text>().text;

        GameObject.FindWithTag("Image6").GetComponent<UnityEngine.UI.Text>().text;
        */
        UnityEngine.SceneManagement.SceneManager.LoadScene("BattleScreen2");
    }

    public void LoadMainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu 3D");
    }

    public void Quit () 
	{
		#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
		#else
		Application.Quit();
		#endif
	}
}
