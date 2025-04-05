interface IContaBancaria
{
    void Depositar(double valor);
    void Sacar(double valor);
    void MostrarSaldo();
}

class ContaBancaria : IContaBancaria
{
    private double saldo;
    private static int proximoNumero = 1;
    public int NumeroConta;
    public string Titular;

    public ContaBancaria(string titular)
    {
        Titular = titular;
        NumeroConta = proximoNumero++;
        saldo = 0;
    }

    public virtual void Depositar(double valor)
    {
        saldo += valor;
        Console.WriteLine($"Depósito de R${valor} realizado.\nSaldo atual: R${saldo}");
    }

    public virtual void Sacar(double valor) 
    { 
        if(valor > saldo)
        {
            Console.WriteLine("Saldo insuficiente.");
        }
        else
        {
            saldo -= valor;
            Console.WriteLine($"Saque de R${valor} realizado.\nSaldo atual: R${saldo}");
        }
    }

    public void MostrarSaldo()
    {
        Console.WriteLine($"Conta: {NumeroConta} | Titular: {Titular} | Saldo: R$ {saldo}");
    }
}

class ContaPoupanca : ContaBancaria
{
    public ContaPoupanca(string titular) : base(titular) { }

    public override void Depositar(double valor)
    {
        double bonus = valor * 0.01;
        base.Depositar(valor + bonus);
        Console.WriteLine($"Bônus de R$ {bonus} adicionado!");
    }
}

class ContaCorrente : ContaBancaria
{
    public ContaCorrente(string titular) : base(titular) { }
    
    public override void Sacar(double valor)
    {
        double taxa = 2.50;
        if(valor + taxa > 0)
        {
            base.Sacar(valor + taxa);
            Console.WriteLine($"Taxa de saque de R$ {taxa} aplicada");
        }
    }
}

class Banco
{
    private List<ContaBancaria> contas = new List<ContaBancaria>();

    public void CriarConta()
    {
        Console.Write("Digite o nome do titular: ");
        string titular = Console.ReadLine();

        Console.WriteLine(@$"
Escolha o tipo de conta 
1 - Corrente
2 - Poupança");
        Console.Write("> ");
        int tipo = int.Parse(Console.ReadLine());

        ContaBancaria novaConta = tipo == 1 ? new ContaCorrente(titular) : new ContaPoupanca(titular);

        contas.Add(novaConta);
        Console.WriteLine($"Conta {novaConta.NumeroConta} criada com sucesso!\n");
    }
}

class Program
{
    static void Main()
    {
        Banco banco = new Banco();
        int opcao;
        do
        {
            Console.WriteLine($@"
====== Sistema Bancário ======
1 - Criar Conta
2 - Depositar
3 - Sacar
4 - Listar Contas
0 - Sair");
            Console.Write("> ");
            opcao = int.Parse(Console.ReadLine());

            switch (opcao)
            {
                case 1:
                    banco.CriarConta();
                    break;
                default:
                    Console.WriteLine("Opção inválida!");
                    break;
            }
        } while (opcao != 0);
    }
}