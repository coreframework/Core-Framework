<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" ContentType="application/xrds+xml" %>

<?xml version="1.0" encoding="UTF-8" ?>
<xrds:xrds xmlns:xrds="xri://$xrds" xmlns:openid="http://openid.net/xmlns/1.0" xmlns="xri://$xrd*($v*2.0)">
    <xrd>
        <!-- OpenID 2.0 login service -->
        <Service priority="10">
            <Type>http://specs.openid.net/auth/2.0/signon</Type>
            <URI><%=new Uri(Request.Url, Response.ApplyAppPathModifier("~/openid-login-widget/create-session"))%></URI>
        </Service>
        <!-- OpenID 1.0 login service -->
        <Service priority="20">
            <Type>http://openid.net/server/1.0</Type>
            <URI><%=new Uri(Request.Url, Response.ApplyAppPathModifier("~/openid-login-widget/create-session"))%></URI>
        </Service>
    </xrd>
</xrds:xrds>