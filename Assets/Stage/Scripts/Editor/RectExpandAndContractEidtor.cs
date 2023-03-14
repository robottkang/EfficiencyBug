using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(RectExpandAndContract), true)]
public class RectExpandAndContractEidtor : Editor
{
    public override void OnInspectorGUI()
    {
        EditorGUILayout.LabelField("-RectExpansion Setting", EditorStyles.boldLabel);

        RectExpandAndContract rectExpandAndContract = (RectExpandAndContract)target;

        rectExpandAndContract.xExpansion = EditorGUILayout.Toggle("X Expansion", rectExpandAndContract.xExpansion);

        if (rectExpandAndContract.xExpansion)
        {
            EditorGUI.indentLevel++;
            rectExpandAndContract.xExpansionScale = EditorGUILayout.FloatField("X Expansion", rectExpandAndContract.xExpansionScale);
            EditorGUI.indentLevel--;
        }

        rectExpandAndContract.yExpansion = EditorGUILayout.Toggle("Y Expansion", rectExpandAndContract.yExpansion);

        if (rectExpandAndContract.yExpansion)
        {
            EditorGUI.indentLevel++;
            rectExpandAndContract.yExpansionScale = EditorGUILayout.FloatField("Y Expansion", rectExpandAndContract.yExpansionScale);
            EditorGUI.indentLevel--;
        }

        EditorGUILayout.Space();

        rectExpandAndContract.expansionSpeed = EditorGUILayout.FloatField("Expansion Speed", rectExpandAndContract.expansionSpeed);

        base.OnInspectorGUI();
    }
}
