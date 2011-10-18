<%@ Assembly Name="Products" %>
<%@ Assembly Name="Products.NHibernate" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Products.Models.CategoryModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%=Model.Title %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

 <h2><%=Model.Title %></h2>
 <p><%=Model.Description %></p>
    <div>
    <%if (Model.Products != null && Model.Products.Count>0) {%>
    <h3>Products</h3>
            <% foreach (var product in Model.Products)
               { %>
                 <p> <%=product.Title %></p> 
             <%  } %>
     <%  } %>
    </div>

</asp:Content>
