using UnityEngine;

namespace LastOneOut
{
    public class Bootstrap : MonoBehaviour
    {
        void Awake()
        {
            UnityFacade.GetInstance().Initialize();
        }
    }
}