using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static List<Livro> catalogo = new List<Livro>();

    static void Main(string[] args)
    {
        CarregarCatalogo();

        while (true)
        {
            
            Console.WriteLine("\nMenu Biblioteca:");
            Console.WriteLine("1. Cadastrar livro");
            Console.WriteLine("2. Consultar catálogo");
            Console.WriteLine("3. Emprestar livro");
            Console.WriteLine("4. Devolver livro");
            Console.WriteLine("5. Salvar e sair");
            string opcao = Console.ReadLine();

            switch (opcao)
            {
                case "1":
                    CadastrarLivro();
                    break;
                case "2":
                    ConsultarCatalogo();
                    break;
                case "3":
                    EmprestarLivro();
                    break;
                case "4":
                    DevolverLivro();
                    break;
                case "5":
                    SalvarCatalogo();
                    return;
                default:
                    Console.WriteLine("Opção inválida.");
                    break;
            }
        }
    }

    static void CadastrarLivro()
    {
        Console.Write("Título: ");
        string titulo = Console.ReadLine();

        Console.Write("Autor: ");
        string autor = Console.ReadLine();

        Console.Write("Quantidade disponível: ");
        int quantidade = int.Parse(Console.ReadLine());

        catalogo.Add(new Livro(titulo, autor, quantidade));
        Console.WriteLine("Livro cadastrado com sucesso!");
    }

    static void ConsultarCatalogo()
    {
        Console.WriteLine("\nCatálogo de livros:");
        foreach (var livro in catalogo)
        {
            Console.WriteLine($"Título: {livro.Titulo}, Autor: {livro.Autor}, Disponível: {livro.QuantidadeDisponivel}");
        }
    }

    static void EmprestarLivro()
    {
        Console.Write("Digite o título do livro que deseja emprestar: ");
        string titulo = Console.ReadLine();

        Livro livro = catalogo.Find(l => l.Titulo.ToLower() == titulo.ToLower());

        if (livro != null && livro.QuantidadeDisponivel > 0)
        {
            livro.QuantidadeDisponivel--;
            Console.WriteLine($"Você emprestou o livro '{livro.Titulo}'.");
        }
        else
        {
            Console.WriteLine("Livro indisponível.");
        }
    }

    static void DevolverLivro()
    {
        Console.Write("Digite o título do livro que deseja devolver: ");
        string titulo = Console.ReadLine();

        Livro livro = catalogo.Find(l => l.Titulo.ToLower() == titulo.ToLower());

        if (livro != null)
        {
            livro.QuantidadeDisponivel++;
            Console.WriteLine($"Você devolveu o livro '{livro.Titulo}'.");
        }
        else
        {
            Console.WriteLine("Livro não encontrado.");
        }
    }

    static void SalvarCatalogo()
    {
        using (StreamWriter writer = new StreamWriter("catalogo.txt"))
        {
            foreach (var livro in catalogo)
            {
                writer.WriteLine($"{livro.Titulo};{livro.Autor};{livro.QuantidadeDisponivel}");
            }
        }
        Console.WriteLine("Catálogo salvo com sucesso!");
    }

    static void CarregarCatalogo()
    {
        if (File.Exists("catalogo.txt"))
        {
            using (StreamReader reader = new StreamReader("catalogo.txt"))
            {
                string linha;
                while ((linha = reader.ReadLine()) != null)
                {
                    string[] dados = linha.Split(';');
                    string titulo = dados[0];
                    string autor = dados[1];
                    int quantidade = int.Parse(dados[2]);

                    catalogo.Add(new Livro(titulo, autor, quantidade));
                }
            }
            Console.WriteLine("Catálogo carregado com sucesso!");
        }
        else
        {
            Console.WriteLine("Nenhum catálogo encontrado.");
        }
    }
}

class Livro
{
    public string Titulo { get; set; }
    public string Autor { get; set; }
    public int QuantidadeDisponivel { get; set; }

    public Livro(string titulo, string autor, int quantidade)
    {
        Titulo = titulo;
        Autor = autor;
        QuantidadeDisponivel = quantidade;
    }
}