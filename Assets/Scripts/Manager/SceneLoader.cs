using RPG.Data.UI;
using RPG.Scriptable.Base.Event.Boolean;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public EventBool LoadingScreen;

    List<AsyncOperation> Scenes = new List<AsyncOperation>();
    void Start()
    {
        Scenes.Add(SceneManager.LoadSceneAsync("Envi-2", LoadSceneMode.Additive));
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene() {
        LoadingScreen.RaiseEvent(true);
        yield return new WaitUntil(() =>
        {
            foreach (AsyncOperation scene in Scenes) {
                while (!scene.isDone) return false;
            }
            return true;
        });
        yield return new WaitForSecondsRealtime(2.5f);
        LoadingScreen.RaiseEvent(false);
    }
}
