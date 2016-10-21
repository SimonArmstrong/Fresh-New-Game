using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class MenuEventHandler : MonoBehaviour{

    AsyncOperation ao;
    public GameObject loadingScreenBG;
    public Slider progBar;
    public Text loadText;
    public Text element_names;

    public bool isFake = false;
    public float fakeIncrement = 0f;
    public float fakeTiming = 0f;

    void Start()
    {
        
    }

    public void selectorP1LeftUpdate()
    {
        element_names.text = "Water";
    }


    public void playClick()
    {
        loadingScreenBG.SetActive(true);
        progBar.gameObject.SetActive(true);
        loadText.gameObject.SetActive(true);
        loadText.text = "Awesomeness Awaits..."; //we can make the loading text tell the player some tips

        if (!isFake)
        {
            StartCoroutine(LoadSceneWithRealProgress());
        }
        else
        {

        }

    }

    IEnumerator LoadSceneWithRealProgress()
    {
        yield return new WaitForSeconds(1);

        ao = SceneManager.LoadSceneAsync(1);
        ao.allowSceneActivation = false;

        while (!ao.isDone)
        {
            progBar.value = ao.progress;

            if (ao.progress == 0.9f)
            {
                loadText.text = "Press 'X' to Continue";
                if (Input.GetKeyDown(KeyCode.X))
                {
                    ao.allowSceneActivation = true;
                }
            }
            Debug.Log(ao.progress);
            yield return null;
        }
    }

}

