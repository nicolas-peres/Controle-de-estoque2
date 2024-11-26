using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Controle_de_estoque.Classes;
using MapaSala.Classes;

namespace Controle_de_estoque.DAO
{
    public class ProdutosDao
    {
        private string LinhaConexao = "Server=localhost;Database=AULA_DS;User Id=root;Password=";
        private MySqlConnection Conexao;
        public ProdutosDao()
        {
            Conexao = new MySqlConnection(LinhaConexao);
        }

       
        public DataTable PreencherComboBox()
        {
            DataTable dataTable = new DataTable();

            string query = "SELECT Id, Nome FROM Produto";

            using (MySqlConnection connection = new MySqlConnection(LinhaConexao))
            {
                MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection);

                try
                {
                    // Preenche o DataTable com os dados da consulta
                    adapter.Fill(dataTable);
                }
                catch (Exception ex)
                {
                    // Lida com erros, se necessário
                    throw new Exception("Erro ao acessar os dados: " + ex.Message);
                }
            }

            return dataTable;
        }
        public DataTable ObterProdutos()
        {
            DataTable dt = new DataTable();
            Conexao.Open();
            string query = "SELECT Id, Nome, Descricao, Quantidade,Preco FROM Professores Order by Id desc";
            MySqlCommand comando = new MySqlCommand(query, Conexao);

            MySqlDataReader Leitura = comando.ExecuteReader();

            foreach (var atributos in typeof(Produto).GetProperties())
            {
                dt.Columns.Add(atributos.Name);
            }

            if (Leitura.HasRows)
            {
                while (Leitura.Read())
                {
                    Produto p = new Produto();
                    p.Id = Convert.ToInt32(Leitura[0]);
                    p.Nome = Leitura[1].ToString();
                    p.Descricao = Leitura[2].ToString();
                    p.Preco = Leitura[3].ToString();
                    dt.Rows.Add(p.Linha());
                }
            }
            Conexao.Close();
            return dt;
        }
        public DataTable Pesquisar(string pesquisa)
        {
            DataTable dt = new DataTable();
            Conexao.Open();
            string query = "";
            if (string.IsNullOrEmpty(pesquisa))
            {
                query = "SELECT Id, Nome, Descricao, Quantidade,Preco FROM Produtos Order by Id desc";
            }
            else
            {
                query = "SELECT Id, Nome, Descricao, Quantidade,Preco FROM Produtos Where Nome like '%" + pesquisa + "%' Order by Id desc";
            }
            MySqlCommand comando = new MySqlCommand(query, Conexao);

            MySqlDataReader Leitura = comando.ExecuteReader();

            foreach (var atributos in typeof(Produto).GetProperties())
            {
                dt.Columns.Add(atributos.Name);
            }

            if (Leitura.HasRows)
            {
                while (Leitura.Read())
                {
                    Produto p = new Produto();
                    p.Id = Convert.ToInt32(Leitura[0]);
                    p.Nome = Leitura[1].ToString();
                    p.Descricao = Leitura[2].ToString();
                    p.Preco = Leitura[3].ToString();
                    dt.Rows.Add(p.Linha());
                }
            }
            Conexao.Close();
            return dt;
        }

        public void Inserir(Produto p)
        {
            try
            {
                Conexao.Open();
                string query = "INSERT INTO Produto (Nome, Descricao, Preco) VALUES (@nome, @descricao, @preco)";
                MySqlCommand comando = new MySqlCommand(query, Conexao);

                comando.Parameters.Add(new MySqlParameter("@nome", p.Nome));
                comando.Parameters.Add(new MySqlParameter("@descricao", p.Descricao)) ;
                comando.Parameters.Add(new MySqlParameter("@preco", decimal.Parse(p.Preco)));

                comando.ExecuteNonQuery();

               // MessageBox.Show("Produto inserido com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
               // MessageBox.Show("Erro ao inserir produto: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Conexao.Close();
            }
        }


    }
}
