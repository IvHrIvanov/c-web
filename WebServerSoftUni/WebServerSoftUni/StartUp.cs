using WebServerSoftUni.HTTP;
using WebServerSoftUni.Responses;
using WebServerSoftUni.Server;

string HtmlForm = @"<form action='/HTML' method='POST'>
   Name: <input type='text' name='Name'/>
   Age: <input type='number' name ='Age'/>
<input type='submit' value ='Save' />
</form>";

string DownloadForm = @"<form action='/Content' method='POST'>
   <input type='submit' value ='Download Sites Content' /> 
</form>";

string FileName = "content.txt";
await DownloadSitesAsTextFile(FileName, new string[] { "https://judge.softuni.org", "https://softuni.org" });


var server = new HttpServer(routse => routse
             .MapGet("/", new TextResponse("Hello from the server!"))
             .MapGet("/HTML", new HtmlResponse(HtmlForm))
             .MapPost("/HTML", new TextResponse("", AddFormDataAction))
             .MapGet("/Content", new HtmlResponse(DownloadForm))
             .MapPost("/Content", new TextFileResponse(FileName)));

await server.Start();

static void AddFormDataAction(Request request, Response response)
{
    response.Body = "";
    foreach (var (key, value) in request.Form)  
    {
        response.Body += $"{key} - {value}";
        response.Body += Environment.NewLine;
    }
}

async Task<string> DownloadWebSiteContent(string url)
{
    var httpClient = new HttpClient();
    using (httpClient)
    {
        var response = await httpClient.GetAsync(url);
        var html = await response.Content.ReadAsStringAsync();
        return html.Substring(0, 2000);
    }
}

async Task DownloadSitesAsTextFile(string fileName, string[] urls)
{
    var downloads = new List<Task<string>>();
    foreach (var url in urls)
    {
        downloads.Add(DownloadWebSiteContent(url));
    }
    var responses = await Task.WhenAll(downloads);

    var responseString = string.Join(Environment.NewLine + new String('-', 100),
        responses);
    await File.WriteAllTextAsync(fileName, responseString);
}