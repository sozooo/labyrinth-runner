using System;
using System.Collections.Generic;

namespace Application.UI
{
    public class UISwitcher
    {
        private readonly Dictionary<Type, UIPanel> _panels;
        private UIPanel _activePanel;

        public UISwitcher(StartPanel start, GameplayPanel gameplay, WinPanel win, LosePanel lose)
        {
            _panels = new Dictionary<Type, UIPanel>
            {
                [typeof(StartPanel)] = start,
                [typeof(GameplayPanel)] = gameplay,
                [typeof(WinPanel)] = win,
                [typeof(LosePanel)] = lose
            };
        }

        public void ShowPanel<T>() where T : UIPanel
        {
            _activePanel?.Hide();
            _panels[typeof(T)].Show();
            _activePanel = _panels[typeof(T)];
        }

        public void HideAll()
        {
            _activePanel?.Hide();
            _activePanel = null;
        }
    }
}
