﻿using DATA.Entities;
using DATA.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
    public static class AuthenticationManager
    {
        public static User LoggedUser
        {
            get
            {
                AuthenticationService authenticationService = null;

                if (HttpContext.Current != null && HttpContext.Current.Session["LoggedUser"] == null)
                    HttpContext.Current.Session["LoggedUser"] = new AuthenticationService();

                authenticationService = (AuthenticationService)HttpContext.Current.Session["LoggedUser"];
                return authenticationService.LoggedUser;
            }
        }

        public static void Authenticate(string username, string password)
        {
            AuthenticationService authenticationService = null;

            if (HttpContext.Current != null && HttpContext.Current.Session["LoggedUser"] == null)
                HttpContext.Current.Session["LoggedUser"] = new AuthenticationService();

            authenticationService = (AuthenticationService)HttpContext.Current.Session["LoggedUser"];
            authenticationService.AuthenticateUser(username, password);
        }
        public static void Logout()
        {
            HttpContext.Current.Session["LoggedUser"] = null;
        }
    }
}