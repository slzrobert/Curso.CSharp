using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RestSharp;
using RestSharp.Authenticators.OAuth2;
using RestSharp.Serializers;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Curso.CSharp.ConsoleTeste
{
    public class Program
    {
        public static void Main(string[] args)
        {


            MapperTeste();

            Console.ReadKey();

            /////
            var loggerFectory = LoggerFactory.Create(builder =>
            {
                builder.AddDebug();
                builder.AddFilter("System", LogLevel.Information);
            });
            
            var logger = loggerFectory.CreateLogger<Program>();

            logger.LogInformation("Você conseguiu configurar Logger.");


            DateTime data = new DateTime(2022, 3, 21, 12, 56, 28);

            Console.WriteLine(data.Date);

            Console.WriteLine(data.Date.AddDays(1).AddSeconds(-1));
            
            Console.ReadKey();

            var sexo = "masculíno";

            Console.WriteLine(sexo.Equals(SexoEnum.Feminino.ToString()));

            Enum.TryParse<SexoEnum>(sexo, true, out var s);

            var cliente = new Cliente( s );

            Console.WriteLine(cliente.ToString());

            var arg0 = args[0];

            // Cliente Cadastro
            // ID: 1077511950-fCtGmsC3T3rsQr
            var clientCadastro = new Client
            {
                Email = "slzrobert@gmail.com",
                Address = new Address
                {
                    Id = "1",
                    StreetName = "Rua 15 Unidade 101 Cidade Operaria",
                    StreetNumber = 5,
                    ZipCode = "65058029"
                },
                DateRegistered = "2022-02-20",
                DefaultAddress = "Home",
                DefaultCard = null,
                Description = "Cliente Teste Integração API",
                FirstName = "Robert",
                LastName = "Pereira",
                Identification = new Identification
                {
                    Number = "00000000000",
                    Type = "CPF"
                },
                Phone = new Phone
                {
                    AreaCode = "098",
                    Number = "32470535"
                }
            };

            var client = new RestClient("https://api.mercadopago.com/v1")
            {
                Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(
                    "TEST-659824150156706-022012-081ccaddcde7979f0a57d948ed539b4e-6934133",
                    "Bearer"
                )
            };

            //CadastrarCliente(clientCadastro, client);
            //EditarCliente(clientCadastro, client);
            //TiposDeDocumento(client);
            //MeiosDePagamento(client);


            Console.ReadKey();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            //we will configure logging here
            services.AddLogging(configure =>
            {
                configure.AddConsole();
            });
        }

        private static void EditarCliente(Client clientCadastro, RestClient client)
        {
            var request = new RestRequest("customers/1077511950-fCtGmsC3T3rsQr", Method.Put);
            request.AddJsonBody(clientCadastro);
            var response = client.ExecuteAsync(request);
            Conteudo(response);
        }

        private static void CadastrarCliente(Client clientCadastro, RestClient client)
        {
            var request = new RestRequest("customers", Method.Post);
            
            // Serializar Cadastro do Cliente Para Json
            var jsonString = JsonSerializer.Serialize(clientCadastro);
            request.AddBody(jsonString, ContentType.Json);
            
            var response = client.ExecuteAsync(request);
            Conteudo(response);
        }

        private static void TiposDeDocumento(RestClient client)
        {
            Console.WriteLine("************ TIPOS DE DOCUMENTO ************\n");
            var request = new RestRequest("identification_types");
            var response = client.ExecuteAsync(request);
            Conteudo(response);
        }

        private static void MeiosDePagamento(RestClient client)
        {
            Console.WriteLine("********* OBTER MEIOS DE PAGAMENTO *********\n");
            var request = new RestRequest("payment_methods");
            var response = client.GetAsync(request);
            Conteudo(response);
        }

        private static void Conteudo(Task<RestResponse> response)
        {
            Console.WriteLine($"Status da Requisição {response.Result.StatusCode}\n");

            if (response.Result.IsSuccessful)
            {
                Console.WriteLine(response.Result.Content);
            }

            Console.WriteLine("\n********************************************\n");
        }

        public static void MapperTeste()
        {
            var config = new MapperConfiguration(cfg =>
            {
                
            });

            var mapper = config.CreateMapper();

            var carregamentos = new List<Carregamento>
            {
                new Carregamento
                {
                    Ticket = "C1",
                    NotaFiscal = Guid.NewGuid()
                },
                new Carregamento
                {
                    Ticket = "C1",
                    NotaFiscal = Guid.NewGuid()
                },
                new Carregamento
                {
                    Ticket = "C2",
                    NotaFiscal = Guid.NewGuid()
                },
                new Carregamento
                {
                    Ticket = "C3",
                    NotaFiscal = Guid.NewGuid()
                },
                new Carregamento
                {
                    Ticket = "C3",
                    NotaFiscal = Guid.NewGuid()
                },
                new Carregamento
                {
                    Ticket = "C3",
                    NotaFiscal = Guid.NewGuid()
                },
                new Carregamento
                {
                    Ticket = "C4",
                    NotaFiscal = Guid.NewGuid()
                }
            };


            var dto = mapper.Map<IEnumerable<CarregamentoDto>>(carregamentos);

            Console.Write(JsonSerializer.Serialize(dto));

        }
    }

    public class Phone
    {
        [JsonPropertyName("area_code")]
        public string AreaCode { get; set; }

        [JsonPropertyName("number")]
        public string Number { get; set; }
    }

    public class Identification
    {
        [JsonPropertyName("type")] 
        public string Type { get; set; }

        [JsonPropertyName("number")]
        public string Number { get; set; }
    }

    public class Address
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("zip_code")]
        public string ZipCode { get; set; }

        [JsonPropertyName("street_name")]
        public string StreetName { get; set; }

        [JsonPropertyName("street_number")]
        public int StreetNumber { get; set; }
    }

    public class Client
    {
        [JsonPropertyName("email")]
        [JsonIgnore]
        public string Email { get; set; }

        [JsonPropertyName("first_name")]
        public string FirstName { get; set; }

        [JsonPropertyName("last_name")]
        public string LastName { get; set; }

        [JsonPropertyName("phone")]
        public Phone Phone { get; set; }

        [JsonPropertyName("identification")]
        public Identification Identification { get; set; }

        [JsonPropertyName("default_address")]
        public string DefaultAddress { get; set; }

        [JsonPropertyName("address")]
        public Address Address { get; set; }

        [JsonPropertyName("date_registered")]
        public string DateRegistered { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("default_card")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string DefaultCard { get; set; }
    }


    public class Carregamento
    {
        public string Ticket { get; set; }
        public Guid NotaFiscal { get; set; }
    }

    public class CarregamentoDto
    {
        public string Ticket { get; set; }
        public IEnumerable<NotaFiscalDto> Notas { get; set; }
    }

    public class NotaFiscalDto
    {
        public Guid NotaFiscal { get; set; }
    }

}
