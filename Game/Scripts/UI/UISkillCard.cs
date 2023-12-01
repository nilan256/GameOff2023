using Doozy.Runtime.UIManager.Components;
using Game.SkillSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    
    public class UISkillCard : MonoBehaviour
    {

        public UIToggle Toggle;
        public TextMeshProUGUI TextName;
        public TextMeshProUGUI TextDescription;
        public Image ImageIcon;
        
        public SkillData Skill { get; private set; }

        public void Initialize(SkillData skill)
        {
            Skill = skill;
            name = $"Skill Card - {skill.Id}";
            
            TextName.text = skill.DisplayName;
            TextDescription.text = skill.DisplayDescription;
            ImageIcon.sprite = skill.Icon;
        }

        public void ResetDefault()
        {
            Skill = null;
            name = "Skill Card";
            
            TextName.text = string.Empty;
            TextDescription.text = string.Empty;
            ImageIcon.sprite = null;
        }

    }

}