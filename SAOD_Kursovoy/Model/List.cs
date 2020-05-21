using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SAOD_Kursovoy.Service;
using SAOD_Kursovoy.Model.Elements;

namespace SAOD_Kursovoy.Model
{
    class List<T>
    {
		private ListElement<T> _current; // Текущий просматриваемый элемент

		private uint _count = 0;
		/// <summary>
		/// Определяет количество элементов в списке.
		/// </summary>
		public uint Count
		{
			get { return _count; }
			set { _count = value; }
		}

	}
}
