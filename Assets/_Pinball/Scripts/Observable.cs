using System;

namespace Pinball
{
    public class Observable<T>
    {
        public T value
        {
            get { return _value; }
            set
            {
                _value = value;
                _EmitOnChange();
            }
        }
        public Action<T> OnChange;

        private T _value;

        private void _EmitOnChange()
        {
            if (OnChange != null) OnChange(value);
        }
    }
}
