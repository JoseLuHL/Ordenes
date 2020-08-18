using System;
using System.Collections.Generic;
using System.Text;

namespace XFFurniture.Models
{
   public static class UrlModelo
    {
        //public static string UrlBase => "https://pedidos2020.azurewebsites.net/api/";
        public static string UrlBase => "http://192.168.1.10/pedidos/api/";
        public static string tiendas => $"{UrlBase}Tiendums";
        public static string odenes => $"{UrlBase}Ordenes";
        public static string odenesTienda => $"{UrlBase}Ordenes/tienda/";
        public static string cliente => $"{UrlBase}Clientes";
        public static string producto => $"{UrlBase}Productoes";
        public static string categoria => $"{UrlBase}Categoriums";
        public static string formaPago => $"{UrlBase}Mediopagoes";
    }
}
