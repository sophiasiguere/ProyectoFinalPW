using BolsasSiguereModel;
using Newtonsoft.Json;
using ProgramacionWeb_1057719_Project.Models;
using System.Text;

namespace ProgramacionWeb_1057719_Project.Functions
{
    public class APIServiceCotizaciones
    {
        public static int timeout = 30;
        public static string baseurl = "https://localhost:7073/api/Cotizacions";

        public static async Task<IEnumerable<Cotizacion>> GetCotizaciones()
        {
            HttpClientHandler clientHandler = new()
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            };

            HttpClient httpClient = new(clientHandler)
            {
                Timeout = TimeSpan.FromSeconds(timeout)
            };

            HttpResponseMessage response = await httpClient.GetAsync(baseurl);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<IEnumerable<Cotizacion>>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                throw new Exception(response.StatusCode.ToString());
            }
        }

        public static async Task<Cotizacion> PostCotizacion(Cotizacion object_to_serialize)
        {
            var json_ = Newtonsoft.Json.JsonConvert.SerializeObject(object_to_serialize);
            var content = new StringContent(json_, Encoding.UTF8, "application/json");

            HttpClientHandler clientHandler = new()
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            };

            HttpClient httpClient = new HttpClient(clientHandler)
            {
                Timeout = TimeSpan.FromSeconds(timeout)
            };
            HttpResponseMessage response = await httpClient.PostAsync($"{baseurl}", content);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)

            {
                return JsonConvert.DeserializeObject<Cotizacion>(await response.Content.ReadAsStringAsync());
            }

            else

            {
                throw new Exception(response.StatusCode.ToString());
            }
        }

        public static async Task<Cotizacion> GetCotizacion(int id)
        {

            HttpClientHandler clientHandler = new()
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            };

            HttpClient httpClient = new HttpClient(clientHandler)
            {
                Timeout = TimeSpan.FromSeconds(timeout)
            };
            HttpResponseMessage response = await httpClient.GetAsync($"{baseurl}/{id}");

            if (response.StatusCode == System.Net.HttpStatusCode.OK)

            {
                return JsonConvert.DeserializeObject<Cotizacion>(await response.Content.ReadAsStringAsync());
            }

            else

            {
                throw new Exception(response.StatusCode.ToString());
            }
        }
        public static async Task<Cotizacion> PutCotizacion(Cotizacion object_to_serialize, int id)
        {
            var json_ = Newtonsoft.Json.JsonConvert.SerializeObject(object_to_serialize);
            var content = new StringContent(json_, Encoding.UTF8, "application/json");

            HttpClientHandler clientHandler = new()
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            };

            HttpClient httpClient = new HttpClient(clientHandler)
            {
                Timeout = TimeSpan.FromSeconds(timeout)
            };
            HttpResponseMessage response = await httpClient.PutAsync($"{baseurl}/{id}", content);

            if (response.StatusCode == System.Net.HttpStatusCode.Accepted)

            {
                return JsonConvert.DeserializeObject<Cotizacion>(await response.Content.ReadAsStringAsync());
            }

            else

            {
                throw new Exception(response.StatusCode.ToString());
            }
        }

        public static async Task<Cotizacion> DeleteCotizacion(int id)
        {

            HttpClientHandler clientHandler = new()
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            };

            HttpClient httpClient = new HttpClient(clientHandler)
            {
                Timeout = TimeSpan.FromSeconds(timeout)
            };
            HttpResponseMessage response = await httpClient.DeleteAsync($"{baseurl}/{id}");

            if (response.StatusCode == System.Net.HttpStatusCode.OK)

            {
                return JsonConvert.DeserializeObject<Cotizacion>(await response.Content.ReadAsStringAsync());
            }

            else

            {
                throw new Exception(response.StatusCode.ToString());
            }
        }

    }
}

