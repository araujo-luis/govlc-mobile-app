using System;
using System.Collections.Generic;
using System.Text;

namespace GoVLC.Models
{
    public class Address
    {
        public string Codtipovia { get; set; }
        public int Codvia { get; set; }
        public int Codviacatastro { get; set; }
        public string Nomoficial { get; set; }
        public string Traducnooficial { get; set; }
        public string FullAddress=> $"{Codtipovia} {Nomoficial}  ({Traducnooficial})";
    }
}
