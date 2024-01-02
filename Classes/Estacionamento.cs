using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NovoProjeto2.Classes
{
    public class Estacionamento
    {
        // ###CONSTRUTOR(ES)
        public Estacionamento(decimal TarifaInicialDoEstacionamento, decimal TarifaPorHoraDoEstacionamento, int TotalDeVagasDoEstacionamento, int TotalDeVagasOcupadasDoEstacionamento = 0, string NomeDoEstacionamento = "Não informado")
        {
            this.TarifaInicialDoEstacionamento = TarifaInicialDoEstacionamento;
            this.TarifaPorHoraDoEstacionamento = TarifaPorHoraDoEstacionamento;

            this.TotalDeVagasDoEstacionamento = TotalDeVagasDoEstacionamento;
            this.TotalDeVagasOcupadasDoEstacionamento = TotalDeVagasOcupadasDoEstacionamento;

            this.NomeDoEstacionamento = NomeDoEstacionamento;
        }
        // CONSTRUTOR(ES)###

        // ###PROPRIEDADE(S)
        private decimal TarifaInicialDoEstacionamento { get; set; }
        private decimal TarifaPorHoraDoEstacionamento { get; set; }

        private int TotalDeVagasDoEstacionamento { get; set; }
        private int TotalDeVagasOcupadasDoEstacionamento { get; set; }

        private string NomeDoEstacionamento { get; set; }

        Dictionary<string, Veiculo> ListaDeTodoOsVeiculosQueForamEstacionados = new Dictionary<string, Veiculo>();
        // PROPRIEDADE(S)###

        // ###MÉTODO(S)
        public void ListarPropriedadesDoEstacionamento()
        {
            Console.WriteLine("Propriedades do estacionamento".ToUpper());
            Console.WriteLine($"\tTarifa inicial do estacionamento: {TarifaInicialDoEstacionamento:C}");
            Console.WriteLine($"\tTarifa por hora do estacionamento: {TarifaPorHoraDoEstacionamento:C}");
            Console.WriteLine($"\tTotal de vagas do estacionamento: {TotalDeVagasDoEstacionamento}");
            Console.WriteLine($"\tTota de vagas ocupadas do estacionamento: {TotalDeVagasOcupadasDoEstacionamento}");
            Console.WriteLine($"\tNome do estacionamento: {NomeDoEstacionamento}");
        }

        public void AdicionarVeiculoAoEstacionamento()
        {
            if (TotalDeVagasOcupadasDoEstacionamento < TotalDeVagasDoEstacionamento)
            {
                Veiculo novoVeiculo = Veiculo.GerarNovoVeiculo();
                ListaDeTodoOsVeiculosQueForamEstacionados.Add(novoVeiculo.PlacaDoVeiculo, novoVeiculo);
                TotalDeVagasOcupadasDoEstacionamento += 1;
                Console.WriteLine("Veiculo adiconado.");
            }
        }

        public void RemoverVeiculoDoEstacionamento()
        {
            if (ListaDeTodoOsVeiculosQueForamEstacionados.Any(x => x.Value.HorarioDeSaidaDoVeiculo == DateTime.MinValue))
            {
                string placaDoVeiculoProcurado = Veiculo.LerPlacaDoVeiculo();
                if (ListaDeTodoOsVeiculosQueForamEstacionados.ContainsKey(placaDoVeiculoProcurado) && ListaDeTodoOsVeiculosQueForamEstacionados[placaDoVeiculoProcurado].HorarioDeSaidaDoVeiculo == DateTime.MinValue)
                {
                    Console.WriteLine("Propriedades do veículo".PadLeft(89));
                    ListaDeTodoOsVeiculosQueForamEstacionados[placaDoVeiculoProcurado].ListarPropriedadesDoVeiculo();
                    bool encerrarLoop = false;
                    while (!encerrarLoop)
                    {
                        Console.Write("\nRemover o veículo [S/N]?: ");
                        string? entrada = Console.ReadLine()?.Trim().ToUpper();
                        if (!string.IsNullOrEmpty(entrada))
                        {
                            switch (entrada)
                            {
                                case "S":
                                    GerarValorAPagarAoEstacionamento(ListaDeTodoOsVeiculosQueForamEstacionados[placaDoVeiculoProcurado]);
                                    TotalDeVagasOcupadasDoEstacionamento -= 1;
                                    Console.WriteLine("Veiculo removido com sucesso.");
                                    encerrarLoop = true;
                                    break;
                                case "N":
                                    Console.WriteLine("Operação cancelada. O veículo não foi removido.");
                                    encerrarLoop = true;
                                    break;
                                default:
                                    Console.WriteLine("Opção inválida!");
                                    break;
                            }
                        }
                        else
                        {
                            Console.WriteLine("A entrada não pode ser nula ou vazia.");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("O veículo não foi encontrado. Por favor, verifique a placa do veículo e tente novamente.");
                }
            }
            else
            {
                Console.WriteLine("O estacionamento não possui veiculos estacionados.");
            }
        }

        private void GerarValorAPagarAoEstacionamento(Veiculo veiculo)
        {
            veiculo.HorarioDeSaidaDoVeiculo = DateTime.Now;
            TimeSpan tempoEstacionado = veiculo.HorarioDeSaidaDoVeiculo - veiculo.HorarioDeEntradaDoVeiculo;
            double horasEstacionado = tempoEstacionado.TotalHours;
            decimal valorAPagarAoEstacionamento = this.TarifaInicialDoEstacionamento + this.TarifaPorHoraDoEstacionamento * Convert.ToDecimal(horasEstacionado);
            Console.WriteLine($"Valor a pagar: {valorAPagarAoEstacionamento:C}");
        }

        public void GerarListaDeVeiculosEstacionados()
        {
            if (ListaDeTodoOsVeiculosQueForamEstacionados.Any(x => x.Value.HorarioDeSaidaDoVeiculo == DateTime.MinValue))
            {
                Console.WriteLine("Lista de veiculos estacionados".ToUpper().PadLeft(15 + 78));
                string cabecalho = $"Proprietário do veículo".PadRight(23 + 5) + $"Placa do veículo".PadRight(16 + 5) + $"Marca do veículo".PadRight(16 + 5) + $"Modelo do veículo".PadRight(17 + 5) + $"Cor do veículo".PadRight(14 + 5) + $"Horário de entrada".PadRight(18 + 5) + $"Horário de saída".PadRight(16 + 5);
                Console.WriteLine(cabecalho);
                foreach (var parDeChaveEValor in ListaDeTodoOsVeiculosQueForamEstacionados)
                {
                    if (parDeChaveEValor.Value.HorarioDeSaidaDoVeiculo == DateTime.MinValue)
                    {
                        parDeChaveEValor.Value.ListarPropriedadesDoVeiculo();
                    }
                }
            }
            else
            {
                Console.WriteLine("Não há veículos estacionados.");
            }
        }

        public void GerarListaDeTodoOsVeiculosQueForamEstacionados()
        {
            if (ListaDeTodoOsVeiculosQueForamEstacionados.Any())
            {
                Console.WriteLine("Lista de veiculos estacionados".ToUpper().PadLeft(15 + 78));
                string cabecalho = $"Proprietário do veículo".PadRight(23 + 5) + $"Placa do veículo".PadRight(16 + 5) + $"Marca do veículo".PadRight(16 + 5) + $"Modelo do veículo".PadRight(17 + 5) + $"Cor do veículo".PadRight(14 + 5) + $"Horário de entrada".PadRight(18 + 5) + $"Horário de saída".PadRight(16 + 5);
                Console.WriteLine(cabecalho);
                foreach (var veiculo in ListaDeTodoOsVeiculosQueForamEstacionados)
                {
                    veiculo.Value.ListarPropriedadesDoVeiculo();
                }
            }
            else
            {
                Console.WriteLine("Nenhum veículo foi estacionado.");
            }
        }
        // ###MÉTODO(S)
    }
}