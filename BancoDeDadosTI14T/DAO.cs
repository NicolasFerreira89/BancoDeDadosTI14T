using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace BancoDeDadosTI14T
{
    class DAO
    {
        public MySqlConnection conexao;
        public string dados;
        public string comando;
        public string resultado;
        public int contador;
        public int i;
        public string msg;
        public string[] nome; // VETOR \\
        public string[] telefone; // VETOR \\
        public string[] endereco; // VETOR \\
        public DateTime[] data; // VETOR \\ 
        public int[] codigo; // VETOR \\
        public DAO()
        {
            // SCRIPT DE CONEXAO DE BANCO DE DADOS \\
            conexao = new MySqlConnection("server=localhost;DataBase=turma14;Uid=root;Password=;Convert Zero DateTime=True");
            try
            {
                conexao.Open(); // TENTANDO CONECTAR AO BD \\
                Console.WriteLine("Conectado com Sucesso!");
                
            }
            catch(Exception e)
            {
                Console.WriteLine("Algo deu Errado!\n\n" + e); // MOSTRAR O ERRO EM TELA \\
                conexao.Close(); // FECHAR A CONEXAO COM O BD \\     
            }
        } // FIM DO METODO CONSTRUTOR \\

        public void Inserir(string nome, string telefone, string endereco, DateTime dtNascimento)
        {
            try
            {

                // MODIFICAR A ESTRUTURA DE DATA \\
                MySqlParameter parameter = new MySqlParameter();
                parameter.ParameterName = "@Date";
                parameter.MySqlDbType = MySqlDbType.Date;
                parameter.Value = dtNascimento.Year + "-" + dtNascimento.Month + "-" + dtNascimento.Day;
                dados = "('','"+ nome + "','" + telefone + "','" + endereco + "','" + parameter.Value + "')";
                comando = "Insert into pessoa(codigo, nome, telefone, endereco, dataDeNascimento) values" + dados;
                // EXECUTAR O COMANDO DE INSERÇAO NO BD \\
                MySqlCommand sql = new MySqlCommand(comando, conexao);
                resultado = "" + sql.ExecuteNonQuery(); // EXECUTA O INSERT NO BD \\
                Console.WriteLine(resultado + "Linhas Afetadas");
            }
           
            catch(Exception e)
            {
                Console.WriteLine("Algo deu Errado!\n\n" + e);
                Console.ReadLine(); // MANTERO O PROMPT ABERTO \\
            }
        }// FIM DO METODO INSERIR \\ 

        
        public void PreencherVetor()
        {
            string query = "select * from pessoa"; // COLETAR OS DADOS DO BD \\

            // INSTANCIAR \\ 
            codigo = new int[100];
            nome = new string[100];
            telefone = new string[100];
            endereco = new string[100];
            data = new DateTime[100];

                for (i = 0; i < 100; i++)
                {
                    codigo[i] = 0;
                    nome[i] = "";
                    telefone[i] = "";
                    endereco[i] = "";
                    data[i] = new DateTime();
                }
                MySqlCommand coletar = new MySqlCommand(query, conexao); // CRIANDO O COMANDO PARA CONSULTAR NO BD \\

                // LEITURA DOS DADOS DO BANCO \\
                MySqlDataReader leitura = coletar.ExecuteReader();

                i = 0;
                contador = 0;
                while (leitura.Read())
                {
                    codigo[i] = Convert.ToInt32(leitura["codigo"]);
                    nome[i] = leitura["nome"] + "";
                    telefone[i] = leitura["telefone"] + "";
                    endereco[i] = leitura["endereco"] + ""; 
                    data[i] = Convert.ToDateTime(leitura["dataDeNascimento"]);
                    i++;
                    contador++;
                } // FIM DO WHILE \\

                // FECHAR LEITURA DE DADOS NO BANCO \\
                leitura.Close();
            
        } // FIM DO METODO DE PREENCHIMENTO O VETOR \\ 
            
        // METODO QUE CONSULTA TODOS OS DADOS NO BANCO DE DADOS \\
        public string ConsultarTudo()
        {
            // PREENCHENDO OS VETORES \\
            PreencherVetor();
            msg = "";

            for(i = 0;  i < contador; i++) 
            {
                msg += "  Código: " + codigo[i] +
                      ", Nome: " + nome[i] +
                      ", Telefone: " + telefone[i] +
                      ", Endereço: " + endereco[i] +
                      ", Data de Nascimento: " + data[i] +
                      "\n\n";

            } // FIM DO FOR \\

            return msg;
        } // FIM DO METODO CONSULTAR TUDO \\

        public string ConsultarTudo(int cod)
        {
            PreencherVetor();
            for(int i=0; i < contador; i++)
            {
                if(codigo[i] == cod)
                {
                    msg = "  Código: " + codigo[i] +
                     ", Nome: " + nome[i] +
                     ", Telefone: " + telefone[i] +
                     ", Endereço: " + endereco[i] +
                     ", Data de Nascimento: " + data[i] +
                     "\n\n";

                    return msg;
                }
            } // FIM DO FOR \\
            return "Código Informado não encontrado!";
        } // FIM DO CONSULTAR NOME \\

        public string Atualizar(int codigo, string campo, string novoDado)
        {
            try 
            {
               string query = "update pessoa set " + campo +  " = '" + novoDado + "'where codigo = '" + codigo +"'";
               // EXECUTAR O COMANDO \\ 
               MySqlCommand sql = new MySqlCommand(query, conexao);
               string resultado = "" + sql.ExecuteNonQuery();
                return resultado + "Linha Afetada";
            }
            catch(Exception e)
            {
                return "Algo deu Errado!\n\n" + e;
            }
        } // FIM DO METODO ATUALIZAR \\

        public string Deletar(int codigo)
        {
            try 
            { 
                 string query = "delete from pessoa where codigo = '" + codigo + "'";
                 MySqlCommand sql = new MySqlCommand(query, conexao);
                 string resultado = "" + sql.ExecuteNonQuery();
                 return resultado + "Linha Afetada";
            }
            catch(Exception e)
            {
                return "Algo deu errado!\n\n" + e;
            }
        } // FIM DO DELETAR \\
    } // FIM DA CLASSE \\ 

}// FIM DO PROJETO \\

