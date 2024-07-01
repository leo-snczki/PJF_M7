using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PJF_M7
{
    class Program
    {
        static public string currentDirectory = Directory.GetCurrentDirectory();
        static string input;
        static int op;
        static void Main(string[] args)
        {
            ChooseOption();
        }
        static void ShowDefaultMenu()
        {
            Console.WriteLine("4 - Mostrar diretória");
        }
        static void ChooseOption()
        {
            ShowDefaultMenu();
            Console.WriteLine("Insira a opção desejada: ");
            input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    // notas
                    ChooseOptionEvaluation();
                    break;
                case "2":
                    // faltas
                    ChooseOptionMissedClass();
                    break;
                case "3":
                    // Consutar o aluno com seus dados individualmente.
                    break;
                case "4":
                        ShowFolderStructure(new DirectoryInfo(currentDirectory));
                    break;
                default:
                    Console.WriteLine("nao existe");
                    break;
            }
        }
        static void ChooseOptionMissedClass() // Escolher a opção referentes ao assunto de faltas.
        {
            ShowMenuMissedClass();
            Console.WriteLine("Insira a opção desejada: ");
            input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    break;
                default:
                    break;
            }
        }
        static void ShowMenuMissedClass() // Mostra as opções dísponíveis referentes ao assunto de faltas.
        {
            Console.WriteLine("1 - Adicionar Faltas");
            Console.WriteLine("2 - Remover Faltas");
            Console.WriteLine("3 - Consultar faltas");
        }
        static void ChooseOptionEvaluation() // Escolher a opção referentes ao assunto de notas.
        {
            ShowMenuEvaluation();
            Console.WriteLine("Insira a opção desejada: ");
            input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    break;
                default:
                    break;
            }
        }
        static void ShowMenuEvaluation()  // Mostra as opções dísponíveis referentes ao assunto de notas.
        {
            Console.WriteLine("1 - Lançar notas");
            Console.WriteLine("2 - Remover nota");
            Console.WriteLine("3 - Consultar notas");
        }
        static void ShowFolderStructure(DirectoryInfo directory)
        {
            Console.WriteLine(directory.Name);

            foreach (FileInfo file in directory.GetFiles("*.txt"))
            {
                Console.WriteLine("   " + file.Name);
            }

            foreach (DirectoryInfo subdirectory in directory.GetDirectories())
            {
                ShowFolderStructure(subdirectory);
            }
            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
        }
    }
}
