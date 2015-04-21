
using elcubo9.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace elcubo9.bll.Utils
{
    public class EmailAccess : BaseBLL
    {
        public static string EmailUrl
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["EmailUrl"];
            }
        }
        public void AddCustomerUser(string Email, string Password)
        {
            string htmlCode = @"
<p>Hola,</p>
<p>Estamos felices de darte la bienvenida a <strong>El Cubo 9</strong>.</p>
<h3>¿Cómo ingresar a mi portal administrativo?</h3>
<p>En el portal administrativo vas a poder configurar el menú, recibir las ordenes y más. A continuación tus datos de acceso:</p>
<p>
    Email: <strong>" + Email + "</strong>";
            if (!string.IsNullOrEmpty(Password))
            {
                htmlCode += @"<text>
            <br />
            Password: <strong>" + Password + @"</strong>
        </text>";
            }
            htmlCode += @"</p>
<p>
    <a href='http://customer.elcubo9.com' class='btn-primary'>Ir al Portal Administrativo</a>
</p>
<p>
    - El Equipo de El Cubo 9
</p>
";
            new EmailService().Send(new List<string> { Email },
                "info@elcubo9.com", "El Cubo 9", "Bienvenido a El Cubo 9", htmlCode, Template.Layout);
        }
        public async Task SendEmailToCustomersAsync(int OrderID)
        {
            await Task.Run(() => SendEmailToCustomers(OrderID));
        }
        public void SendEmailToCustomers(int OrderID)
        {
            using (var _c = db)
            {
                var _Order = _c.Orders.Where(m => m.OrderID == OrderID).SingleOrDefault();
                var _Emails = _c.CustomerEmails.Where(m => m.CustomerID == _Order.CustomerID).ToList();
                if (_Emails.Any())
                {
                    //BUILD HTML
                    string htmlCode = @"<p>Hola,</p>
                        <p>Nueva Orden en <strong>" + _Order.Customer.CustomerName + @"</strong>.</p>
                        <h3>Mesa: " + _Order.TableNumber + @" - Cliente: " + _Order.User.Name + @"</h3>";
                    foreach (var menu in _Order.OrderMenus)
                    {
                        htmlCode += "<p>" + menu.Quantity + " " + menu.Menu.Title + " - " + menu.Menu.Price + "</p>";
                        if (menu.OrderMenuAdditionals.Any())
                            htmlCode += "<p>Adicionales</p>";
                        foreach (var add in menu.OrderMenuAdditionals)
                        {
                            htmlCode += "<p>" + add.AdditionalItem.AdditionalItemName + "</p>";
                        }
                        if (!string.IsNullOrEmpty(menu.AdditionalInfo))
                            htmlCode += "<p>Aclaraciones: " + menu.AdditionalInfo + "</p>";
                        htmlCode += "<hr>";
                    }
                    htmlCode += "<p>- El Equipo de El Cubo 9</p>";
                    //END BUILD HTML
                    foreach (var item in _Emails)
                    {
                        new EmailService().Send(new List<string> { item.Email },
                            "no.reply@elcubo9.com",
                            "El Cubo 9",
                            "Nueva Orden en " + _Order.Customer.CustomerName,
                            htmlCode,
                            Template.Layout);
                    }
                }
            }
        }
    }
}