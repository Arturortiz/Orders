
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Orders.Frontend.Repositories
{
    public class Repository : IRepository
    {
        private readonly HttpClient _httpClient;
        private JsonSerializerOptions _jsonDefaultOptions => new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        public Repository(HttpClient httpClient) 
        {
            _httpClient = httpClient;
        }

        public async Task<HttpResponseWrapper<T>> GetAsync<T>(string url) //get - READ - listar los paises en el front
        {
            var responseHttp = await _httpClient.GetAsync(url);
            if(responseHttp.IsSuccessStatusCode)
            {
                var response = await UnserializeAnswer<T>(responseHttp);
                return new HttpResponseWrapper<T>(response, false, responseHttp); //esto creo que esta enlazado con el HttpResponseWrapper.cs 
                                                                                  //indicamos que hay objeto, que no hay error, y el responseHttp donde se almacena la salida
            }
            return new HttpResponseWrapper<T>(default, true, responseHttp);// indicamos que no hay objeto, que hay error y el responseHttp donde esta almacenado el tipo de error
        }

        public async Task<HttpResponseWrapper<object>> PostAsync<T>(string url, T model)//post - INSERT - el insert que no devuelve respuesta - cuando sea la salida noContent
        {
            var messageJson = JsonSerializer.Serialize(model);
            var messageContent = new StringContent(messageJson, Encoding.UTF8, "application/json");
            var responseHttp = await _httpClient.PostAsync(url, messageContent);
            return new HttpResponseWrapper<object>(null, !responseHttp.IsSuccessStatusCode, responseHttp);// indicamos el objeto a null pq no queremos devolverlo, el error indicamos que sea true cuando no sea SUSCESS por eso el !
        }

        public async Task<HttpResponseWrapper<TActionResponse>> PostAsync<T, TActionResponse>(string url, T model) //post - INSERT - el insert que devuelve respuesta 
        {
            var messageJson = JsonSerializer.Serialize(model);//serializamos el model que puede ser de cualquier Tipo, eso indica la T
            var messageContent = new StringContent(messageJson, Encoding.UTF8, "application/json");// codificamos el mensaje que hemos serializado a json segun utf8
            var responseHttp = await _httpClient.PostAsync(url, messageContent);//para hacer la insercion nos pide la url y que mensaje vas a insertar
            if (responseHttp.IsSuccessStatusCode)
            {
                var response = await UnserializeAnswer<TActionResponse>(responseHttp);
                return new HttpResponseWrapper<TActionResponse>(response, false, responseHttp);
            }
            return new HttpResponseWrapper<TActionResponse>(default, true, responseHttp);
        }

        private async Task<T> UnserializeAnswer<T>(HttpResponseMessage responseHttp)//deserializamos la salida del backend para poder visualizarlo en el front
        {
            var response = await responseHttp.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(response, _jsonDefaultOptions)!;//revisar la !
        }
    }
}
