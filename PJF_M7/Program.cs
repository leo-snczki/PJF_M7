using System;
using System.IO;

namespace PJF_M7
{
            struct Login
        {
            private string user;
            public string User
            {
                get { return user; }
                set { user = value; }
            }
            private string passwd;

            public string Passwd
            {
                get { return passwd; }
                set { passwd = value; }
            }
        }
    class Program
    {
        static public string currentDirectory = Directory.GetCurrentDirectory();
        static string input;

        static void Main(string[] args)
        {
            ChooseOption();
        }

        static void LoginManager()
        {

            Login Teacher = new Login();

            Teacher.User = "professor";
            Teacher.Passwd = "123";

        }
        
        static void ShowDefaultMenu()
        {
            Console.WriteLine("Gestão de Ficheiros da Escola");
            Console.WriteLine("1 - Gestão de Turma");
            Console.WriteLine("2 - Operações com Ficheiros");
            Console.WriteLine("0 - Sair");
        }

        static void ChooseOption()
        {
            while (true)
            {
                ShowDefaultMenu();
                Console.Write("Insira a opção desejada: ");
                input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        ChooseMenuStudents();
                        break;
                    case "2":
                        //ChooseFileOperations();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Opção inválida. Tente novamente.");
                        break;
                }
            }
        }

        // Métodos de Gestão de Turma
        static void ShowMenuStudents()
        {
            Console.WriteLine("Gestão de Turma");
            Console.WriteLine("1 - Criar Pasta de Turma");
            Console.WriteLine("2 - Adicionar Aluno");
            Console.WriteLine("3 - Listar Alunos");
            Console.WriteLine("4 - Ler Dados de um Aluno");
            Console.WriteLine("5 - Atualizar Dados de um Aluno");
            Console.WriteLine("6 - Eliminar Aluno");
            Console.WriteLine("7 - Informações sobre Aluno");
            Console.WriteLine("0 - Voltar");
        }

        static void ChooseMenuStudents()
        {
            while (true)
            {
                ShowMenuStudents();
                Console.Write("Insira a opção desejada: ");
                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        CreateClassFolder();
                        break;
                    case "2":
                        AddStudent();
                        break;
                    case "3":
                        ListStudents();
                        break;
                    case "4":
                        ReadAboutStudent();
                        break;
                    case "5":
                        UpdateStudent();
                        break;
                    case "6":
                        DeleteStudent();
                        break;
                    case "7":
                        InfoAboutStudent();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Opção inválida. Tente novamente.");
                        break;
                }
            }
        }

        static void CreateClassFolder()
        {
            Console.Write("Insira o nome da turma: ");
            string turmaNome = Console.ReadLine();
            string turmaPath = Path.Combine(currentDirectory, turmaNome);

            if (!Directory.Exists(turmaPath))
            {
                Directory.CreateDirectory(turmaPath);
                Console.WriteLine("Pasta da turma criada com sucesso.");
            }
            else
            {
                Console.WriteLine("A pasta da turma já existe.");
            }
        }

        static void AddStudent()
        {
            Console.Write("Insira o nome da turma: ");
            string turmaNome = Console.ReadLine();
            string turmaPath = Path.Combine(currentDirectory, turmaNome);

            if (!Directory.Exists(turmaPath))
            {
                Console.WriteLine("A turma não existe. Por favor, crie a turma primeiro.");
                return;
            }

            Console.Write("Insira o nome do aluno: ");
            string nomeAluno = Console.ReadLine();
            string filePath = Path.Combine(turmaPath, nomeAluno + ".txt");

            if (File.Exists(filePath))
            {
                Console.WriteLine("O aluno já existe.");
            }
            else
            {
                using (StreamWriter sw = File.CreateText(filePath))
                {
                    sw.WriteLine($"Nome: {nomeAluno}");
                    sw.WriteLine("Nota: ");
                    sw.WriteLine($"Turma: {turmaNome}");
                    sw.WriteLine("Faltas: ");
                }
                Console.WriteLine("Aluno adicionado com sucesso.");
            }
        }

        static void ListStudents()
        {
            Console.Write("Insira o nome da turma: ");
            string turmaNome = Console.ReadLine();
            string turmaPath = Path.Combine(currentDirectory, turmaNome);

            if (!Directory.Exists(turmaPath))
            {
                Console.WriteLine("A turma não existe.");
                return;
            }

            string[] files = Directory.GetFiles(turmaPath, "*.txt");
            Console.WriteLine("Lista de Alunos:");
            foreach (string file in files)
            {
                Console.WriteLine(Path.GetFileNameWithoutExtension(file));
            }
        }

        static void ReadAboutStudent()
        {
            Console.Write("Insira o nome da turma: ");
            string turmaNome = Console.ReadLine();
            string turmaPath = Path.Combine(currentDirectory, turmaNome);

            if (!Directory.Exists(turmaPath))
            {
                Console.WriteLine("A turma não existe.");
                return;
            }

            Console.Write("Insira o nome do aluno: ");
            string nomeAluno = Console.ReadLine();
            string filePath = Path.Combine(turmaPath, nomeAluno + ".txt");

            if (File.Exists(filePath))
            {
                string conteudo = File.ReadAllText(filePath);
                Console.WriteLine("Dados do Aluno:");
                Console.WriteLine(conteudo);
            }
            else
            {
                Console.WriteLine("O aluno não existe.");
            }
        }

        static void UpdateStudent()
        {
            Console.Write("Insira o nome da turma: ");
            string turmaNome = Console.ReadLine();
            string turmaPath = Path.Combine(currentDirectory, turmaNome);

            if (!Directory.Exists(turmaPath))
            {
                Console.WriteLine("A turma não existe.");
                return;
            }

            Console.Write("Insira o nome do aluno: ");
            string nomeAluno = Console.ReadLine();
            string filePath = Path.Combine(turmaPath, nomeAluno + ".txt");

            if (File.Exists(filePath))
            {
                Console.WriteLine("Escolha a opção:");
                Console.WriteLine("1 - Substituir conteúdo");
                Console.WriteLine("2 - Adicionar conteúdo");
                string opcao = Console.ReadLine();

                Console.WriteLine("Insira o texto:");
                string texto = Console.ReadLine();

                if (opcao == "1")
                {
                    File.WriteAllText(filePath, texto);
                }
                else if (opcao == "2")
                {
                    File.AppendAllText(filePath, texto + Environment.NewLine);
                }
                else
                {
                    Console.WriteLine("Opção inválida.");
                }
            }
            else
            {
                Console.WriteLine("O aluno não existe.");
            }
        }

        static void DeleteStudent()
        {
            Console.Write("Insira o nome da turma: ");
            string turmaNome = Console.ReadLine();
            string turmaPath = Path.Combine(currentDirectory, turmaNome);

            if (!Directory.Exists(turmaPath))
            {
                Console.WriteLine("A turma não existe.");
                return;
            }

            Console.Write("Insira o nome do aluno: ");
            string nomeAluno = Console.ReadLine();
            string filePath = Path.Combine(turmaPath, nomeAluno + ".txt");

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                Console.WriteLine("Aluno eliminado com sucesso.");
            }
            else
            {
                Console.WriteLine("O aluno não existe.");
            }
        }

        static void InfoAboutStudent()
        {
            Console.Write("Insira o nome da turma: ");
            string turmaNome = Console.ReadLine();
            string turmaPath = Path.Combine(currentDirectory, turmaNome);

            if (!Directory.Exists(turmaPath))
            {
                Console.WriteLine("A turma não existe.");
                return;
            }

            Console.Write("Insira o nome do aluno: ");
            string nomeAluno = Console.ReadLine();
            string filePath = Path.Combine(turmaPath, nomeAluno + ".txt");

            if (File.Exists(filePath))
            {
                FileInfo info = new FileInfo(filePath);
                Console.WriteLine("Informações sobre o aluno:");
                Console.WriteLine($"Nome: {info.Name}");
                Console.WriteLine($"Tamanho: {info.Length} bytes");
                Console.WriteLine($"Data de criação: {info.CreationTime}");
                Console.WriteLine($"Última modificação: {info.LastWriteTime}");
                Console.WriteLine($"Último acesso: {info.LastAccessTime}");
            }
            else
            {
                Console.WriteLine("O aluno não existe.");
            }
        }

        // Métodos de Operações com Ficheiros
        //static void ShowFileOperationsMenu()
        //{
        //    Console.WriteLine("Operações com Ficheiros");
        //    Console.WriteLine("1 - Criação de Ficheiros");
        //    Console.WriteLine("2 - Leitura de Ficheiros");
        //    Console.WriteLine("3 - Escrita em Ficheiros");
        //    Console.WriteLine("4 - Cópia de Ficheiro");
        //    Console.WriteLine("5 - Movimentação/Renomeação de Ficheiros");
        //    Console.WriteLine("6 - Eliminação de Ficheiros");
        //    Console.WriteLine("7 - Informações sobre Ficheiros");
        //    Console.WriteLine("0 - Voltar");
        //}

        //static void ChooseFileOperations()
        //{
        //    while (true)
        //    {
        //        ShowFileOperationsMenu();
        //        Console.Write("Insira a opção desejada: ");
        //        string input = Console.ReadLine();

        //        switch (input)
        //        {
        //            case "1":
        //                CreateFile();
        //                break;
        //            case "2":
        //                ReadFile();
        //                break;
        //            case "3":
        //                WriteFile();
        //                break;
        //            case "4":
        //                CopyFile();
        //                break;
        //            case "5":
        //                MoveOrRenameFile();
        //                break;
        //            case "6":
        //                DeleteFile();
        //                break;
        //            case "7":
        //                FileInfo();
        //                break;
        //            case "0":
        //                return;
        //            default:
        //                Console.WriteLine("Opção inválida. Tente novamente.");
        //                break;
        //        }
        //    }
        //}

        //static void CreateFile()
        //{
        //    Console.Write("Insira o nome do ficheiro a criar: ");
        //    string nomeFicheiro = Console.ReadLine();
        //    if (!nomeFicheiro.EndsWith(".txt")) nomeFicheiro += ".txt";

        //    if (File.Exists(nomeFicheiro))
        //    {
        //        Console.WriteLine("O ficheiro já existe.");
        //    }
        //    else
        //    {
        //        File.Create(nomeFicheiro).Close();
        //        Console.WriteLine("Ficheiro criado com sucesso.");
        //    }
        //}

        //static void ReadFile()
        //{
        //    Console.Write("Insira o nome do ficheiro a ler: ");
        //    string nomeFicheiro = Console.ReadLine();

        //    if (File.Exists(nomeFicheiro))
        //    {
        //        string conteudo = File.ReadAllText(nomeFicheiro);
        //        Console.WriteLine("Conteúdo do ficheiro:");
        //        Console.WriteLine(conteudo);
        //    }
        //    else
        //    {
        //        Console.WriteLine("O ficheiro não existe.");
        //    }
        //}

        //static void WriteFile()
        //{
        //    Console.Write("Insira o nome do ficheiro: ");
        //    string nomeFicheiro = Console.ReadLine();

        //    if (File.Exists(nomeFicheiro))
        //    {
        //        Console.WriteLine("Escolha a opção:");
        //        Console.WriteLine("1 - Substituir conteúdo");
        //        Console.WriteLine("2 - Adicionar conteúdo");
        //        string opcao = Console.ReadLine();

        //        Console.WriteLine("Insira o texto:");
        //        string texto = Console.ReadLine();

        //        if (opcao == "1")
        //        {
        //            File.WriteAllText(nomeFicheiro, texto);
        //        }
        //        else if (opcao == "2")
        //        {
        //            File.AppendAllText(nomeFicheiro, texto + Environment.NewLine);
        //        }
        //        else
        //        {
        //            Console.WriteLine("Opção inválida.");
        //        }
        //    }
        //    else
        //    {
        //        Console.WriteLine("O ficheiro não existe.");
        //    }
        //}

        //static void CopyFile()
        //{
        //    Console.Write("Insira o nome do ficheiro a copiar: ");
        //    string nomeFicheiro = Console.ReadLine();

        //    if (File.Exists(nomeFicheiro))
        //    {
        //        Console.Write("Insira o novo caminho e nome do ficheiro: ");
        //        string novoCaminho = Console.ReadLine();

        //        File.Copy(nomeFicheiro, novoCaminho, true);
        //        Console.WriteLine("Ficheiro copiado com sucesso.");
        //    }
        //    else
        //    {
        //        Console.WriteLine("O ficheiro não existe.");
        //    }
        //}

        //static void MoveOrRenameFile()
        //{
        //    Console.Write("Insira o nome do ficheiro a mover/renomear: ");
        //    string nomeFicheiro = Console.ReadLine();

        //    if (File.Exists(nomeFicheiro))
        //    {
        //        Console.Write("Insira o novo caminho e nome do ficheiro: ");
        //        string novoCaminho = Console.ReadLine();

        //        File.Move(nomeFicheiro, novoCaminho);
        //        Console.WriteLine("Ficheiro movido/renomeado com sucesso.");
        //    }
        //    else
        //    {
        //        Console.WriteLine("O ficheiro não existe.");
        //    }
        //}

        //static void DeleteFile()
        //{
        //    Console.Write("Insira o nome do ficheiro a eliminar: ");
        //    string nomeFicheiro = Console.ReadLine();

        //    if (File.Exists(nomeFicheiro))
        //    {
        //        File.Delete(nomeFicheiro);
        //        Console.WriteLine("Ficheiro eliminado com sucesso.");
        //    }
        //    else
        //    {
        //        Console.WriteLine("O ficheiro não existe.");
        //    }
        //}

        //static void FileInfo()
        //{
        //    Console.Write("Insira o nome do ficheiro: ");
        //    string nomeFicheiro = Console.ReadLine();

        //    if (File.Exists(nomeFicheiro))
        //    {
        //        FileInfo info = new FileInfo(nomeFicheiro);
        //        Console.WriteLine("Informações sobre o ficheiro:");
        //        Console.WriteLine($"Nome: {info.Name}");
        //        Console.WriteLine($"Tamanho: {info.Length} bytes");
        //        Console.WriteLine($"Data de criação: {info.CreationTime}");
        //        Console.WriteLine($"Última modificação: {info.LastWriteTime}");
        //        Console.WriteLine($"Último acesso: {info.LastAccessTime}");
        //    }
        //    else
        //    {
        //        Console.WriteLine("O ficheiro não existe.");
        //    }
        //}
    }
}
