using BrochDataAccess.Models;
using Dapper;
using Microsoft.Data.SqlClient;

const string connectionString = "Server=localhost,1433;Database=broch;User ID=sa;Password=1q2w3e4r@#$;TrustServerCertificate=True";




using (var connection = new SqlConnection(connectionString))
{
    // CreateManyCategory(connection);
    // UpdateCategory(connection);

    // ListCategories(connection);
    // ExecuteStoredProcedure(connection);
    // ExecuteReadStoredProcedure(connection);
    // ExecuteScalar(connection);
    // ReadView(connection);
    // OneToOne(connection);
    // OneToMany(connection);
    // QueryMultiple(connection);
    // SelectIn(connection);
    // Like(connection);
    RunTransaction(connection);
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

static void DeleteCategory(SqlConnection connection)
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

static void CreateManyCategory(SqlConnection connection)
{
    var category = new Category();

    category.Id = Guid.NewGuid();
    category.Title = "Amazon AWS";
    category.Url = "amazon-aws";
    category.Summary = "AWS Cloud";
    category.Order = 1;
    category.Description = "Categoria destinada ao servicos do AWS";
    category.Featured = true;

    var category2 = new Category();

    category2.Id = Guid.NewGuid();
    category2.Title = "Categoria Nova";
    category2.Url = "categoria-nova";
    category2.Summary = "categoria nova";
    category2.Order = 2;
    category2.Description = "Categoria destinada ao servicos do AWS";
    category2.Featured = true;

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

    var rows = connection.Execute(insertSql, new[]{
        new {
            abacate = category.Id,
            category.Title,
            category.Url,
            category.Summary,
            category.Order,
            category.Description,
            category.Featured,

        },
        new {
            abacate = category2.Id,
            category2.Title,
            category2.Url,
            category2.Summary,
            category2.Order,
            category2.Description,
            category2.Featured,

        }
    });

    Console.WriteLine($"{rows} linha(s) inserida(s)");
}

static void ExecuteStoredProcedure(SqlConnection connection)
{
    var procedure = "spDeleteStudent";

    var param = new { StudentId = "79b82071-80a8-4e78-a79c-92c8cd1fd052" };

    var affectedRows = connection.Execute(procedure, param, commandType: System.Data.CommandType.StoredProcedure);

    Console.WriteLine($"{affectedRows} linha(s) inserida(s)");
}

static void ExecuteReadStoredProcedure(SqlConnection connection)
{
    var procedure = "spGetCoursesByCategory";

    var param = new { CategoryId = "af3407aa-11ae-4621-a2ef-2028b85507c4" };

    var courses = connection.Query(procedure, param, commandType: System.Data.CommandType.StoredProcedure);
    // IEnumerable<Courses> courses = connection.Query<Courses>(); // O IDEAL é Criar um Tipo Courses

    foreach (var item in courses)
    {
        Console.WriteLine(item.Title);
    }
}

static void ExecuteScalar(SqlConnection connection)
{
    var category = new Category();

    category.Title = "Amazon AWS";
    category.Url = "amazon-aws";
    category.Summary = "AWS Cloud";
    category.Order = 1;
    category.Description = "Categoria destinada ao servicos do AWS";
    category.Featured = true;

    // inserted.[Id] Diz qual campo sera retornar no ExecuteScalar()
    var insertSql = @"
    INSERT INTO 
        [Category] 
    OUTPUT inserted.[Id]
    VALUES(
        NEWID(), 
        @Title, 
        @Url, 
        @Summary, 
        @Order, 
        @Description, 
        @Featured
        )";

    var id = connection.ExecuteScalar<Guid>(insertSql, new
    {
        category.Title, // mas se o nome for igual, não precisa atribuir.
        category.Url,
        category.Summary,
        category.Order,
        category.Description,
        category.Featured,
    });

    Console.WriteLine($"O id do elemento inserido é: {id}");
}

static void ReadView(SqlConnection connection)
{
    var sql = "SELECT * FROM [vwCourses]";

    var courses = connection.Query(sql);

    foreach (var course in courses)
    {
        Console.WriteLine($"{course.Id} - {course.Title}");
    }
}

static void OneToOne(SqlConnection connection)
{
    var sql = @"
        SELECT * FROM [CareerItem] 
        INNER JOIN 
            [Course] ON [CareerItem].[CourseId] = [Course].[Id]";

    var items = connection.Query<CareerItem, Course, CareerItem>(
        sql,
        (carrerItem, course) =>
        {
            carrerItem.Course = course;
            return carrerItem;
        },
        splitOn: "Id");

    foreach (var item in items)
    {
        Console.WriteLine($"{item.Course.Title} - {item.Title}");
    }
}

static void OneToMany(SqlConnection connection)
{
    var sql = @"
        SELECT
            [Career].[Id],
            [Career].[Title],
            [CareerItem].[CareerId],
            [CareerItem].[Title]
        FROM
            [Career]
        INNER JOIN
            [CareerItem] ON [CareerItem].[CareerId] = [Career].[Id]
        ORDER BY
            [Career].[Title]
        ";
    var careers = new List<Career>();
    var items = connection.Query<Career, CareerItem, Career>(
        sql,
        (career, item) =>
        {
            var car = careers.Where(x => x.Id == career.Id).FirstOrDefault();
            if (car == null)
            {
                car = career;
                car.Items.Add(item);
                careers.Add(car);
            }
            else
            {
                car.Items.Add(item);
            }

            return career;
        },
        splitOn: "CareerId");

    foreach (var career in items)
    {
        Console.WriteLine($"{career.Title}");

        foreach (var item in career.Items)
        {
            Console.WriteLine($" - {item.Title}");

        }
    }
}

static void QueryMultiple(SqlConnection connection)
{
    var query = "SELECT * FROM [Category]; SELECT * FROM [Course]";

    using (var multi = connection.QueryMultiple(query))
    {
        var categories = multi.Read<Category>();
        var courses = multi.Read<Course>();

        foreach (var item in categories)
        {
            Console.WriteLine(item.Title);
        }
        foreach (var item in courses)
        {
            Console.WriteLine(item.Title);
        }
    }
}

static void SelectIn(SqlConnection connection)
{
    var query = @"SELECT * FROM [Career] 
                    WHERE [Id] IN @Id";
    var items = connection.Query<Career>(query, new
    {
        Id = new[]{
            "01ae8a85-b4e8-4194-a0f1-1c6190af54cb",
            "e6730d1c-6870-4df3-ae68-438624e04c72"
        }
    });

    foreach (var item in items)
    {
        Console.WriteLine(item.Title);
    }

}

static void Like(SqlConnection connection)
{
    var term = "api";
    var query = @"SELECT * FROM [Course] 
                    WHERE [Title] LIKE @exp";
    var items = connection.Query<Course>(query, new
    {
        exp = $"%{term}%"
    });

    foreach (var item in items)
    {
        Console.WriteLine(item.Title);
    }

}

static void RunTransaction(SqlConnection connection)
{
    var category = new Category();

    category.Id = Guid.NewGuid();
    category.Title = "Categoria que não quero salvar";
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

    connection.Open();

    using (var transaction = connection.BeginTransaction())
    {
        var rows = connection.Execute(insertSql, new
        {
            abacate = category.Id, // abacate é só para exemplificar que pode ser qualquer nome
            category.Title, // mas se o nome for igual, não precisa atribuir.
            category.Url,
            category.Summary,
            category.Order,
            category.Description,
            category.Featured,
        },
        transaction
        );

        // transaction.Commit(); // vai salvar as alterações
        transaction.Rollback(); // vai inserir e depois desfazer as alterações

        Console.WriteLine($"{rows} linha(s) inserida(s)");

    }

}

