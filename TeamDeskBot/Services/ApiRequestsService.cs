using System.Text.Json;
using RestSharp;
using RestSharp.Serializers.NewtonsoftJson;
using TeamDeskBot.Exceptions;
using TeamDeskBot.Helpers;
using TeamDeskBot.Models;

namespace TeamDeskBot.Services;

public class ApiRequestsService
{
    private const int DEFAULT_TIMEOUT_IN_MINUTES = 30;
    private RestClient _restClient;
    private const string TEAM_DESK_API = "http://localhost:5005";
    private const string GET_USERS = "api/Users/GetUsers";
    private const string GET_USER = "api/Users/GetUser";
    private const string ADD_USER = "api/Users/AddUser";
    private const string DELETE_USER = "api/Users/DeleteUser";
    private const string EDIT_USER = "api/Users/EditUser";

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
                BaseUrl = new Uri(TEAM_DESK_API),
                ThrowOnAnyError = false,
                ThrowOnDeserializationError = false
            }).UseNewtonsoftJson();
    }

    public async Task<IEnumerable<UserViewModel>> GetUsers()
    {
        RestRequest request = new(GET_USERS);
        RestResponse response = await _restClient.GetAsync(request);
        IEnumerable<UserViewModel>? users = BotHelper.GetResult<IEnumerable<UserViewModel>>(response);

        if (users is null)
        {
            throw new GetUsersException( $"Users have value of null in method {nameof(GetUsers)}");
        }
        
        return users;
    }

    public async Task<User> GetUser(int id)
    {
        RestRequest request = new(GET_USER);
        request.AddParameter("userId", id);
        User? user = await _restClient.GetAsync<User>(request);

        if (user is null)
        {
            const string message = $"Null user at {nameof(User)}";
            throw new GetUserException(id, message);
        }
        
        return user;
    }

    public async Task<bool> AddUser(User user)
    {
        RestRequest request = new(ADD_USER);
        request.AddBody(user);
        RestResponse response = await _restClient.PostAsync(request);

        if (!response.IsSuccessful)
        {
            throw new AddUserException(response.StatusCode.ToString());
        }
        
        return true;
    }

    public async Task<bool> EditUser(User user)
    {
        RestRequest request = new(EDIT_USER);
        request.AddBody(user);
        RestResponse response = await _restClient.PostAsync(request);

        if (!response.IsSuccessful)
        {
            string message = $"The response had status {response.StatusCode}";
            throw new EditUserException(user.Id, message);
        }
        
        return true;
    }

    public async Task DeleteUser(int id)
    {
        RestRequest request = new(DELETE_USER);
        request.AddParameter("id", id);
        RestResponse response = await _restClient.GetAsync(request);
        
        if (!response.IsSuccessful)
        {
            string message = $"The response had status {response.ResponseStatus}";
            throw new DeleteUserException(id, message);
        }
    }
}