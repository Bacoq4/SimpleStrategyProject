using Project.Building;
using Project.Sender;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Project.Manager
{
   // This class is violating open-closed principle but couldn't find more proper design solution :)
   public class UIManager : MonoBehaviour
   {
      [FoldoutGroup("Array"), SerializeField] private GameObject[] m_Informations;

      [FoldoutGroup("Array"), SerializeField] private GameObject[] m_ChosenInformations;
      

      private void Awake()
      {
         DisableInfos();
      }

      private void OnEnable()
      {
         ProductionButtonMessageSender.OnButtonPressed += OpenSuitableInformationUI;
         BuildingBase.OnChosenByPlayer += OpenSuitableChosenUI;
      }
      private void OnDisable()
      {
         ProductionButtonMessageSender.OnButtonPressed -= OpenSuitableInformationUI;
         BuildingBase.OnChosenByPlayer += OpenSuitableChosenUI;
      }
      private void OpenSuitableInformationUI(BuildingType _BuildingType)
      {
         DisableInfos();

         switch (_BuildingType)
         {
            case BuildingType.Barrack:
               m_Informations[0].SetActive(true);
               break;
            case BuildingType.PowerPlant:
               m_Informations[1].SetActive(true);
               break;
         }
      }
      
      private void OpenSuitableChosenUI(BuildingType _BuildingType)
      {
         DisableInfos();

         switch (_BuildingType)
         {
            case BuildingType.Barrack:
               m_ChosenInformations[0].SetActive(true);
               break;
            case BuildingType.PowerPlant:
               m_ChosenInformations[1].SetActive(true);
               break;
         }
      }

      private void DisableInfos()
      {
         foreach (GameObject _Info in m_Informations)
         {
            _Info.SetActive(false);
         }
         foreach (GameObject _Info in m_ChosenInformations)
         {
            _Info.SetActive(false);
         }
      }
      
      
   }

}
