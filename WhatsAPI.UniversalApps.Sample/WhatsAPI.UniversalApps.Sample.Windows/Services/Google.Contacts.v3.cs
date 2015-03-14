﻿// Coded By Billy Riantono (at) 2015
//
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file except in compliance with
// the License. You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software distributed under the License is distributed on
// an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the License for the
// specific language governing permissions and limitations under the License.

/**
 * \mainpage
 *   Contacts API Version v3
 *
 * \section ApiInfo API Version Information
 *    <table>
 *      <tr><th>API
 *          <td><a href='https://developers.google.com/google-apps/contacts/v3/'>Contacts API</a>
 *      <tr><th>API Version<td>v3
 *      <tr><th>API Docs
 *          <td><a href='https://developers.google.com/google-apps/contacts/v3/'>
 *              https://developers.google.com/google-apps/contacts/v3/</a>
 *      <tr><th>Discovery Name<td>contacts
 *    </table>
 *
 * \section ForMoreInfo For More Information
 *
 * The complete API documentation for using Contacts API can be found at
 * <a href='https://developers.google.com/google-apps/contacts/v3/'>https://developers.google.com/google-apps/contacts/v3/</a>.
 *
 * For more information about the Google APIs Client Library for .NET, see
 * <a href='https://developers.google.com/api-client-library/dotnet/get_started'>
 * https://developers.google.com/api-client-library/dotnet/get_started</a>
 */


namespace Google.Apis.Contacts.v3.Data
{
    using System.Collections;
    using System.Collections.Generic;
    /// <summary>An item with user information and settings.</summary>
    public class Contact
    {
        [Newtonsoft.Json.JsonPropertyAttribute("title")]
        public virtual Title Name { get; set; }

        [Newtonsoft.Json.JsonPropertyAttribute("gd$phoneNumber")]
        public virtual IList<PhoneNumber> PhoneNumbers { get; set; }


    }
    public class Feed
    {
        [Newtonsoft.Json.JsonPropertyAttribute("entry")]
        public virtual IList<Contact> Entries { get; set; }
    }

    public class ContactList : Google.Apis.Requests.IDirectResponseSchema
    {
        public string version { get; set; }
        public string encoding { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("feed")]
        public Google.Apis.Contacts.v3.Data.Feed Feed { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("etag")]
        public virtual string ETag { get; set; }
    }

    public class Title
    {
        [Newtonsoft.Json.JsonPropertyAttribute("type")]
        public virtual string Type { get; set; }

        [Newtonsoft.Json.JsonPropertyAttribute("$t")]
        public virtual string Text { get; set; }
    }

    public class PhoneNumber
    {
        [Newtonsoft.Json.JsonPropertyAttribute("uri")]
        public virtual string Uri { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("$t")]
        public virtual string Text { get; set; }
    }
}

namespace Google.Apis.Contacts.v3
{
    /// <summary>The Contacts Service.</summary>
    public class ContactsService : Google.Apis.Services.BaseClientService
    {
        /// <summary>The API version.</summary>
        public const string Version = "v3";

        /// <summary>The discovery version used to generate this service.</summary>
        public static Google.Apis.Discovery.DiscoveryVersion DiscoveryVersionUsed =
            Google.Apis.Discovery.DiscoveryVersion.Version_1_0;

        /// <summary>Constructs a new service.</summary>
        public ContactsService() :
            this(new Google.Apis.Services.BaseClientService.Initializer()) { }

        /// <summary>Constructs a new service.</summary>
        /// <param name="initializer">The service initializer.</param>
        public ContactsService(Google.Apis.Services.BaseClientService.Initializer initializer)
            : base(initializer)
        {
            contact = new ContactsResource(this);
        }

        /// <summary>Gets the service supported features.</summary>
        public override System.Collections.Generic.IList<string> Features
        {
            get { return new string[0]; }
        }

        /// <summary>Gets the service name.</summary>
        public override string Name
        {
            get { return "contacts"; }
        }

        /// <summary>Gets the service base URI.</summary>
        public override string BaseUri
        {
            get { return "https://www.google.com/m8/feeds/"; }
        }

        /// <summary>Gets the service base path.</summary>
        public override string BasePath
        {
            get { return "m8/feeds/"; }
        }

        /// <summary>Available OAuth 2.0 scopes for use with the Contacts API.</summary>
        public class Scope
        {
            /// <summary>View and manage the contact in your Google Contacts</summary>
            public static string FullContacts = "https://www.google.com/m8/feeds";

            /// <summary>View and manage its own configuration data in your Google Contacts</summary>
            public static string ReadOnly = "https://www.googleapis.com/auth/contacts.readonly";

        }



        private readonly ContactsResource contact;

        /// <summary>Gets the List Contacts resource.</summary>
        public virtual ContactsResource Contact
        {
            get { return contact; }
        }

    }

    ///<summary>A base abstract class for Contacts requests.</summary>
    public abstract class ContactsServiceRequest<TResponse> : Google.Apis.Requests.ClientServiceRequest<TResponse>
    {
        ///<summary>Constructs a new ContactsBaseServiceRequest instance.</summary>
        protected ContactsServiceRequest(Google.Apis.Services.IClientService service)
            : base(service)
        {
        }

        /// <summary>Data format for the response.</summary>
        /// [default: json]
        [Google.Apis.Util.RequestParameterAttribute("alt", Google.Apis.Util.RequestParameterType.Query)]
        public virtual System.Nullable<AltEnum> Alt { get; set; }

        /// <summary>Data format for the response.</summary>
        public enum AltEnum
        {
            /// <summary>Responses with Content-Type of application/json</summary>
            [Google.Apis.Util.StringValueAttribute("json")]
            Json,
        }

        /// <summary>Selector specifying which fields to include in a partial response.</summary>
        [Google.Apis.Util.RequestParameterAttribute("fields", Google.Apis.Util.RequestParameterType.Query)]
        public virtual string Fields { get; set; }

        /// <summary>API key. Your API key identifies your project and provides you with API access, quota, and reports.
        /// Required unless you provide an OAuth 2.0 token.</summary>
        [Google.Apis.Util.RequestParameterAttribute("key", Google.Apis.Util.RequestParameterType.Query)]
        public virtual string Key { get; set; }

        /// <summary>OAuth 2.0 token for the current user.</summary>
        [Google.Apis.Util.RequestParameterAttribute("oauth_token", Google.Apis.Util.RequestParameterType.Query)]
        public virtual string OauthToken { get; set; }

        /// <summary>Returns response with indentations and line breaks.</summary>
        /// [default: true]
        [Google.Apis.Util.RequestParameterAttribute("prettyPrint", Google.Apis.Util.RequestParameterType.Query)]
        public virtual System.Nullable<bool> PrettyPrint { get; set; }

        /// <summary>Available to use for quota purposes for server-side applications. Can be any arbitrary string
        /// assigned to a user, but should not exceed 40 characters. Overrides userIp if both are provided.</summary>
        [Google.Apis.Util.RequestParameterAttribute("quotaUser", Google.Apis.Util.RequestParameterType.Query)]
        public virtual string QuotaUser { get; set; }

        /// <summary>IP address of the site where the request originates. Use this if you want to enforce per-user
        /// limits.</summary>
        [Google.Apis.Util.RequestParameterAttribute("userIp", Google.Apis.Util.RequestParameterType.Query)]
        public virtual string UserIp { get; set; }

        /// <summary>Initializes Contacts parameter list.</summary>
        protected override void InitParameters()
        {
            base.InitParameters();

            RequestParameters.Add(
                "alt", new Google.Apis.Discovery.Parameter
                {
                    Name = "alt",
                    IsRequired = true,
                    ParameterType = "query",
                    DefaultValue = "json",
                    Pattern = null,
                });
            RequestParameters.Add(
                "fields", new Google.Apis.Discovery.Parameter
                {
                    Name = "fields",
                    IsRequired = false,
                    ParameterType = "query",
                    DefaultValue = null,
                    Pattern = null,
                });
            RequestParameters.Add(
                "key", new Google.Apis.Discovery.Parameter
                {
                    Name = "key",
                    IsRequired = false,
                    ParameterType = "query",
                    DefaultValue = null,
                    Pattern = null,
                });
            RequestParameters.Add(
                "oauth_token", new Google.Apis.Discovery.Parameter
                {
                    Name = "oauth_token",
                    IsRequired = false,
                    ParameterType = "query",
                    DefaultValue = null,
                    Pattern = null,
                });
            RequestParameters.Add(
                "prettyPrint", new Google.Apis.Discovery.Parameter
                {
                    Name = "prettyPrint",
                    IsRequired = false,
                    ParameterType = "query",
                    DefaultValue = "true",
                    Pattern = null,
                });
            RequestParameters.Add(
                "quotaUser", new Google.Apis.Discovery.Parameter
                {
                    Name = "quotaUser",
                    IsRequired = false,
                    ParameterType = "query",
                    DefaultValue = null,
                    Pattern = null,
                });
            RequestParameters.Add(
                "userIp", new Google.Apis.Discovery.Parameter
                {
                    Name = "userIp",
                    IsRequired = false,
                    ParameterType = "query",
                    DefaultValue = null,
                    Pattern = null,
                });
        }
    }

    /// <summary>The "about" collection of methods.</summary>
    public class ContactsResource
    {
        private const string Resource = "contacts";

        /// <summary>The service which this resource belongs to.</summary>
        private readonly Google.Apis.Services.IClientService service;

        /// <summary>Constructs a new resource.</summary>
        public ContactsResource(Google.Apis.Services.IClientService service)
        {
            this.service = service;

        }


        /// <summary>Gets the information about the current user along with Contacts API settings</summary>
        public virtual GetRequest Get(string email, int maxResult = 2000)
        {

            return new GetRequest(service, email, maxResult);
        }

        /// <summary>Gets the information about the current user along with Contacts API settings</summary>
        public class GetRequest : ContactsServiceRequest<Google.Apis.Contacts.v3.Data.ContactList>
        {
            /// <summary>Constructs a new Get request.</summary>
            public GetRequest(Google.Apis.Services.IClientService service, string email,int maxResult = 2000,AltEnum alt = AltEnum.Json)
                : base(service)
            {
                Email = email;
                InitParameters();
                Alt = alt;
                MaxResults = maxResult.ToString();
            }


            /// <summary>When calculating the number of remaining change IDs, whether to include public files the user
            /// has opened and shared files. When set to false, this counts only change IDs for owned files and any
            /// shared or public files that the user has explicitly added to a folder they own.</summary>
            /// [default: true]
            [Google.Apis.Util.RequestParameterAttribute("max-results", Google.Apis.Util.RequestParameterType.Query)]
            public virtual string MaxResults { get; set; }

            [Google.Apis.Util.RequestParameterAttribute("email", Google.Apis.Util.RequestParameterType.Path)]
            public virtual string Email { get; private set; }

            ///<summary>Gets the method name.</summary>
            public override string MethodName
            {
                get { return "get"; }
            }

            ///<summary>Gets the HTTP method.</summary>
            public override string HttpMethod
            {
                get { return "GET"; }
            }

            ///<summary>Gets the REST path.</summary>
            public override string RestPath
            {
                get { return "contacts/{email}/full"; }
            }

            /// <summary>Initializes Get parameter list.</summary>
            protected override void InitParameters()
            {
                base.InitParameters();

                RequestParameters.Add(
                    "max-results", new Google.Apis.Discovery.Parameter
                    {
                        Name = "max-results",
                        IsRequired = true,
                        ParameterType = "query",
                        DefaultValue = "10000",
                        Pattern = null,
                    });

                RequestParameters.Add(
                    "email", new Google.Apis.Discovery.Parameter
                    {
                        Name = "email",
                        IsRequired = true,
                        ParameterType = "path",
                        DefaultValue = null,
                        Pattern = null,
                    });
            }

        }
    }
}