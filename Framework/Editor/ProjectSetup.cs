using static System.IO.Directory;
using static System.IO.Path;

using UnityEditor;


namespace MyFramework.Editor
{
    // 项目初始化
    public static class ProjectSetup
    {
        // 创建默认项目目录
        [MenuItem("Tools/Setup/Refresh")]
        public static void RefreshAssets()
        {
            AssetDatabase.Refresh();
        }

        [MenuItem("Tools/Setup/CreateFolders")]
        public static void CreateDefaultFolders()
        {
            // 资产
            Folders.CreateDefault("StreamingAssets", "Animation", "Art", "Materials", "Prefabs", "ScriptableObjects");
            // 外部资产
            Folders.CreateDefault("ExternalAssetPackages");
            // 设置
            Folders.CreateDefault("Settings");
            // 代码文件
            Folders.CreateDefault("Scripts", "Framework", "Main");

            AssetDatabase.Refresh();
        }

        #region Folders
        static class Folders
        {
            public static void CreateDefault(string root, params string[] folders)
            {
                // 获取创建文件夹根目录的完整路径
                var fullpath = Combine(UnityEngine.Application.dataPath, root);

                // 创建文件夹
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





