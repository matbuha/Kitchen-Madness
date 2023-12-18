using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using UnityEditor.SceneManagement;

public class FindMissingScripts : EditorWindow
{
    [MenuItem("Tools/Find Missing Scripts")]
    public static void ShowWindow()
    {
        GetWindow(typeof(FindMissingScripts));
    }

    public void OnGUI()
    {
        if (GUILayout.Button("Find Missing Scripts in Prefabs and Scenes"))
        {
            FindInPrefabs();
            FindInScenes();
        }
    }

    private static void FindInPrefabs()
    {
        string[] allPrefabs = AssetDatabase.GetAllAssetPaths();
        foreach (string prefabPath in allPrefabs)
        {
            if (prefabPath.Contains(".prefab"))
            {
                GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);
                FindInGameObject(prefab, prefabPath);
            }
        }
    }

    private static void FindInScenes()
    {
        foreach (EditorBuildSettingsScene scene in EditorBuildSettings.scenes)
        {
            if (scene.enabled)
            {
                EditorSceneManager.OpenScene(scene.path);
                GameObject[] allGameObjects = Object.FindObjectsOfType<GameObject>();
                foreach (GameObject go in allGameObjects)
                {
                    FindInGameObject(go, scene.path);
                }
            }
        }
    }

    private static void FindInGameObject(GameObject go, string assetPath)
    {
        Component[] components = go.GetComponents<Component>();
        foreach (Component component in components)
        {
            if (component == null)
            {
                Debug.LogError("Missing script in GameObject: " + go.name + ", Asset Path: " + assetPath);
            }
        }
    }
}
