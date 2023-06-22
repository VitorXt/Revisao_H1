using ConsoleApp1;
using System.Text;

string baseUrl = "http://localhost:3000/aluno";

while (true)
{
    Console.WriteLine("Selecione uma opção:");
    Console.WriteLine(" ");
    Console.WriteLine("1 - Registrar novo aluno");
    Console.WriteLine("2 - Sair");
    Console.WriteLine(" ");
    Console.Write("Opção: ");
    string opcao = Console.ReadLine();

    if (opcao == "1")
    {
        await RegistrarNovoAluno(baseUrl);
    }
    else if (opcao == "2")
    {
        break;
    }
    else
    {
        Console.WriteLine("Opção inválida. Por favor, tente novamente.");
    }

    Console.WriteLine();
}

await GetAluno(baseUrl);

static async Task RegistrarNovoAluno(string baseUrl)
{
    Aluno novoAluno = new Aluno();

    Console.Write("Digite o nome do aluno: ");
    novoAluno.Nome = Console.ReadLine();

    Console.Write("Digite a idade do aluno: ");
    int idade;
    if (int.TryParse(Console.ReadLine(), out idade))
    {
        novoAluno.Idade = idade;
    }
    else
    {
        Console.WriteLine("Idade inválida. O aluno será registrado com idade zero.");
        novoAluno.Idade = 0;
    }

    Console.Write("Digite o RA do aluno: ");
    novoAluno.RA = Console.ReadLine();

    await PostAluno(baseUrl, novoAluno);
}
async static Task GetAluno(string baseUrl)
{
    using (HttpClient client = new HttpClient())
    {
        try
        {
            HttpResponseMessage response = await client.GetAsync(baseUrl);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            List<Aluno> alunos = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Aluno>>(responseBody);

            foreach (var item in alunos)
            {
                //console
            }
        }
        catch (Exception)
        {

            throw;
        }
    }
}
async static Task PostAluno(string baseurl, Aluno novoAluno)
{
    using (HttpClient client = new HttpClient())

    {
        try
        {
            // Converter o objeto para JSON
            string jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(novoAluno);

            // Criar um conteúdo HTTP com o JSON
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            // Fazer a requisição POST para a URL da API
            HttpResponseMessage response = await client.PostAsync(baseurl, content);

            // Verificar se a requisição foi bem-sucedida
            if (response.IsSuccessStatusCode)
            {
                // Ler a resposta como uma string
                string responseBody = await response.Content.ReadAsStringAsync();

                // Fazer algo com a resposta da API
                Console.WriteLine(responseBody);
            }
            else
            {
                Console.WriteLine("A requisição não foi bem-sucedida. Código de status: " + response.StatusCode);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ocorreu um erro: " + ex.Message);
        }
    }
}
