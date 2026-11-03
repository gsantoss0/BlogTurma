using System;
using MySql.Data.MySqlClient;

public class Conexao
{
    public MySqlConnection CriarConexao()
    {
        var connection = new MySqlConnection("Server=localhost;User ID=root;Password=root;Database=blogturma");
        
        return connection;
    }
}