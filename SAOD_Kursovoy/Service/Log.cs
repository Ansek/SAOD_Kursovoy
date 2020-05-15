using System;
using System.IO;

namespace SAOD_Kursovoy.Service
{
    /// <summary>
    /// Журнал изменений.
    /// </summary>
    class Log
    {
        // Имя файла журнала
        const string path = "Log.txt";
        
        /// <summary>
        /// Добавляет сообщение в файл журнала с пометкой времени.
        /// </summary>
        /// <param name="message">Сообщение.</param>
        public static void Add(string message)
        {
            using (StreamWriter sw = new StreamWriter(path, true))
            {
                sw.WriteLine($"Log - {DateTime.Now:g}");    // Вывод времени
                message = message.Replace("\n", "\n\t");    // Добавление табуляции
                sw.WriteLine($"\t{message}");               // Вывод сообщения
            }
        }
    }
}
