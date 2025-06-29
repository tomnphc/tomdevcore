using UnityEngine;

namespace TomDev.GUI
{
    public class PopupWin : TomPopup
    {
        public override void Show()
        {
            base.Show();
            Debug.Log("PopupWin Show!");
        }

        public override void HideAndDestroy()
        {
            base.HideAndDestroy();
            Debug.Log("PopupWin Hide!");
        }

      
    }
} 