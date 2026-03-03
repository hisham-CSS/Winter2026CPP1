using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public BaseMenu[] allMenus;

    public MenuStates initState = MenuStates.MainMenu;

    public BaseMenu currentMenu => _currentMenu;
    private BaseMenu _currentMenu;

    private Dictionary<MenuStates, BaseMenu> menuDictionary = new Dictionary<MenuStates, BaseMenu>();
    private Stack<MenuStates> menuStack = new Stack<MenuStates>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (allMenus.Length == 0)
        {
            allMenus = GetComponentsInChildren<BaseMenu>(true);
        }

        foreach (BaseMenu menu in allMenus)
        {
            if (menu == null) continue;
            menu.Initialize(this);

            if (menuDictionary.ContainsKey(menu.state))
            {
                Debug.LogWarning($"Duplicate menu state {menu.state} found in {menu.gameObject.name}. This menu will be ignored.");
                continue;
            }

            menuDictionary.Add(menu.state, menu);
        }

        JumpTo(initState);
    }

    public void JumpBack()
    {
        if (menuStack.Count <= 0)
        {
            Debug.LogWarning("No previous menu to jump back to.");
            return;
        }
        menuStack.Pop();
        JumpTo(menuStack.Peek(), true);
    }

    public void JumpTo(MenuStates newState, bool isBack = false)
    {
        if (!menuDictionary.ContainsKey(newState))
        {
            Debug.LogError($"Menu state {newState} not found in menu dictionary.");
            return;
        }
        if (_currentMenu == menuDictionary[newState])
        {
            Debug.LogWarning($"Already on menu state {newState}. No action taken.");
            return;
        }

        if (_currentMenu != null)
        {
            _currentMenu.Exit();
            _currentMenu.gameObject.SetActive(false);
        }

        _currentMenu = menuDictionary[newState];
        _currentMenu.gameObject.SetActive(true);
        _currentMenu.Enter();

        if (!isBack)
        {
            if (menuStack.Count > 0 && menuStack.Contains(newState))
            {
                List<MenuStates> tempStack = new List<MenuStates>();
                while (menuStack.Peek() != newState)
                {
                    tempStack.Add(menuStack.Pop());
                }

                menuStack.Pop(); // Remove the duplicate state

                for (int i = tempStack.Count - 1; i >= 0; i--)
                {
                    menuStack.Push(tempStack[i]);
                }
            }
            menuStack.Push(newState);
        }    
    }
}
