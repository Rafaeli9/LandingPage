Para o projeto foram usados os seguintes pacote:

microsoft.entityframeworkcore\9.0.0\
microsoft.entityframeworkcore.design\9.0.0\
microsoft.entityframeworkcore.sqlserver\9.0.0\
microsoft.entityframeworkcore.sqlserver.design\1.1.6\
microsoft.entityframeworkcore.sqlserver.nettopologysuite\9.0.0\
microsoft.entityframeworkcore.tools\9.0.0\
swashbuckle.aspnetcore\7.0.0\
swashbuckle.aspnetcore.swagger\7.0.0\
swashbuckle.aspnetcore.swaggergen\7.0.0\
swashbuckle.aspnetcore.swaggerui\7.0.0\
system.text.json\9.0.0\
------------------------------------------------------------------//-------------------------------------------------------------

Configurar o appSettings.json para fazer a conexão com o banco adicionando:

  "ConnectionStrings": {
      "DefaultConnection": "Data source={Conforme banco no SQLServer};database={nome do Banco a ser criado};Trusted_connection=true; Encrypt=false; TrustServerCertificate=true"
  },

Configurar o banco de dados no program.cs

builder.Services.AddDbContext"<"ApplicationDbContext">" (options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

*(Tirar as aspas "" do < e > do ApplicationDbContext)

------------------------------------------------------------------//-------------------------------------------------------------

Para fazer as migration:

add-migration {nome da migration}

update-database

remove-migration (caso precise remover a migration feita)

------------------------------------------------------------------//-------------------------------------------------------------

Injeção de dependência no program.cs:

builder.Services.AddScoped<ILeadInterface, LeadService>();

builder.Services.AddScoped<IFiltroInterface, FiltroService>();

builder.Services.AddScoped<IAlunoInterface, AlunoService>();

------------------------------------------------------------------//-------------------------------------------------------------

Como sugestão, ao configurar e conectar o banco fazer as querys abaixo para testes:

    INSERT INTO Cursos (Descricao)
    VALUES 
        ('Administração'),
        ('Ciências Contábeis'),
        ('Direito');

	INSERT INTO Turmas (Descricao, CursoId) 
    VALUES 
      ('Turma A', 1), 
      ('Turma B', 2),
      ('Turma C', 3);
