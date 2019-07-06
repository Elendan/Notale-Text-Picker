using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polygon.ColorPicker.Events
{
    /// <inheritdoc />
    /// <summary>
    /// Generic events
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EventArgs<T> : EventArgs
    {
        /// <inheritdoc />
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="value"></param>
        public EventArgs(T value) => Value = value;

        /// <summary>
        /// Event args value
        /// </summary>
        public T Value { get; }
    }
}
