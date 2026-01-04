using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json; // Se precisar de configurações personalizadas

namespace Microservice.Web.Utils
{
    public static class HttpClientExtensions
    {
        private static MediaTypeHeaderValue contentTipe = new MediaTypeHeaderValue("application/json");

        //public static async Task<T> ReadContentAsImproved<T>(
        //this HttpResponseMessage response)
        //{
        //    // 1. Lançamento de Exceção para Erros
        //    // EnsureSuccessStatusCode() lança um HttpRequestException se IsSuccessStatusCode for false.
        //    // Ele inclui o código de status e a razão, sendo mais informativo.
        //    response.EnsureSuccessStatusCode();

        //    // 2. Deserialização Otimizada e Idiomática
        //    // Usa ReadFromJsonAsync, que é mais rápido e trata streams diretamente (evitando
        //    // a leitura de toda a string para a memória).

        //    // Configurações de Deserialização (opcional: se necessário, defina um Default)
        //    var options = new JsonSerializerOptions
        //    {
        //        PropertyNameCaseInsensitive = true // Mantém a insensibilidade a maiúsculas/minúsculas
        //    };

        //    // O método ReadFromJsonAsync trata a leitura do Content Stream e a Deserialização
        //    var result = await response.Content.ReadFromJsonAsync<T>(options)
        //        .ConfigureAwait(false);

        //    // 3. Tratamento de Conteúdo Vazio ou Nulo
        //    // Retorna o resultado ou lança uma exceção se o resultado da deserialização for null, 
        //    // indicando um problema com o corpo da resposta ou tipo 'T'.
        //    return result ??
        //           throw new JsonException($"A deserialização para o tipo {typeof(T).Name} resultou em null.");
        //}


        public static async Task<T> ReadContentAs<T>(
            this HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
                throw new ArgumentException($"Something went wrong calling the API: " +
                                            $"{response.ReasonPhrase}");

            //Converter para JSON
            var dataAsString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            return JsonSerializer.Deserialize<T>(dataAsString, new JsonSerializerOptions{PropertyNameCaseInsensitive = true });
        }


        public static Task<HttpResponseMessage> PostAsJson<T>(this HttpClient httpClient, string url, T data)
        {
            var dataAsString = JsonSerializer.Serialize(data);
            var content = new StringContent(dataAsString);
            content.Headers.ContentType = contentTipe;
            return httpClient.PostAsJson(url, content);
        }

        public static Task<HttpResponseMessage> PutAsJson<T>(this HttpClient httpClient, string url, T data)
        {
            var dataAsString = JsonSerializer.Serialize(data);
            var content = new StringContent(dataAsString);
            content.Headers.ContentType = contentTipe;
            return httpClient.PutAsJson(url, content);
        }
    }
}
