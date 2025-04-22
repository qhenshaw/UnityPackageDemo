using UnityEngine;
using Sirenix.OdinInspector;
using System.Collections.Generic;

namespace RPGEditor
{
    [CreateAssetMenu(menuName = "RPG Editor/New Character")]
    public class CharacterData : ScriptableObject
    {
        [field: SerializeField, PreviewField(Height = 100)]
        public Sprite Portrait { get; private set; }

        [field: SerializeField, InlineEditor]
        public ClassData Class { get; private set; }

        [field: SerializeField]
        public WeaponData Weapon { get; private set; }

        [field: SerializeField]
        public ArmorData Armor { get; private set; }

        [field: SerializeField]
        public List<ItemData> Inventory { get; private set; }
    }
}