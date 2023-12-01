using System;
using Sirenix.OdinInspector;

namespace Game
{

    [Serializable]
    public struct GameProperty<T>
    {

        [HorizontalGroup]
        public T Initial;
        
        [HorizontalGroup, ShowInInspector, HideInEditorMode]
        public T Value { get; set; }

        public void Initialize()
        {
            Value = Initial;
        }
        
    }

}