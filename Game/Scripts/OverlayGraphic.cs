using System.Collections;
using DG.Tweening;
using Irisheep.Runtime.Singleton;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{

    public class OverlayGraphic : SingletonMonoBehaviour<OverlayGraphic>
    {

        public Image Image;
        private Coroutine coroutine;

        public void Flash(Color color, float fadeInTime, float fadeOutTime)
        {
            DOTween.Sequence()
                .Append(Image.DOColor(color, fadeInTime))
                .Append(Image.DOColor(Color.clear, fadeOutTime))
                .SetUpdate(true);
        }

    }

}