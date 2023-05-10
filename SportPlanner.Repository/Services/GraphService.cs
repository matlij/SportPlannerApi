using Azure.Identity;
using Microsoft.Extensions.Options;
using Microsoft.Graph;
using Microsoft.Graph.Models;
using SportPlanner.ModelsDto;

namespace SportPlanner.Repository.Services
{
    public class GraphService : IGraphService
    {
        public GraphService(IOptions<ServcicePrincipalOptions> options)
        {
            if (options is null)
            {
                throw new ArgumentNullException(nameof(options));
            }
            if (string.IsNullOrEmpty(options.Value.ClientSecret))
            {
                throw new ArgumentException($"{nameof(options.Value.ClientSecret)} is null or empty");
            }
            if (string.IsNullOrEmpty(options.Value.ClientId))
            {
                throw new ArgumentException($"{nameof(options.Value.ClientId)} is null or empty");
            }
            if (string.IsNullOrEmpty(options.Value.TenantId))
            {
                throw new ArgumentException($"{nameof(options.Value.TenantId)} is null or empty");
            }
            Options = options.Value;
        }

        private ServcicePrincipalOptions Options { get; }

        public async Task<IEnumerable<UserDto>?> GetUsersAssignedToServicePrincipal(string principalId)
        {
            var graphClient = CreateClient();

            var result = await graphClient.ServicePrincipals[principalId].AppRoleAssignedTo.GetAsync();

            return result?.Value?.Select(x => new UserDto()
            {
                Name = x.PrincipalDisplayName ?? string.Empty,
                Id = x.Id ?? string.Empty
            });
        }

        public async Task<IEnumerable<UserDto>?> GetUsers()
        {
            var graphClient = CreateClient();

            var result = await graphClient.Users.GetAsync();

            return result?.Value?.Select(ToUserDto);
        }

        public async Task<UserDto?> UpdateUser(UserDto updatedUser)
        {
            var graphClient = CreateClient();

            var user = await graphClient.Users[updatedUser.Id].GetAsync();
            if (user is null)
            {
                throw new ArgumentException("Cannot find user with ID " + updatedUser.Id);
            }

            user.DisplayName = updatedUser.Name;
            var result = await graphClient.Users[updatedUser.Id].PatchAsync(user);

            return ToUserDto(result);
        }

        private static UserDto ToUserDto(User? result)
        {
            return new UserDto()
            {
                Name = result?.DisplayName ?? string.Empty,
                Id = result?.Id ?? string.Empty
            };
        }

        private GraphServiceClient CreateClient()
        {
            var scopes = new[] { "https://graph.microsoft.com/.default" };

            var options = new TokenCredentialOptions
            {
                AuthorityHost = AzureAuthorityHosts.AzurePublicCloud
            };

            var clientSecretCredential = new ClientSecretCredential(
                Options.TenantId, Options.ClientId, Options.ClientSecret, options);

            var graphClient = new GraphServiceClient(clientSecretCredential, scopes);
            return graphClient;
        }
    }
}