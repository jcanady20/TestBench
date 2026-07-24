using System;
using System.Collections.Generic;
using System.Linq;

namespace TestBench;

public class Menu
{
  private int _currentLocation = 0;
  private IList<MenuChoice> _choices;
  
  public Menu()
  { }
  public Menu(IList<string> choices)
  {
    if (_choices == null)
      _choices = new List<MenuChoice>();

    _choices.Clear();

    foreach (string s in choices)
    {
      MenuChoice mc = new MenuChoice();
      mc.Description = s;
      _choices.Add(mc);
    }
  }
  public Menu(MenuChoice[] choices)
  {
    _choices = choices.ToList();
  }
  public Menu(IList<MenuChoice> choices)
  {
    _choices = choices;
  }
  public IList<MenuChoice> Choices
  {
    get => _choices;
    set => _choices = value;
  }
  public bool Canceled { get; private set; }
  public void RunMenu()
  {
    bool _run = true;
    while (_run)
    {
      Console.Clear();
      int i = 0;
      foreach (MenuChoice mc in _choices)
      {
        if (i == _currentLocation)
        {
          Console.BackgroundColor = ConsoleColor.Gray;
          Console.ForegroundColor = ConsoleColor.Black;
        }
        else
        {
          Console.BackgroundColor = ConsoleColor.Black;
          Console.ForegroundColor = ConsoleColor.White;
        }
        Console.WriteLine("{0}. [{1}]  {2}", i, (mc.Selected ? "X" : " "), mc.Description);
        i++;
      }

      Console.BackgroundColor = ConsoleColor.Black;
      Console.ForegroundColor = ConsoleColor.White;
      Console.WriteLine("");
      Console.WriteLine("Press Enter to Execute");
      Console.WriteLine("Press Q to Cancel");

      ConsoleKeyInfo cki = Console.ReadKey(true);
      switch (cki.Key)
      {
        case ConsoleKey.UpArrow:
          if (_currentLocation > 0)
          {
            --_currentLocation;
          }
          break;
        case ConsoleKey.DownArrow:
          if (_currentLocation < _choices.Count -1)
          {
            ++_currentLocation;
          }
          break;
        case ConsoleKey.Spacebar:
          _choices[_currentLocation].Selected = !_choices[_currentLocation].Selected;
          break;
        case ConsoleKey.Q:
          Canceled = true;
          _run = false;
          break;
        case ConsoleKey.Enter:
          _run = false;
          break;
      }
      
      System.Threading.Thread.Sleep(100);
    }
  }
}

public class MenuChoice
{
  private Action _action;
  public MenuChoice()
  { }
  public MenuChoice(Action action)
    : this()
  {
    _action = action;
  }
  public MenuChoice(string description)
    : this()
  {
    Description = description;
  }
  public MenuChoice(Action action, string description)
    : this()
  {
    _action = action;
    Description = description;
  }
  public string Description { get; set; }
  public bool Selected { get; set; }
  public void Execute()
  {
    _action.Invoke();
  }
}
