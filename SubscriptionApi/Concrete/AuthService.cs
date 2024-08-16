using SubscriptionApi.Abstraction;
using SubscriptionApi.Dtos.Request;
using SubscriptionApi.Dtos.Response;
using SubscriptionApi.Models;
using System;

public class AuthService : IAuthService
{
    private readonly IServiceRepository _serviceRepository;
    private readonly ITokenRepository _tokenRepository;
    private readonly IConfiguration _configuration;
    private readonly ILogger<AuthService> _logger;

    private string tokentime;
    private string SecretKey;
    public AuthService(IServiceRepository serviceRepository, IConfiguration configuration, ILogger<AuthService> logger)
    {
        _serviceRepository = serviceRepository;
        _configuration = configuration;
        tokentime = _configuration["AppSettings:TokenTime"];
        SecretKey = _configuration["AppSettings:SecretKey"];
        _logger = logger;
    }

    //public LoginResponse Login(LoginRequest request)
    //{
    //    var service = _serviceRepository.GetServiceByServiceId(request.ServiceId.ToString());
    //    if (service == null || !PasswordUtility.VerifyPassword(service.PasswordHash, request.Password))
    //    {
    //        return null;
    //    }

    //    if (service.Token == null || service.TokenExpiration <= DateTime.UtcNow)
    //    {
    //        service.Token = TokenUtility.GenerateToken(service.ServiceId, GetTokenExpirationFromConfig());
    //        service.TokenExpiration = DateTime.UtcNow.AddHours(GetTokenExpirationFromConfig());
    //        _serviceRepository.UpdateService(service);
    //    }

    //    return new LoginResponse
    //    {
    //        Token = service.Token
    //    };
    //}
    public LoginResponse Login(LoginRequest request)
    {
        try
        {
            var service = _serviceRepository.GetServiceByServiceId(request.ServiceId.ToString());
            if (service == null || !PasswordUtility.VerifyPassword(service.PasswordHash, request.Password))
            {
                return null; // Unauthorized
            }

            // Check if there's an existing valid token
             var existingToken = _serviceRepository.GetValidToken(service.ServiceId);
            if (existingToken == null)
            {
                // Generate a new token if no valid token exists
                string tokenValue = TokenUtility.GenerateToken(service.ServiceId, service.TokenExpirationHours, SecretKey);
                DateTime expiresOn = DateTime.UtcNow.AddHours(service.TokenExpirationHours);

                var newToken = new Token
                {
                    TokenId = Guid.NewGuid(),
                    ServiceId = service.ServiceId,
                    TokenValue = tokenValue,
                    ExpiresOn = expiresOn
                };

                _serviceRepository.AddToken(newToken);

                return new LoginResponse
                {
                    Token = tokenValue,
                    ExpiresOn = expiresOn
                };
            }

            // Return the existing token if it is still valid
            return new LoginResponse
            {
                Token = existingToken.TokenValue,
                ExpiresOn = existingToken.ExpiresOn
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            return new LoginResponse();
        }
       
    }

    private int GetTokenExpirationFromConfig()
    {
        // Fetch the expiration time from the config or database
        return 2; // Default to 2 hours
    }
    public string CreateService(CreateServiceRequest request)  
    {
        try
        {
            // Check if the service already exists  
            var existingService = _serviceRepository.GetServiceByServiceName(request.ServiceName);
            if (existingService != null)
            {
                throw new InvalidOperationException("Service ID already exists");
            }

            // Create new service instance  
            var newService = new Service
            {
                ServiceId = Guid.NewGuid().ToString(),
                ServiceName = request.ServiceName,
                PasswordHash = PasswordUtility.HashPassword(request.Password),
                TokenExpirationHours = Convert.ToInt32(tokentime)
            };

            // Add the new service and get the ID of the added service  
            return _serviceRepository.AddService(newService);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            throw new InvalidOperationException(ex.Message);
        }
        
    }  
}
