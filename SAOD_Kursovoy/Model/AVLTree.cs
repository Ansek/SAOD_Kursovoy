using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SAOD_Kursovoy.Model.Elements;

namespace SAOD_Kursovoy.Model
{
    class AVLTree<T>
    {
        TreeElement<T> _root;   // Корень дерева

        /// <summary>
        /// Вращение поддерева влево.
        /// </summary>
        /// <param name="a">Ссылка на узел.</param>
        public void RotateLeft(ref TreeElement<T> a)
        {
            if (a != null)
            {
                var b = a.Right;
                a.Right = b.Left;
                b.Left = a;
                a.RefreshHeight();
                b.RefreshHeight();
                a = b;
            }
        }

        /// <summary>
        /// Вращение поддерева вправо.
        /// </summary>
        /// <param name="a">Ссылка на узел.</param>
        public void RotateRight(ref TreeElement<T> a)
        {
            if (a != null)
            {
                var b = a.Left;
                a.Left = b.Right;
                b.Right = a;
                a.RefreshHeight();
                b.RefreshHeight();
                a = b;
            }
        }

    }
}
