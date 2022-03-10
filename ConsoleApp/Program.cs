using ConsoleApp.Data;
using ConsoleApp.Interface;
using ConsoleApp.Model;
using ConsoleApp.Rules;
using SimpleIoC;
using System;

namespace ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var model1 = new ModelOne() { Name = "John Dou Smith", Age = 30, Address = "Moscow, Lincoln.Str, 56" };
            var model2 = new ModelTwo();
            
            SimpleFactory.Initialize(typeof(Program).Assembly);
            
            SimpleFactory.Register<AfterRuleAge>();
            SimpleFactory.Singleton<Counter>();
            SimpleFactory.Bind<IRuleOrderConvert<ModelOne, ModelTwo>, AgeRule>();
           
            RulesAggregator<ModelOne, ModelTwo>.EvaluateOrder(model1, model2, true);
            
            var afterAgeRule = SimpleFactory.Get<AfterRuleAge>(25);
            afterAgeRule.Convert(model2);

            var afterRuleCountry = SimpleFactory.Get("AfterRuleCountry") as AfterRuleCountry;
            afterRuleCountry.Convert(model2);

            Console.WriteLine(model2);

            // Singleton
            var counter1 = SimpleFactory.Get<Counter>();
            counter1.Inc();
            counter1.Inc();
            counter1.Inc();
            var counter2 = SimpleFactory.Get<Counter>();
            Console.WriteLine(counter1.Count);
            Console.WriteLine(counter2.Count);
            Console.WriteLine(counter1 == counter2);       
            
            // ReBind Singleton            
            SimpleFactory.ReBind<Counter, Counter>(true);
            var counter3 = SimpleFactory.Get<Counter>();
            Console.WriteLine(counter1.Count);
            Console.WriteLine(counter3.Count);
            Console.WriteLine(counter1 == counter3);

            Console.ReadKey();
        }
    }
}
