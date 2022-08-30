using RestSharp;
using RestSharp.Serializers.NewtonsoftJson;
using TeamDeskBot.Models;

namespace TeamDeskBot.Services;

public class ApiRequestsService
{
    private const int DEFAULT_TIMEOUT_IN_MINUTES = 30;
    private RestClient _restClient;
    private const string TEAM_DESK_API = "http://localhost:5005";
    private const string GET_USERS = "api/Users/GetUsers";
    private const string ADD_USER = "api/Users/AddUser";
    private const string DELETE_USER = "api/Users/DeleteUser";

    public ApiRequestsService()
    {
        TimeSpan timeout = TimeSpan.FromMinutes(DEFAULT_TIMEOUT_IN_MINUTES);
        HttpClient httpClient = new()
        {
            Timeout = timeout
        };
        _restClient = new RestClient(
            httpClient,
            new RestClientOptions(TEAM_DESK_API)
            {
                ThrowOnAnyError = true,
                ThrowOnDeserializationError = true
            }).UseNewtonsoftJson();
    }

    public async Task<IEnumerable<UserViewModel>> GetUsers()
    {
        RestRequest request = new($"{TEAM_DESK_API}/{GET_USERS}");
        IEnumerable<UserViewModel>? users = await _restClient.GetAsync<IEnumerable<UserViewModel>>(request);

        if (users is null)
        {
            //TODO: WORK -> custom exception
            throw new Exception("An error occured during the execution");;
        }
        
        return users;
    }

    public async Task<bool> AddUser(User user)
    {
        RestRequest request = new($"{TEAM_DESK_API}/{ADD_USER}");
        request.AddBody(user);
        RestResponse response = await _restClient.PostAsync(request);

        if (!response.IsSuccessful)
        {
            //TODO: WORK -> custom exception
            throw new Exception("An error occured during the execution");
        }
        
        return true;
    }

    public async Task DeleteUser(int id)
    {
        RestRequest request = new($"{TEAM_DESK_API}/{DELETE_USER}");
        request.AddParameter("id", id);
        RestResponse response = await _restClient.GetAsync(request);
        
        if (!response.IsSuccessful)
        {
            //TODO: WORK -> custom exception
            throw new Exception("An error occured during the execution");
        }
    }
}