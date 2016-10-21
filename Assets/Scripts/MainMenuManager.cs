using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenuManager : MonoBehaviour {

	public void PlayClick(){
		SceneManager.LoadScene (1);
	}
    public void e_click()
    {
        SceneManager.LoadScene(2);
    }
    public void BackClick()
    {
        SceneManager.LoadScene(0);
    }
}
