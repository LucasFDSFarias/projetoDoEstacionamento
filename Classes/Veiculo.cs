using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NovoProjeto2.Classes
{
    public class Veiculo
    {
        // ###CONSTRUTOR(ES)
        public Veiculo(string ProprietarioDoVeiculo, string PlacaDoVeiculo, string MarcaDoVeiculo = "Não informado", string ModeloDoVeiculo = "Não informado", string CorDoVeiculo = "Não informado")
        {
            this.ProprietarioDoVeiculo = ProprietarioDoVeiculo;
            this.PlacaDoVeiculo = PlacaDoVeiculo;

            this.MarcaDoVeiculo = MarcaDoVeiculo;
            this.ModeloDoVeiculo = ModeloDoVeiculo;
            this.CorDoVeiculo = CorDoVeiculo;

            HorarioDeEntradaDoVeiculo = DateTime.Now;
            HorarioDeSaidaDoVeiculo = DateTime.MinValue;
        }
        // CONSTRUTOR(ES)###

        // ###PROPRIEDADE(S)
        public string ProprietarioDoVeiculo { get; set; }
        public string PlacaDoVeiculo { get; set; }

        public string MarcaDoVeiculo { get; set; }
        public string ModeloDoVeiculo { get; set; }
        public string CorDoVeiculo { get; set; }

        public DateTime HorarioDeEntradaDoVeiculo { get; set; }
        public DateTime HorarioDeSaidaDoVeiculo { get; set; }
        // PROPRIEDADE(S)###

        // ###MÉTODO(S)  
        public void ListarPropriedadesDoVeiculo()
        {
            string mensagem = $"{ProprietarioDoVeiculo}".PadRight(23 + 5) + $"{PlacaDoVeiculo}".PadRight(16 + 5) + $"{MarcaDoVeiculo}".PadRight(16 + 5) + $"{ModeloDoVeiculo}".PadRight(17 + 5) + $"{CorDoVeiculo}".PadRight(14 + 5) + $"{HorarioDeEntradaDoVeiculo}".PadRight(18 + 5) + $"{HorarioDeSaidaDoVeiculo}".PadRight(16 + 5);
            Console.WriteLine(mensagem);
        }

        public static Veiculo GerarNovoVeiculo()
        {
            string proprietarioDoVeiculo = LerProprietarioDoVeiculo();
            Console.WriteLine($"O nome do proprietário do veículo \"{proprietarioDoVeiculo}\" foi salvo com sucesso.");
            string placaDoVeiculo = LerPlacaDoVeiculo();
            Console.WriteLine($"A placa {placaDoVeiculo} foi salva com sucesso.");
            return new Veiculo(proprietarioDoVeiculo, placaDoVeiculo);
        }

        public static string LerPlacaDoVeiculo()
        {
            while (true)
            {
                Console.Write("Placa do veículo: ");
                string? placaDoVeiculo = Console.ReadLine()?.Trim().ToUpper();

                if (!string.IsNullOrEmpty(placaDoVeiculo) && placaDoVeiculo?.Length == 7)
                {
                    if (ValidarPlacaAntiga(placaDoVeiculo) || ValidarPlacaMercosul(placaDoVeiculo))
                    {
                        return placaDoVeiculo;
                    }
                    else
                    {
                        Console.WriteLine($"O modelo de placa do veículo \"{placaDoVeiculo}\" é inválido. Por favor, informe um modelo de placa válido.");
                        Console.WriteLine("Modelos de placa de veículo:");
                        Console.WriteLine("\tModelo de placa (Antigo): LLLNNNN");
                        Console.WriteLine("\tModleo de placa (Mercosul): LLLNLNN");
                    }
                }
                else
                {
                    Console.WriteLine($"A placa do veículo \"{placaDoVeiculo}\" não pode ser nula ou vazia e deve possuir 7 caracteres.");
                }
            }
        }

        private static bool ValidarPlacaAntiga(string placaDoVeiculo)
        {
            if (char.IsLetter(placaDoVeiculo[0]) && char.IsLetter(placaDoVeiculo[1]) && char.IsLetter(placaDoVeiculo[2]) && char.IsDigit(placaDoVeiculo[3]) && char.IsDigit(placaDoVeiculo[4]) && char.IsDigit(placaDoVeiculo[5]) && char.IsDigit(placaDoVeiculo[6]))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static bool ValidarPlacaMercosul(string placaDoVeiculo)
        {
            if (char.IsLetter(placaDoVeiculo[0]) && char.IsLetter(placaDoVeiculo[1]) && char.IsLetter(placaDoVeiculo[2]) && char.IsDigit(placaDoVeiculo[3]) && char.IsLetter(placaDoVeiculo[4]) && char.IsDigit(placaDoVeiculo[5]) && char.IsDigit(placaDoVeiculo[6]))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static string LerProprietarioDoVeiculo()
        {
            while (true)
            {
                Console.Write("Proprietário do veículo: ");
                string? proprietarioDoVeiculo = Console.ReadLine()!.Trim().ToUpper();
                if (!string.IsNullOrEmpty(proprietarioDoVeiculo))
                {
                    return proprietarioDoVeiculo;
                }
                else
                {
                    Console.WriteLine($"O nome do proprietário do veículo \"{proprietarioDoVeiculo}\" não pode ser nulo ou vazio.");
                }
            }
        }
        // MÉTODO(S)###
    }
}