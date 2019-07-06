using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polygon.ColorPicker.Events
{
    /// <summary>
    /// EventHandler extension class
    /// </summary>
    public static class EventRaiser
    {
        /// <summary>
        /// raises an event with a sender
        /// </summary>
        /// <param name="handler"></param>
        /// <param name="sender"></param>
        public static void Raise(this EventHandler handler, object sender)
        {
            handler?.Invoke(sender, EventArgs.Empty);
        }

        /// <summary>
        /// Raises an event with a sender and a value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="handler"></param>
        /// <param name="sender"></param>
        /// <param name="value"></param>
        public static void Raise<T>(this EventHandler<EventArgs<T>> handler, object sender, T value)
        {
            handler?.Invoke(sender, new EventArgs<T>(value));
        }

        /// <summary>
        /// Raises an event with a sender and a value
        /// The type must inherit from EventArgs
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="handler"></param>
        /// <param name="sender"></param>
        /// <param name="value"></param>
        public static void Raise<T>(this EventHandler<T> handler, object sender, T value) where T : EventArgs
        {
            handler?.Invoke(sender, value);
        }

        /// <summary>
        /// Raises an event from an EventArgs with generic type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="handler"></param>
        /// <param name="sender"></param>
        /// <param name="value"></param>
        public static void Raise<T>(this EventHandler<EventArgs<T>> handler, object sender, EventArgs<T> value)
        {
            handler?.Invoke(sender, value);
        }
    }
}
