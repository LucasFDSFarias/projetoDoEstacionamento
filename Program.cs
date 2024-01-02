using NovoProjeto2.Classes;

Estacionamento estacionamento = new Estacionamento(5, 10, 30);

string opcao = string.Empty;
bool exibirMenu = true;

// Realiza o loop do menu
while (exibirMenu)
{
    Console.Clear();
    Console.WriteLine("Digite a sua opção:");
    Console.WriteLine("1 - Cadastrar veículo");
    Console.WriteLine("2 - Remover veículo");
    Console.WriteLine("3 - Listar os veículos estacionados");
    Console.WriteLine("4 - Listar todos os veículos que foram estacionados");
    Console.WriteLine("5 - Encerrar");

    switch (Console.ReadLine())
    {
        case "1":
            estacionamento.AdicionarVeiculoAoEstacionamento();
            break;

        case "2":
            estacionamento.RemoverVeiculoDoEstacionamento();
            break;

        case "3":
            estacionamento.GerarListaDeVeiculosEstacionados();
            break;

        case "4":
            estacionamento.GerarListaDeTodoOsVeiculosQueForamEstacionados();
            break;

        case "5":
            exibirMenu = false;
            break;

        default:
            Console.WriteLine("Opção inválida");
            break;
    }

    Console.WriteLine("Pressione uma tecla para continuar");
    Console.ReadLine();
}

Console.WriteLine("O programa se encerrou");