using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using System.Linq;
/*
Взаимодействие с файловой системой.

1.	В файле записана непустая последовательность целых чисел,
являющихся числами Фибоначчи. Приписать еще столько же чисел этой последовательности.
2.	Сложить два целых числа А и В.
-Входные данные:
В единственной строке входного файла INPUT.TXT записано два натуральных числа через пробел.
-Выходные данные:
В единственную строку выходного файла OUTPUT.TXT нужно вывести одно целое число — сумму чисел А и В.
*/
namespace thirteenth_homework_files
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("\n----------------------1---------------------\n");
			string fibonacciFirstText = "0, 1, 1, 2, 3, 5, ";
			string fibonacciSecondText = "8, 13, 21, 34, 55, 89";
			string path = "text.txt";

			string result = "";
			if (!File.Exists(path))
			{
				var stream = File.Create(path);
				stream.Close();
			}
			else
			{
				//то удаляем и заново создаем
				FileInfo temp = new FileInfo(path);
				temp.Delete();
				var stream = File.Create(path);
				stream.Close();
			}

			using (FileStream fileStream = new FileStream(path, FileMode.Open))
			{
				byte[] data = Encoding.UTF8.GetBytes(fibonacciFirstText);
				fileStream.Write(data, 0, data.Length);
			}

			using (FileStream fileStream = File.OpenRead(path))
			{
				byte[] buffer = new byte[fileStream.Length];
				fileStream.Read(buffer, 0, buffer.Length);

				result += Encoding.UTF8.GetString(buffer);
				Console.WriteLine($"первая часть: {result}");
			}

			//File.AppendAllText(path, fibonacciSecondText);
			using (StreamWriter writer = File.AppendText(path))
			{
				writer.WriteLine(fibonacciSecondText);
				result += fibonacciSecondText;
			}
			Console.WriteLine($"всё: {result}");
			

			Console.WriteLine("\n----------------------2---------------------\n");

			int firstNumber = 3;
			int secondNumber = 5;

			string input = "Input.txt";
			string output = "Output.txt";

			//инициализация input.txt
			if (!File.Exists(input))
			{
				var stream = File.Create(input);
				stream.Close();
			}
			else
			{
				FileInfo temp = new FileInfo(input);
				temp.Delete();
				var stream = File.Create(input);
				stream.Close();
			}
			
			using (StreamWriter fileWriter = new StreamWriter(input))
			{
				string inputText = $"{firstNumber} {secondNumber}";
				fileWriter.WriteLine(inputText);
			}
			//выведем из файла input.txt строку
			string twoNumbers = null;
			using (StreamReader fileReader = new StreamReader(input))
			{
				twoNumbers = fileReader.ReadToEnd();
				Console.WriteLine($"Вывод 2-ух чисел из файла Input.txt в виде строки: {twoNumbers}");
			}

			//Превратим строку в List<int>:
			List<int> numbers = new List<int>();
			Console.Write("наши числа в int: ");
			for (int i = 0; i < twoNumbers.Length; i++)
			{
				if (Char.IsNumber(twoNumbers[i]))
				{
					int numberToInt = (int)Char.GetNumericValue(twoNumbers[i]);
					//или:
					//int numberToInt = Convert.ToInt32(twoNumbers[i] - '0');

					numbers.Add(numberToInt);
					Console.Write(numberToInt +" ");
				}
			}
			var sum = numbers.Sum();

			Console.WriteLine($"\n\nсумма равна {sum}, затем запись в файл Output.txt");

			//инициализация output.txt
			if (!File.Exists(output))
			{
				var stream = File.Create(output);
				stream.Close();
			}
			else
			{
				FileInfo temp = new FileInfo(output);
				temp.Delete();
				var stream = File.Create(output);
				stream.Close();
			}

			using (StreamWriter fileWriter = new StreamWriter(output))
			{
				string outputText = $"{sum.ToString()}";
				fileWriter.WriteLine(outputText);
			}
			using (StreamReader fileReader = new StreamReader(output))
			{
				string sumNumber = fileReader.ReadToEnd();
				Console.WriteLine($"\nВывод из файла Output.txt: {sumNumber}");
			}

			Console.ReadKey();
		}
	}
}