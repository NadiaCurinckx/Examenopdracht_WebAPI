using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Model
{
   public class Genre
    {
        public Int32 Id { get; set; }
        public String Omschrijving { get; set; }

        [IgnoreDataMember]
        // https://docs.microsoft.com/en-us/aspnet/web-api/overview/formats-and-model-binding/json-and-xml-serialization#xml_media_type_formatter
        public ICollection<Boek> Boeken { get; set; }

        public override string ToString()
        {
            return $"{Omschrijving}";
        }
    }
}