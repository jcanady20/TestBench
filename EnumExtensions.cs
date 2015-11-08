using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestBench
{
	public static class EnumExtensions
	{
		public static IEnumerable<T> GetAllItems<T>(this Enum value)
		{
			foreach(object item in Enum.GetValues(typeof(T)))
			{
				yield return (T)item;
			}
		}

		public static IEnumerable<T> GetAllItems<T>() where T : struct
		{
			foreach(object item in Enum.GetValues(typeof(T)))
			{
				yield return (T)item;
			}
		}

		public static IEnumerable<T> GetAllSelectedITems<T>(this Enum value)
		{
			int valueAsInt = Convert.ToInt32(value, CultureInfo.InvariantCulture);

			foreach(object item in Enum.GetValues(typeof(T)))
			{
				int itemAsInt = Convert.ToInt32(item, CultureInfo.InvariantCulture);
				if(itemAsInt == (valueAsInt & itemAsInt))
				{
					yield return (T)item;
				}
			}
		}
	}
}
