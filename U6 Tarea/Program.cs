using System;
using System.IO;

namespace BinaryExceptionValidator
{
    class Program
    {
        static void Main(string[] args)
        {
            // Crear la base de datos de excepciones
            int[] exceptionCodes = new int[] { 100, 200, 300, 400 };
            CreateExceptionDatabase(exceptionCodes);

            // Validar una excepción
            int exceptionCode = 50;
            if (IsExceptionValid(exceptionCode))
            {
                Console.WriteLine($"La excepción {exceptionCode} es válida");
            }
            else
            {
                Console.WriteLine($"La excepción {exceptionCode} no es válida");
            }

            Console.ReadLine();
        }

        static void CreateExceptionDatabase(int[] exceptionCodes)
        {
            using (BinaryWriter writer = new BinaryWriter(File.Open("exceptionDatabase.bin", FileMode.Create)))
            {
                foreach (int exceptionCode in exceptionCodes)
                {
                    writer.Write(exceptionCode);
                }
            }
        }

        static bool IsExceptionValid(int exceptionCode)
        {
            bool isValid = false;
            using (BinaryReader reader = new BinaryReader(File.Open("exceptionDatabase.bin", FileMode.Open)))
            {
                while (reader.BaseStream.Position != reader.BaseStream.Length)
                {
                    int code = reader.ReadInt32();
                    if (code == exceptionCode)
                    {
                        isValid = true;
                        break;
                    }
                }
            }
            return isValid;
        }
    }
}