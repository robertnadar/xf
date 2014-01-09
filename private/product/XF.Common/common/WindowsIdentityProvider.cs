// <copyright file="WindowsIdentityProvider.cs" company="eXtensoft LLC">
// Copyright © 2014 All Right Reserved
// </copyright>

namespace XF.Common
{

    public class WindowsIdentityProvider : IUserIdentityProvider
    {
        string IUserIdentityProvider.Username
        {
            get { return System.Security.Principal.WindowsIdentity.GetCurrent().Name; }
        }

        string IUserIdentityProvider.Culture
        {
            get { return System.Globalization.CultureInfo.CurrentCulture.EnglishName; }
        }

        string IUserIdentityProvider.UICulture
        {
            get { return System.Globalization.CultureInfo.CurrentUICulture.EnglishName; }
        }
    }
}
