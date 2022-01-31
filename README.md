# APICartaoDeCredito
![C# Badge](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)
![Visual Studio](https://img.shields.io/badge/Visual_Studio-5C2D91?style=for-the-badge&logo=visual%20studio&logoColor=white)
![.NET](https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)

The Credit Card Generator API it's actually my very first coding project, which was responsible for getting me into my first coding Job, at VaiVoa, a company which it's mission is to graduate high quality coders.  
It is a very simple, rawr, to the point API, keep in mind that it is the very first project I even made, so it has no exception treating and so on, nothing fancy.

# The Api
- In Memory Data Base
- C#
- EF Core

The API basically generates a random fake credit card number which is associated with a email. After is created, it displays de credit card created


# DataBase Set-Up
Startup.cs  
Creating a in memory database named "ClienteDb"

``` services.AddDbContext<ApiContex>(opt => opt.UseInMemoryDatabase("ClientesDb")); ```

APIContex.cs  
Using Fluent API to set the Primary Key of our database

```
protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Models.ClienteDados>()
                .HasKey(c => c.CardNumber);
        }
        public DbSet<ClienteDados> ClientesDb { get; set; }
```

Models.ClienteDados.cs  
The model I created with the objects that I need for the API  
Id - To identify the credit card. With the DataNotation to ensure that Id is my primary key.  
Email - Every credit card is linked to an email. The email is the way we have to identify the customer.  
CardNumber - Random generated credit card number.  

```
public class ClienteDados
    {
        [Key]
        
        public int Id { get; set; }
        public string Email { get; set; }
        
        public string CardNumber { get; set; }
    }
```

# Controller
ClienteDadosController  

At first in the controller we have the DataNotations that sets the route of the api, sets thats it´s actually a API Controller.  
Seconly, below, we have the dependency injection for our Context. That´s how we can access the context. (For this dependency to work you must add the service in the Startup.cs)
```
[Route("api/[controller]")]
    [ApiController]
    public class ClienteDadosController : ControllerBase
    {
        private readonly ApiContex _context;

        public ClienteDadosController(ApiContex context)
        {
            _context = context;
        }
 ```
 
 # GET METHOD
 
 Starting with the HTTP Methods, we have the GET method, where the API returns the list of credit card numbers link to the email given in the parameter.
 It has no exception treatment.
 
 ```
 [HttpGet("{email}")] //Definindo na rota que é possivel consultar o email apenas digitndo ele no navegador.
        public ActionResult<IEnumerable<ClienteDados>> GetEmail(string email)
        {
            var list = _context.ClientesDb.Where(a => a.Email.Equals(email)).Select(s => s.CardNumber).ToList();
            return Ok(list);
        }
```






