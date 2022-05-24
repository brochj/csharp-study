using BrochDataAccess.Models;
using Dapper;
using Microsoft.Data.SqlClient;

const string connectionString = "Server=localhost,1433;Database=broch;User ID=sa;Password=1q2w3e4r@#$;TrustServerCertificate=True";




using (var connection = new SqlConnection(connectionString))
{
    UpdateCategory(connection);
    ListCategories(connection);
}


static void ListCategories(SqlConnection connection)
{
    var categories = connection.Query<Category>("SELECT [Id], [Title] FROM [Category]");

    foreach (var category in categories)
    {
        Console.WriteLine($"{category.Id} - {category.Title}");
    }
}

static void CreateCategory(SqlConnection connection)
{
    var category = new Category();

    category.Id = Guid.NewGuid();
    category.Title = "Amazon AWS";
    category.Url = "amazon-aws";
    category.Summary = "AWS Cloud";
    category.Order = 1;
    category.Description = "Categoria destinada ao servicos do AWS";
    category.Featured = true;

    // NUNCA CONCATENE STRING, POIS FACILITA SQL INJECTION
    var insertSql = @"INSERT INTO 
    [Category] 
VALUES(
    @abacate, 
    @Title, 
    @Url, 
    @Summary, 
    @Order, 
    @Description, 
    @Featured
    )";

    var rows = connection.Execute(insertSql, new
    {
        abacate = category.Id, // abacate é só para exemplificar que pode ser qualquer nome
        category.Title, // mas se o nome for igual, não precisa atribuir.
        category.Url,
        category.Summary,
        category.Order,
        category.Description,
        category.Featured,
    });

    Console.WriteLine($"{rows} linha(s) inserida(s)");
}

static void UpdateCategory(SqlConnection connection)
{
    var updateQuery = "UPDATE [Category] SET [Title]=@title WHERE [Id]=@id";
    int rows = connection.Execute(updateQuery, new
    {
        id = new Guid("06d73e6b-315f-4cfc-b462-f643e1a50e97"),
        title = "Mobile 2022"
    });

    // Se nao encontrar o id no banco, rows vai retornar 0 (ZERO)
    Console.WriteLine($"{rows} registros atualizados");
}

