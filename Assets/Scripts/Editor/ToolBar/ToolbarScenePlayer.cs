using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityToolbarExtender;
using System.Linq;

[InitializeOnLoad]
public static class ToolbarScenePlayer
{
    private static int _selectedSceneIndex = 0;
    private static Dictionary<string, string> _sceneNameToPath;
    private const string FOLDER_SCENE = "Assets/_";
    private const string ORIGINAL_SCENE_PREF_KEY = "OriginalScenePathEditor";
    private const string SELECTED_SCENE_PREF_KEY = "SelectedSceneIndexEditor"; 
    private const string PLAY_ICON_PATH = "Assets/_/Scripts/Editor/Toolbar/Icon_ToolBar.png";
    
    private static Texture2D _playIcon;

    static ToolbarScenePlayer()
    {
        ToolbarExtender.LeftToolbarGUI.Add(OnToolbarGUI);
        RefreshSceneList();
        _playIcon = AssetDatabase.LoadAssetAtPath<Texture2D>(PLAY_ICON_PATH);
        EditorApplication.playModeStateChanged += OnPlayModeChanged;
    }

    private static void RefreshSceneList()
    {
        var sceneGuids = AssetDatabase.FindAssets("t:Scene", new[]{FOLDER_SCENE});
        _sceneNameToPath = sceneGuids
            .Select(AssetDatabase.GUIDToAssetPath)
            .ToDictionary(
                path => System.IO.Path.GetFileNameWithoutExtension(path),
                path => path
            );
    }
    
    private static void OnToolbarGUI()
    {
        GUILayout.FlexibleSpace();
        var sceneNames = _sceneNameToPath.Keys.ToArray();
        _selectedSceneIndex = EditorPrefs.GetInt(SELECTED_SCENE_PREF_KEY, 0);
        _selectedSceneIndex = EditorGUILayout.Popup(_selectedSceneIndex, sceneNames, GUILayout.Width(150));
        EditorPrefs.SetInt(SELECTED_SCENE_PREF_KEY, _selectedSceneIndex);
        GUIContent playContent = _playIcon != null ? new GUIContent("Play", _playIcon) : new GUIContent("Play");
        if (GUILayout.Button(playContent, GUILayout.Width(64), GUILayout.Height(24)))
        {
            PlaySelectedScene();
        }
    }

    private static void PlaySelectedScene()
    {
        var sceneNames = _sceneNameToPath.Keys.ToArray();
        if (_selectedSceneIndex >= 0 && _selectedSceneIndex < sceneNames.Length)
        {
            var selectedSceneName = sceneNames[_selectedSceneIndex];
            if (_sceneNameToPath.TryGetValue(selectedSceneName, out var selectedScenePath) && !string.IsNullOrEmpty(selectedScenePath))
            {
                EditorPrefs.SetString(ORIGINAL_SCENE_PREF_KEY, EditorSceneManager.GetActiveScene().path);
                EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
                EditorSceneManager.OpenScene(selectedScenePath);
                EditorApplication.isPlaying = true;
            }
        }
    }

    private static void OnPlayModeChanged(PlayModeStateChange state)
    {
        if (state == PlayModeStateChange.EnteredEditMode)
        {
            var originalScenePath = EditorPrefs.GetString(ORIGINAL_SCENE_PREF_KEY);
            if (!string.IsNullOrEmpty(originalScenePath))
            {
                EditorSceneManager.OpenScene(originalScenePath);
            }
        }
    }
}