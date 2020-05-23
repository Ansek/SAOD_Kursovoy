using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAOD_Kursovoy.Model
{
    static class Algorithm
    {
        /// <summary>
        /// Получает хеш-значение на основе ключа. 
        /// </summary>
        /// <param name="key">Ключ.</param>
        public static ushort GetHash(string key)
        {
            long d = long.Parse(key.Replace("-", ""));
            ushort i = (ushort)(d * 40503); 
            return Convert.ToUInt16(i >> 6);
        }

        /// <summary>
        /// Поиск слова в тексте по алгоритму Боуера и Мура.
        /// </summary>
        /// <param name="word">Слово для поиска.</param>
        /// <param name="text">Текст, в котором ищут.</param>
        public static bool SearchBM(string word, string text)
        {
            // Создание массива Shift
            int[] shift = new int['Z' + 1];
            int i, j;

            for (i = 0; i <= 'Z'; i++)
                shift[i] = word.Length;     // Длина слова как длина сдвига по умолчанию
            for (j = 0; j < word.Length - 1; j++)
                shift[word[j]] = word.Length - j - 1;   // Установка сдвига для каждого символа слова

            i = 0; // Начало слова совпадает с началом текста
            while (j >= 0 && i <= text.Length - word.Length)
            {
                j = word.Length; // Сравнение с последнего символа
                // Посимвольное  сравнение справа
                do
                    j--;
                while (j >= 0 && word[j] == text[i + j]);

                if (j >= 0) // Сдвиг слова вправо
                    i += shift[text[i + j]];
            }
            return j < 0;
        }
    }
}
