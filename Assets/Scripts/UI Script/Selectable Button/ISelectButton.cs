using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LusfulPanda.Selectable
{
    public interface ISelectButton
    {
        GameObject SelectedButton { get; set; }
        public void LoadScene(int sceneInt);
        public void OpenPage(int activePage, params GameObject[] allPages);
        public void FirstButton(ButtonSelected buttonSelected);
        public void SelectButton();
        public void UnselectButton();
        
    }
}
