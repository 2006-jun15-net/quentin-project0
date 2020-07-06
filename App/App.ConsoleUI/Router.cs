using System;
using System.Collections.Generic;
using System.Text;
using App.DataAccess.Entities;
using Newtonsoft.Json.Linq;
using App.API.Controllers;
namespace App.ConsoleUI
{
    public class Router
    {
        OrderController OrderC = new OrderController();
        CustomerController CustomerC = new CustomerController();
        public String mode;
        public void Route(string domain)
        {
            this.mode = domain;
        }
        
    }
}
