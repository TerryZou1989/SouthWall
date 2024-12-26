using OpenAI.Chat;

//ChatClient client = new(
//    model: "gpt-4o",
//    apiKey: "123456");
//ChatCompletion completion = client.CompleteChat("Say 'this is a test.'");

//Console.WriteLine($"[ASSISTANT]: {completion.Content[0].Text}");


using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

public class Program
{
    public static void Main()
    {
        Dictionary<string, object> dict = new Dictionary<string, object>();
        Console.WriteLine(dict["key"]);

        Console.ReadLine();
        // 创建动态对象列表
        var dynamicList = new List<ExpandoObject>
        {    CreateDynamicObject("Charlie", 35),
            CreateDynamicObject("Alice", 30),
            CreateDynamicObject("Alice", 10),
            CreateDynamicObject("Bob", 25)
        };

        // 按 Age 属性排序
        var sortedList = SortDynamicList(dynamicList, new List<string> { "Name", "Age" });

        foreach (var item in sortedList)
        {
            Console.WriteLine($"{item.Name}, {item.Age}");
        }
    }

    private static dynamic CreateDynamicObject(string name, int age)
    {
        var expando = new ExpandoObject() as IDictionary<string,object>;
        expando["Age"]= age;
        expando["Name"]= name;
        //expando.Name = name;
        //expando.Age = age;
        return expando;
    }

    public static IEnumerable<dynamic> SortDynamicList(IEnumerable<dynamic> source, List<string> sortFields)
    {
        IOrderedEnumerable<dynamic> orderedQuery = null;
        int i = 0;
        // 使用 OrderBy 进行排序
        foreach (var item in sortFields)
        {
            if (i == 0)
                orderedQuery = source.OrderBy(d => GetPropertyValue(d, item));
            else
                orderedQuery = orderedQuery.ThenBy(d => GetPropertyValue(d, item));
            i++;
        }
        return orderedQuery;
    }

    private static object GetPropertyValue(dynamic obj, string propertyName)
    {
        // 将 dynamic 对象转换为字典并获取属性值
        var dict = (IDictionary<string, object>)obj;
        try
        {
            return  dict[propertyName];
        }
        catch (Exception)
        {
            return null;
        }
       
    }
}
