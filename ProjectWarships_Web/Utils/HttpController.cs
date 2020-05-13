using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Authentication;
using System.Threading.Tasks;

namespace ProjectWarships_Web.Utils
{
	public class HttpController
	{
		private HttpClient _client;

		public HttpClient Client
		{
			get { return _client; }
			set { _client = value; }
		}


		public HttpController(Uri uri)
		{
			HttpClientHandler handler = new HttpClientHandler() { SslProtocols = SslProtocols.Tls12 };
			//la configuration de l'api étant sur le console et non en applicatif, le protocole ssl doit être en SSL12 (ssl 1.2)
			handler.ServerCertificateCustomValidationCallback = (request, cert, chain, errors) => { return true; };
			_client = new HttpClient(handler);

			_client.BaseAddress = uri;
			_client.DefaultRequestHeaders.Accept.Clear();
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
		
		}

	}
}
