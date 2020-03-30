using Doozy.Engine.UI;
using UnityEngine;

namespace Doozy.Examples
{
    public class ExampleShowPopup : MonoBehaviour
    {
        //public string PopupName;
        public void ShowPopup(string PopupName) { UIPopupManager.ShowPopup(PopupName, false, false); }
        public void ShowQueuedPopup(string PopupName) { UIPopupManager.ShowPopup(PopupName, true, false); }
        public void HidePopup(string PopupName) { UIPopup.HidePopup(PopupName); }
    }
}