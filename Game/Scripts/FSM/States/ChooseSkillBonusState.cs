using System.Collections;
using Game.UI;
using UnityEngine;

namespace Game.FSM.States
{

    public class ChooseSkillBonusState : CoroutineState
    {

        public string ViewName;
        public string ChooseCallbackName;

        protected override IEnumerator Run()
        {
            var request = new UIViewRequest(ViewName, ChooseCallbackName);
            yield return request.StartCoroutine();
            var skillId = request.ResponseString;

            var skill = AssetManager.Current.GetSkill(skillId);
            if (!skill)
            {
                Debug.LogError($"Skill not found, id: {skillId}");
                yield break;
            }

            Player.AddSkill(skill);

            if (Gameplay.PendingSkillBonus > 0) Gameplay.PendingSkillBonus -= 1;
        }

    }

}