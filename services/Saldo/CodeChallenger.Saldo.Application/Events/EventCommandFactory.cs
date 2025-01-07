namespace CodeChallenger.Lancamentos.Application.Events
{
    using CodeChallenger.Lancamentos.Application.Events.Base;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using System;
    using System.Linq;
    using System.Reflection;

    public class EventCommandFactory : IEventCommandFactory
    {
        public BaseEventCommand Create(string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                var eventType = JObject.Parse(message)?.GetValue("EventType", StringComparison.InvariantCultureIgnoreCase)?.ToString();

                var type = (from t in Assembly.GetExecutingAssembly().GetTypes()
                            where t.Name == $"{eventType}Command"
                            select t)
                            .FirstOrDefault();

                if (type != null)
                {
                    return JsonConvert.DeserializeObject(message, type) as BaseEventCommand 
                        ?? null!;
                }
            }

            return null!;
        }
    }
}
