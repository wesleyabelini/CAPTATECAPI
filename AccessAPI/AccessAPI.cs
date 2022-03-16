using Entity;
using Newtonsoft.Json;
using System.Configuration;
using System.Net;
using System.Text;

namespace AccessAPI
{
    public class AccessAPI
    {
        private string apicliente = ConfigurationManager.AppSettings["apicliente"];
        private string apisituacaocli = ConfigurationManager.AppSettings["apisituacaocli"];
        private string apitipocli = ConfigurationManager.AppSettings["apitipocli"];

        #region API Cliente
        public async Task<List<Cliente>> ReceiveAllCliente()
        {
            using (HttpClient client = new HttpClient())
            {
                string response = await client.GetStringAsync(apicliente);
                return JsonConvert.DeserializeObject<List<Cliente>>(response);
            }
        }

        public async Task<Cliente> ReceiveACliente(string cpf)
        {
            using (HttpClient client = new HttpClient())
            {
                string response = await client.GetStringAsync(apicliente + "/" + cpf);
                return JsonConvert.DeserializeObject<List<Cliente>>(response).First();
            }    
        }

        public async Task<HttpStatusCode> InsertAClient(Cliente cli)
        {
            using (HttpClient client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(cli);
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(apicliente, data);
                return response.StatusCode;
            }   
        }

        public async Task<HttpStatusCode> UpdateAClient(Cliente cli)
        {
            using (HttpClient client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(cli);
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PutAsync(apicliente + "/" + cli.Clienteid, data);
                return response.StatusCode;
            }     
        }

        public async Task<HttpStatusCode> DeleteAClient(Cliente cli)
        {
            using(HttpClient client = new HttpClient())
            {
                var response = await client.DeleteAsync(apicliente + "/" + cli.Cpf);
                return response.StatusCode;
            } 
        }

        #endregion

        #region Outras API

        public async Task<List<SituacaoCliente>> ReceiveAllSituacaoCliente()
        {
            using (HttpClient client = new HttpClient())
            {
                string response = await client.GetStringAsync(apisituacaocli);
                return JsonConvert.DeserializeObject<List<SituacaoCliente>>(response);
            }
        }

        public async Task<List<TipoCliente>> ReceiveAllTipoCliente()
        {
            using (HttpClient client = new HttpClient())
            {
                string response = await client.GetStringAsync(apitipocli);
                return JsonConvert.DeserializeObject<List<TipoCliente>>(response);
            }
        }

        #endregion
    }
}