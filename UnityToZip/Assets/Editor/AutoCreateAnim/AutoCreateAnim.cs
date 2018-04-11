using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/// <summary>
/// 自动创建动画
/// </summary>
public class AutoCreateAnim : Editor
{
    [MenuItem("生成动画/生成动画")]
    public static void OnCreateAnim()
    {
        OnCreateAnimClip();
    }

    /// <summary>
    /// 创建动画Clip
    /// </summary>
    private static void OnCreateAnimClip()
    {
        string animName = "clip_0";
        // 动画片段
        AnimationClip clip = new AnimationClip();
        clip.frameRate = 30f;

        // 动画曲线参数
        //EditorCurveBinding curveBinding = new EditorCurveBinding();

        // 动画帧
        ObjectReferenceKeyframe[] keyframe = new ObjectReferenceKeyframe[10];

        // 添加、修改或删除给定剪辑中的对象引用曲线
        //AnimationUtility.SetObjectReferenceCurve(clip, curveBinding, keyframe);

        // 将动画Clip保存的工程中
        System.IO.Directory.CreateDirectory("Assets/Resources/Animation/testClip");
        AssetDatabase.CreateAsset(clip, "Assets/Resources/Animation/testClip/" + animName + ".anim");
        AssetDatabase.SaveAssets();
    }

    /// <summary>
    /// 创建动画Controller
    /// </summary>
    private void OnCreateAnimContr()
    {

    }

    //static AnimationClip BuildAnimationClip(DirectoryInfo dictorys)
    //{
    //    string animationName = dictorys.Name;
    //    //查找所有图片，因为我找的测试动画是.jpg 
    //    FileInfo[] images = dictorys.GetFiles("*.jpg");
    //    AnimationClip clip = new AnimationClip();
    //    AnimationUtility.SetAnimationType(clip, ModelImporterAnimationType.Generic);
    //    EditorCurveBinding curveBinding = new EditorCurveBinding();
    //    curveBinding.type = typeof(SpriteRenderer);
    //    curveBinding.path = "";
    //    curveBinding.propertyName = "m_Sprite";
    //    ObjectReferenceKeyframe[] keyFrames = new ObjectReferenceKeyframe[images.Length];
    //    //动画长度是按秒为单位，1/10就表示1秒切10张图片，根据项目的情况可以自己调节
    //    float frameTime = 1 / 10f;
    //    for (int i = 0; i < images.Length; i++)
    //    {
    //        Sprite sprite = Resources.LoadAssetAtPath<Sprite>(DataPathToAssetPath(images[i].FullName));
    //        keyFrames[i] = new ObjectReferenceKeyframe();
    //        keyFrames[i].time = frameTime * i;
    //        keyFrames[i].value = sprite;
    //    }
    //    //动画帧率，30比较合适
    //    clip.frameRate = 30;

    //    //有些动画我希望天生它就动画循环
    //    if (animationName.IndexOf("idle") >= 0)
    //    {
    //        //设置idle文件为循环动画
    //        SerializedObject serializedClip = new SerializedObject(clip);
    //        AnimationClipSettings clipSettings = new AnimationClipSettings(serializedClip.FindProperty("m_AnimationClipSettings"));
    //        clipSettings.loopTime = true;
    //        serializedClip.ApplyModifiedProperties();
    //    }
    //    string parentName = System.IO.Directory.GetParent(dictorys.FullName).Name;
    //    System.IO.Directory.CreateDirectory(AnimationPath + "/" + parentName);
    //    AnimationUtility.SetObjectReferenceCurve(clip, curveBinding, keyFrames);
    //    AssetDatabase.CreateAsset(clip, AnimationPath + "/" + parentName + "/" + animationName + ".anim");
    //    AssetDatabase.SaveAssets();
    //    return clip;
    //}
}
