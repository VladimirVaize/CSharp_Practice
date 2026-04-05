using System;
using System.Linq;
using System.Reflection;

namespace Reflection
{
    public static class GameDebugger
    {

        public static void DumpObject(object obj, string objectName)
        {
            Console.WriteLine($"=== Дамп объекта: {objectName} ===");
            if (obj == null)
            {
                Console.WriteLine("Ошибка: объект равен null");
                Console.WriteLine("=== Конец дампа ===");
                return;
            }

            Type objType = obj.GetType();
            Console.WriteLine($"Тип: {objType.Name}");

            var allFields = objType.GetFields(
                BindingFlags.Public |
                BindingFlags.NonPublic |
                BindingFlags.Instance |
                BindingFlags.Static |
                BindingFlags.FlattenHierarchy
            );

            var visibleFields = allFields.Where(field => !IsBackingField(field)).ToList();

            if (visibleFields.Any())
            {
                Console.WriteLine("Поля:");
                foreach (var field in visibleFields)
                {
                    try
                    {
                        object value = field.GetValue(obj);
                        string valueStr = (value == null) ? "null" : value.ToString();

                        string accessModifier = field.IsPublic ? "" : "(private) ";

                        Console.WriteLine($"  - {accessModifier}{field.Name} : {field.FieldType.Name} = {valueStr}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"  - {field.Name} : {field.FieldType.Name} = <Ошибка доступа: {ex.Message}>");
                    }
                }
            }

            var properties = objType.GetProperties(
                BindingFlags.Public |
                BindingFlags.NonPublic |
                BindingFlags.Instance |
                BindingFlags.Static |
                BindingFlags.FlattenHierarchy
            );

            if (properties.Any())
            {
                Console.WriteLine("Свойства:");
                foreach (var prop in properties)
                {
                    try
                    {
                        if (prop.CanRead)
                        {
                            object value = prop.GetValue(obj);
                            string valueStr = (value == null) ? "null" : value.ToString();
                            string accessInfo = prop.CanWrite ? "" : " (readonly)";
                            Console.WriteLine($"  - {prop.Name} : {prop.PropertyType.Name} = {valueStr}{accessInfo}");
                        }
                        else
                            Console.WriteLine($"  - {prop.Name} : {prop.PropertyType.Name} = <только запись>");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"  - {prop.Name} : {prop.PropertyType.Name} = <Ошибка доступа: {ex.Message}>");
                    }
                }
            }

            Console.WriteLine("=== Конец дампа ===\n");
        }

        private static bool IsBackingField(FieldInfo field)
        {
            return field.Name.Contains("k__BackingField") || (field.Name.StartsWith("<") && field.Name.EndsWith(">k__BackingField"));
        }

        public static bool InvokeMethod(object obj, string methodName, params object[] parameters)
        {
            if (obj == null)
            {
                Console.WriteLine($"Ошибка: объект равен null, метод '{methodName}' не может быть вызван");
                return false;
            }

            Type objType = obj.GetType();

            var methods = objType.GetMethods(
                BindingFlags.Public |
                BindingFlags.NonPublic |
                BindingFlags.Instance |
                BindingFlags.Static
            ).Where(m => m.Name == methodName).ToArray();

            if (methods.Length == 0)
            {
                Console.WriteLine($"Метод '{methodName}' не найден в типе {objType.Name}");
                return false;
            }

            MethodInfo method = methods.FirstOrDefault();

            try
            {
                ParameterInfo[] methodParams = method.GetParameters();

                if (methodParams.Length != parameters.Length)
                {
                    Console.WriteLine($"Метод '{methodName}' ожидает {methodParams.Length} параметров, получено {parameters.Length}");
                    return false;
                }

                object result = method.Invoke(obj, parameters);

                if (method.ReturnType != typeof(void))
                {
                    string resultStr = (result == null) ? "null" : result.ToString();
                    Console.WriteLine($"Метод '{methodName}' вернул: {resultStr}");
                }
                else
                {
                    Console.WriteLine($"Метод '{methodName}' успешно выполнен");
                }

                return true;
            }
            catch (TargetInvocationException ex)
            {
                Console.WriteLine($"Ошибка при выполнении метода '{methodName}': {ex.InnerException?.Message ?? ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при вызове метода '{methodName}': {ex.Message}");
                return false;
            }
        }
    }
}
