using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

[InitializeOnLoad]
public class AutoSave
{

    static AutoSave()
    {

        EditorApplication.playmodeStateChanged = () =>
        {

            if (EditorApplication.isPlayingOrWillChangePlaymode && !EditorApplication.isPlaying)
            {
                EditorSceneManager.SaveScene(SceneManager.GetActiveScene());
                EditorApplication.SaveAssets();
            }

        };

    }

}