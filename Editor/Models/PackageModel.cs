namespace TalusBackendData.Editor.Models
{
    [System.Serializable]
    public class PackageModel
    {
        public string package_id;
        public string url;
        public string hash;

        public override string ToString()
        {
            return UnityEngine.JsonUtility.ToJson(this, true);
        }
    }
}