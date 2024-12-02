using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Lab1Var58_Task3
{
    internal class Program
    {
        class Matrix
        {
            //  размерность матрицы
            public int rows_num;
            public int columns_num;

            //  конструктор матрицы
            public int[,] rows;

            public void ShowMatrix()
            {
                //  вывод матрицы в презентабельном виде

                //  param:
                //  int[,] matrix - инициализированная матрица
                //  int rows, columns - строки и колонки матрицы

                for (int i = 0; i < rows_num; i++)
                {
                    for (int j = 0; j < columns_num; j++)
                    {
                        Console.Write(rows[i,j] + "\t");
                    }
                    Console.WriteLine();
                }
            }

            public string CountStringWithZero()
            {
                //  подсчет кол-ва строк с нулем

                //  return:
                //  string - кол-во строк в матрице, содержащих 0

                int res = 0;

                for (int i = 0; i < rows_num; i++)
                {
                    for (int j = 0; j < columns_num; j++)
                    {
                        if (rows[i, j] == 0)
                        {
                            res++;
                            break;
                        }
                    }
                }

                return $"Количество строк с нулем - {res}";
            }

            public string LongestIndentSequenceColumn()
            {
                //  подсчет кол-ва столбцов с максимально длинной последовательностью
                //  одинаковых символов

                //  return:
                //  string - кол-во столбцов и длина максимально длинной последовательности
                //  одинаковых символов

                //  номер столбца
                int res = 1;

                //  кол-во подряд идущих символов
                int max_seq = 1;
                
                //  прогоняем цикл по столбцам
                for (int j = 0; j < columns_num; j++)
                {
                    //  кол-во подряд идущих символов для конкретного столбца
                    //  обнуляем при каждой итерации
                    int temp_seq = 1;

                    //  первый символ текущего столбца в первой итерации
                    //  далее символ меняется, если не совпадает со следующим
                    int temp_row = rows[0, j];

                    //  идем по строкам
                    for (int i = 1; i < rows_num; i++)
                    {
                        //  если прошлый символ и текущий равны
                        //  к кол-ву символов добавляем 1
                        if (temp_row == rows[i, j])
                            temp_seq++;
                        //  если символы разные, то обновляем символ для следующей итерации
                        //  и обнуляем текущую последовательность
                        else
                        {
                            temp_row = rows[i, j];
                            temp_seq = 1;
                        }
                    }

                    //  если последовательность для текущего столбца > чем для прошлых
                    //  то обновляем номер столбца и максимальное значение
                    if (temp_seq >= max_seq)
                    { 
                        max_seq = temp_seq;
                        res = j+1;
                    }
                }

                return $"Номер столбца с наибольшим кол-вом подряд идущих символов в столбце - {res} ({max_seq}) цифр";
            }
        }

        static void Main()
        {
            //  создаем экземпляр матрицы
            Matrix matrix = new Matrix();
            matrix.rows_num = 5;
            matrix.columns_num = 3;
            matrix.rows = new int[matrix.rows_num, matrix.columns_num];

            //  заполняем матрицу из файла
            FillMatrixFromFile(matrix);

            //  выводим матрицу
            matrix.ShowMatrix();

            //  выводим количество строк с нулем
            Console.WriteLine(matrix.CountStringWithZero());

            //  выводим кол-во столбцов и длину максимально длинной последовательности
            //  одинаковых символов
            Console.WriteLine(matrix.LongestIndentSequenceColumn());

            WriteResultToFile(matrix);
        }

        static void FillMatrixFromFile(Matrix matrix)
        {
            //  param:
            //  int[,] matrix - инициализированная матрица
            //  int rows, columns - строки и колонки матрицы

            //  переменная под разделитель, если разделитель вдруг изменится
            char splitter = ' ';

            //  путь до файла
            string file_path = $"..\\..\\Matrix.txt";

            try
            {
                //  через менеджер открываем файл на чтение
                using (StreamReader sr = new StreamReader(file_path))
                {
                    string line;

                    //  вычитываем строки и записываем в массив строк строку матицы
                    for (int i = 0; i < matrix.rows_num; i++)
                    {
                        line = sr.ReadLine();

                        string[] line_split;
                        line_split = line.Split(splitter);

                        //  добавляем в матрицу строку
                        for (int j = 0; j < matrix.columns_num; j++)
                            matrix.rows[i,j] = Convert.ToInt32(line_split[j]);
                    }
                }
            }
            //  обрабатываем выкидыши
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        static void WriteResultToFile(Matrix matrix)
        {
            //  запись результатов в файл

            string file_path = $"..\\..\\Res.txt";

            try 
            { 
                using(StreamWriter sw = new StreamWriter(file_path, false))
                {
                    //  записываем результаты функций в файл
                    sw.WriteLine(matrix.CountStringWithZero());
                    sw.WriteLine(matrix.LongestIndentSequenceColumn());
                }
            }
            //  ловим выкидыши
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Process.Start(file_path);
        }
    }
}
