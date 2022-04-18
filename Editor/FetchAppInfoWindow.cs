﻿using UnityEditor;
using UnityEngine;

using TalusBackendData.Editor.Models;

namespace TalusBackendData.Editor
{
    public class FetchAppInfoWindow : EditorWindow
    {
        private string _AppId;

        private string _FetchButton = "Fetch";

        [MenuItem("TalusKit/Backend/Fetch App Info")]
        private static void Init()
        {
            FetchAppInfoWindow window = (FetchAppInfoWindow) GetWindow(typeof(FetchAppInfoWindow));
            window.titleContent = new GUIContent("Fetch App Info");
            window.Show();
        }

        private void OnGUI()
        {
            GUILayout.BeginVertical();

            GUILayout.Label("API Settings", EditorStyles.boldLabel);

            GUILayout.Space(4);
            GUILayout.Label("App Settings", EditorStyles.boldLabel);
            _AppId = EditorGUILayout.TextField("App ID:", _AppId);

            if (GUILayout.Button(_FetchButton))
            {
                new FetchAppInfo(BackendSettings.ApiUrl, BackendSettings.ApiToken, _AppId).GetInfo(UpdateBackendData);
            }

            GUILayout.EndVertical();
        }

        private void UpdateBackendData(AppModel app)
        {
            PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.iOS, app.app_bundle);
            PlayerSettings.productName = app.app_name;

            Debug.Log("Updating product name and bundle id...");
        }
    }
}