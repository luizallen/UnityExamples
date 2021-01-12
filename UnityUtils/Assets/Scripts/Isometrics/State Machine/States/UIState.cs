using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Isometrics.State_Machine.States
{
    public class UIState : State
    {
        protected int Index;
        protected Image CurrentUISelector;

        protected void ChangeUISelector(List<Image> buttons)
        {
            if (Index == -1)
            {
                Index = buttons.Count - 1;
            }
            else if (Index == buttons.Count)
            {
                Index = 0;
            }

            CurrentUISelector.transform.localPosition =
                buttons[Index].transform.localPosition;
        }

        protected void PaintButton(Image image, bool check)
        {
            if (check)
            {
                image.color = Color.gray;
            }
            else
            {
                image.color = Color.white;
            }
        }
    }
}
