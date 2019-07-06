using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Polygon.Models.Enums;

namespace Polygon.ColorPicker
{
    /// <summary>
    /// Class implementing the mediator design pattern
    /// </summary>
    public static class Mediator
    {
        private static readonly IDictionary<string, List<Action<object>>> PlDict =
            new Dictionary<string, List<Action<object>>>();

        /// <summary>
        /// Subscribe to an event
        /// </summary>
        /// <param name="token"></param>
        /// <param name="callback"></param>
        public static void Subscribe(ScreenEventType token, Action<object> callback)
        {
            var tokenAsString = token.ToString();
            if (!PlDict.ContainsKey(tokenAsString))
            {
                var list = new List<Action<object>> { callback };
                PlDict.Add(tokenAsString, list);
            }
            else
            {
                foreach (var item in PlDict[tokenAsString])
                {
                    if (item.Method.ToString() != callback.Method.ToString())
                    {
                        continue;
                    }

                    PlDict[tokenAsString].Add(callback);
                    return;
                }
            }
        }

        /// <summary>
        /// Unsubscribe from an event
        /// </summary>
        /// <param name="token"></param>
        /// <param name="callback"></param>
        public static void Unsubscribe(ScreenEventType token, Action<object> callback)
        {
            var tokenAsString = token.ToString();
            if (PlDict.ContainsKey(tokenAsString))
            {
                PlDict[tokenAsString].Remove(callback);
            }
        }

        /// <summary>
        /// Finds and executes a callback that has been subscribed
        /// </summary>
        /// <param name="token"></param>
        /// <param name="args"></param>
        public static void Notify(ScreenEventType token, object args = null)
        {
            var tokenAsString = token.ToString();
            if (!PlDict.ContainsKey(tokenAsString))
            {
                return;
            }

            foreach (var callback in PlDict[tokenAsString])
            {
                callback(args);
            }
        }
    }
}
