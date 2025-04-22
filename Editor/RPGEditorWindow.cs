using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.IO;

namespace RPGEditor.Editor
{
    public class RPGEditorWindow : OdinMenuEditorWindow
    {
        [MenuItem("Tools/RPG Editor Window")]
        private static void OpenEditor()
        {
            GetWindow<RPGEditorWindow>();
        }

        protected override OdinMenuTree BuildMenuTree()
        {
            var tree = new OdinMenuTree();

            List<Type> includedTypes = new List<Type>();
            includedTypes.Add(typeof(CharacterData));
            includedTypes.Add(typeof(ClassData));
            includedTypes.Add(typeof(SkillData));
            includedTypes.Add(typeof(WeaponData));
            includedTypes.Add(typeof(ArmorData));

            foreach (Type type in includedTypes)
            {
                tree.AddAllAssetsAtPath(type.Name, "Assets/", type, true, false);
                tree.Add("New " + type.Name, new CreateNewAsset(type));
            }

            return tree;
        }

        protected override void OnBeginDrawEditors()
        {
            base.OnBeginDrawEditors();

            MenuTree.DrawSearchToolbar();
        }

        private class CreateNewAsset
        {
            public string Name = "New Data";
            private Type _type;
            private ScriptableObject _data;

            public CreateNewAsset(Type type)
            {
                _type = type;
                _data = ScriptableObject.CreateInstance(_type);
            }

            [Button("Create New")]
            private void CreateNew()
            {
                string path = GetProjectWindowPath();
                if(!Directory.Exists(path)) Directory.CreateDirectory(path);
                AssetDatabase.CreateAsset(_data, path + Name + ".asset");
                AssetDatabase.SaveAssets();
            }

            private string GetProjectWindowPath()
            {
                Type projectWindowUtilType = typeof(ProjectWindowUtil);
                MethodInfo getActiveFolderPath = projectWindowUtilType.GetMethod("GetActiveFolderPath", BindingFlags.Static | BindingFlags.NonPublic);
                object obj = getActiveFolderPath.Invoke(null, new object[0]);
                string pathToCurrentFolder = obj.ToString() + "/";
                return pathToCurrentFolder;
            }
        }
    }
}
