using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polygon.ColorPicker.ViewModels
{
    /// <inheritdoc />
    /// <summary>
    ///     Implements the INotifyPropertyChanged interface
    /// </summary>
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        /// <inheritdoc />
        /// <summary>
        /// INotifyPropertyChanged Property
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// INotifyPropertyChanged implementation
        /// using a property name (ex: nameof(Property))
        /// </summary>
        /// <param name="propertyName"></param>
        protected void OnPropertyChanged(string propertyName)
        {
            VerifyPropertyName(propertyName);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        [Conditional("DEBUG")]
        private void VerifyPropertyName(string propertyName)
        {
            if (TypeDescriptor.GetProperties(this)[propertyName] == null)
            {
                throw new ArgumentNullException(GetType().Name + " does not contain property: " + propertyName);
            }
        }
    }
}
