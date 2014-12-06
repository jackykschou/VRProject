using Assets.Scripts.Attributes;
using GameScriptEvent = Assets.Scripts.Constants.GameScriptEvent;
using GameScriptEventAttribute = Assets.Scripts.Attributes.GameScriptEvent;
using GameEvent = Assets.Scripts.Constants.GameEvent;

namespace Assets.Scripts.GameScripts.GameLogic.LevelMechanics.Section
{
    public abstract class SectionLogic : GameLogic 
    {
        public int SectionId;
        public bool SectionActivated = false;

        protected override void Initialize()
        {
            base.Initialize();
            SectionActivated = false;
        }

        [GameScriptEventAttribute(GameScriptEvent.UpdateSectionId)]
        public void UpdateSectionId(int id)
        {
            SectionId = id;
        }

        [GameEvent(GameEvent.OnSectionActivated)]
        public virtual void OnSectionActivated(int sectionId)
        {
            if (sectionId == SectionId)
            {
                SectionActivated = true;
            }
        }

        [GameEvent(GameEvent.OnSectionDeactivated)]
        public virtual void OnSectionDeactivated(int sectionId)
        {
            if (sectionId == SectionId)
            {
                SectionActivated = false;
            }
        }
    }

}
