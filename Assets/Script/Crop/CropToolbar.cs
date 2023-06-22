using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;

public class CropToolbar : OdinMenuEditorWindow
{
    [MenuItem("Tools/DataTable/Crop")]
    private static void OpenWindow(){
        GetWindow<CropToolbar>().Show();
    }
    protected override OdinMenuTree BuildMenuTree()
    {
        var tree = new OdinMenuTree();

        tree.Add("Create New Crop", new CreateNewCropData());
        tree.AddAllAssetsAtPath("Crop", "Assets/DataAsset/CropData", typeof(CropData));

        return tree;
    }

    public class CreateNewCropData{
        public CreateNewCropData(){
            cropData = ScriptableObject.CreateInstance<CropData>(); 
            
        }
        [InlineEditor(ObjectFieldMode = InlineEditorObjectFieldModes.Hidden)]
        public CropData cropData;

        [Button("Add New Crop")]
        private void CreateNewData(){
            AssetDatabase.CreateAsset(cropData, "Assets/DataAsset/CropData/" + cropData.Name + ".asset");
            AssetDatabase.SaveAssets();

            cropData = ScriptableObject.CreateInstance<CropData>(); 
            
        }
    }
}