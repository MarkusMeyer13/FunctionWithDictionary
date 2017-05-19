using System.Net;
using System.Text;

public static async Task<HttpResponseMessage> Run(HttpRequestMessage req, TraceWriter log)
{
    log.Info($"C# HTTP trigger function processed a request. RequestUri={req.RequestUri}");

    // parse query parameter
    string name = req.GetQueryNameValuePairs()
        .FirstOrDefault(q => string.Compare(q.Key, "name", true) == 0)
        .Value;

    // Get request body
    dynamic data = await req.Content.ReadAsAsync<object>();
    var carDataResults = new Dictionary<string, CarData>();
    carDataResults.Add("MyCar", new CarData());

    using (var client = new HttpClient())
    {
        HttpResponseMessage response = await client.PutAsync("http://localhost", new StringContent("Test", Encoding.UTF8, "application/json"));
    }
    // Set name to query string or body data
    name = name ?? data?.name;

    return name == null
        ? req.CreateResponse(HttpStatusCode.BadRequest, "Please pass a name on the query string or in the request body")
        : req.CreateResponse(HttpStatusCode.OK, "Hello " + name);
}

public class CarData
{
    public string Name = "Quattro";
}