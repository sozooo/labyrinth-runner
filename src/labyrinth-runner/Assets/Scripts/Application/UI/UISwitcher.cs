using System;
using System.Collections.Generic;

namespace Application.UI
{
    public class UISwitcher
    {
        private readonly Dictionary<Type, UIPanel> _panels;
        private UIPanel _activePanel;

        public UISwitcher(List<UIPanel> panels)
        {
            _panels = new Dictionary<Type, UIPanel>(panels.Count);
            foreach (var panel in panels)
                _panels[panel.GetType()] = panel;
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
