using System;

namespace Shun_Utilities
{
    public interface IObservableData<T>
    {
        
    }
    
    public class ObservableData<T> : IObservableData<T>
    {
        private T _value;

        public T Value
        {
            get => _value;
            set
            {
                if (_value.Equals(value))
                {
                    return;
                }
                var oldValue = _value;
                _value = value;
                OnValueChanged?.Invoke(oldValue, value);
            }
        }

        public Action<T,T> OnValueChanged { get; set; } = delegate { };

        public ObservableData(T value)
        {
            _value = value;
        }
    }
}