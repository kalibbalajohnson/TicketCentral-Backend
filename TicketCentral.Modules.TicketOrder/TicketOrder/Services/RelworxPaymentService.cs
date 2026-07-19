using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;

namespace TicketCentral.Modules.Payments.Services;

public class RelworxPaymentService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;


    public RelworxPaymentService(
        HttpClient httpClient,
        IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
    }



    public async Task<RelworxPaymentResponse?> RequestPayment(
        string phone,
        decimal amount,
        string reference)
    {

        var apiKey =
            _configuration["Relworx:ApiKey"];


        var accountNo =
            _configuration["Relworx:AccountNo"];



        var payload = new
        {
            account_no = accountNo,

            reference = reference,

            msisdn = phone,

            currency = "UGX",

            amount = amount,

            description = "TicketCentral ticket purchase"
        };



        var json =
            JsonSerializer.Serialize(payload);



        var request =
            new HttpRequestMessage(
                HttpMethod.Post,
                "https://payments.relworx.com/api/mobile-money/request-payment"
            );


        request.Headers.Authorization =
            new AuthenticationHeaderValue(
                "Bearer",
                apiKey);


        request.Headers.Add(
            "Accept",
            "application/vnd.relworx.v2");


        request.Content =
            new StringContent(
                json,
                Encoding.UTF8,
                "application/json");



        var response =
            await _httpClient.SendAsync(request);



        var result =
            await response.Content.ReadAsStringAsync();



        return JsonSerializer.Deserialize<RelworxPaymentResponse>(
            result);
    }
}



public class RelworxPaymentResponse
{
    public bool Success { get; set; }

    public string Message { get; set; } = "";

    public string Internal_reference { get; set; } = "";
}