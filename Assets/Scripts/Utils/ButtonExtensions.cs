using System;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Clicker.UI
{
    public static class ButtonExtensions
    {
        public static IDisposable SubscribeOnClick(this Button button, UnityAction callback)
        {
            return new Subscription(button, callback);
        }

        private class Subscription : IDisposable
        {
            private Button _button;
            private UnityAction _callback;
            
            public Subscription(Button button, UnityAction callback)
            {
                _button = button;
                _callback = callback;
            }

            public void Dispose()
            {
                if (_button == null || _callback == null)
                    throw new ObjectDisposedException(ToString());
                
                _button.onClick.RemoveListener(_callback);

                _button = null;
                _callback = null;
            }
        }
    }
}