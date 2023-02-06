using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CanvasManager
{
    public class MainScreenEvents : MonoBehaviour
    {
        public void Resume()
        {
            if (Time.timeScale == 0)
            {
                Time.timeScale = 1;
                EventSystem.current.currentSelectedGameObject.GetComponentInChildren<TextMeshProUGUI>().text = "Pause";
            }
            else
            {
                Time.timeScale = 0;
                EventSystem.current.currentSelectedGameObject.GetComponentInChildren<TextMeshProUGUI>().text = "Play";
            }
            
            
          
        }
      
    }
}
