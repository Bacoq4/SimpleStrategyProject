using UnityEngine;

namespace Project.General
{
    public class MouseManager : MonoBehaviour
    {
        public static Vector3 GetMousePositionIn2DWorld()
        {
            Vector3 _MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _MousePos.z = 0;
            return _MousePos;
        }
    }
}