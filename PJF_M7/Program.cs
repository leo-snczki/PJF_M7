using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        static string[] files = { "nomes.txt", "notas.txt", "faltas.txt" };

        static string[] lines;

        static Aluno[] alunos = new Aluno[0];

        static void Main(string[] args)
        {
            Initializer();
        }

        static void Initializer()
        {
            if (File.Exists(files[0])) 
            { 
                Array.Resize(ref alunos, File.ReadAllLines(files[0]).Length);
                for(int i = 0; i < alunos.Length; i += 3)
                {
                    alunos[i].nome = File.ReadAllLines(files[0])[i];
                    alunos[i].turma = File.ReadAllLines(files[0])[i + 1];
                    int.TryParse(File.ReadAllLines(files[0])[i + 2], out alunos[i].numero);
                }
            }
            else File.Create(files[0]);

            if (File.Exists(files[1]))
            {
                lines = File.ReadAllText(files[1]).Split(new char[] { 'b' }, StringSplitOptions.None);
                for (int i = 0; i < lines.Length; i++)
                {
                    string[] sublines = lines[i].Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

                    Array.Resize(ref alunos[i].materia, sublines.Length / 2);

                    for (int j = 0; j < sublines.Length; j++)
                    {
                        for (int k = 0; k < alunos[i].materia.Length; k++)
                        {
                            if (j == 0 || j % 2 == 0) alunos[i].materia[k].nome = sublines[j];
                            else double.TryParse(sublines[j], out alunos[i].materia[k].nota);
                        }
                    }
                }
            }
            else File.Create(files[1]);

            if (File.Exists(files[2]))
            {
                lines = File.ReadAllText(files[2]).Split(new char[] { 'b' }, StringSplitOptions.None);
                for (int i = 0; i < lines.Length; i++)
                {
                    string[] sublines = lines[i].Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

                    Array.Resize(ref alunos[i].faltas, sublines.Length / 2);

                    for (int j = 0; j < sublines.Length; j++)
                    {
                        for (int k = 0; k < alunos[i].faltas.Length; k++)
                        {
                            if (j == 0 || j % 2 == 0) alunos[i].faltas[k].disciplina = sublines[j];
                            else int.TryParse(sublines[j], out alunos[i].faltas[k].faltas);
                        }
                    }
                }
            }
            else File.Create(files[2]);
        }

        static void Saver()
        {
            using(StreamWriter writer = new StreamWriter(files[0]))
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
                for(int i = 0; i < alunos.Length; i++)
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
