﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WhatsAPI.UniversalApps.Libs.Extensions;
using WhatsAPI.UniversalApps.Libs.Models;
using WhatsAPI.UniversalApps.Libs.Utils.Common;

namespace WhatsAPI.UniversalApps.Libs.Core.Registration
{
    public class Register
    {
        public static string GenerateIdentity(string phoneNumber, string salt = "")
        {
            string identity = (phoneNumber + salt);
            return identity.Reverse().ToShaString();
        }

        public static string GetToken(string number)
        {
            return Token.GenerateToken(number);
        }

        public static async Task<string> GetTokenAsync(string number)
        {
            return await Token.GenerateTokenS40Online(number);
        }

        public static string GetTokenOfflineAsync(string number)
        {
            return Token.GenerateTokenS40Offline(number);
        }

        public static async Task<RegisterResponse> RequestCode(string phoneNumber, string method = "sms", string id = null)
        {
            if (phoneNumber.Contains("+"))
            {
                phoneNumber = phoneNumber.Replace("+", "");
            }
            string response = null;
            string password = null;
            string request = null;
            RegisterResponse result = new RegisterResponse();
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    //auto-generate
                    id = GenerateIdentity(phoneNumber);
                }
                PhoneNumber pn = new PhoneNumber(phoneNumber);
                await pn.ProcessPhoneNumber();
                string token = System.Uri.EscapeDataString(GetTokenOfflineAsync(pn.Number));
                request = string.Format("https://"+Constants.Information.WhatsRequestCodeHost+"?cc={0}&in={1}&to={0}{1}&method={2}&sim_mcc={3}&sim_mnc={4}&token={5}&id={6}&lg={7}&lc={8}", pn.CC, pn.Number, method, pn.MCC, pn.MNC, token, id, pn.ISO639, pn.ISO3166);
                response = await HttpRequest.Get(request);
                password = response.GetJsonValue("pw");
                if (password != null && !string.IsNullOrEmpty(password))
                {
                    result.IsSuccess = true;
                    result.Password = password;
                    result.Response = response;
                    return  result;
                }
                result.IsSuccess = response.GetJsonValue("status") == "sent";
                result.Password = "";
                result.Response = response.GetJsonValue("reason")!=null ? response.GetJsonValue("reason") : "Has Sent !Please try again later." ;
                return (result);
            }
            catch (Exception e)
            {
                response = e.Message;
                return new RegisterResponse() { IsSuccess = false, Response = e.Message };
            }
        }

        public static async Task<string> RegisterCode(string phoneNumber, string code, string id = null)
        {
            var response = string.Empty;
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    //auto generate
                    id = GenerateIdentity(phoneNumber);
                }
                PhoneNumber pn = new PhoneNumber(phoneNumber);
                await pn.ProcessPhoneNumber();
                string uri = string.Format("https://" + Constants.Information.WhatsRegisterHost + "?cc={0}&in={1}&id={2}&code={3}", pn.CC, pn.Number, id, code);
                response = await HttpRequest.Get(uri);
                if (response.GetJsonValue("status") == "ok")
                {
                    return response.GetJsonValue("pw");
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        public static async Task<string> RequestExist(string phoneNumber, string id = null)
        {
            string response = string.Empty;
            try
            {
                if (String.IsNullOrEmpty(id))
                {
                    id = GenerateIdentity(phoneNumber);
                }
                PhoneNumber pn = new PhoneNumber(phoneNumber);
                 await pn.ProcessPhoneNumber();
                string uri = string.Format("https://" + Constants.Information.WhatsCheckHost + "?cc={0}&in={1}&id={2}", pn.CC, pn.Number, id);
                response = await HttpRequest.Get(uri);
                if (response.GetJsonValue("status") == "ok")
                {
                    return response.GetJsonValue("pw");
                }
                return null;
            }
            catch
            {
                return null;
            }
        }
    }
}
