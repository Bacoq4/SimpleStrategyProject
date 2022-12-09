using Sirenix.OdinInspector;
using UnityEngine;

namespace Project.Manager
{
    public class LevelManager : MonoBehaviour
    {
        // unused for now ,can be removed
        [field: SerializeField ,FoldoutGroup("References")] public UIManager m_UIManager { get; private set; }
    }
}

