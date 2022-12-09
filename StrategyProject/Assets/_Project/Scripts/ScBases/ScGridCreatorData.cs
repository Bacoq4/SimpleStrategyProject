using UnityEngine;

namespace Project.Data
{
    [CreateAssetMenu(fileName = "GridCreatorSettings", menuName = "ScriptableObjects/GridCreatorData")]
    public class ScGridCreatorData : ScriptableObject
    {
        [SerializeField] private int _Height;
        [SerializeField] private int _Width;
    
        public int m_Height
        {
            get { return _Height; }
            set { _Height = value; }
        }
    
        public int m_Width
        {
            get { return _Width; }
            set { _Width = value; }
        }
    }

}
