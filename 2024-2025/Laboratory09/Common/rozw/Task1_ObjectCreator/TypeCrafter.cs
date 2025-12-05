using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Task1_ObjectCreator;

public class ParseException(string message) : Exception(message);
public static class TypeCrafter
{
    public static T TypeCraft<T>()
    {
        Type type = typeof(T);
        Console.WriteLine($"Constructing {type.FullName} type");
        PropertyInfo[] properties = type.GetProperties();
        ConstructorInfo? constructor = type.GetConstructor(Type.EmptyTypes);
        if (constructor == null)
        {
            throw new InvalidOperationException($"Type {type.FullName} has no parameterless constructor.");
        }

        T result = (T)constructor.Invoke(null);
        foreach (PropertyInfo property in properties)
        {
            Type propertyType = property.PropertyType;
            Type parsableType = typeof(IParsable<>);
            var parsable = propertyType.GetInterfaces()
                .Any(t => t.IsGenericType && t.GetGenericTypeDefinition() == parsableType &&
                          t.GetGenericArguments()[0] == propertyType);
            if (propertyType == typeof(string))
            {
                string input = AskForInput(property.Name, propertyType.Name);
                property.SetValue(result, input);
            }
            else if (parsable)
            {
                string input = AskForInput(property.Name, propertyType.Name);
                MethodInfo? parseMethod = propertyType.GetMethod(
                    "TryParse",
                    BindingFlags.Public | BindingFlags.Static,
                    binder: null,
                    types: [typeof(string), typeof(IFormatProvider), propertyType.MakeByRefType()],
                    modifiers: null
                );
                if (parseMethod == null)
                {
                    throw new Exception($"{propertyType.FullName} does not have a static TryParse method.");
                }
                object[] args = { input, null!, null };
                bool status = (bool)parseMethod.Invoke(null, args)!;
                if(status)
                {
                    property.SetValue(result, args[2]);
                }
                else 
                {
                    throw new ParseException($"Couldn't parse {input} to {propertyType}.");
                }
            }
            else 
            {
                Console.WriteLine($"Type of property '{property.PropertyType} {property.Name}' is not parsable. Attempting to craft object recursively:");
                var craftMethod = typeof(TypeCrafter).GetMethod(nameof(TypeCraft), BindingFlags.Public | BindingFlags.Static);
                var genericMethod = craftMethod?.MakeGenericMethod(property.PropertyType);
                var complexProperty = genericMethod?.Invoke(null, null);
                property.SetValue(result, complexProperty);
            }
        }

        return result;
    }

    private static string AskForInput(string propertyName, string type)
    {
        Console.WriteLine($"Provide a variable {propertyName} of type {type}:");
        if (Console.ReadLine() is { } line)
        {
            return line;
        }

        throw new IOException("Line from the Console is not available");
    }
}
