using System;
using System.Collections;

using UnityEngine;
using UnityEngine.Networking;

using Unity.EditorCoroutines.Editor;

using TalusBackendData.Editor.Models;

namespace TalusBackendData.Editor
{
    public class BackendApi
    {
        private readonly string _ApiUrl;
        private readonly string _ApiToken;

        public BackendApi(string apiUrl, string apiToken)
        {
            _ApiUrl = apiUrl;
            _ApiToken = apiToken;
        }

        public void GetAppInfo(string appId, Action<AppModel> onFetchComplete)
        {
            string apiUrl = $"{_ApiUrl}/api/apps/get-app/{appId}";

            EditorCoroutineUtility.StartCoroutineOwnerless(GetApiResponse(apiUrl, onFetchComplete));
        }

        public void GetPackageInfo(string packageId, Action<PackageModel> onFetchComplete)
        {
            string apiUrl = $"{_ApiUrl}/api/packages/get-package/{packageId}";

            EditorCoroutineUtility.StartCoroutineOwnerless(GetApiResponse(apiUrl, onFetchComplete));
        }

        public void GetAllPackages(Action<PackagesModel> onFetchComplete)
        {
            string apiUrl = $"{_ApiUrl}/api/packages/get-packages";

            EditorCoroutineUtility.StartCoroutineOwnerless(GetApiResponse(apiUrl, onFetchComplete));
        }

        private IEnumerator GetApiResponse<T>(string url, Action<T> onFetchComplete)
        {
            using UnityWebRequest www = UnityWebRequest.Get(url);
            www.SetRequestHeader("api-key", _ApiToken);

            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                string logMessage = (www.responseCode == 503) ? "Talus Web Server is under maintenance!" : www.error;
                Debug.LogError($"[TalusBackendData-Package] {logMessage}");
            }
            else
            {
                var model = JsonUtility.FromJson<T>(www.downloadHandler.text);

                yield return null;

                if (Application.isBatchMode)
                {
                    Debug.Log($"[TalusBackendData-Package] Fetched model: {model}");
                }

                onFetchComplete(model);
            }
        }
    }
}
