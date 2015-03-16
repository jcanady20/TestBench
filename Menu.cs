using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestBench
{
	public class Menu
	{
		private int m_currentlocation = 0;
		private List<MenuChoice> m_choices;
		
		public Menu()
		{
		}
		public Menu(List<string> choices)
		{
			if (m_choices == null)
				m_choices = new List<MenuChoice>();

			m_choices.Clear();

			foreach (string s in choices)
			{
				MenuChoice mc = new MenuChoice();
				mc.Description = s;
				m_choices.Add(mc);

			}
		}
		public Menu(MenuChoice[] choices)
		{
			m_choices = choices.ToList();
		}
		public Menu(List<MenuChoice> choices)
		{
			m_choices = choices;
		}

		public List<MenuChoice> Choices
		{
			get
			{
				return m_choices;
			}
			set
			{
				m_choices = value;
			}
		}
		public bool Canceled { get; private set; }

		public void RunMenu()
		{
			bool _run = true;
			while (_run)
			{
				Console.Clear();
				int i = 0;
				foreach (MenuChoice mc in m_choices)
				{
					if (i == m_currentlocation)
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
						if (m_currentlocation > 0)
						{
							--m_currentlocation;
						}
						break;
					case ConsoleKey.DownArrow:
						if (m_currentlocation < m_choices.Count -1)
						{
							++m_currentlocation;
						}
						break;
					case ConsoleKey.Spacebar:
						m_choices[m_currentlocation].Selected = !m_choices[m_currentlocation].Selected;
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
}
