﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Interfaces.Repositories;
using Interfaces.UOW;
using System.Web;
using Microsoft.Owin.Security;
using NLog.Internal;
using API_DAL.Repositories;
using Newtonsoft.Json.Linq;
using NLog;
using ConfigurationManager = System.Configuration.ConfigurationManager;

namespace API_DAL
{
    public class UOW : BaseIUOW, IDisposable
    {
        private readonly IAuthenticationManager _authenticationManager;

        private readonly IDictionary<Type, Func<HttpClient, IAuthenticationManager, object>> _repositoryFactories;

        private readonly IDictionary<Type, object> _repositories = new Dictionary<Type, object>();

        private readonly HttpClient _httpClient = new HttpClient();

        private readonly ILogger _logger = NLog.LogManager.GetCurrentClassLogger();

        public UOW(IAuthenticationManager authenticationManager)
        {
            _authenticationManager = authenticationManager;
            // initialize list of repo factories
            _repositoryFactories = GetCustomFactories();

            // set up httpclient
            var baseAddr = ConfigurationManager.AppSettings["WebApi_BaseUri"];
            if (string.IsNullOrWhiteSpace(baseAddr))
            {
                throw new KeyNotFoundException("WebApi_BaseUri not defined in config!");
            }
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.BaseAddress = new Uri(baseAddr);

        }

        private static IDictionary<Type, Func<HttpClient, IAuthenticationManager, object>> GetCustomFactories()
        {
            return new Dictionary<Type, Func<HttpClient, IAuthenticationManager, object>>
            {
                {typeof(IUserIntRepository), (httpClient, authenticationManager) => new UserIntRepository(httpClient, ConfigurationManager.AppSettings["WebApi_EndPoint_UsersInt"], authenticationManager)},
                    {typeof(IUserRoleIntRepository), (httpClient, authenticationManager) => new UserRoleIntRepository(httpClient, ConfigurationManager.AppSettings["WebApi_EndPoint_UserRolesInt"], authenticationManager)},
                    {typeof(IRoleIntRepository), (httpClient, authenticationManager) => new RoleIntRepository(httpClient, ConfigurationManager.AppSettings["WebApi_EndPoint_RolesInt"], authenticationManager)},
                    {typeof(IUserClaimIntRepository), (httpClient, authenticationManager) => new UserClaimIntRepository(httpClient, ConfigurationManager.AppSettings["WebApi_EndPoint_UserClaimsInt"], authenticationManager)},
                    {typeof(IUserLoginIntRepository), (httpClient, authenticationManager) => new UserLoginIntRepository(httpClient, ConfigurationManager.AppSettings["WebApi_EndPoint_UserLoginsInt"], authenticationManager)},


              
                {typeof (IVehicleRepository), (httpClient, authenticationManager) => new VehicleRepository(
                    httpClient,
                    ConfigurationManager.AppSettings["WebApi_EndPoint_Vehicles"],
                    authenticationManager)
                },
                {typeof (IBlogRepository), (httpClient, authenticationManager) => new BlogRepository(
                    httpClient,
                    ConfigurationManager.AppSettings["WebApi_EndPoint_Blogs"],
                    authenticationManager)
                },
                {typeof (IBlogPostRepository), (httpClient, authenticationManager) => new BlogPostRepository(
                    httpClient,
                    ConfigurationManager.AppSettings["WebApi_EndPoint_BlogPosts"],
                    authenticationManager)
                },
                {typeof (IMessageThreadRepository), (httpClient, authenticationManager) => new MessageThreadRepository(
                    httpClient,
                    ConfigurationManager.AppSettings["WebApi_EndPoint_Articles"],
                    authenticationManager)
                },
                {typeof (IMessageRepository), (httpClient, authenticationManager) => new MessageRepository(
                    httpClient,
                    ConfigurationManager.AppSettings["WebApi_EndPoint_Articles"],
                    authenticationManager)
                },
                {typeof (IMessageReceiverRepository), (httpClient, authenticationManager) => new MessageReceiverRepository(
                    httpClient,
                    ConfigurationManager.AppSettings["WebApi_EndPoint_Articles"],
                    authenticationManager)
                },
                {typeof (IMessageThreadReceiverRepository), (httpClient, authenticationManager) => new MessageThreadReceiverRepository(
                    httpClient,
                    ConfigurationManager.AppSettings["WebApi_EndPoint_Articles"],
                    authenticationManager)
                },
                {typeof (IUserBlogConnectionRepository), (httpClient, authenticationManager) => new UserBlogConnectionRepository(
                    httpClient,
                    ConfigurationManager.AppSettings["WebApi_EndPoint_Articles"],
                    authenticationManager)
                },

                    };
        }

        /// <summary>
        /// Returns repo instance by repo interface
        /// Repo is first searched from cache, if not found then its created new and stored into cache
        /// </summary>
        /// <typeparam name="TRepo">Repo interface</typeparam>
        /// <returns></returns>
        public TRepo GetRepository<TRepo>() where TRepo : class
        {
            var repo = GetWebApiRepo<TRepo>() as TRepo;
            if (repo == null)
            {
                throw new NotImplementedException("No repository for type, " + typeof(TRepo).FullName);
            }
            return repo;
        }

        private TRepo GetWebApiRepo<TRepo>() where TRepo : class
        {

            // Look for TRepo in dictionary cache under typeof(TRepo).
            object repo;
            _repositories.TryGetValue(typeof(TRepo), out repo);
            if (repo != null)
            {
                return (TRepo)repo;
            }

            return MakeRepository<TRepo>();
        }

        protected virtual TRepo MakeRepository<TRepo>() where TRepo : class
        {
            // repo factory (delegate), not yet initialized
            Func<HttpClient, IAuthenticationManager, object> repoFactory;

            // try to get factroy for this repo type
            _repositoryFactories.TryGetValue(typeof(TRepo), out repoFactory);
            if (repoFactory == null)
            {
                throw new NotImplementedException("No factory for repository type: " + typeof(TRepo).FullName);
            }

            // there should be user identity built up already at this moment
            if (HttpContext.Current?.Request.IsAuthenticated ?? false)
            {
                var identity = HttpContext.Current.User.Identity as ClaimsIdentity;
                var token = identity?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Authentication)?.Value;
                if (!string.IsNullOrWhiteSpace(token))
                {
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }
                _logger.Debug("Bearer token added to the httpclient header!");
            }

            // make the repo
            var repo = (TRepo)repoFactory(_httpClient, _authenticationManager);

            // save it to dictionary
            _repositories[typeof(TRepo)] = repo;

            //return it
            return repo;
        }


        public IMultiLangStringRepository MultiLangStrings => GetWebApiRepo<IMultiLangStringRepository>();
        public ITranslationRepository Translations => GetWebApiRepo<ITranslationRepository>();

        public IUserIntRepository UsersInt => GetWebApiRepo<IUserIntRepository>();
        public IUserRoleIntRepository UserRolesInt => GetWebApiRepo<IUserRoleIntRepository>();
        public IRoleIntRepository RolesInt => GetWebApiRepo<IRoleIntRepository>();
        public IUserClaimIntRepository UserClaimsInt => GetWebApiRepo<IUserClaimIntRepository>();
        public IUserLoginIntRepository UserLoginsInt => GetWebApiRepo<IUserLoginIntRepository>();

        /// <summary>
        /// Not used in Web API
        /// </summary>
        public void Commit()
        {

        }

        public void Dispose()
        {
        }

        public string GetWebApiToken(string userName, string password)
        {
            var bearerToken = "";
            using (var tokenHttpClient = new HttpClient())
            {
                var tokenAddr = ConfigurationManager.AppSettings["WebApi_TokenUri"];
                if (string.IsNullOrWhiteSpace(tokenAddr))
                {
                    throw new KeyNotFoundException("WebApi_TokenUri not defined in config!");
                }
                tokenHttpClient.BaseAddress = new Uri(tokenAddr);
                tokenHttpClient.DefaultRequestHeaders.Accept.Clear();
                tokenHttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var tokenContent = new StringContent($"grant_type=password&username={userName}&password={password}");

                tokenContent.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded")
                {
                    CharSet = "UTF-8"
                };

                var response = tokenHttpClient.PostAsync("", tokenContent).Result;

                if (response.IsSuccessStatusCode)
                {
                    var tokenResult = response.Content.ReadAsStringAsync().Result;
                    bearerToken = JObject.Parse(tokenResult)["access_token"].ToString();
                }
                else
                {
                    _logger.Debug(response.RequestMessage.RequestUri + " - " + response.StatusCode + " - " + response.ReasonPhrase);
                }

            }
            return bearerToken;
        }
    }
}