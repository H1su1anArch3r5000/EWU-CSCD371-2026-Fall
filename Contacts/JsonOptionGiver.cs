using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;

namespace Contacts
{
    public sealed class JsonOptionGiver()
    {
        public static readonly JsonSerializerOptions Options = new()
        {
            WriteIndented = true,
            PropertyNameCaseInsensitive = true,
            TypeInfoResolver = new DefaultJsonTypeInfoResolver()
            {
                Modifiers = {
                    ti =>
                    {
                        if(ti.Type == typeof(IContact))
                        {
                            ti.PolymorphismOptions = new JsonPolymorphismOptions{
                                TypeDiscriminatorPropertyName = "$type",
                                IgnoreUnrecognizedTypeDiscriminators = true,
                                UnknownDerivedTypeHandling = JsonUnknownDerivedTypeHandling.FailSerialization,
                                DerivedTypes =
                                {
                                    new JsonDerivedType(typeof(Student), "student"),
                                    new JsonDerivedType(typeof(Employee), "employee")
                                }
                            };
                        }
                    }
                }
            }
        };
    }
}
