using Project.Building;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;


namespace Project.Sender
{
    public class ProductionButtonMessageSender : ButtonMessageSender
    {
        [FoldoutGroup("Settings"), SerializeField] protected BuildingType m_BuildingType;

        public static UnityAction<BuildingType> OnButtonPressed { get; set; }

        public override void ButtonPressed()
        {
            OnButtonPressed?.Invoke(m_BuildingType);
        }
    }

}
