using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication2
{
    static class Decode
    {
        /// <summary>
        /// Метод для конвертации ёбнутой кодировки USC2 в нормальную
        /// </summary>
        /// <param name="str">Конвертируемая строка</param>
        /// <returns></returns>
        public static string USC2ToString(string str)
        {
            // Загоняем строку в массив попутно переставляя местами каждые 2 байта каждого символа
            byte[] ucs2 = new byte[str.Length / 2];
            int j = 0;

            for (int i = 0; i < str.Length; i += 4)
            {
                try
                {
                    string s = str.Substring(i + 2, 2);
                    ucs2[j] = Convert.ToByte(s, 16);
                    j++;
                }
                catch (Exception ex)
                {
                }
                try
                {
                string ss = str.Substring(i, 2);
                ucs2[j] = Convert.ToByte(ss, 16);
                j++;
                }
                catch (Exception ex)
                {
                }

            }

            // Получаем из массива чистый юникод. "Ядра - чистый юникод... " Ай да Пушкин, он знал ... о_О 

            str = Encoding.Unicode.GetString(ucs2);

            return str;
        }

        /// <summary>
        /// Декодер 7-битной кодировки
        /// </summary>
        /// <param name="s">Конвертируемая строка</param>
        /// <returns></returns>
        public static string Decode7bit(string s)
        {
            try
            {
                // преобразуем строку в массив байт. На 2 делить длину строки не будем, пусть массив будет в 2 раза больше, т.к. строка увеличится при декодировании
                byte[] arr = new byte[s.Length];

                for (int i = 0; i < s.Length; i += 2)
                {
                    arr[i / 2] = Convert.ToByte(s.Substring(i, 2), 16);
                }

                int bit7pre;    // Здесь будет храниться 7 бит предыдущего байта
                int bit7cur;    // Здесь будет храниться 7 бит текущего байта

                for (int i = 1; i < arr.Length - 2; i++)
                {
                    bit7pre = arr[i - 1] & 0x80; // запоминаем последний бит предыдущего байта
                    arr[i - 1] = (byte)(arr[i - 1] & 0x7F); // Обнуляем последний бит предыдущего

                    for (int j = i; j < arr.Length - 1; j++)
                    {
                        bit7cur = arr[j] & 0x80; // запоминаем последний бит текущего байта

                        arr[j] = (byte)(arr[j] << 1); // Сдвигаем текущий байт влево

                        arr[j] = bit7pre > 0 ? (byte)(arr[j] | 0x01) : arr[j];  // Седьмой бит предыдущего становится нулевым битом текущего

                        bit7pre = bit7cur;  // Бывший седьмой бит текущего теперь будет 7 битом предыдущего для следующего байта
                    }
                }

                // Переносим массив в строку, заменяем нули пробелами и обрезаем
                s = Encoding.ASCII.GetString(arr).Replace((char)0x00, (char)0x20).Trim();

                return s;
            }
            catch (Exception ex)
            {
                return "Ошибка: "+ex.Message;
            }
        }


        public static string TryDecode(string s)
        {
            try
            {
                s = USC2ToString(s);
            }
            catch (Exception ex)
            {
                s = Decode7bit(s);
            }
            return s;
        }
    }
}
