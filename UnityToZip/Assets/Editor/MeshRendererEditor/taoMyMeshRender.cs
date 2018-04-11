using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MeshRenderer), true)]
public class taoMyMeshRender : DecoratorEditor
{

    private SerializedProperty m_SortingLayerID;
    private SerializedProperty m_SortingOrder;
    private SerializedProperty m_BortingOrder;
    public taoMyMeshRender()
        : base("MeshRendererEditor")
    {

    }


    public override void OnEnable()
    {
        base.OnEnable();
        this.m_SortingOrder = serializedObject.FindProperty("m_SortingOrder");
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        serializedObject.Update();
        EditorGUILayout.PropertyField(this.m_SortingOrder, true, new GUILayoutOption[0]);
        serializedObject.ApplyModifiedProperties();
    }
}
