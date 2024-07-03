using static System.IO.Directory;
using static System.IO.Path;

using UnityEditor;


namespace MyFramework.Editor
{
    // ��Ŀ��ʼ��
    public static class ProjectSetup
    {
        // ����Ĭ����ĿĿ¼
        [MenuItem("Tools/Setup/Refresh")]
        public static void RefreshAssets()
        {
            AssetDatabase.Refresh();
        }

        [MenuItem("Tools/Setup/CreateFolders")]
        public static void CreateDefaultFolders()
        {
            // �ʲ�
            Folders.CreateDefault("StreamingAssets", "Animation", "Art", "Materials", "Prefabs", "ScriptableObjects");
            // �ⲿ�ʲ�
            Folders.CreateDefault("ExternalAssetPackages");
            // ����
            Folders.CreateDefault("Settings");
            // �����ļ�
            Folders.CreateDefault("Scripts", "Framework", "Main");

            AssetDatabase.Refresh();
        }

        #region Folders
        static class Folders
        {
            public static void CreateDefault(string root, params string[] folders)
            {
                // ��ȡ�����ļ��и�Ŀ¼������·��
                var fullpath = Combine(UnityEngine.Application.dataPath, root);

                // �����ļ���
                foreach (var folder in folders)
                {
                    var path = Combine(fullpath, folder);
                    if( !Exists(path) )
                    {
                        CreateDirectory(path);
                    }
                }
            }

        }
        #endregion

    }

}





