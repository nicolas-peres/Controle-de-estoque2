using MapaSala.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Controle_de_estoque.Classes
{
    public class Produto
    {
        private SqlConnection Conexao = new SqlConnection("Server=LS05MPF;Database=AULA_DS;User Id=sa;Password=admsasql;");
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Preco { get; set; }




        public object[] Linha()
        {
            return new object[] { Id, Nome, Descricao, Preco, };
        }
        public void Inserir()
        {
            try
            {
                Conexao.Open();
                string query = "INSERT INTO Produto (Nome, Descricao, Preco) VALUES (@nome, @descricao, @preco)";
                SqlCommand comando = new SqlCommand(query, Conexao);

                comando.Parameters.Add(new SqlParameter("@nome", Nome));
                comando.Parameters.Add(new SqlParameter("@descricao", Descricao));
                comando.Parameters.Add(new SqlParameter("@preco", decimal.Parse(Preco)));

                comando.ExecuteNonQuery();

                MessageBox.Show("Produto inserido com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao inserir produto: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Conexao.Close();
            }
        }
        public DataTable ObterProdutos()
        {
            DataTable dataTable = new DataTable();

            try
            {
                Conexao.Open();
                string query = "SELECT Id, Nome, Descricao, Preco FROM Produto";

                SqlDataAdapter adapter = new SqlDataAdapter(query, Conexao);
                adapter.Fill(dataTable);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao obter produtos: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Conexao.Close();
            }

            return dataTable;
        }


        public void Excluir()
        {
            string query = "Delete from Usuarios WHERE  Id = @id";
            Conexao.Open();
            SqlCommand comando = new SqlCommand(query, Conexao);
            comando.Parameters.Add(new SqlParameter("@id", Id));
            int resposta = comando.ExecuteNonQuery();
            if (resposta == 1)
            {
                MessageBox.Show("Usuário Excluído com sucesso", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Erro ao excluir", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        public void Editar()
        {
            string query = "update Usuarios set Nome = @nome, Descricao = @descricao, Preco = @preco,   WHERE  Id = @id";
            Conexao.Open();
            SqlCommand comando = new SqlCommand(query, Conexao);
            comando.Parameters.Add(new SqlParameter("@nome", Nome));
            comando.Parameters.Add(new SqlParameter("@Descricao", Descricao));
            comando.Parameters.Add(new SqlParameter("@preco", Preco));

            comando.Parameters.Add(new SqlParameter("@id", Id));
            int resposta = comando.ExecuteNonQuery();
            if (resposta == 1)
            {
                MessageBox.Show("Usuário Atualizado com sucesso", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Erro ao atualizar", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void PesquisarPorId(int id)
        {
            DataTable dataTable = new DataTable();
            Conexao.Open();
            string query = "SELECT Id, Nome, Quantidade, Nome FROM Usuarios Where Id = @id Order by Id desc";
            SqlCommand Comando = new SqlCommand(query, Conexao);
            Comando.Parameters.AddWithValue("@id", id);
            SqlDataReader resultado = Comando.ExecuteReader();

            if (resultado.Read())
            {
                Id = resultado.GetInt32(0);
                Nome = resultado.GetString(1);

                Descricao = resultado.GetString(3);
                Preco = resultado.GetString(4);
            }

            Conexao.Close();
        }
       
    }
}
