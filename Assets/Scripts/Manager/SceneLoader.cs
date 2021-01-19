using RPG.Data.UI;
using RPG.Scriptable.Base.Event;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{

    List<AsyncOperation> Scenes = new List<AsyncOperation>();
    void Start()
    {
        Scenes.Add(SceneManager.LoadSceneAsync("Envi-1", LoadSceneMode.Additive));
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene() {
        UIData.instance.Resources.LoadingScreen.SetActive(true);
        yield return new WaitUntil(() =>
        {
            foreach (AsyncOperation scene in Scenes) {
                while (!scene.isDone) return false;
            }
            return true;
        });
        yield return new WaitForSecondsRealtime(2.5f);
        UIData.instance.Resources.LoadingScreen.SetActive(false);
    }
}
