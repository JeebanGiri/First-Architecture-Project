
using Microsoft.AspNetCore.Identity;
using StudentCRUD.Application;
using StudentCRUD.Domain;
using StudentCRUD.Insfrastructure;

//namespace StudentCRUD.Presentation
//{
//    public class Program
//    {
//        public static void Main(string[] args)
//        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddDbContext<ApplicationDBContext>();
            builder.Services.AddScoped<IStudentService, StudentService>();

                builder.Services.AddContr   ollers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            //Add Authentication
            builder.Services.AddAuthentication()
            .AddBearerToken(IdentityConstants.BearerScheme);

            builder.Services.AddAuthorizationBuilder();

            builder.Services.AddIdentity<StudentTask,IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDBContext>()
                .AddApiEndpoints();

            var app = builder.Build();

            // Seed roles into the database
            using (var scope = app.Services.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                // Define Roles
                string[] role = { "Admin", "Blogger" };

                foreach (string roleName in role)
                {
                    if (!await roleManager.RoleExistsAsync(roleName))
                    {
                        await roleManager.CreateAsync(new IdentityRole(roleName));

                    }
                }
            }

            app.MapIdentityApi<StudentTask>();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
//        }
//    }
//}
    