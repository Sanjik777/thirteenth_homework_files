using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
/*
 Взаимодействие с файловой системой.

1.	Написать программу, читающую побайтно заданный файл и подсчитывающую 
число появлений каждого из 256 возможных знаков.
2.	С помощью класса StreamWriter записать в текстовый файл свое имя,
фамилию и возраст. Каждая запись должна начинаться с новой строки.
 */
namespace practical_work
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("\n----------------------1---------------------\n");

			string textRead = null;//для способа StreamReader
			int counter = 0;

			string filePath = "textFile.txt";
			if (!File.Exists(filePath))
			{
				var stream = File.Create(filePath);
				stream.Close();
			}
			else
			{
				var temp = new FileInfo(filePath);
				temp.Delete();
				var stream = File.Create(filePath);
				stream.Close();
			}

			//Проверим длину изначальной строки
			string textFile = "Asd@F)3449&GF*#NCV|{:>?>:L<:L<";
			Console.WriteLine($"Проверка длины строки: {textFile.Length}\n");

			//Способ чтения через StreamReader
			using (StreamWriter fileWriter = new StreamWriter(filePath))
			{
				fileWriter.WriteLine(textFile);
			};			
			using (StreamReader fileReader = new StreamReader(filePath))
			{
				while (fileReader.Peek()>=0)
				{
					counter++;
					textRead += (char)fileReader.Read();
				}
				//Второй способ чтения:
				//fileReader.BaseStream.Position = 0;// возвращаем на начало
				//textRead = fileReader.ReadToEnd(); // читаем целиком
			};
			Console.WriteLine($"Вывод символов из файла: {textRead}");
			Console.WriteLine($"Кол-во символов в файле через StreamReader: {counter}");

			//Способ чтения через FileStream
			int secondCounter = 0;
			using (FileStream fileStream = File.OpenRead(filePath))
			{
				for (int i=0; i < fileStream.Length; i++)
				{
					fileStream.ReadByte();
					secondCounter++;
				}
			};
			Console.WriteLine($"Кол-во символов в файле через FileStream: {secondCounter}");


			Console.WriteLine("\n----------------------2---------------------\n");
			string secondPath = "SecondFile.txt";
			
			using (StreamWriter fileWriter = new StreamWriter(secondPath))
			{
				fileWriter.WriteLine("Ivanov");
				fileWriter.WriteLine("Ivan");
				fileWriter.WriteLine("23");
			};
			using (StreamReader fileReader = new StreamReader(secondPath))
			{
				string dataPerson = fileReader.ReadToEnd();
				Console.WriteLine(dataPerson);
			};
			Console.ReadKey();
		}
	}
}
