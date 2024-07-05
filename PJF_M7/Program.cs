using System;
using System.Linq;
using System.IO;

namespace PJF_M7
{
    class Program
    {
        struct Falta
        {
            public string disciplina;
            public int faltas;
        }

        struct Disciplina
        {
            public string nome;
            public double nota;
        }

        struct Aluno
        {
            public string nome;
            public string turma;
            public int numero;
            public Disciplina[] materia;
            public Falta[] faltas;
        }

        static string input, output = "Info Estudantes";

        static int op;

        static string[] subjects = new string[0];

        static readonly string[] files = { "nomes.txt", "notas.txt", "faltas.txt" };

        static string[] lines;

        static Aluno[] alunos = new Aluno[0];

        static void Main(string[] args)
        {
            Initializer();
        }

        static void Initializer()
        {
            if (File.Exists(files[0]) || File.Exists(files[0] + ".bak")) 
            {
                if (!File.Exists(files[0]) && File.Exists(files[0] + ".bak")) File.Copy(files[0] + ".bak", files[0], true);
                lines = File.ReadAllLines(files[0]);
                Array.Resize(ref alunos, lines.Length / 3);
                int i = 0;
                for (int j = 0; j < lines.Length; j += 3)
                {
                    alunos[i].nome = File.ReadAllLines(files[0])[j];
                    alunos[i].turma = File.ReadAllLines(files[0])[j + 1];
                    int.TryParse(File.ReadAllLines(files[0])[j + 2], out alunos[i].numero);
                    alunos[i].materia = new Disciplina[0];
                    alunos[i].faltas = new Falta[0];
                    i++;
                }
            }
            else File.Create(files[0]).Close();

            if (File.Exists(files[1]) || File.Exists(files[1] + ".bak"))
            {
                if (!File.Exists(files[1]) && File.Exists(files[1] + ".bak")) File.Copy(files[1] + ".bak", files[1], true);
                lines = File.ReadAllText(files[1]).Split(new char[] { 'b' }, StringSplitOptions.None);

                for (int i = 0; i < lines.Length; i++)
                {
                    int k = 0;

                    string[] sublines = lines[i].Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                    
                    if (sublines.Length > 0)
                    {
                        alunos[i].materia = new Disciplina[0];
                        
                        Array.Resize(ref alunos[i].materia, sublines.Length / 2);

                        for (int j = 0; j < sublines.Length; j += 2)
                        {
                            alunos[i].materia[k].nome = sublines[j];
                            double.TryParse(sublines[j + 1], out alunos[i].materia[k].nota);
                            k++;
                        }
                    }
                }
            }
            else File.Create(files[1]).Close();

            if (File.Exists(files[2]) || File.Exists(files[2] + ".bak") )
            {
                if (!File.Exists(files[2]) && File.Exists(files[2] + ".bak")) File.Copy(files[2] + ".bak", files[2], true);
                lines = File.ReadAllText(files[2]).Split(new char[] { 'b' }, StringSplitOptions.None);
                for (int i = 0; i < lines.Length; i++)
                {
                    int k = 0;

                    string[] sublines = lines[i].Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

                    if (sublines.Length > 0)
                    {
                        alunos[i].faltas = new Falta[0];

                        Array.Resize(ref alunos[i].faltas, sublines.Length / 2);

                        for (int j = 0; j < sublines.Length; j += 2)
                        {
                            alunos[i].faltas[k].disciplina = sublines[j];
                            int.TryParse(sublines[j + 1], out alunos[i].faltas[k].faltas);
                            k++;
                        }
                    }
                }
            }
            else File.Create(files[2]).Close();

            ChooseOption();
        }

        static void ShowMenu()
        {
            Console.WriteLine(
                $"1 - Salva as informações dos estudantes na pasta '{output}' que localiza-se em 'Ambiente de trabalho'" +
                "\n2 - Adiciona um estudante" +
                "\n3 - Remove um estudante" +
                "\n4 - Edita as informações de um estudante" +
                "\n5 - Adiciona uma disciplina a um estudante" +
                "\n6 - Remove uma disciplina a um estudante" +
                "\n7 - Mostra as informações de todos os arquivos usados pelo o programa" +
                "\n0 - Salva e fecha do programa"
                );
        }

        static void ChooseOption()
        {
            bool check = true;
            do
            {
                if (check)
                {
                    ShowMenu();
                    Console.Write("\nEscolha uma das opções acima: ");
                }
                check = true;
                input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        OutputInfo();
                        break;
                    case "2":
                        AddStudent();
                        break;
                    case "3":
                        RemoveStudent();
                        break;
                    case "4":
                        EditStudent();
                        break;
                    case "5":
                        AddSubject();
                        break;
                    case "6":
                        RemoveSubject();
                        break;
                    case "7":
                        FilesInfo();
                        break;
                    case "0":
                        input = "\u0000";
                        break;
                    default:
                        Console.Write("\nOpção inválida, tente novamente: ");
                        check = false;
                        break;
                }
            } while (input != "\u0000");
            Saver();
        }

        static void RemoveSubject()
        {
            if(alunos.Length > 0)
            {
                ListStudents();
                Console.Write("Insira o estudante que quer remover a disciplina: ");
                do 
                {
                    int.TryParse(Console.ReadLine(), out op);
                    if (op < 1 || op - 1 >= alunos.Length) Console.Write("Opção inválida, tente novamente: ");
                    else if (alunos[op - 1].materia.Length < 1) Console.Write("Esse estudante está em nunhuma disciplina, tente outro estudante: ");
                } while (op < 1 || op - 1 >= alunos.Length || alunos[op - 1].materia.Length < 1);
                op--;
                if (alunos[op].materia.Length > 0)
                {
                    Console.WriteLine($"Disciplinas que {alunos[op].nome} participa: ");
                    for (int i = 0; i < alunos[op].materia.Length; i++)
                    {
                        Console.WriteLine($"{i + 1} - {alunos[op].materia[i].nome}");
                    }
                    Console.Write("Insire a disciplina que gostaria de remover" +
                        "\nInsire nada se quiser cancelar: ");
                    do
                    {
                        input = Console.ReadLine();
                        if ((int.Parse(input) < 1 || int.Parse(input) - 1 >= alunos[op].materia.Length) && input != "") Console.Write("Valor inválido, tente novamente: ");
                    } while ((int.Parse(input) < 1 || int.Parse(input) - 1 >= alunos[op].materia.Length) && input != "");
                    if (input != "")
                    {
                        for (int i = 0; i < alunos[op].materia.Length; i++)
                        {
                            alunos[op].materia[i - 1] = alunos[op].materia[i];
                            alunos[op].faltas[i - 1] = alunos[op].faltas[i];
                        }
                        Array.Resize(ref alunos[op].materia, alunos[op].materia.Length - 1);
                        Array.Resize(ref alunos[op].faltas, alunos[op].faltas.Length - 1);
                    }
                }
                else Console.WriteLine("Não existem disciplinas registradas neste aluno\n");
            }
            else Console.WriteLine("Não existem alunos registrados");

            Console.WriteLine("\n");
        }

        static void AddSubject()
        {
            if(alunos.Length > 0)
            {
                ListStudents();
                Console.Write("\nInsira o estudante que gostaria de adicionar uma disciplina" +
                    "\nInsire nada para cancelar: ");
                do
                {
                    input = Console.ReadLine();
                    int.TryParse(input, out op);
                    if ((op < 1 || op - 1 >= alunos.Length) && input != "") Console.Write("Valor inválido, tente novamente: ");
                } while ((op < 1 || op - 1 >= alunos.Length) && input != "");
                if (input != "")
                {
                    op--;
                    Console.Write($"Insira a disciplina que gostarida de adicionar a {alunos[op].nome}: ");
                    do
                    {
                        input = Console.ReadLine().ToLower();
                        if (Array.FindIndex(alunos[op].materia, s => s.nome.ToLower() == input.ToLower()) != -1) Console.Write("Esse estudante já está nessa disciplina: ");
                        if (input.Length < 2) Console.Write("O nome da disciplina não pode ser menor que 2 caracteres: ");
                    } while (Array.FindIndex(alunos[op].materia, s => s.nome.ToLower() == input.ToLower()) != -1 || input.Length < 2);
                    Array.Resize(ref alunos[op].materia, alunos[op].materia.Length + 1);
                    Array.Resize(ref alunos[op].faltas, alunos[op].faltas.Length + 1);
                    alunos[op].materia[alunos[op].materia.Length - 1] = new Disciplina { nome = input, nota = 0 };
                    alunos[op].faltas[alunos[op].faltas.Length - 1] = new Falta { disciplina = input, faltas = 0 };
                }
            }
            else Console.WriteLine("Não existem alunos registrados");

            Console.WriteLine("\n");
        }

        static void OutputInfo()
        {
            if (alunos.Length > 0)
            {
                bool check;
                Console.WriteLine("1 - Listar as notas de todos os alunos em todas as disciplina" +
                    "\n2 - Listar todas as faltas de todos os alunos em todas as disciplinas" +
                    "\n0 - Cancela a execução");
                Console.Write("\nEscolha uma das opções a cima: ");
                if (!Directory.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), output))) Directory.CreateDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), output));
                string pathGrades = Path.Combine(new string[] { Environment.GetFolderPath(Environment.SpecialFolder.Desktop), output, $"Notas - {DateTime.Now:dd-MM-yyyy}" });
                string pathAbsences = Path.Combine(new string[] { Environment.GetFolderPath(Environment.SpecialFolder.Desktop), output, $"Faltas - {DateTime.Now:dd-MM-yyyy}" });
                string extra = "";
                for (int i = 0; i < alunos.Length; i++)
                {
                    if (alunos[i].materia.Length > 0)
                        for (int j = 0; j < alunos[i].materia.Length; j++)
                        {
                            if (Array.FindIndex(subjects, s => s.ToLower() == alunos[i].materia[j].nome.ToLower()) == -1)
                            {
                                Array.Resize(ref subjects, subjects.Length + 1);
                                subjects[subjects.Length - 1] = alunos[i].materia[j].nome;
                            }
                        }
                }
                do
                {
                    check = true;
                    switch (Console.ReadLine())
                    {
                        case "1":
                            if (subjects.Length > 0)
                            {
                                if (!File.Exists(pathGrades + ".txt")) File.Create(pathGrades + ".txt").Close();
                                else
                                {
                                    for (int i = 2; File.Exists(pathGrades + extra + ".txt"); i++)
                                    {
                                        extra = $" ({i})";
                                    }
                                    File.Create(pathGrades + extra + ".txt").Close();
                                }

                                using (StreamWriter writer = new StreamWriter(pathGrades + extra + ".txt"))
                                {
                                    for (int i = 0; i < subjects.Length; i++)
                                    {
                                        writer.WriteLine(subjects[i] + "\n");
                                        for (int j = 0; j < alunos.Length; j++)
                                        {
                                            op = Array.FindIndex(alunos[j].materia, s => s.nome.ToLower() == subjects[i].ToLower());
                                            writer.WriteLine($"{alunos[j].nome} - {alunos[j].turma} - {((op == -1) ? "N/A" : alunos[j].materia[op].nota.ToString() + " Valor(es)")}");
                                        }
                                        writer.WriteLine();
                                    }
                                    writer.WriteLine("N/A - o aluno não participa da disciplina");

                                    writer.Close();
                                }
                            }
                            else Console.WriteLine("Não existem registros de disciplinas");
                            break;
                        case "2":
                            if (subjects.Length > 0)
                            {
                                if (!File.Exists(pathAbsences + ".txt")) File.Create(pathAbsences + ".txt").Close();
                                else
                                {
                                    for (int i = 2; File.Exists(pathAbsences + extra + ".txt"); i++)
                                    {
                                        extra = $" ({i})";
                                    }
                                    File.Create(pathAbsences + extra + ".txt").Close();
                                }

                                using (StreamWriter writer = new StreamWriter(pathAbsences + extra + ".txt"))
                                {
                                    for (int i = 0; i < subjects.Length; i++)
                                    {
                                        writer.Write(subjects[i] + "\n");
                                        for (int j = 0; j < alunos.Length; j++)
                                        {
                                            op = Array.FindIndex(alunos[j].faltas, s => s.disciplina.ToLower() == subjects[i].ToLower());
                                            writer.WriteLine($"{alunos[j].nome} - {alunos[j].turma} - {((op == -1) ? "N/A" : alunos[j].faltas[op].faltas.ToString() + " falta(s)")}");
                                        }
                                        writer.WriteLine();
                                    }
                                    writer.WriteLine("N/A - o aluno não participa da disciplina");

                                    writer.Close();
                                }
                            }
                            else Console.WriteLine("Não existem registros de faltas em todas as disciplinas");
                            break;
                        case "0":
                            break;
                        default:
                            Console.Write("Opção inválida, tente novamente: ");
                            check = false;
                            break;
                    }
                } while (!check);
                Array.Resize(ref subjects, 0);
            }
            else Console.WriteLine("Não existem alunos registrados");

            Console.WriteLine("\n");
        }

        static void AddStudent()
        {
            Console.Write("Insira o nome do estudante" +
                "\nInsire nada para cancelar: ");

            do
            {
                input = Console.ReadLine();
                if (input.Length < 3 && input != "") Console.Write("O nome não pode ser menor que 3 caracteres: ");
                if (Array.FindIndex(alunos, s => s.nome == input) != -1 && input != "") Console.Write("Já existe um aluno com esse nome: ");
            } while ((input.Length < 3 || Array.FindIndex(alunos, s => s.nome.ToLower() == input.ToLower()) != -1) && input != "");
            if (input != "")
            {
                Array.Resize(ref alunos, alunos.Length + 1);
                alunos[alunos.Length - 1].nome = input;

                Console.Write("Insira a turma do estudante: ");
                do
                {
                    input = Console.ReadLine();
                    if (input.Length == 0) Console.Write("O nome da turma não pode ser menor de 1 carácter: ");
                } while (input.Length == 0);
                alunos[alunos.Length - 1].turma = input;

                alunos[alunos.Length - 1].numero = alunos.Max(s => s.numero) + 1;

                alunos[alunos.Length - 1].materia = new Disciplina[0];

                alunos[alunos.Length - 1].faltas = new Falta[0];

                Array.Sort(alunos, (x, y) => x.nome.CompareTo(y.nome));
            }

            Console.WriteLine("\n");
        }

        static void ListStudents()
        {
            for(int i = 0; i < alunos.Length; i++)
            {
                Console.WriteLine(i + 1 + " - " + alunos[i].nome);
            }
        }

        static void EditStudent()
        {
            if(alunos.Length > 0)
            {
                bool check;
                ListStudents();
                Console.Write("Insira o estudante que gostaria de editar: ");
                do
                {
                    int.TryParse(Console.ReadLine(), out op);
                    if (op < 1 || op - 1 >= alunos.Length) Console.Write("Valor inválido tente novamente: ");
                } while (op < 1 || op - 1 >= alunos.Length);
                op--;

                Console.WriteLine("1 - Editar notas " +
                    "\n2 - Editar faltas " +
                    "\n3 - Editar Informações do estudante " +
                    "\n0 - Cancelar \n");
                Console.Write("Escolha uma das opções acima: ");
                do
                {
                    check = true;
                    switch (Console.ReadLine())
                    {
                        case "1":
                            for (int i = 0; i < alunos[op].materia.Length; i++)
                            {
                                Console.Write($"Insira a nova nota de {alunos[op].nome} " +
                                    $"na disciplina {alunos[op].materia[i].nome}" +
                                    $"\nO valor atual é {alunos[op].materia[i].nota}" +
                                    $"\nInsire nada se não quiser alterar: ");
                                do
                                {
                                    input = Console.ReadLine();
                                    if ((double.Parse(input) < 0 || double.Parse(input) > 20) && input != "") Console.Write("Valor inválido, tente novamente: ");
                                } while ((double.Parse(input) < 0 || double.Parse(input) > 20) && input != "");
                                if(input != "") double.TryParse(input, out alunos[op].materia[i].nota);
                            }
                            break;
                        case "2":
                            for(int i = 0; i < alunos[op].faltas.Length; i++)
                            {
                                Console.Write($"Insira o número de faltas de {alunos[op].nome} " +
                                    $"na disciplina {alunos[op].faltas[i].disciplina}" +
                                    $"\nO número de faltas atual é {alunos[op].faltas[i].faltas}" +
                                    $"\nInsire nada se não quiser alterar: ");
                                do
                                {
                                    input = Console.ReadLine();
                                    if (int.Parse(input) < 0 && input != "") Console.Write("Valor inválido, tente novamente: ");
                                } while (int.Parse(input) < 0 && input != "");
                                if(input != "")int.TryParse(input, out alunos[op].faltas[i].faltas);
                            }
                            break;
                        case "3":
                            Console.Write($"Insira o novo nome de {alunos[op].nome}" +
                                $"\nInsire nada se não quiser alterar: ");
                            do
                            {
                                input = Console.ReadLine();
                                if (input.Length < 2 && input != "") Console.Write("O novo nome não pode ser menor que 3 caracteres: ");
                            } while (input.Length < 2 && input != "");
                            if (input != "") alunos[op].nome = input;
                            Console.Write($"Insira a nova turma, a atual é {alunos[op].turma}" +
                                $"\nInsire nada se não quiser alterar: ");
                            input = Console.ReadLine();
                            if (input != "") alunos[op].turma = input;
                            break;
                        case "0":
                            Console.WriteLine("Saindo...");
                            break;
                        default:
                            check = false;
                            Console.Write("Opção inválida, tente novamente: ");
                            break;
                    }
                } while (!check);
            }
            else Console.WriteLine("Não existem alunos registrados");

            Console.WriteLine("\n");
        }

        static void RemoveStudent()
        {
            if(alunos.Length > 0) 
            { 
                ListStudents();
                Console.Write("Insira o estudante que gostaria de remover: ");
                do
                {
                    int.TryParse(Console.ReadLine(), out op);
                    if (op <= -1 || op - 1 >= alunos.Length) Console.Write("Valor inválido tente novamente: ");
                } while (op <= -1 || op - 1 >= alunos.Length);

                Console.WriteLine($"Tem mesmo a certeza que quer remover {alunos[op - 1].nome}?" +
                    $"\nInsire '1' se quiser continuar" +
                    $"\nQualquer outro valor cancelerá a operação");
                
                input = Console.ReadLine();

                if(input == "1")
                {
                    for (int i = op; i < alunos.Length; i++) alunos[i - 1] = alunos[i];
                    Array.Resize(ref alunos, alunos.Length - 1);
                }
            }
            else Console.WriteLine("Não existem alunos registrados");

            Console.WriteLine("\n");
        }

        static void FilesInfo()
        {
            try
            {
                string[] outFiles = Directory.GetFiles(Path.Combine(Environment.SpecialFolder.Desktop.ToString(), output), "*.txt");
                foreach (var file in outFiles)
                {
                    FileInfo info = new FileInfo(file);
                    Console.WriteLine(
                        $"\nNome - {info.Name}" +
                        $"\nTamanho - {info.Length} Bytes" +
                        $"\nData de criação - {info.CreationTime:dddd dd-MM-yyyy HH:mm}"
                        );
                }
            }
            catch { }

            foreach (var file in files)
            {
                FileInfo info = new FileInfo(file);
                Console.WriteLine( 
                    $"\nNome - {info.Name}" +
                    $"\nTamanho - {info.Length} Bytes" +
                    $"\nData de criação - {info.CreationTime:dddd dd-MM-yyyy HH:mm}" +
                    $"\nÚltima modificação - {info.LastWriteTime:dddd dd-MM-yyyy HH:mm}" +
                    $"\nÚltimo acesso - {info.LastAccessTime:dddd dd-MM-yyyy HH:mm}"
                    );
                try
                {
                    info = new FileInfo(file + ".bak");
                    Console.WriteLine(
                        $"\nNome - {info.Name}" +
                        $"\nTamanho - {info.Length} Bytes" +
                        $"\nData de criação - {info.CreationTime:dddd dd-MM-yyyy HH:mm}" +
                        $"\nÚltima modificação - {info.LastWriteTime:dddd dd-MM-yyyy HH:mm}" +
                        $"\nÚltimo acesso - {info.LastAccessTime:dddd dd-MM-yyyy HH:mm}"
                        );
                }
                catch { }
            }
            Console.WriteLine("\n");
        }

        static void Saver()
        {
            for (int i = 0; i < files.Length; i++)
            {
                File.Copy(files[i], files[i] + ".bak", true);
            }
            using (StreamWriter writer = new StreamWriter(files[0]))
            {
                for(int i = 0; i < alunos.Length; i++)
                {
                    writer.WriteLine(alunos[i].nome);
                    writer.WriteLine(alunos[i].turma);
                    writer.WriteLine(alunos[i].numero);
                }
                writer.Close();
            }

            using(StreamWriter writer = new StreamWriter(files[1]))
            {
                for (int i = 0; i < alunos.Length; i++)
                {
                    for(int j = 0; j < alunos[i].materia.Length; j++)
                    {
                        writer.WriteLine(alunos[i].materia[j].nome);
                        writer.WriteLine(alunos[i].materia[j].nota);
                    }
                    if (i + 1 != alunos.Length) writer.WriteLine("b");
                }
            }

            using (StreamWriter writer = new StreamWriter(files[2]))
            {
                for (int i = 0; i < alunos.Length; i++)
                {
                    for (int j = 0; j < alunos[i].faltas.Length; j++)
                    {
                        writer.WriteLine(alunos[i].faltas[j].disciplina);
                        writer.WriteLine(alunos[i].faltas[j].faltas);
                    }
                    if (i + 1 != alunos.Length) writer.WriteLine("b");
                }
            }
        }
    }
}
