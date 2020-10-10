﻿using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UciProxy
{
    public enum SourceType { EXECUTABLE, CONSOLE, NETWORK }

    public class Source
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public SourceType SourceType { get; set; }

        public string Address { get; set; }
    }
}