using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

#if true
[CustomEditor(typeof(Asteroids.AsteroidSpawner))]
public class AsteroidEditor : Editor
{

    public VisualTreeAsset m_UXML;
    public override VisualElement CreateInspectorGUI()
    {
        var root = new VisualElement();
        m_UXML.CloneTree(root);


        var foldout = new Foldout() { viewDataKey = "AsteroidSpawnerFoldOut", text = "Asteroid Spawning Data" };
        InspectorElement.FillDefaultInspector(foldout, serializedObject, this);
        root.Add(foldout);
        return root;
    }

}
#endif