using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;

public class MonsterToolbar : OdinMenuEditorWindow
{
    [MenuItem("Tools/DataTable/Monster")]
    private static void OpenWindow(){
        GetWindow<MonsterToolbar>().Show();
    }
    protected override OdinMenuTree BuildMenuTree()
    {
        var tree = new OdinMenuTree();

        tree.Add("Create New Monster", new CreateNewMonsterData());
        tree.AddAllAssetsAtPath("Monster", "Assets/DataAsset/MonsterData", typeof(MonsterData));

        return tree;
    }

    public class CreateNewMonsterData{
        public CreateNewMonsterData(){
            monsterData = ScriptableObject.CreateInstance<MonsterData>(); 
        }
        [InlineEditor(ObjectFieldMode = InlineEditorObjectFieldModes.Hidden)]
        public MonsterData monsterData;

        [Button("Add New Monster")]
        private void CreateNewData(){
            AssetDatabase.CreateAsset(monsterData, "Assets/DataAsset/MonsterData/" + monsterData.Name + ".asset");
            AssetDatabase.SaveAssets();

            monsterData = ScriptableObject.CreateInstance<MonsterData>(); 
        }
    }
}