using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;


/* 
데이터컨테이너에셋을 효율적으로 관리하기 위해 윈도우 메뉴를 만들었습니다.
CustomEdit와 오딘인스팩터를 통해 작성했습니다.
*/
public class CropToolbar : OdinMenuEditorWindow
{   
    // 경로를 지정하고 빈 윈도우를 생성합니다.
    [MenuItem("Tools/DataTable/Crop")]
    private static void OpenWindow(){
        GetWindow<CropToolbar>().Show();
    }

    /* 
    메뉴트리를 작성해주었습니다.
    메뉴트리는 새로운 데이터컨테이너에셋을 만드는 메뉴와 기존 데이터컨테이너를 수정 및 관리할수 있는 메뉴로 구성됩니다.
    */
    protected override OdinMenuTree BuildMenuTree()
    {
        var tree = new OdinMenuTree();

        tree.Add("Create New Crop", new CreateNewCropData());
        tree.AddAllAssetsAtPath("Crop", "Assets/DataAsset/CropData", typeof(CropData));

        return tree;
    }

    /*
    새로운 데이터에셋을 만드는 메뉴를 구성하는 스크립트입니다.
    CropData를 생성하고 경로와 저장되는 이름을 정해줍니다.
    생성이 완료되고나면 안에 데이터를 한번 리셋해줍니다.
    */
    public class CreateNewCropData : CropData{
        public CreateNewCropData(){
            cropData = ScriptableObject.CreateInstance<CropData>(); 
            
        }
        [InlineEditor(ObjectFieldMode = InlineEditorObjectFieldModes.Hidden)]
        public CropData cropData;

        [Button("Add New Crop")]
        private void CreateNewData(){
            AssetDatabase.CreateAsset(cropData, "Assets/DataAsset/CropData/" + cropData.name + ".asset");
            AssetDatabase.SaveAssets();

            cropData = ScriptableObject.CreateInstance<CropData>(); 
            
        }
    }
}
