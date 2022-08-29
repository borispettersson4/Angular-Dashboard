using System;

namespace Core.API.Models
{
    public class Server
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool isOnline { get; set; }
    }
}