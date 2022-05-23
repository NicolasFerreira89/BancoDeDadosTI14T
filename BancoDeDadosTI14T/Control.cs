using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoDeDadosTI14T
{
    class Control
    {
        DAO conexao; // CRIANDO A VARIAVEL CONEXAO \\
        public int opcao;
        public DateTime dtNascimento;
        public Control()
        {
            conexao = new DAO(); // INSATANCIANDO A VARIAVEL CONEXAO \\
            dtNascimento = new DateTime(); // 00/00/0000 \\
        } // FIM DO METODO CONSTRUTOR \\

        public void Menu()
        {
            Console.WriteLine("Escolha uma das opções abaixo: \n\n" +
                               "1. Cadastrar.\n" +
                               "2. Consultar Tudo.\n" +
                               "3. Consultar por Código.\n" +
                               "4. Atualizar.\n" +
                               "5. Excluir.\n"  +
                               "0. Sair.");
            opcao = Convert.ToInt32(Console.ReadLine());
        } // FIM DO MENU \\

        public void Executar()
        {
            Menu();
          do{ 
            switch (opcao)
            {
                case 1:
                    Console.WriteLine("Informe seu nome:");
                    string nome = Console.ReadLine();
                    Console.WriteLine("Informe seu telefone:");
                    string telefone = Console.ReadLine();
                    Console.WriteLine("Informe seu endereço");
                    string endereco = Console.ReadLine();
                    Console.WriteLine("Informe sua data de nascimento:");
                    dtNascimento = Convert.ToDateTime(Console.ReadLine());
                    conexao.Inserir(nome, telefone, endereco, dtNascimento);
                    break;

                case 2:
                    Console.WriteLine(conexao.ConsultarTudo());
                    break;

                case 3:
                    Console.WriteLine("Informe o código do cliente que deseja consultar.");
                    int codigo = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine(conexao.ConsultarTudo(codigo));
                    break;

                case 4:
                    // SOLICITAR OS CAMPOS QUE SERÃO ATUALIZADOS \\
                    Console.WriteLine("Informe o Campo que deseja atualizar: ");
                    string campo = Console.ReadLine();
                    Console.WriteLine("Informe o novo dado para esse campo: ");
                    string novoDado = Console.ReadLine();
                    Console.WriteLine("Informe o código do usuário que deseja atualizar: ");
                    codigo = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine(conexao.Atualizar(codigo, campo, novoDado));
                    break;

                case 5:
                    Console.WriteLine("Informe o codigo que deseja apagar");
                    codigo = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine(conexao.Deletar(codigo));
                    break;

                default:
                    Console.WriteLine("Código informado Inválido!");
                    break;
            } 
          } while (opcao != 0);
        }
    
    }// FIM DA CLASSE \\  
}// FIM DO PROJETO \\
