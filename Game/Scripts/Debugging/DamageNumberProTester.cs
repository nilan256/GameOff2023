using DamageNumbersPro;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Debugging
{

    public class DamageNumberProTester : MonoBehaviour
    {

        public DamageNumber Prefab;
        public Transform Target;
        public float Number;
        public string Text;

        [Button]
        public void ShowNumber()
        {
            Prefab.Spawn(Target.position, Number);
        }

        [Button]
        public void ShowText()
        {
            Prefab.Spawn(Target.position, Text);
        }

    }

}