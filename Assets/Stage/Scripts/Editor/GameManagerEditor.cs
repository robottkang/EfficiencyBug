using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GameManager))]
public class GameManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GameManager gameManager = (GameManager)target;
        EditorGUILayout.Space();

        gameManager.field.fieldObject = (GameObject)EditorGUILayout.ObjectField("Game Field Object", gameManager.field.fieldObject, typeof(GameObject), true);

        EditorGUI.indentLevel++;

        if (gameManager.field.fieldObject != null)
        {

            EditorGUI.indentLevel = 0;

            GUIStyle tableStyle = new GUIStyle("box");
            tableStyle.padding = new RectOffset(10, 10, 10, 10);
            tableStyle.margin.left = 32;

            GUIStyle headerColumnStyle = new GUIStyle();
            headerColumnStyle.fixedWidth = 3;

            GUIStyle columnStyle = new GUIStyle();
            columnStyle.fixedWidth = 37;

            GUIStyle rowStyle = new GUIStyle();
            rowStyle.fixedHeight = 25;
            rowStyle.fixedWidth = 65;

            GUIStyle rowHeaderStyle = new GUIStyle();
            rowHeaderStyle.fixedWidth = columnStyle.fixedWidth - 1;

            GUIStyle columnHeaderStyle = new GUIStyle();
            columnHeaderStyle.fixedWidth = 30;
            columnHeaderStyle.fixedHeight = 25.5f;

            GUIStyle columnLabelStyle = new GUIStyle();
            columnLabelStyle.fixedWidth = rowHeaderStyle.fixedWidth - 6;
            columnLabelStyle.alignment = TextAnchor.MiddleCenter;
            columnLabelStyle.fontStyle = FontStyle.Bold;

            GUIStyle cornerLabelStyle = new GUIStyle();
            cornerLabelStyle.fixedWidth = 42;
            cornerLabelStyle.alignment = TextAnchor.MiddleRight;
            cornerLabelStyle.fontStyle = FontStyle.BoldAndItalic;
            cornerLabelStyle.fontSize = 14;
            cornerLabelStyle.padding.top = -5;

            GUIStyle rowLabelStyle = new GUIStyle();
            rowLabelStyle.fixedWidth = 25;
            rowLabelStyle.alignment = TextAnchor.MiddleRight;
            rowLabelStyle.fontStyle = FontStyle.Bold;

            GUIStyle intStyle = new GUIStyle("textField");
            intStyle.fixedWidth = 30f;
            intStyle.alignment = TextAnchor.MiddleCenter;

            int spaceCount = 0;

            EditorGUILayout.BeginVertical(tableStyle);
            for (int y = -1; y < FieldBorad.columns; y++)
            {
                EditorGUILayout.BeginHorizontal((y == -1) ? headerColumnStyle : columnStyle);
                for (int x = -1; x < FieldBorad.rows; x++)
                {
                    if (y == -1 && x == -1)
                    {
                        EditorGUILayout.BeginHorizontal(rowHeaderStyle);
                        EditorGUILayout.LabelField("[Y,X]", cornerLabelStyle);
                        EditorGUILayout.EndVertical();
                    }
                    else if (y == -1)
                    {
                        EditorGUILayout.BeginHorizontal(columnHeaderStyle);
                        EditorGUILayout.LabelField(x.ToString(), rowLabelStyle);
                        EditorGUILayout.EndVertical();
                    }
                    else if (x == -1)
                    {
                        EditorGUILayout.BeginHorizontal(rowHeaderStyle);
                        EditorGUILayout.LabelField(y.ToString(), columnLabelStyle);
                        EditorGUILayout.EndVertical();
                    }

                    if (y >= 0 && x >= 0)
                    {
                        if (spaceCount < gameManager.field.fieldObject.transform.childCount) // row 행
                        {
                            gameManager.field.borad[x, y] = gameManager.field.fieldObject.transform.GetChild(spaceCount).gameObject;
                            GameFieldSpace space = gameManager.field.borad[x, y].GetComponent<GameFieldSpace>();
                            EditorGUILayout.BeginVertical(columnStyle);
                            space.value = EditorGUILayout.IntField(space.value, intStyle, GUILayout.Width(intStyle.fixedWidth));
                            EditorGUILayout.EndVertical();
                            spaceCount++;
                        }
                        else
                        {
                            EditorGUILayout.BeginVertical(columnStyle);
                            EditorGUILayout.IntField(0, intStyle, GUILayout.Width(intStyle.fixedWidth));
                            EditorGUILayout.EndVertical();
                        }
                    }
                }
                EditorGUILayout.EndHorizontal();
            }
            EditorGUILayout.EndVertical();

        }

        if (GUILayout.Button("Skip Stage"))
        {
            gameManager.stageNum++;
            gameManager.RearrangeBorad(gameManager.stageNum / 4 + 3);
        }
    }
}