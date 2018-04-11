using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;
using UnityEditor;
using LitJson;

/// <summary>
/// 生成配置数据
/// </summary>
public class BuildConf : Editor
{
    [MenuItem("资源序列化/生成--全部资源")]
    public static void OnCreateAllAsset()
    {
        // 生成Json序列化资源
        OnSerializeConfAsset();
        // 生成图集序列化资源
        OnSerializeUIAtlasAsset();
    }

    [MenuItem("资源序列化/生成--配置资源")]
    public static void OnSerializeConfAsset()
    {
        CreateJsonConfAsset();
    }

    /// <summary>
    /// 生成Json配置序列号资源
    /// </summary>
    private static void CreateJsonConfAsset()
    {
        // json文件夹
        string jsonPath = "Assets/Resassets/Data/JsonConf";
        // asset文件夹
        string assetPath = "Assets/Resassets/Data/AssetConf";
        
        // 填充配置对应脚本  PS:如果加了新的配置表，就需要新加一个配置类。加好配置类后，生成时这里需要添加以下
        Dictionary<string, ConfAssetVo> scriptNameDict = new Dictionary<string, ConfAssetVo>();
        scriptNameDict.Add(typeof(AchieveVo).Name, new AchieveVo());
        // add.........

        // 获取所有的配置json文件
        DirectoryInfo directInfo = new DirectoryInfo(jsonPath);
        // 得到当前目录下所有文件
        foreach (FileInfo file in directInfo.GetFiles())
        {
            if (file.Extension == ".json")
            {
                string replPath = file.Name;
                string confName = replPath.Substring(0, replPath.LastIndexOf("."));
                string scriptName = confName + "Vo";

                if (scriptNameDict.ContainsKey(scriptName))
                {
                    string repPath = file.FullName.Replace("//", "/").Substring(file.FullName.IndexOf("Assets"));
                    TextAsset confContent = AssetDatabase.LoadAssetAtPath<TextAsset>(repPath);
                    JsonData jsonData = JsonMapper.ToObject(confContent.text);

                    ConfAssetVo confType = scriptNameDict[scriptName];
                    confType.OnConfDataFill(jsonData);
                }
                else
                {
                    Debug.LogError(replPath + "   没有创建对应配置脚本");
                }
            }
        }

        // 生成Asset
        foreach (KeyValuePair<string, ConfAssetVo> conf in scriptNameDict)
        {
            string filePath = assetPath + "/" + conf.Key + ".asset";
            AssetDatabase.DeleteAsset(filePath);
            AssetDatabase.CreateAsset(conf.Value, filePath);
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    [MenuItem("资源序列化/生成--UI图集资源")]
    public static void OnSerializeUIAtlasAsset()
    {
        // 图集文件夹
        string atlasPath = "Assets/Resassets/Data/UIAtlas";

        CreateUIAtlasAsset(atlasPath);
    }

    /// <summary>
    /// 生成UI图集序列化资源
    /// </summary>
    private static void CreateUIAtlasAsset(string path_)
    {
        // asset文件夹
        string assetPath = "Assets/Resassets/Data/AssetUIAtlas";

        DirectoryInfo directInfo = new DirectoryInfo(path_);
        FileSystemInfo[] infos = directInfo.GetFileSystemInfos();

        List<Sprite> spriteList = new List<Sprite>();

        for (int i = 0; i < infos.Length; ++i)
        {
            FileSystemInfo info = infos[i];

            if (Directory.Exists(info.FullName))
            {
                CreateUIAtlasAsset(info.FullName);
            }
            else
            {
                if (info.Extension == ".png" || info.Extension == ".jpg")
                {
                    string repPath = info.FullName.Replace("//", "/").Substring(info.FullName.IndexOf("Assets"));
                    Sprite sprite = AssetDatabase.LoadAssetAtPath<Sprite>(repPath);
                    spriteList.Add(sprite);
                }
            }
        }

        if (spriteList.Count > 0)
        {
            AtlasVo atlasVo = new AtlasVo();
            atlasVo.OnConfDataFill(spriteList);

            string filePath = assetPath + "/" + directInfo.Name + ".asset";
            AssetDatabase.DeleteAsset(filePath);
            AssetDatabase.CreateAsset(atlasVo, filePath);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}
