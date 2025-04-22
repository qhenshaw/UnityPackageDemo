using System.Collections.Generic;
using UnityEngine;

namespace RPGEditor
{
    [CreateAssetMenu(menuName = "RPG Editor/New Class")]
    public class ClassData : ScriptableObject
    {
        [field: SerializeField]
        public string Name { get; private set; }
        [field: SerializeField, TextArea]
        public string Description { get; private set; }
    }
}